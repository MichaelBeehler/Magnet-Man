using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string nextLevel;
    public float delay = 1.5f;

    public bool requiresBallTouch;
    private bool levelCompleted = false;
    public int levelNumber = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (!levelCompleted)
        {
            if (!requiresBallTouch && other.CompareTag("Player"))
            {
                levelCompleted = true;
                QuizManager.Instance.StartPostLevelQuiz(levelNumber);
            }
            
            else if (requiresBallTouch && other.transform.root.GetComponent<PointCharge>()!= null)
            {
                levelCompleted = true;
                QuizManager.Instance.StartPostLevelQuiz(levelNumber);
            }
        }
    }
}
