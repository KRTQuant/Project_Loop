using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Camera control")]
    [SerializeField]
    private Camera camera;
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

    [Header("Camera FOV")]
    [SerializeField]
    private float normalFov;
    [SerializeField]
    private float zoomFov;

    [Header("Crouching")]
    [SerializeField]
    private float crouchHeight;
    [SerializeField]
    private float standHeight;

    [Header("Crouch")]
    [SerializeField]
    private float crouchDuration;
    private float beginCrouchValue;
    private float endCrouchValue;
    private float crouchElapsedTime;
    private bool isCrouching;

    [Header("Lerp FOV")]
    [SerializeField]
    private float zoomDuration;
    private float beginZoomValue;
    private float endZoomValue;
    private float zoomElapsedTime;
    private bool isZooming;

    private float rotationX;
    private float rotationY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        this.UpdateCameraRotation();
        this.SyncCameraPos();

        this.ComputeInput();
        this.LerpFOV();
        this.LerpCrouch();
    }

    private void ComputeInput()
    {
        if(Input.GetMouseButtonDown(1))
        {
            this.isZooming = true;
            if(this.camera.fieldOfView == zoomFov)
            {
                this.UnzoomCamera();
            }

            else
            {
                this.ZoomCamera();
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            this.isCrouching = true;
            if(this.playerCamHolder.transform.position.y == this.standHeight)
            {
                this.Crouch();
            }

            else
            {
                this.Stand();
            }
        }
    }

    private void SyncCameraPos()
    {
        this.cameraTransform.position = this.playerCamHolder.position;
    }

    private void UpdateCameraRotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * this.senseX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * this.senseY;

        this.rotationY += mouseX;

        this.rotationX -= mouseY;
        this.rotationX = Mathf.Clamp(this.rotationX, -90f, 90f);

        this.transform.rotation = Quaternion.Euler(this.rotationX, this.rotationY, 0);
        this.orientation.rotation = Quaternion.Euler(0, this.rotationY, 0);
    }

    private void ZoomCamera()
    {
        this.beginZoomValue = this.normalFov;
        this.endZoomValue = this.zoomFov;
        this.zoomElapsedTime = 0;
    }

    private void UnzoomCamera()
    {
        this.beginZoomValue = this.zoomFov;
        this.endZoomValue = this.normalFov;
        this.zoomElapsedTime = 0;
    }

    private void Crouch()
    {
        this.beginCrouchValue = this.standHeight;
        this.endCrouchValue = this.crouchHeight;
        this.crouchElapsedTime = 0;
    }

    private void Stand()
    {
        this.beginCrouchValue = this.crouchHeight;
        this.endCrouchValue = this.standHeight;
        this.crouchElapsedTime = 0;
    }

    private void LerpFOV()
    {
        if(!this.isZooming) return ;

        this.zoomElapsedTime += Time.deltaTime;
        var fov = this.camera.fieldOfView;
        var percentage = this.zoomElapsedTime / this.zoomDuration;

        this.camera.fieldOfView = Mathf.Lerp(this.beginZoomValue, this.endZoomValue, percentage);
    }

    private void LerpCrouch()
    {
        if(!this.isCrouching) return ;

        this.crouchElapsedTime += Time.deltaTime;
        var percentage = this.crouchElapsedTime / this.crouchDuration;

        var camHeight = Mathf.Lerp(this.beginCrouchValue, this.endCrouchValue, percentage);

        var camPos = this.playerCamHolder.transform.position;
        this.playerCamHolder.transform.position = new Vector3(camPos.x, camHeight, camPos.z);
    }
}
