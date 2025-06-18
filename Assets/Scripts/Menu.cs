using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject dialogBox; 
    public void PlayButton ()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitButton ()
    {
        Application.Quit();
    }

    public void HelpButton()
    {
       dialogBox.SetActive(true);
    }

    public void CloseHelpButton()
    {
        dialogBox.SetActive(false);
    }
}
