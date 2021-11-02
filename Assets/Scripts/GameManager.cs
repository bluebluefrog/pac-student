using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject StartTimer;

    public GameObject pacman;

    public int score = 0;
    public Text remainText;
    public Text nowText;
    public Text scoreText;

    public GameObject Player;

    public int HealthValue = 3;

    public float Starttimer;


    public bool OnStartGame = true;


    private void Start()
    {
        OnStartGame = true;
    }

    private void Update()
    {
        //After obtaining the highest score script, compare whether the current score is greater than the highest score, and if it is greater, re-assign the highest score
        DontDestroy dontDestroy = GameObject.Find("DontDestroy").GetComponent<DontDestroy>();
        if (dontDestroy.MaxScore < score)
        {
            dontDestroy.MaxScore = score;
        }
    }

    public void OnStartButton()
    {
         
    }

    public void PlayAudio() {
        GetComponent<AudioSource>().Play();
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

}
