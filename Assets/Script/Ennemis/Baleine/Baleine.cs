using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baleine : MonoBehaviour
{
    public float distance;  //Distance entre le joueur et la baleine
    public GameObject positionPersonnage;  //GameObject du joueur pour déterminer sa position

    // Update is called once per frame
    void Update()
    {
        //La baleine fait son animation d'attaque lorsque le personnage est proche (- de 8)
        distance = transform.position.x - positionPersonnage.transform.position.x;

        if(distance < 8 && distance > -8)
        {
            GetComponent<Animator>().SetBool("animAttaque", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("animAttaque", false);
        }
    }
}
