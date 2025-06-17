using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayButton ()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitButton ()
    {
        Application.Quit();
    }
}
