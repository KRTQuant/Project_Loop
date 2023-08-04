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

    [Header("Lerp")]
    [SerializeField]
    private float beginValue;
    [SerializeField]
    private float endValue;
    [SerializeField]
    private float lerpDuration;
    [SerializeField]
    private float lerpElapedTime;
    [SerializeField]
    private bool isLerp;

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
    }

    private void ComputeInput()
    {
        if(Input.GetMouseButtonDown(1))
        {
            this.isLerp = true;
            if(this.camera.fieldOfView == zoomFov)
            {
                this.UnzoomCamera();
            }

            else
            {
                this.ZoomCamera();
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
        this.beginValue = this.normalFov;
        this.endValue = this.zoomFov;
        this.lerpElapedTime = 0;
    }

    private void UnzoomCamera()
    {
        this.beginValue = this.zoomFov;
        this.endValue = this.normalFov;
        this.lerpElapedTime = 0;
    }

    private void LerpFOV()
    {
        if(!this.isLerp) return;

        var fov = this.camera.fieldOfView;
        
        this.lerpElapedTime += Time.deltaTime;
        var percentage = this.lerpElapedTime / this.lerpDuration;
        
        this.camera.fieldOfView = Mathf.Lerp(this.beginValue, this.endValue, percentage);
        if(percentage >= 1)
        {
            this.isLerp = false;
        }
    }
}
