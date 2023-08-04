using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject heldObject;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private float castDistance;

    [SerializeField]
    private LayerMask interactableLayer;

    void Update()
    {
        this.ComputeInput();
    }

    private void ComputeInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = this.camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if(Physics.Raycast(ray, out RaycastHit hit, this.castDistance, this.interactableLayer))
            {
                var interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                if(interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}
