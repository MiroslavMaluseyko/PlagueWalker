using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float chargePerFrame;

    public bool isPaused;
    public bool isGameplay;

    public Player player {get; private set;}
    public Target target {get; private set;}
    
    private void Awake()
    { 
        //destroy unwanted copies for Singleton
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(gameObject);
        
        
        if (isGameplay)
        {
            target = FindObjectOfType<Target>();
            player = FindObjectOfType<Player>();
        }
    }

    private void Start()
    {
        if (isGameplay)
        {
            target = FindObjectOfType<Target>();
            player = FindObjectOfType<Player>();
        }
    }

    public void LoadLevel(string level)
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(level);
        if (isGameplay)
        {
            target = FindObjectOfType<Target>();
            player = FindObjectOfType<Player>();
        }
    }

    public void GameOver()
    {
        Debug.Log("GmeOver");
    }

}
