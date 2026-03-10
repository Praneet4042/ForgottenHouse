using UnityEngine;
public class HorrorFPPController : MonoBehaviour {
    public float moveSpeed = 3f;
    public float mouseSensitivity = 100f;
    public Transform camHolder;
    CharacterController cc;
    float xRot = 0f;
    Vector3 velocity;
    void Start() {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update() {
        float mx = Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime;
        float my = Input.GetAxis("Mouse Y")*mouseSensitivity*Time.deltaTime;
        xRot = Mathf.Clamp(xRot - my, -70f, 70f);
        camHolder.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        transform.Rotate(Vector3.up * mx);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right*x + transform.forward*z;
        cc.Move(move * moveSpeed * Time.deltaTime);
        if (cc.isGrounded && velocity.y < 0) velocity.y = -2f;
        velocity.y += -9.81f * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
}
