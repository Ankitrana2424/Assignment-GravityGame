using UnityEngine;


/// <summary>
/// Handles gravity direction switching based on arrow key input,
/// displays a hologram preview at the target direction,
/// and applies new gravity when the Enter key is pressed.
public class GravityControl : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject hologramPrefab;

    [Header("Hologram Settings")]
    [SerializeField] private float hologramDistance = 2f;

    private GameObject holo;
    private Vector3 selectedDir = Vector3.zero;

    private void Start()
    {
        if (hologramPrefab)
        {
            holo = Instantiate(hologramPrefab);
            holo.SetActive(false);
        }
    }

    private void Update()
    {
        HandleInput();
        UpdateHologram();
        ApplyGravityChange();
    }

    private void HandleInput()
    {
        selectedDir = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            selectedDir = playerTransform.forward;

        if (Input.GetKey(KeyCode.DownArrow))
            selectedDir = -playerTransform.forward;

        if (Input.GetKey(KeyCode.LeftArrow))
            selectedDir = -playerTransform.right;

        if (Input.GetKey(KeyCode.RightArrow))
            selectedDir = playerTransform.right;
    }

    private void UpdateHologram()
    {
        if (!holo) return;

        if (selectedDir != Vector3.zero)
        {
            holo.SetActive(true);

            holo.transform.position = playerTransform.position +
            selectedDir.normalized * hologramDistance;


            holo.transform.rotation = Quaternion.LookRotation(selectedDir, playerTransform.up);
        }
        else
        {
            holo.SetActive(false);
        }
    }

    private void ApplyGravityChange()
    {
        if (Input.GetKeyDown(KeyCode.Return) && selectedDir != Vector3.zero)
        {
            Vector3 newDown = selectedDir.normalized;
            player.SetGravityDirection(newDown);
        }
    }
}