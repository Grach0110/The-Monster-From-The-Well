using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_Script : MonoBehaviour
{
    static int player;
    public GameObject virtualGuy;
    public GameObject pinkMan;

    public void Awake()
    {
        if (virtualGuy != null && pinkMan != null)
        {
            if (player == 0)
            {
                Instantiate(virtualGuy, new Vector3(0, 1.6f,0), Quaternion.identity);
            }

            if (player == 1)
            {
                Instantiate(pinkMan, new Vector3(0, 1.6f, 0), Quaternion.identity);
            }
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void VirtualGuy()
    {
        player = 0;
        NewGame();
    }

    public void PinkMan()
    {
        player = 1;
        NewGame();
    }
}