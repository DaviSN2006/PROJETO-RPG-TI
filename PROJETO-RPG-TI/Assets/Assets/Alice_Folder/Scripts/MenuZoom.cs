using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuZoom : MonoBehaviour
{
    public RectTransform imageToZoom; // arraste sua imagem de fundo aqui
    public float zoomDuration = 1.5f;
    public float zoomAmount = 1.2f;
    public GameObject menuReal; // painel do menu com os botões
    public AudioSource audioSource; // arraste o componente AudioSource aqui
    public AudioClip zoomSound; // arraste o áudio desejado aqui

    public void StartZoom()
    {
        PlaySound(); // Toca o som
        StartCoroutine(ZoomIn());
    }

    private void PlaySound()
    {
        if (audioSource != null && zoomSound != null)
        {
            audioSource.PlayOneShot(zoomSound); // Toca o áudio
        }
    }

    IEnumerator ZoomIn()
    {
        Vector3 startScale = imageToZoom.localScale;
        Vector3 targetScale = startScale * zoomAmount;

        float elapsed = 0;

        while (elapsed < zoomDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / zoomDuration;
            imageToZoom.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        menuReal.SetActive(true);
    }
}
