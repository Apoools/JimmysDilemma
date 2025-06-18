using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // <- Legacy UI

public class GameOverScreen : MonoBehaviour
{
    public Text moneyText; // Legacy UI Text

    void OnEnable()
    {
        if (moneyText != null && GameManager.Instance != null)
        {
            int totalMoney = GameManager.Instance.GetTotalMoney();
            moneyText.text = "Total Money Earned: ₱" + totalMoney.ToString();
        }
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TitlePage");
    }
}
