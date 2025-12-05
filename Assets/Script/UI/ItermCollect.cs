using TMPro;
using UnityEngine;


/// <summary>
/// Handles collecting items, updating UI count,
/// and checking the win condition when all items are collected.
/// </summary>
public class ItermCollect : MonoBehaviour
{
    public static ItermCollect Instance;
    [Header("References")]
    [SerializeField] private GameTImer timer;          // drag GameTimer
    [SerializeField] private TextMeshProUGUI counterText;  // drag UI text

    public int totalItems;
    public int collectedItems;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        totalItems = FindObjectsOfType<Collectible>().Length;
        collectedItems = 0;

        UpdateCounterText();  // Initial UI update
    }

    public void CollectItem()
    {
        collectedItems++;
        UpdateCounterText();

        if (collectedItems >= totalItems)
        {
            Debug.Log("WIN!!!");
            timer.GameOverWin(); // you will create this
        }
    }

    private void UpdateCounterText()
    {
        counterText.text = $"Collected: {collectedItems} / {totalItems}";
    }
}
