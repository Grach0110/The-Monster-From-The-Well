using UnityEngine;
using UnityEngine.UI;

public class Finish_Script : MonoBehaviour
{
    public GameObject timeGame;
    public GameObject panelMenu;
    public Text textTimeGame;
    AudioSource audioSource;
    public GameObject playList;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Time.timeScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
            collision.gameObject.GetComponent<PlayerContriller_Script>().stars)
        {
            playList.GetComponent<PlayList_Script>().isGame = false;
            audioSource.Play();
            collision.gameObject.GetComponent<PlayerContriller_Script>().speed = 0;
            timeGame.GetComponent<TimeGame_Script>().runTime = false;
            panelMenu.SetActive(true);
            textTimeGame.text = "Время игры: " + timeGame.GetComponent<TimeGame_Script>().gameTime.text;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timeGame.GetComponent<TimeGame_Script>().runTime = true;
        }
    }

    public void PlayerDead()
    {
        timeGame.GetComponent<TimeGame_Script>().runTime = false;
        panelMenu.SetActive(true);
        textTimeGame.text = "Время игры: " + timeGame.GetComponent<TimeGame_Script>().gameTime.text;

        Time.timeScale = 0;
    }
}