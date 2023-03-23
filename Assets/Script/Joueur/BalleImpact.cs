using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalleImpact : MonoBehaviour
{
    public int viesGrosPirate = 3;  //Nombre de vie restante du gros pirate

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Jouer l'animation d'impact de balle lorsque la balle touche une surface (sauf celle de la porte)
        if(collision.gameObject.name != "Porte")
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Destroy(gameObject, 0.15f);
        }

        //Éliminer l'ennemi s'il est touché par une balle
        if(collision.gameObject.tag == "Ennemi")
        {
            collision.gameObject.GetComponent<Animator>().SetBool("animMortEnnemi", true);
            collision.transform.parent.gameObject.GetComponent<Animator>().enabled = false;
            collision.gameObject.GetComponent<AudioSource>().enabled = true;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(collision.gameObject, 1.5f);
        }
    }
}
