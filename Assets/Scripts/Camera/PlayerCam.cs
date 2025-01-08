using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    //[SerializeField] private CameraHeadBob cameraHeadBob;
    [SerializeField] private PlayerMovement playerMovement;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Existing camera rotation logic
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        //// Update the head bob effect
        //cameraHeadBob.BobUpdate(playerMovement, this);

        //// Apply head bob offset and tilt sway
        //Vector3 headBobOffset = cameraHeadBob.ViewBobOffset;
        //float tiltSway = cameraHeadBob.TiltSway;

        //transform.localPosition += headBobOffset;
        //transform.localRotation *= Quaternion.Euler(0, 0, tiltSway);
    }
}