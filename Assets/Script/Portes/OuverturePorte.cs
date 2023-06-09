using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OuverturePorte : MonoBehaviour
{
    public bool ouvrirPorte = false;  //Bool qui d�termine si le joueur est devant la porte

    private void Update()
    {
        //La porte est ouverte si le joueur est devant celle-ci et qu'il appuie sur "w"
        if (Input.GetKeyDown("w") && ouvrirPorte == true)
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Collider2D>().enabled = false;
            Invoke("AnimationFondu", 0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //D�tecter lorsque le joueur est devant la porte
        if (collision.gameObject.name == "Joueur")
        {
            ouvrirPorte = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //D�tecter lorsque le joueur n'est pas devant la porte
        if (collision.gameObject.name == "Joueur")
        {
            ouvrirPorte = false;
        }
    }

    void AnimationFondu()
    {
        Invoke("SceneSuivante", 2f);
    }

    void SceneSuivante()
    {
        //Charger la sc�ne suivante lorsque le joueur traverse la porte
        int laScene = SceneManager.GetActiveScene().buildIndex;
        laScene++;
        SceneManager.LoadScene(laScene);
    }
}
