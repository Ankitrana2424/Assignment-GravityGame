using UnityEngine;


/// <summary>
/// Detects when the player is freely falling without touching any surface
/// for a defined duration, and triggers Game Over when the limit is exceeded.
/// </summary>
public class PlayerFall : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float freeFallTimeLimit = 10f;

    private float fallTimer = 0f;

    private void Update()
    {
        Debug.Log("IsGrounded = " + player.IsGrounded + "   FallTimer = " + fallTimer);
        if (!player.IsGrounded)
        {
            fallTimer += Time.deltaTime;

            if (fallTimer >= freeFallTimeLimit)
            {

                Time.timeScale = 0;
                GameUi.Instance.ShowGameOver();
            }
        }
        else
        {
            fallTimer = 0f;
        }
    }
}
