using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string nextLevel;
    public float delay = 1.5f;
    private bool levelCompleted = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !levelCompleted)
        {
            levelCompleted = true;
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        // we need to add cool stuff that should occur when a level is completed

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(nextLevel);
    }
}
