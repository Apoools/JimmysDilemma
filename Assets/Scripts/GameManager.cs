using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float gameTime = 60f; // total time limit in seconds
    private float timeRemaining;

    public int money = 0;
    public int quota = 100;

    public Vector2Int moneyRange = new Vector2Int(10, 25); // min-max pesos per customer

    public Text timeText;
    public Text moneyText;
    public GameObject winScreen;
    public GameObject loseScreen;
    public float coinDelay = 5f; // Time customer takes to finish eating

    private bool gameOver = false;

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

    void UpdateUI()
    {
        if (timeText != null)
            timeText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString();

        if (moneyText != null)
            moneyText.text = "Money: ₱" + money.ToString();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();

        if (money >= quota)
        {
            EndGame(true);
        }
    }

    void EndGame(bool won)
    {
        gameOver = true;
        Time.timeScale = 0f;

        if (won && winScreen != null) winScreen.SetActive(true);
        if (!won && loseScreen != null) loseScreen.SetActive(true);
    }
}
