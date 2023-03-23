using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CochonCollision : MonoBehaviour
{
    public GameObject cochon;  //Variable représentant le cochon
    public GameObject cochonDeplacement;  //Variable représentant le déplacement du cochon
    public Personnage scriptJoueur; //Variable donnant accès au script du joueur
    public AudioClip sonMortJoueur;  //Son de la mort du joueur

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Joueur")
        {
            cochon.gameObject.GetComponent<Animator>().SetBool("animMortEnnemi", true);
            collision.gameObject.GetComponent<Animator>().SetBool("animMort", true);
            collision.gameObject.GetComponent<Animator>().SetBool("animSaut", false);
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(sonMortJoueur);
            scriptJoueur.estEnVie = false;
            scriptJoueur.peutDeplacer = false;
            cochon.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            cochonDeplacement.GetComponent<Animator>().enabled = false;
            GetComponent<AudioSource>().enabled = true;
            Destroy(cochon, 0.5f);
        }
    }

}
