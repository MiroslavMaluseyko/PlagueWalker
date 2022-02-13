
using TMPro;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    //reduced Singleton
    public static MainMenuUI Instance;

    public TextMeshProUGUI maxScore;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    //when menu activated show best score
    private void OnEnable()
    {
        maxScore.SetText(PlayerPrefs.GetInt("Score").ToString());
    }
}
