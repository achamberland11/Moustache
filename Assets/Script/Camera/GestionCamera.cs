using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionCamera : MonoBehaviour
{
    public GameObject CameraJoueur;
    public GameObject CameraBoss;

    void Start()
    {
        CameraJoueur.SetActive(true);
        CameraBoss.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Joueur")
        {
            CameraJoueur.SetActive(false);
            CameraBoss.SetActive(true);
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Joueur")
        {
            CameraJoueur.SetActive(true);
            CameraBoss.SetActive(false);
        }
    }
}
