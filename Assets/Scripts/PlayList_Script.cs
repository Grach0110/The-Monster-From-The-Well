using UnityEngine;

public class PlayList_Script : MonoBehaviour
{
    public AudioClip[] music;
    AudioSource audioSource;
    public bool isGame;

    private void Start()
    {
        isGame = true;
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    private void Update()
    {
        if (isGame)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = GetRandomClip();
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
        
    }

    private AudioClip GetRandomClip()
    {
        return music[Random.Range(0, music.Length)];
    }
}