using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class Personnage : MonoBehaviour
{
    //Variable floot
    public float vitesseX;  //Vitesse horizontale
    public float vitesseXMax;  //Vitesse horizontale max
    public float vitesseY;  //Vitesse verticale
    public float vitesseYMax;  //Vitesse verticale max
    public float munitions = 6;  //Nombre de munitions dont dispose le joueur

    //Variable bool
    public bool peutAttaque = true;  //Variable qui permet au joueur de faire une attaque si ==true
    public bool peutDeplacer = true;  //Variable qui permet au joueur de se d�placer si ==true
    public bool estEnVie = true;  //Variable qui d�finit si le joueur est en vie ou nom (utilis� pour les animations des ennemis lorsqu'ils sont en collision)
    public bool aLaClee = false;  //Variable qui permet de d�tecter si le joueur poss�de la cl�e pour ouvrir la porte de la fin du niveau
    public bool menuFermer;

    //Variable de script
    public Capitaine scriptCapitaine; //Variable donnant acc�s au script du capitaine

    //Variable GameObject
    public GameObject balleOriginale;  //Balle � cloner venant du tir
    public GameObject menu;  //GameObject repr�sentant le menu qui s'affiche lorsque le joueur appui sur la touche ESC ou lorsqu'il meurt
    public GameObject dialogue;  //GameObject repr�sentant le dialogue qui s'active seulement dans la sc�ne Intro (scene 1)
    public GameObject moustache;  //GameObject de la moustache

    //Variable Son (AudioClip)
    public AudioClip sonSaut;  //Son qui s'active lorsque le joueur saute
    public AudioClip sonMort;  //Son qui s'active lorsque le joueur meurt
    public AudioClip sonAttaque;  //Son qui s'active lorsque le joueur attaque
    public AudioClip sonTir;  //Son qui s'active lorsque le joueur tir
    public AudioClip sonMunitions;  //Son qui s'active lorsque le joueur r�cup�re des munitions
    public AudioClip sonCl�e;  //Son qui s'active lorsque le joueur r�cup�re la cl�e

    public AudioClip sonCapitaineMort;  //Son qui s'active lorsque le joueur tue le Capitaine

    //Variable pour le text
    public TextMeshProUGUI textMunitions;

    void Start()
    {
        //Variable repr�sentant la sc�ne actuelle
        int laScene = SceneManager.GetActiveScene().buildIndex;

        //D�finir l'�tat du joueur lorsque le niveau commence
        estEnVie = true;
        aLaClee = false;
        menuFermer = true;

        //Le personnage du joueur dors au d�but de la premi�re sc�ne
        if(laScene == 1)
        {
            peutAttaque = false;
            peutDeplacer = false;
            GetComponent<Animator>().SetBool("animSommeil", true);
        }
        else
        {
            peutAttaque = true;
            peutDeplacer = true;
            GetComponent<Animator>().SetBool("animSommeil", false);
        }


    }


    // Update is called once per frame
    void Update()
    {
        int laScene = SceneManager.GetActiveScene().buildIndex;
        if (Input.GetKeyDown(KeyCode.Space) && laScene == 1 && GetComponent<Animator>().GetBool("animSommeil") == true)
        {
            GetComponent<Animator>().SetBool("animSommeil", false);
            GetComponent<Animator>().SetBool("animReveil", true);
            Invoke("FinReveil", 0.005f);
        }

        //Appel du text
        textMunitions.text = "Munitions : " + munitions;

        if (peutDeplacer == true && estEnVie == true)
        {
            //--------------------------Gestion des touches----------------------------
            ////Touches pour le d�placement du personnage
            //D�placement horizontale
            if (Input.GetKey("a"))      //D�placement vers la gauche
            {
                vitesseX = -vitesseXMax;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetKey("d"))     //D�placement vers la droite
            {
                vitesseX = vitesseXMax;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else        //M�morisation de la vitesse x
            {
                vitesseX = GetComponent<Rigidbody2D>().velocity.x;
                vitesseX = 0;
                GetComponent<Animator>().SetBool("animCourse", false);
            }

            //D�placement saut
            if (Input.GetKeyDown(KeyCode.Space) && Physics2D.OverlapCircle(transform.position, 0.25f) == true)
            {
                vitesseY = vitesseYMax;
                GetComponent<AudioSource>().PlayOneShot(sonSaut);
            }
            else
            {
                vitesseY = GetComponent<Rigidbody2D>().velocity.y;
            }
            //Appliquer les vitesse X et Y
            GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);


            ////Touche pour ouvrir le menu
            if (Input.GetKeyDown(KeyCode.Escape) && menuFermer == true)
            {
                AfficherMenu();
                menuFermer = false;
            }
            else if(Input.GetKeyDown(KeyCode.Escape) && menuFermer == false)
            {
                FermerMenu();
                menuFermer = true;
            }
        }



        //---------------------------Gestion des animations------------------------------
        //Animation course
        if (vitesseX > 0.1f || vitesseX < -0.1f)
        {
            GetComponent<Animator>().SetBool("animCourse", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("animCourse", false);
        }

        //Animation saut
        if (vitesseY > 0.1f && GetComponent<Animator>().GetBool("animMort") == false)
        {
            GetComponent<Animator>().SetBool("animSaut", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("animSaut", false);
        }

        //Animation attaque
        if (Input.GetMouseButtonDown(0) && peutAttaque == true && estEnVie == true)
        {
            peutAttaque = false;  //Emp�cher au joueur d'attaquer
            peutDeplacer = false;  //Emp�cher le joueur de se d�placer pendant l'attaque
            GetComponent<Animator>().SetBool("animAttaque", true);
            GetComponent<AudioSource>().PlayOneShot(sonAttaque);

            Invoke("FinAnimAttaque", 0.25f);
            Invoke("FinAttaque", 0.9f);
            Invoke("PermettreDeplacement", 0.9f);
        }

        //Animation tir
        if (Input.GetMouseButtonDown(1) && peutAttaque == true && munitions > 0)
        {
            peutAttaque = false;  //Emp�cher au joueur d'attaquer
            peutDeplacer = false;  //Emp�cher le joueur de se d�placer pendant l'attaque
            GetComponent<Animator>().SetBool("animTir", true);
            GetComponent<AudioSource>().PlayOneShot(sonTir);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); //Stopper le d�placement du personnage

            Invoke("FinAnimTir", 0.25f);
            Invoke("FinAttaque", 0.05f);
            Invoke("PermettreDeplacement", 0.7f);

            munitions--;  //Enlever une munition � chaque tir
        }

        //Animation de mort pour le bouton suicide
        if (Input.GetKey(KeyCode.Backspace))
        {
            GetComponent<Animator>().SetBool("animMort", true);
            GetComponent<AudioSource>().PlayOneShot(sonMort);
            peutDeplacer = false;
            estEnVie = false;
            peutAttaque = false;
            Invoke("RecommencerScene", 2f);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*Collision avec les ennemis,
        Tag "Ennemi" tue le joueur s'il n'attaque pas,
        Tag "Ennemi2" tue le joueur peu importe (les "Ennemi2" peuvent �tre tu�s autrement que par les attaques)*/
        if ((collision.gameObject.tag == "Ennemi" && peutAttaque == true) || collision.gameObject.tag == "Ennemi2" || collision.gameObject.tag == "Ennemi3" || (collision.gameObject.tag == "Capitaine" && scriptCapitaine.panique == false && scriptCapitaine.viesRestantes >= 1))
        {
            GetComponent<Animator>().SetBool("animMort", true);
            GetComponent<AudioSource>().PlayOneShot(sonMort);
            peutDeplacer = false;
            estEnVie = false;
            peutAttaque = false;
            Invoke("AfficherMenu", 2f);

            if (transform.position.x > collision.transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(10, 10);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 10);
            }
        }
        if (collision.gameObject.tag == "Boulet")
        {
            GetComponent<Animator>().SetBool("animMort", true);
            GetComponent<Animator>().SetBool("animSaut", false);
            GetComponent<AudioSource>().PlayOneShot(sonMort);
            peutDeplacer = false;
            estEnVie = false;
            peutAttaque = false;
            Invoke("AfficherMenu", 2f);
        }
        //L'ennemi meurt si l'attaque est jou� et si le joueur est en vie (illustrer par peutDeplacer == true et estEnVie == true)
        if (collision.gameObject.tag == "Ennemi" && peutAttaque == false && estEnVie == true)
        {
            collision.gameObject.GetComponent<Animator>().SetBool("animMortEnnemi", true);
            collision.transform.parent.gameObject.GetComponent<Animator>().enabled = false;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            collision.gameObject.GetComponent<AudioSource>().enabled = true;
            Destroy(collision.gameObject, 1.5f);
        }
        if (collision.gameObject.tag == "Ennemi3" || (collision.gameObject.tag == "Capitaine"))
        {
            if (transform.position.x > collision.transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(20, 10);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 10);
            }
        }
        //Lorsque le Capitaine meurt
        if (collision.gameObject.tag == "Capitaine" && peutAttaque == false && scriptCapitaine.viesRestantes < 1)
        {
            collision.gameObject.GetComponent<Animator>().SetBool("animMort", true);
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(sonCapitaineMort);
            if (collision.transform.position.x > transform.position.x)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
            }

            scriptCapitaine.peutDeplacer = true;
            Destroy(collision.gameObject, 4f);
            Invoke("ApparitionMoustache", 4.25f);
        }
        //Losrque le Capitaine tue le Joueur pendant sa derni�re phase
        if (collision.gameObject.tag == "Capitaine" && peutAttaque == true && scriptCapitaine.viesRestantes < 0 && scriptCapitaine.panique == false)
        {
            GetComponent<Animator>().SetBool("animMort", true);
            GetComponent<AudioSource>().PlayOneShot(sonMort);
            peutDeplacer = false;
            estEnVie = false;
            peutAttaque = false;
            Invoke("AfficherMenu", 2f);
        }

        ///////////////////////////////////////////////////

        //D�tection de collision avec la cl�e
        if (collision.gameObject.tag == "Clee")
        {
            aLaClee = true;
            GetComponent<AudioSource>().PlayOneShot(sonCl�e);
            Destroy(collision.gameObject, 0f);
        }
        if (collision.gameObject.tag == "Munitions")
        {
            munitions = munitions + 3;
            GetComponent<AudioSource>().PlayOneShot(sonMunitions);
            Destroy(collision.gameObject, 0f);
        }


        ///////////////////////////////////////////////////////////

        //D�tection de collision avec les plateformes
        if (Physics2D.OverlapCircle(transform.position, 0.25f))
        {
            GetComponent<Animator>().SetBool("animSaut", false);

            //Le personnage devient l'enfant de la plateforme qu'il touche avec le OverlapCircle
            if (collision.gameObject.tag == "Plateformes")
            {
                transform.parent = collision.gameObject.transform;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ZoneExplosion")
        {
            Invoke("AnimMort", 0.2f);
            peutDeplacer = false;
            peutAttaque = false;

            if (transform.position.x > collision.transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(10, 30);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 30);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //Enlever le parent du personnage
        transform.parent = null;
    }


    //Permettre le d�placement apr�s une attaque
    void PermettreDeplacement()
    {
        peutDeplacer = true;
    }

    //Mettre fin � l'animation de l'attaque
    void FinAnimAttaque()
    {
        GetComponent<Animator>().SetBool("animAttaque", false);
    }

    //Faire joueur l'animation de mort avec un petit d�lai lorsqu'il meurt � cause du cochon
    void AnimMort()
    {
        GetComponent<Animator>().SetBool("animMort", true);
        Invoke("AfficherMenu", 2f);
    }

    //Mettre fin � l'animation du tir et cloner la balle tir�
    void FinAnimTir()
    {
        GetComponent<Animator>().SetBool("animTir", false);
        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY); //Reprendre le d�placement du personnage

        GameObject objetClone = Instantiate(balleOriginale);
        objetClone.SetActive(true);

        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            objetClone.transform.position = transform.position + new Vector3(1.5f, 1, 0);
            objetClone.GetComponent<Rigidbody2D>().velocity = new Vector2(25, 0);
        }
        else
        {
            objetClone.transform.position = transform.position + new Vector3(-1.5f, 1, 0);
            objetClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-25, 0);
        }
    }

    //Permettre au joueur d'attaquer de nouveau
    void FinAttaque()
    {
        peutAttaque = true;
    }

    //Fin du r�veil dans le premier niveau
    void FinReveil()
    {
        GetComponent<Animator>().SetBool("animReveil", false);
        Invoke("AfficherDialogue", 1f);
    }

    //Affichage du dialogue pour apr�s le r�veil
    void AfficherDialogue()
    {
        dialogue.gameObject.SetActive(true);
    }

    //Fermeture du dialogue lorsque le joueur appuie sur le bouton "Continuer"
    public void FermerDialogue()
    {
        dialogue.gameObject.SetActive(false);
        peutAttaque = true;
        peutDeplacer = true;
    }

    //Apparition de la moustache apr�s la mort du Capitaine
    void ApparitionMoustache()
    {
        moustache.gameObject.SetActive(true);
    }

    //Afiche le menu � la mort du personnage ou lorsqu'il appui sur la touche ESC
    void AfficherMenu()
    {
        menu.gameObject.SetActive(true);
        if (estEnVie)
        {
            Time.timeScale = 0;
        }
    }

    //Ferme le menu avec la touche ESC lorsque celui-ci a �t� ouvert avec la touche ESC
    void FermerMenu()
    {
        menu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    //Recharger le niveau
    void RecommencerScene()
    {
        int laScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(laScene);
    }
}
