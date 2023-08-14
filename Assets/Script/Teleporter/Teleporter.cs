using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Transform Destination;
    [SerializeField] Transform Player;
    [SerializeField] bool rotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void teleportPlayer()
    {
        Player.transform.position = Destination.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == Player.transform)
        {
            
            teleportPlayer();
        }
    }
}
