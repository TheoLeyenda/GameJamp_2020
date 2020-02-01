using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    GameObject[] botones_pausa;

    void Start()
    {
        Time.timeScale = 1;
        botones_pausa = GameObject.FindGameObjectsWithTag("Mostrar_en_pausa");
        esconder_botones();
    }

    // Update is called once per frame
    void Update()
    {

        //Pausa o resume el juego con la "P"
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                Debug.Log("Detener");
                Time.timeScale = 0;
                mostrar_botones();
            }
            else if (Time.timeScale == 0)
            {
                Debug.Log("Continuar");
                Time.timeScale = 1;
                esconder_botones();
            }
        }
    }


    public void reiniciar()
    {
        //Reiniciar el nivel
    }

    //Chequeo de pausa
    public void chequear_pausa()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            mostrar_botones();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            esconder_botones();
        }
    }

    //Muestra los botones si es que esta en pausa
    public void mostrar_botones()
    {
        foreach (GameObject g in botones_pausa)
        {
            g.SetActive(true);
        }
    }

    //Oculta los botones
    public void esconder_botones()
    {
        foreach (GameObject g in botones_pausa)
        {
            g.SetActive(false);
        }
    }

    //Carga el nivel
    public void cargar_nivel(string nivel)
    {
       //Carga el nivel
    }



}
