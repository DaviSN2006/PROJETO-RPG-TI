using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ArenaManager : MonoBehaviour
{
    public Transform spawnPoint;
    private GameObject enemyThatTriggeredArena;
    private List<CharacterStats> enemiesInArena = new List<CharacterStats>();

    private void Start()
    {
        GameObject selectedCharacter = SelectedCharacter.Instance.characterPrefab;

        if (selectedCharacter != null)
        {
            Instantiate(selectedCharacter, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Nenhum personagem selecionado!");
        }

        enemyThatTriggeredArena = SelectedCharacter.Instance.enemyInTrigger;
        FindAllEnemies();
    }

    private void Update()
    {
        if (enemiesInArena.Count > 0)
        {
            for (int i = enemiesInArena.Count - 1; i >= 0; i--)
            {
                if (enemiesInArena[i] == null || enemiesInArena[i].health <= 0)
                {
                    enemiesInArena.RemoveAt(i);
                }
            }

            if (enemiesInArena.Count == 0)
            {
                ExitArena();
            }
        }
    }

    private void FindAllEnemies()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyObjects)
        {
            CharacterStats enemyStats = enemy.GetComponent<CharacterStats>();
            if (enemyStats != null)
            {
                enemiesInArena.Add(enemyStats);
            }
        }
    }

    public void ExitArena()
    {
        if (enemyThatTriggeredArena != null)
        {
            Destroy(enemyThatTriggeredArena);
        }

        SceneManager.LoadScene("IlhaLoop"); // Volta para o mapa
    }
}
