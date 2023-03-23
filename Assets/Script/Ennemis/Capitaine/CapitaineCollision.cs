using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitaineCollision : MonoBehaviour
{
    public GameObject capitaine;  //GameObject du capitaine
    public Capitaine scriptCapitaine;  //Variable donnant accès au script du capitaine
    public GameObject capitaineDeplacement;  //GameObject du déplacement du capitaine (parent direct du capitaine)

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Le capitaine est tué lorsque le joueur saute sur sa tête (GameObject: Baleine collision)
        if (collision.gameObject.name == "Joueur")
        {
            capitaine.gameObject.GetComponent<Animator>().SetBool("animPanique", false);
            capitaine.gameObject.GetComponent<Animator>().SetBool("animDegat", true);
            capitaineDeplacement.gameObject.GetComponent<Animator>().enabled = false;
            Invoke("FinAnim", 0.5f);
        }
    }

    void FinAnim()
    {
        capitaine.gameObject.GetComponent<Animator>().SetBool("animDegat", false);
        scriptCapitaine.panique = false;
    }
}
