using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //Charger le niveau 1 (scene 1) lorsque le joueur appui sur Nouvelle Partie
    public void NouvellePartie()
    {
        int laScene = SceneManager.GetActiveScene().buildIndex;
        laScene++;
        SceneManager.LoadScene(laScene);
    }

    //Fermer l'application lorsque le joueur appui sur Quitter
    public void Quitter()
    {
        Application.Quit();
    }
}
