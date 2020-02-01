using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void start_game()
    {
        Debug.Log("Empeze el juego");
        load_screen("Game_screen");

    }
    public void credits_screen()
    {
        Debug.Log("Mira quien carajo hizo el juego");
        load_screen("FG_Credits_Screen");
    }
    public void exit_game()
    {
        Debug.Log("Me rajo de esta mierda");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif

    }

    private void load_screen(string juego)
    {
        SceneManager.LoadScene(juego);
    }

}
