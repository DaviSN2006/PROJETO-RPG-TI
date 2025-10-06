using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class SomScroll : MonoBehaviour
{
    [SerializeField] private Scrollbar volumeScrollbar;
    private void Start()
    {
        volumeScrollbar.value = AudioListener.volume;
        volumeScrollbar.onValueChanged.AddListener(SetVolume);
    }
    private void SetVolume(float value)
    {
        AudioListener.volume = value;
        Debug.Log($"Volume ajustado para: {value}");
    }
    private void OnDestroy()
    {
        volumeScrollbar.onValueChanged.RemoveListener(SetVolume);
    }
}