using ABCToolkit;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public Vector3 respawnPosition;

    ABC_MovementController movementController;
    PlayerController playerController;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogWarning("Player Out of Bounds");
            movementController = other.GetComponent<ABC_MovementController>();
            if (movementController)
            {
                movementController.DisableMovement();
                movementController.DisableGravity();
            }
            else
            {
                playerController = other.GetComponent<PlayerController>();
                if (playerController)
                {
                    playerController.enableMovement = false;
                    playerController.enableGravity = false;
                    Debug.Log("PC");
                }
            }

            other.transform.SetPositionAndRotation(respawnPosition, other.transform.rotation);

            Invoke("ReenableGravity", .5f);
        }
    }

    void ReenableGravity()
    {
        if (movementController)
        { 
            movementController.EnableMovement();
            movementController.EnableGravity();
        }
        else 
        {
            if (playerController)
            {
                playerController.enableMovement = true;
                playerController.enableGravity = true;
            }
        }
    }
}
