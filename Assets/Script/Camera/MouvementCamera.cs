using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementCamera : MonoBehaviour
{
    public GameObject cible;  //GameObject à suivre pour la caméra

    public float limiteHaut;  //Limite de mouvement vers le haut
    public float limiteBas;  //Limite de mouvement vers le bas
    public float limiteGauche;  //Limite de mouvement vers la droite
    public float limiteDroite;  //Limite de mouvement vers la gauche


    // Update is called once per frame
    void Update()
    {
        Vector3 positionActuelle = cible.transform.position;

        if (positionActuelle.x < limiteGauche) positionActuelle.x = limiteGauche;
        if (positionActuelle.x > limiteDroite) positionActuelle.x = limiteDroite;
        if (positionActuelle.y > limiteHaut) positionActuelle.y = limiteHaut;
        if (positionActuelle.y < limiteBas) positionActuelle.y = limiteBas;

        positionActuelle.z = -10;

        transform.position = positionActuelle;
    }
}
