using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float gameTime = 120f; // total time limit in seconds
    private float timeRemaining;

    public int money = 0;
    public int quota = 100;

    public Vector2Int moneyRange = new Vector2Int(10, 25); // min-max pesos per customer

    public Text timeText;
    public Text moneyText;
    public Text endmoneyText;
    public GameObject winScreen;
    public GameObject loseScreen;
    public float coinDelay = 5f; // Time customer takes to finish eating

    private bool gameOver = false;
    public int GetTotalMoney()
    {
        return money;
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        timeRemaining = gameTime;
        UpdateUI();
    }

    void Update()
    {
        if (gameOver) return;

        timeRemaining -= Time.deltaTime;
        UpdateUI();

        if (timeRemaining <= 0)
        {
            EndGame(money >= quota);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume normal time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
    }

    void UpdateUI()
    {
        if (timeText != null)
            timeText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString();

        if (moneyText != null)
            moneyText.text = "Money: $" + money.ToString();

        if (endmoneyText != null)
            endmoneyText.text = "$" + money.ToString();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();

        if (money >= quota)
        {
            //EndGame(true);
        }
    }

    void EndGame(bool won)
    {
        gameOver = true;
        Time.timeScale = 0f;

        if (won && winScreen != null)
            loseScreen.SetActive(true);

        if (!won && loseScreen != null)
            loseScreen.SetActive(true);
    }

}
