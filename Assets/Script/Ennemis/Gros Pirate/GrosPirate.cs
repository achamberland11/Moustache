using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrosPirate : MonoBehaviour
{
    public Vector3 direction;
    public Vector2 vitesse;
    public float distanceX;  //Distance en X entre le joueur et le Gros Pirate
    public float distanceY;  //Distance en Y entre le joueur et le Gros Pirate
    public GameObject positionPersonnage;  //GameObject du joueur pour déterminer sa position
    public int viesRestantes;  //Nombre de vies restantes du gros pirate
    public bool peutDeplacer;  //Variable qui permet de définir si l'ennemi peut se déplacer

    public AudioClip sonDegat;  //Son qui joue lorsque le gros pirate prend des déguats et lorsqu'il meurt

    // Update is called once per frame
    void Update()
    {
        distanceX = transform.position.x - positionPersonnage.transform.position.x;
        distanceY = transform.position.y - positionPersonnage.transform.position.y;
        vitesse = GetComponent<Rigidbody2D>().velocity;

        if(peutDeplacer == false)
        {
            if((distanceX > -12 && distanceX < 12) && (distanceY > -12 && distanceY < 12))
            {
                direction = (positionPersonnage.transform.position - transform.position).normalized;
                GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * 2, 0);
            }

            if(distanceX < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            //---------------------------Gestion des animations------------------------------
            //Animation course
            if (vitesse.x > 0.3f || vitesse.x < -0.3f)
            {
                GetComponent<Animator>().SetBool("animMouvement", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("animMouvement", false);
            }

        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile" && viesRestantes > 1)
        {
            viesRestantes--;
            peutDeplacer = true;
            GetComponent<Animator>().SetBool("animDegat", true);
            GetComponent<AudioSource>().PlayOneShot(sonDegat);
            Invoke("FinAnim", 0.5f);

            if (transform.position.x > collision.transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
            }
        }
        else if(collision.gameObject.tag == "Projectile" && viesRestantes == 1)
        {
            peutDeplacer = true;
            GetComponent<Animator>().SetBool("animMortEnnemi", true);
            GetComponent<AudioSource>().PlayOneShot(sonDegat);
            Destroy(gameObject, 1.5f);

            if (transform.position.x > collision.transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(6, 0.5f);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-6, 0.5f);
            }
        }
    }

    void FinAnim()
    {
        GetComponent<Animator>().SetBool("animDegat", false);
        peutDeplacer = false;
    }
}
