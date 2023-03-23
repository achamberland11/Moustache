using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuJeu : MonoBehaviour
{
    //Recommencer le niveau lorsque le joueur appui sur Recommencer
    public void Recommencer()
    {
        int laScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(laScene);
    }

    //Charger le menu (scene 0) lorsque le joueur appui sur Menu
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    //Fermer l'application lorsque le joueur appui sur Quitter
    public void Quitter()
    {
        Application.Quit();
    }
}
