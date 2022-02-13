using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    //reduced Singleton
    public static GameOverUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

}
