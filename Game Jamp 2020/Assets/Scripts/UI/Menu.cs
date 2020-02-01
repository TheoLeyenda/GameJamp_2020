using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public void start_game()
    {
        Debug.Log("Empeze el juego");
        cargar_juego("Game_screen");
        
    }
    public void credits_screen()
    {
        Debug.Log("Mira quien carajo hizo el juego");
    }
    public void exit_game()
    {
        Debug.Log("Me rajo de esta mierda");
        
    }

    private void cargar_juego(string juego)
    {
        SceneManager.LoadScene(juego);
    }

}
