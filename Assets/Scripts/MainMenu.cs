using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private LevelManager levelManager;
    public Button continueButton;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>(); // Busca el LevelManager en la escena actual

        if (continueButton != null)
        {
            continueButton.onClick.AddListener(ContinueGame); // Agrega el m�todo ContinueGame al evento onClick del bot�n
        }

        // Comprueba si el bot�n "Continuar" deber�a estar interactuable
        if (levelManager != null && PlayerPrefs.HasKey("SavedLevel"))
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    public void ContinueGame()
    {
        if (levelManager != null)
        {
            levelManager.LoadSavedLevel(); // Intenta cargar el nivel guardado por LevelManager
        }
        else
        {
            Debug.LogWarning("LevelManager not found!");
        }
    }

    public void NewGame()
    {
        // L�gica para comenzar un nuevo juego, por ejemplo, cargar el primer nivel
        SceneManager.LoadScene(0); // Asume que el primer nivel es el �ndice 0
    }
}
