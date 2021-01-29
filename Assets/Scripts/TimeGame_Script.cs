using UnityEngine;
using UnityEngine.UI;

public class TimeGame_Script : MonoBehaviour
{
    private GameObject player;
    private GameObject sc;

    public bool runTime;
    public Text gameTime;

    private float param;
    private float sec;
    private float min;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        runTime = false;
        gameTime.text = "00:00";
    }

    private void Update()
    {
        if (runTime)
        {
            TimeGame();
        }
    }

    public void TimeGame()
    {
        param -= Time.deltaTime;

        if (param <= 0)
        {
            param = 1;
            sec = sec + 1;
        }

        if (sec >= 60)
        {
            min = min + 1;
            sec = 0;
        }

        if (min >= 60)
        {
            sc = GameObject.FindGameObjectWithTag("SceneManager");
            sc.GetComponent<SceneManager_Script>().MainMenu();
        }
        else
        {
            gameTime.text = min + ":" + sec;
        }
    }
}