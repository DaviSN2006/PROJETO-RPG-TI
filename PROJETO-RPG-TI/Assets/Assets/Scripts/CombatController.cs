using UnityEngine;

public class CombatController : MonoBehaviour
{
    public Respawner respawner;

    private void OnDisable()
    {
        respawner.StartRespawn(gameObject);
    }
}
