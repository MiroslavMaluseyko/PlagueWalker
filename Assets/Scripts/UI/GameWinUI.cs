
using UnityEngine;

public class GameWinUI : MonoBehaviour
{
    //reduced Singleton
    public static GameWinUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
