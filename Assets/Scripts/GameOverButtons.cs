using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public GameObject loseScreen;
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
        loseScreen.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TitlePage");
    }
}
