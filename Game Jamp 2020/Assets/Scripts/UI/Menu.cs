using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Canvas canvas_menu ;
    public Canvas canvas_credits;


    private void Awake()
    {
        canvas_menu = canvas_menu.GetComponent<Canvas>();
        canvas_credits = canvas_credits.GetComponent<Canvas>();


        canvas_menu.enabled = true;
        canvas_credits.enabled = false;
    }

    public void start_game()
    {
        load_screen("Nivel Tutorial");

    }
    public void credits_screen()
    {
        canvas_menu.enabled = false;
        canvas_credits.enabled = true;
    }
    public void exit_game()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif

    }
    public void menu_screen()
    {
        canvas_menu.enabled = true;
        canvas_credits.enabled = false;
    }

    private void load_screen(string juego)
    {
        SceneManager.LoadScene(juego);
    }

}
