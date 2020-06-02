using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private int nextScene;
    [SerializeField] private bool checkIfPlayed;

    public void LoadNextLevel(float delay)
    {
        bool played = false;
        if (PlayerPrefs.HasKey("played"))
            played = PlayerPrefs.GetInt("played") == 1;

        if (!checkIfPlayed)
        {
            StartCoroutine(LoadLevel(delay, nextScene));
        }
        else
        {
            if (played)
                StartCoroutine(LoadLevel(delay, nextScene));
            else
                StartCoroutine(LoadLevel(delay, 1));
        }
    }

    private IEnumerator LoadLevel(float delay, int index)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
    }
}
