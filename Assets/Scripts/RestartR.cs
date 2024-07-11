using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartR: MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        // Obtén el nombre de la escena actual
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Carga la escena actual nuevamente
        SceneManager.LoadScene(currentSceneName);
    }
}
