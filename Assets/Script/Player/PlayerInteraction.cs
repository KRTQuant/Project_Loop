using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private Transform orientationXY;
    [SerializeField]
    private Transform handTransform;
    [SerializeField]
    private Transform camHolder;
    [SerializeField]
    private GameObject heldObject;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private float castDistance;

    [SerializeField]
    private LayerMask interactableLayer;

    [SerializeField]
    private string tag;

    [Header("Lerp Timer")]
    [SerializeField]
    private float interactDuration;
    private float interactElapsedTime;

    private Vector3 beginLerpPos;
    private Vector3 endLerpPos;

    private Vector3 beginRotationValue;
    private Vector3 endRotationValue;

    [Header("Throw")]
    [SerializeField]
    private float throwForce;
    [SerializeField]
    private float throwUpwardForce;

    [Header("Crosshair")]
    [SerializeField]
    private GameObject crosshair;
    

    void Update()
    {
        this.ComputeInput();
        this.RaycastForward();
        this.LerpTransform();
        this.SyncHandRotation();
        this.FixHeldObjectRotation();
    }

    private void ComputeInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(this.heldObject == null)
            {
                Ray ray = this.camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if(Physics.Raycast(ray, out RaycastHit hit, this.castDistance, this.interactableLayer))
                {
                    var isCorrectTag = hit.collider.gameObject.CompareTag(this.tag);
                    var interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                    var baseInteractable = hit.collider.gameObject.GetComponent<BaseInteractableObject>();
                    var hitObject = hit.collider.gameObject;

                    if(isCorrectTag)
                    {
                        this.crosshair.SetActive(true);
                        if(interactable != null && baseInteractable == null)
                        {
                            interactable.Interact();
                            return ;
                        }
                        
                        this.VacumnObject(hitObject);
                        baseInteractable.OnPickup();
                    }
                }
            }

            else
            {
                this.crosshair.SetActive(false);
                this.ThrowObject();
            }
        }
    }

    private void VacumnObject(GameObject heldObject)
    {
        this.heldObject = heldObject;
        this.beginLerpPos = heldObject.transform.position;
        this.endLerpPos = this.handTransform.position;
        this.beginRotationValue = heldObject.transform.rotation.eulerAngles;
        this.endRotationValue = Quaternion.identity.eulerAngles;
        this.interactElapsedTime = 0;

        this.heldObject.GetComponent<Collider>().enabled = false;
    }

    private void ThrowObject()
    {
        this.heldObject.transform.parent = null;
        
        var rb = this.heldObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        var force = this.orientationXY.forward * this.throwForce + this.orientationXY.up * this.throwUpwardForce;
        rb.AddForce(force, ForceMode.Impulse);

        var baseInteractable = this.heldObject.GetComponent<BaseInteractableObject>(); 
        if(baseInteractable != null)
        {
            baseInteractable.Yeet();
        }
        
        this.heldObject.GetComponent<Collider>().enabled = true;
        this.heldObject = null;
    }

    private void LerpTransform()
    {
        if(this.heldObject == null) return;

        Debug.Log("Lerping");
        this.interactElapsedTime += Time.deltaTime;
        var percentage = this.interactElapsedTime / this.interactDuration;
        var posValue = Vector3.Lerp(this.beginLerpPos, this.endLerpPos, percentage);
        var rotValue = Vector3.Lerp(this.beginRotationValue, this.endRotationValue, percentage);

        Debug.Log(posValue);
        this.heldObject.transform.SetPositionAndRotation(posValue, Quaternion.Euler(rotValue));

        if(percentage >= 1)
        {
            this.heldObject.transform.position = this.handTransform.position;
            //this.heldObject.transform.parent = this.handTransform;
        }
    }

    private void SyncHandRotation()
    {
        this.handTransform.rotation = this.orientationXY.rotation;
    }

    private void FixHeldObjectRotation()
    {
        if(this.heldObject == null) return ;

        this.heldObject.transform.LookAt(this.player.transform.position);
    }

    private void RaycastForward()
    {
        if(this.heldObject != null)
        {
            this.crosshair.SetActive(false);
        }

        else
        {
            Ray ray = this.camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if(Physics.Raycast(ray, out RaycastHit hit, this.castDistance, this.interactableLayer))
            {
                var isCorrectTag = hit.collider.gameObject.CompareTag(this.tag);
                var interactableDoor = hit.collider.gameObject.GetComponent<InteractableDoor>();

                if(isCorrectTag)
                {
                    this.crosshair.SetActive(true);
                }

                if(interactableDoor != null && interactableDoor.IsOpen)
                {
                    this.crosshair.SetActive(false);
                }

            }

            else
            {
                this.crosshair.SetActive(false);
            }
        }
    }
}
