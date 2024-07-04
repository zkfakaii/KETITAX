using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Scrollbar volumeScrollbar;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Cargar el volumen guardado
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 1.0f);
        volumeScrollbar.value = savedVolume;
        if (audioSource != null)
        {
            audioSource.volume = savedVolume;
        }

        // Suscribirse al evento de cambio de valor del scrollbar
        volumeScrollbar.onValueChanged.AddListener(delegate { AdjustVolume(); });
    }

    void AdjustVolume()
    {
        if (audioSource != null)
        {
            audioSource.volume = volumeScrollbar.value;
        }

        // Guardar el volumen ajustado
        PlayerPrefs.SetFloat("GameVolume", volumeScrollbar.value);
    }
}
