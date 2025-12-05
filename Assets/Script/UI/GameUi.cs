using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Handles Game Over and Win UI panels,
/// manages pause state, cursor visibility,
/// and provides restart functionality.
/// </summary>
public class GameUi : MonoBehaviour
{
    public static GameUi Instance;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinPanel;

    private void Awake()
    {
        Time.timeScale = 1f;
        Instance = this;
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameOverPanel.SetActive(true);

    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void ShowWin()
    {
        Cursor.lockState = CursorLockMode.None;
        gameWinPanel.SetActive(true);
        // Change text to "YOU WIN"
        Time.timeScale = 0f;
    }
}
