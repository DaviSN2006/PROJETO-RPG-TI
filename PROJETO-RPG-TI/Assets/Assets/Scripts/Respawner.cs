using System.Collections;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public void StartRespawn(GameObject player)
    {
        StartCoroutine(Respawn(player));
    }

    IEnumerator Respawn(GameObject player)
    {
        player.transform.position = transform.position;
        yield return new WaitForSecondsRealtime(1);
        player.SetActive(true);
    }
}
