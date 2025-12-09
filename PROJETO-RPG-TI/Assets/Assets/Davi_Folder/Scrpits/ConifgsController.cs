using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Menu : MonoBehaviour
{


    Animator animator;
    [SerializeField] private string LevelJogo;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Jogar()
    {
        Invoke("LoadScene", 2f);
    }
    public void AbrirPainel(GameObject painel)
    {
        painel.SetActive(true);
    }
    public void FecharPainel(GameObject painel)
    {
        painel.SetActive(false);
    }
    private void LoadScene()
    {
        SceneManager.LoadScene(LevelJogo);
    }
    public void SairJogo ()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();

    }

}