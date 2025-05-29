using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Mato_Manager : MonoBehaviour
{
    [System.Serializable]
    public class MonsterEncounter
    {
        public string sceneName = "NomeDaCena";
        [Range(0f, 1f)] public float encounterRate; // Definível no Inspector
    }
    public MonsterEncounter[] encounters; // Lista de encontros possíveis
    private bool playerInside = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Certifique-se de que o jogador tem a tag "Player"
        {
            playerInside = true;
            StartCoroutine(CheckForEncounterRoutine());
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            StopCoroutine(CheckForEncounterRoutine());
        }
    }
    IEnumerator CheckForEncounterRoutine()
    {
        while (playerInside)
        {
            foreach (var encounter in encounters)
            {
                if (Random.value < encounter.encounterRate) // Se o valor aleatório for menor que a taxa de encontro
                {
                    SceneManager.LoadScene(encounter.sceneName);
                    yield break; // Sai da coroutine ao carregar uma cena
                }
            }
            yield return new WaitForSeconds(1f); // Verifica a cada 1 segundo
        }
    }
}