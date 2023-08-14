using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody characterController;
    [SerializeField]
    private Transform orientation;

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
        this.characterController.velocity = direction.normalized * this.speed * Time.deltaTime;
    }
}
