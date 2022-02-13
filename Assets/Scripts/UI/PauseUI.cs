
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    //reduced Singleton
    public static PauseUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
