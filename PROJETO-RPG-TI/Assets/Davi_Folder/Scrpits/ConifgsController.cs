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
    [SerializeField] private GameObject painelMenuIcinial;
    [SerializeField] private GameObject painelOpções;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Jogar()
    {
        Invoke("LoadScene", 2f);
    }
    public void AbrirOpções()
    {
        painelOpções.SetActive(true);
    }
    public void FecharOpções()
    {
        painelOpções.SetActive(false);
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