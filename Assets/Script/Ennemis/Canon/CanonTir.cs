using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTir : MonoBehaviour
{

    public GameObject bouletOriginal;  //Boulet de canon original à cloner
    public float tempsRecharge;  //Délai entre chaque tir

    // Start is called before the first frame update
    void Start()
    {
        tir();
    }

    void tir()
    {
        GetComponent<Animator>().SetBool("animTir", true);
        GetComponent<AudioSource>().enabled = true;
        Invoke("finTir", 0.20f);
    }

    void finTir()
    {
        GameObject objetClone = Instantiate(bouletOriginal);
        objetClone.SetActive(true);

        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            objetClone.transform.position = transform.position + new Vector3(-0.45f, 0.2f, 0);
            objetClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-25, 0);
        }
        else
        {
            objetClone.transform.position = transform.position + new Vector3(0.45f, 0.2f, 0);
            objetClone.GetComponent<Rigidbody2D>().velocity = new Vector2(25, 0);
        }

        Invoke("finAnimTir", 0.5f);
    }

    void finAnimTir()
    {
        GetComponent<Animator>().SetBool("animTir", false);

        Invoke("tir", tempsRecharge);
    }
}
