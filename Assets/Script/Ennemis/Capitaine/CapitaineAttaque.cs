using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitaineAttaque : MonoBehaviour
{
    public GameObject capitaine;  //Variable repr�sentant le gros pirate

    public bool peutDeplacer;  //Variable qui permet de d�finir si l'ennemi peut se d�placer

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Joueur")
        {
            capitaine.gameObject.GetComponent<Animator>().SetBool("animAttaque", true);
            Invoke("FinAnimAttaque", 0.5f);
        }
    }

    void FinAnimAttaque()
    {
        capitaine.gameObject.GetComponent<Animator>().SetBool("animAttaque", false);
    }
}
