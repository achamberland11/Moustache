using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuCrédit : MonoBehaviour
{
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
