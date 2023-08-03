using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Camera control")]
    [SerializeField]
    private float senseX;
    [SerializeField]
    private float senseY;
    [SerializeField]
    private Transform orientation;

    [Header("Cam pos sync")]
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Transform playerCamHolder;

    private float rotationX;
    private float rotationY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * this.senseX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * this.senseY;

        this.rotationY += mouseX;

        this.rotationX -= mouseY;
        this.rotationX = Mathf.Clamp(this.rotationX, -90f, 90f);

        this.transform.rotation = Quaternion.Euler(this.rotationX, this.rotationY, 0);
        this.orientation.rotation = Quaternion.Euler(0, this.rotationY, 0);

        this.SyncCameraPos();
    }

    private void SyncCameraPos()
    {
        this.cameraTransform.position = this.playerCamHolder.position;
    }
}
