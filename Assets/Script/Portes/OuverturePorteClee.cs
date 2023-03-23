using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OuverturePorteClee : MonoBehaviour
{
    public bool ouvrirPorte = false;  //Bool qui détermine si le joueur est devant la porte
    public Personnage scriptJoueur; //GameObject du joueur
    public GameObject fondu;  //GameObject du fondu en début et en fin de niveau


    private void Update()
    {
        //La porte est ouverte si le joueur est devant celle-ci et qu'il appuie sur "w" et qu'il à la clée en sa possession
        if (Input.GetKeyDown("w") && ouvrirPorte == true && scriptJoueur.aLaClee == true)
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Collider2D>().enabled = false;
            Invoke("AnimationFondu", 0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Détecter lorsque le joueur est devant la porte
        if (collision.gameObject.name == "Joueur")
        {
            ouvrirPorte = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Détecter lorsque le joueur n'est pas devant la porte
        if (collision.gameObject.name == "Joueur")
        {
            ouvrirPorte = false;
        }
    }

    void AnimationFondu()
    {
        fondu.gameObject.GetComponent<Animator>().SetTrigger("fonduFermeture");
        Invoke("SceneSuivante", 2f);
    }

    void SceneSuivante()
    {
        //Charger la scène suivante lorsque le joueur traverse la porte
        int laScene = SceneManager.GetActiveScene().buildIndex;
        laScene++;
        SceneManager.LoadScene(laScene);
    }
}
