using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform orientation;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float horizontalInput;
    [SerializeField]
    private float verticalInput;
    [SerializeField]
    private Vector3 direction;

    void Update()
    {
        this.ComputeInput();
    }

    private void ComputeInput()
    {
        this.horizontalInput = Input.GetAxisRaw("Horizontal");
        this.verticalInput = Input.GetAxisRaw("Vertical");

        this.MovePlayer();
    }

    private void MovePlayer()
    {
        this.direction = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.velocity = this.direction * this.speed;
    }
}
