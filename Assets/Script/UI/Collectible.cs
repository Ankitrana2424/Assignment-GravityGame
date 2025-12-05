using UnityEngine;



/// <summary>
/// Detects when the player touches a collectible object.
/// When triggered, it notifies the ItemCollect system and destroys itself.
/// Requires the player to have the tag "Player".
public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItermCollect.Instance.CollectItem();
            Destroy(gameObject);
        }
    }
}
