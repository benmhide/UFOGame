using UnityEngine;
using System.Collections;

public class GameStartScript : MonoBehaviour
{
    private GameController gameController;
    public AudioClip start3;
    public AudioClip start2;
    public AudioClip start1;
    public AudioClip startGo;

    AudioSource CountDown;


    void Start()
    {
        CountDown = GetComponent<AudioSource>();
    }
    public void SetCountDownNow()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.startCountDownFinished = true;
    }

    public void Play3Sound()
    {
        CountDown.clip = start3;
        CountDown.Play();
    }

    public void Play2Sound()
    {
        CountDown.clip = start2;
        CountDown.Play();
    }

    public void Play1Sound()
    {
        CountDown.clip = start1;
        CountDown.Play();
    }

    public void PlayGoSound()
    {
        CountDown.clip = startGo;
        CountDown.Play();
    }
}
