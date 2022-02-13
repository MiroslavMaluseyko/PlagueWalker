using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //Singleton pattern fo all managers
    public static MenuManager Instance;

    //prefabs of all pop-up menus
    public PauseUI pausePrefab;
    public GameOverUI gameOverPrefab;
    public GameWinUI gameWinPrefab;
    public SettingsUI settingsPrefab;
    private void Awake()
    { 
        //destroy unwanted copies for Singleton
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MenuManager");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        
        if (Instance == null) Instance = this;
        
        DontDestroyOnLoad(this);
    }
    
    //pause option
    public void Pause()
    {
        if (PauseUI.Instance == null)
        {
            Instantiate(pausePrefab);
        }

        if (!GameManager.Instance.gamePaused)
        {
            GameManager.Instance.Pause();
            PauseUI.Instance.gameObject.SetActive(true);
        }
    }
    
    //unpause option
    public void Unpause()
    {
        if (PauseUI.Instance == null)
        {
            Instantiate(pausePrefab);
        }
        
        PauseUI.Instance.gameObject.SetActive(false);
    }

    //game over option
    public void GameOver()
    {
        if (GameOverUI.Instance == null)
        {
            Instantiate(gameOverPrefab);
        }
        GameOverUI.Instance.gameObject.SetActive(true);
    }

    public void GameWin()
    {
        if (GameWinUI.Instance == null)
        {
            Instantiate(gameWinPrefab);
        }
        GameWinUI.Instance.gameObject.SetActive(true);
    }
    
    //to main menu option
    public void ToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    //open settings menu option
    public void SettingsOn()
    {
        if (SettingsUI.Instance == null)
        {
            Instantiate(settingsPrefab);
        }
        
        SettingsUI.Instance.gameObject.SetActive(true);
    }
    
    //close settings menu option
    public void SettingsOff()
    {
        if (SettingsUI.Instance == null)
        {
            Instantiate(settingsPrefab);
        }
        
        SettingsUI.Instance.gameObject.SetActive(false);
    }
}
