using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Dvi varijable za zvukove
    public AudioClip BallCollision;
    public AudioClip Goal;
    //Za pobjedu i gubitak
    public AudioClip LostGame;
    public AudioClip WinGame;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBallCollision()
    {
        audioSource.PlayOneShot(BallCollision);
    }

    public void PlayGoal()
    {
        audioSource.PlayOneShot(Goal);
    }

    public void PlayLostGame()
    {
        audioSource.PlayOneShot(LostGame);
    }

    public void PlayWinGame()
    {
        audioSource.PlayOneShot(WinGame);
    }
}


