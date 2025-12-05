using UnityEngine;



/// <summary>
/// Handles third-person camera movement and rotation around the player
/// based on mouse input and player gravity direction.
public class CameraHandle : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private PlayerMovement player;

    [Header("Camera Settings")]
    [SerializeField] private float distance = 4f;
    [SerializeField] private float height = 2f;

    [Header("Mouse Look")]
    [SerializeField] private float sensitivityX = 150f;
    [SerializeField] private float sensitivityY = 120f;

    private float yaw;
    private float pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yaw = 0f;
        pitch = 0f;
    }

    private void LateUpdate()
    {
        if (!player) return;

        Vector3 gravityUp = -player.CurrentGravityDirection().normalized;


        float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * -1 * sensitivityY * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;


        Vector3 right = Vector3.Cross(Vector3.forward, gravityUp).normalized;
        if (right == Vector3.zero) right = Vector3.Cross(Vector3.up, gravityUp).normalized;

        Quaternion yawRot = Quaternion.AngleAxis(yaw, gravityUp);
        Quaternion pitchRot = Quaternion.AngleAxis(pitch, right);
        Quaternion finalRot = yawRot * pitchRot;

        Vector3 offset = finalRot * new Vector3(0f, 0f, -distance);
        Vector3 targetPos = player.transform.position + gravityUp * height;

        transform.position = targetPos + offset;
        transform.rotation = Quaternion.LookRotation(targetPos - transform.position, gravityUp);
    }
}


