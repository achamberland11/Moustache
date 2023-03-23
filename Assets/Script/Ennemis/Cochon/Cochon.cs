using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cochon : MonoBehaviour
{

    public GameObject cochonDeplacement;  //Variable représentant le déplacement du cochon
    public GameObject cochonCollision;  //Variable représentant la zone d'explosion du cochon

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            GetComponent<Animator>().SetBool("animMortEnnemi", true);
            GetComponent<BoxCollider2D>().enabled = false;
            cochonDeplacement.GetComponent<Animator>().enabled = false;
            cochonCollision.GetComponent<AudioSource>().enabled = true;
            Destroy(gameObject, 0.5f);
        }
    }
}
