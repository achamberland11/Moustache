using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouletImpact : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().mass = 0;
        GetComponent<AudioSource>().enabled = true;
        Destroy(gameObject, 0.6f);

        /*if(collision.gameObject.tag == "Joueur")
        {
            collision.gameObject.GetComponent<Animator>().SetBool("animSaut", false);
        }*/
    }
}
