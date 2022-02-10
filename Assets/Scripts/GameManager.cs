using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartDelay = 1f;

    public void GameOver()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over!");
            Invoke("Restart", restartDelay);
            //new scene or ui panel
        }

    }

    public void YouWin()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("You Win!");
            SceneManager.LoadScene("Winner");
        }

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
