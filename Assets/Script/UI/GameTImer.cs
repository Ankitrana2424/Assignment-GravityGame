using TMPro;
using UnityEngine;


/// <summary>
/// Controls the countdown game timer, checks lose/win conditions
/// based on collected items, and triggers Game Over or Victory.
/// </summary>
public class GameTImer : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timeLimit = 120f;
    public bool IsGameOver { get; private set; }

    private void Update()
    {
        if (IsGameOver) return;

        timeLimit -= Time.deltaTime;
        DisplayTime(timeLimit);

        if (timeLimit <= 0)
        {
            timeLimit = 0;
            CheckLoseCondition();
        }
    }

    private void DisplayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void CheckLoseCondition()
    {
        if (ItermCollect.Instance.collectedItems < ItermCollect.Instance.totalItems)
        {
            Debug.Log("Time Up! WE LOSE!");
            GameOver();
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
        GameUi.Instance.ShowGameOver();
    }

    public void GameOverWin()
    {
        IsGameOver = true;
        Debug.Log("WIN CONDITION!!!");
        GameUi.Instance.ShowWin();   // Create win UI inside GameUi
    }
}