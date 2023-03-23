using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaleineCollision : MonoBehaviour
{
    public GameObject baleine;  //GameObject de la baleine
    public GameObject baleineDeplacement;  //GameObject du déplacement de la baleine (parent direct de la baleine)

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //La baleine est tué lorsque le joueur saute sur sa tête (GameObject: Baleine collision)
        if(collision.gameObject.name == "Joueur")
        {
            Invoke("DesactiverBaleine", 0.6f);
            baleine.gameObject.GetComponent<Animator>().SetBool("animAttaque", false);
            baleine.gameObject.GetComponent<Animator>().SetBool("animMortEnnemi", true);
            baleine.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            baleineDeplacement.gameObject.GetComponent<Animator>().enabled = false;
        }
    }

    void DesactiverBaleine()
    {
        baleine.SetActive(false);
    }
}
