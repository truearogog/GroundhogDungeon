using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private int score = 0;

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int add)
    {
        score += add;
        scoreText.text = "Score : \n" + score;
    }
}
