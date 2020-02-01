using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pause;

    void Start()
    {
    }
    public void action_continue()
    {
        //Vuelve al juego
        pause.SetActive(false);
    }
    public void action_reset()
    {
        //Reinicia el nivel
        pause.SetActive(false);
        reset_level();

    }
    public void main_menu()
    {
        pause.SetActive(false);
    }

    void reset_level()
    {
        //resetea el nivel
        pause.SetActive(false);
    }

}
