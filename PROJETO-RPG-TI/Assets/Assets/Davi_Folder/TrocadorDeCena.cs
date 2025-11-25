using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocadorDeCena : MonoBehaviour
{
    public string nomeDaCena;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nomeDaCena);
        }
    }
}