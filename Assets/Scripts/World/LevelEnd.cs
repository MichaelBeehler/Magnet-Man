using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string nextLevel;
    public float delay = 1.5f;

    public bool requiresBallTouch;
    private bool levelCompleted = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!levelCompleted)
        {
            if (!requiresBallTouch && other.CompareTag("Player"))
            {
                levelCompleted = true;
                StartCoroutine(LoadNextLevel());
            }
            
            else if (requiresBallTouch && other.transform.root.GetComponent<PointCharge>()!= null)
            {
                levelCompleted = true;
                StartCoroutine(LoadNextLevel());
            }
        }
    }

    IEnumerator LoadNextLevel()
    {
        // we need to add cool stuff that should occur when a level is completed

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(nextLevel);
    }
}
