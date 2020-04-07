using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject Go_pause;
    private bool pause;

    private void Start()
    {
        pause = true;
        Time.timeScale = 1;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("Pausa");
            pause = !pause;
            if (pause)
            {
                Time.timeScale = 1;
                Go_pause.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                Go_pause.SetActive(true);
            }
        }

    }

    public void action_continue()
    {
        pause = !pause;
        Go_pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void main_menu()
    {
        Go_pause.SetActive(false);
        SceneManager.LoadScene("FG_Main_screen");
    }

}
