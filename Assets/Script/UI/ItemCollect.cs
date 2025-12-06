using TMPro;
using UnityEngine;


/// <summary>
/// Handles collecting items, updating UI count,
/// and checking the win condition when all items are collected.
/// </summary>
public class ItemCollect : MonoBehaviour
{
    public static ItemCollect Instance;
    [Header("References")]
    [SerializeField] private GameTImer timer;
    [SerializeField] private TextMeshProUGUI counterText;

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

        UpdateCounterText();
    }

    public void CollectItem()
    {
        collectedItems++;
        UpdateCounterText();

        if (collectedItems >= totalItems)
        {
            Debug.Log("WIN!!!");
            timer.GameOverWin();
        }
    }

    private void UpdateCounterText()
    {
        counterText.text = $"Collected: {collectedItems} / {totalItems}";
    }
}
