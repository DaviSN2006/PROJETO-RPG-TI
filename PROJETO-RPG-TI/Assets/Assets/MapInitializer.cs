using UnityEngine;

public class MapInitializer : MonoBehaviour
{
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GameManager.Instance.LoadPlayerData(player);
        }
    }
}
