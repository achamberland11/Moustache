using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrosPirateAttaque : MonoBehaviour
{
    public GameObject grosPirate;  //Variable représentant le gros pirate

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Joueur")
        {
            grosPirate.gameObject.GetComponent<Animator>().SetBool("animAttaque", true);
            Invoke("FinAnimAttaque", 0.5f);
        }
    }

    void FinAnimAttaque()
    {
        grosPirate.gameObject.GetComponent<Animator>().SetBool("animAttaque", false);
    }
}
