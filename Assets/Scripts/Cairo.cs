using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cairo : MonoBehaviour
{
    public GameObject panel; // El panel que contiene la imagen y los botones
    public Image displayImage; // El componente de imagen en el panel
    public Sprite[] images; // Array de im�genes para mostrar
    private int currentIndex = 0;

    void Start()
    {
        // Aseg�rate de que el panel est� desactivado al inicio
        panel.SetActive(false);
    }

    public void ShowPanel()
    {
        currentIndex = 0;
        displayImage.sprite = images[currentIndex];
        panel.SetActive(true);
        Time.timeScale = 0; // Pausar el juego
    }

    public void NextImage()
    {
        currentIndex++;
        if (currentIndex < images.Length)
        {
            displayImage.sprite = images[currentIndex];
        }
        else
        {
            // Si no hay m�s im�genes, desactiva el panel o haz cualquier otra acci�n necesaria
            ClosePanel();
        }
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1; // Reanudar el juego
    }
}
