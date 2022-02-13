using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float chargePerFrame;

    public bool gamePaused;

    //how many different levels are in game
    [SerializeField] private int levelsCount;

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
        
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstStart", 0) == 0)
        {
            PlayerPrefs.SetInt("FirstStart", 1);
            AudioManager.Instance.ChangeMusicVolume(1f);
            AudioManager.Instance.ChangeSfxVolume(1f);
        }
    }

    public void LoadLevel(string level)
    {
        Time.timeScale = 1f;
        Instance.gamePaused = false;
        SceneManager.LoadScene(level);
    }

    public void LoadNextLevel()
    {
        int levelsPassed = PlayerPrefs.GetInt("Score");
        LoadLevel("Level" + (levelsPassed % levelsCount + 1));
    }

    public void GameOver()
    {
        AudioManager.Instance.Play("Losing");
        Time.timeScale = 0;
        Instance.gamePaused = true;
        MenuManager.Instance.GameOver();
    }
    

    public void GameWin()
    {
        AudioManager.Instance.Play("Winning");
        Instance.gamePaused = true;
        Time.timeScale = 0;
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
        MenuManager.Instance.GameWin();
    }

    public void Pause()
    {
        if (!Instance.gamePaused)
        {

            Instance.gamePaused = true;
            Time.timeScale = 0;

            MenuManager.Instance.Unpause();
        }
    }

    public void Unpause()
    {
        if (Instance.gamePaused)
        {
            Instance.gamePaused = false;
            Time.timeScale = 1;

            MenuManager.Instance.Unpause();
        }
    }

    public void Restart()
    {
        LoadLevel(SceneManager.GetActiveScene().name);
    }
    
    
    public void Quit()
    {
        Application.Quit();
    }

}
