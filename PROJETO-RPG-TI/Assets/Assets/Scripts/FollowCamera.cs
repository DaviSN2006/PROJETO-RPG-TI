using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private void Awake()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
