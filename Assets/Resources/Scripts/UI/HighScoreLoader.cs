using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreLoader : MonoBehaviour
{
    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            if (PlayerPrefs.HasKey("highscore"))
                text.text = "Highscore : " + PlayerPrefs.GetInt("highscore");
        }
    }
}
