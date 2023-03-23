using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateAttaque : MonoBehaviour
{
    public GameObject pirate;  //Variable représentant le pirate

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Joueur")
        {
            pirate.gameObject.GetComponent<Animator>().SetBool("animAttaque", true);
            Invoke("FinAnimAttaque", 0.25f);
        }
    }

    void FinAnimAttaque()
    {
        pirate.gameObject.GetComponent<Animator>().SetBool("animAttaque", false);
    }
}
