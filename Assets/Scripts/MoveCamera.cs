using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    [SerializeField] PlayerMovement movement;

    // Update is called once per frame
    private void Update()
    {
        transform.position = cameraPosition.position;

        if (movement.state == PlayerMovement.MovementState.dead) {
            transform.rotation = movement.transform.rotation;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
            transform.rotation = Quaternion.identity;
    }
}
