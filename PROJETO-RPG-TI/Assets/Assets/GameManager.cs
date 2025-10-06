using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartBattle(GameObject enemy)
    {
        SelectedCharacter.Instance.enemyInTrigger = enemy;
        SavePlayerData();
        SceneManager.LoadScene("Arena1");
    }

    public void SavePlayerData()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector3 pos = player.transform.position;
            Vector3 rot = player.transform.eulerAngles;

            PlayerPrefs.SetFloat("PlayerPosX", pos.x);
            PlayerPrefs.SetFloat("PlayerPosY", pos.y);
            PlayerPrefs.SetFloat("PlayerPosZ", pos.z);

            PlayerPrefs.SetFloat("PlayerRotX", rot.x);
            PlayerPrefs.SetFloat("PlayerRotY", rot.y);
            PlayerPrefs.SetFloat("PlayerRotZ", rot.z);

            PlayerPrefs.Save();
        }
    }

    public void LoadPlayerData(GameObject player)
    {
        if (PlayerPrefs.HasKey("PlayerPosX"))
        {
            Vector3 pos = new Vector3(
                PlayerPrefs.GetFloat("PlayerPosX"),
                PlayerPrefs.GetFloat("PlayerPosY"),
                PlayerPrefs.GetFloat("PlayerPosZ")
            );

            Vector3 rot = new Vector3(
                PlayerPrefs.GetFloat("PlayerRotX"),
                PlayerPrefs.GetFloat("PlayerRotY"),
                PlayerPrefs.GetFloat("PlayerRotZ")
            );

            player.transform.position = pos;
            player.transform.eulerAngles = rot;
        }
        else
        {
            Debug.LogWarning("Nenhuma posição salva encontrada!");
        }
    }
}
