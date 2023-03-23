using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capitaine : MonoBehaviour
{
    public Vector3 direction;
    public Vector2 vitesse;
    public float distanceX;  //Distance en X entre le joueur et le Gros Pirate
    public float distanceY;  //Distance en Y entre le joueur et le Gros Pirate

    public GameObject positionPersonnage;  //GameObject du joueur pour déterminer sa position
    public GameObject capitaineCollision;  //GameObject représantant la boîte de collision sur la tête du capitaine
    public GameObject capitaineDeplacement;  //Variable représantant le déplacement du capitaine(parent direct dont l'animator n'est actif que lorsqu'il panique)

    public AudioClip sonDegat;  //Son qui joue lorsque le Capitaine prend des dégats et lorsqu'il meurt

    public int viesRestantes;  //Nombre de vies restantes du gros pirate

    public bool peutDeplacer;  //Variable qui permet de définir si l'ennemi peut se déplacer
    public bool panique;

    // Start is called before the first frame update
    void Start()
    {
        panique = false;
        capitaineCollision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceX = transform.position.x - positionPersonnage.transform.position.x;
        distanceY = transform.position.y - positionPersonnage.transform.position.y;
        vitesse = GetComponent<Rigidbody2D>().velocity;

        if (peutDeplacer == false)
        {
            if(panique == false)
            {
                if ((distanceX > -9 && distanceX < 9) && (distanceY > -9 && distanceY < 9))
                {
                    direction = (positionPersonnage.transform.position - transform.position).normalized;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * 2, 0);
                }

                if (distanceX < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            else
            {
                vitesse.x = 5;
                vitesse.x = -5;
            }

            //---------------------------Gestion des animations------------------------------
            //Animation course
            if (vitesse.x > 0.2f || vitesse.x < -0.2f)
            {
                GetComponent<Animator>().SetBool("animMouvement", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("animMouvement", false);
            }

        }

        if(viesRestantes < 1 && panique == false)
        {
            capitaineCollision.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile" && viesRestantes > 1)
        {
            viesRestantes--;
            peutDeplacer = true;
            GetComponent<Animator>().SetBool("animDegat", true);
            GetComponent<AudioSource>().PlayOneShot(sonDegat);
            Invoke("FinAnimDeguat", 0.5f);

            if (transform.position.x > collision.transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
            }
        }
        else if (collision.gameObject.tag == "Projectile" && viesRestantes == 1)
        {
            viesRestantes--;
            peutDeplacer = true;
            panique = true;
            GetComponent<Animator>().SetBool("animPanique", true);
            GetComponent<AudioSource>().PlayOneShot(sonDegat);
            capitaineCollision.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            Invoke("FinAnimDeguat", 0.5f);

            if (transform.position.x > collision.transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
            }

        }
    }

    void FinAnimDeguat()
    {
        GetComponent<Animator>().SetBool("animDegat", false);
        if(panique == true)
        {
            capitaineDeplacement.gameObject.GetComponent<Animator>().enabled = true;
        }
        peutDeplacer = false;
    }
}
