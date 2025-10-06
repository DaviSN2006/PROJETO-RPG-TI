using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Mato : MonoBehaviour
{
    [System.Serializable]
    public class MonsterEncounter
    {
        public string pokemonName;
        public float encounterRate; // Chance de encontrar esse Monstro (ex: 50% = 0.5f)
    }
    public List<MonsterEncounter> possibleEncounters; // Lista dos Monstros e suas chances
    public float stepInterval; // Tempo entre cada verificação
    public int maxConsecutiveEncounters; // Limite de encontros repetidos
    bool isInGrass = false;
    string lastEncounteredMonster = "";
    int consecutiveCount = 0;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInGrass = true;
            StartCoroutine(CheckForEncounter());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInGrass = false;
            StopAllCoroutines();
        }
    }
    IEnumerator CheckForEncounter()
    {
        while (isInGrass)
        {
            yield return new WaitForSeconds(stepInterval);
            if (Random.value < 0.2f) // 20% de chance de um encontro ocorrer
            {
                string encounteredPokemon = GetRandomMonster();
                // Se for o mesmo Monstro da última vez, aumentar o contador
                if (encounteredPokemon == lastEncounteredMonster)
                {
                    consecutiveCount++;
                    // Se o limite for atingido, escolher outro Monstro diferente
                    if (consecutiveCount >= maxConsecutiveEncounters)
                    {
                        encounteredPokemon = GetDifferentPokemon(encounteredPokemon);
                        consecutiveCount = 0; // Resetar a contagem
                    }
                }
                else
                {
                    consecutiveCount = 0; // Resetar a contagem ao encontrar outro Monstro
                }
                lastEncounteredMonster = encounteredPokemon;
                Debug.Log($"Encontro com {encounteredPokemon}!");
                break; // Para o loop ao encontrar um Pokémon
            }
        }
    }
    private string GetRandomMonster()
    {
        float totalWeight = 0;
        foreach (var encounter in possibleEncounters)
        {
            totalWeight += encounter.encounterRate;
        }
        float randomValue = Random.value * totalWeight;
        float currentWeight = 0;
        foreach (var encounter in possibleEncounters)
        {
            currentWeight += encounter.encounterRate;
            if (randomValue <= currentWeight)
            {
                return encounter.pokemonName;
            }
        }
        return "Desconhecido";
    }
    private string GetDifferentPokemon(string currentPokemon)
    {
        List<string> otherPokemon = new List<string>();
        // Criar uma lista apenas com Mosntros diferentes do atual
        foreach (var encounter in possibleEncounters)
        {
            if (encounter.pokemonName != currentPokemon)
            {
                otherPokemon.Add(encounter.pokemonName);
            }
        }
        // Se não houver outra opção (caso improvável), retorna o mesmo
        if (otherPokemon.Count == 0) return currentPokemon;
        return otherPokemon[Random.Range(0, otherPokemon.Count)];
    }
}