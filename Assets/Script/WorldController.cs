using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldController : MonoBehaviour
{
    private AudioSource stageMusic;
    [SerializeField] private AudioSource end;
    [SerializeField] private GameObject pause;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreShadow;
    [SerializeField] private GameObject deadCanvas;
    [SerializeField] private PlayerController pl;
    private bool dead;
    private bool paused;
   
    
    void Start()
    {      
        WorldData.killSmallAsteroids = 0;
        WorldData.needToKill = 8;
        WorldData.resAsteroids = 2;
        stageMusic = GetComponent<AudioSource>();
        stageMusic.Play();
        paused = false;
        pause.SetActive(false);
        deadCanvas.SetActive(false);
        dead = false;
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        GamePausedAndDead();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    private void GamePausedAndDead()
    {
        scoreText.text = WorldData.points.ToString();
        scoreShadow.text = WorldData.points.ToString();
        if (WorldData.hp <= 0)
        {
            stageMusic.Stop();
            end.Play();
            deadCanvas.SetActive(true);
            dead = true;
            Time.timeScale = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && dead == false)
        {
            if (!paused)
            {
                paused = !paused;
                Time.timeScale = 0;
                pause.SetActive(true);
                stageMusic.Pause();
                Time.timeScale = 0f;
            }
            else
            {
                paused = !paused;
                Time.timeScale = 1;
                pause.SetActive(false);
                stageMusic.Play();
            }
        }
    }

    public void ChoiseController(int value)
    {
        if (value == 0)
        {
            pl.mouseController = false;
        }
        else if (value == 1)
        {
            pl.mouseController = true;
        }
    }
}
