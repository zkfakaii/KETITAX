using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
public class MenuController : MonoBehaviour
{
    public void LoadNextScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }

        public void QuitGame()
        {
            Application.Quit();
            EditorApplication.isPaused = !EditorApplication.isPaused;

        }
}