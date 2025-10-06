using UnityEngine;
public class Player_Controller : MonoBehaviour
{
    public float moveSpeed;
    CharacterController controller;
    Vector3 moveDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = transform.forward * moveZ + transform.right * moveX;
        moveDirection *= moveSpeed;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
