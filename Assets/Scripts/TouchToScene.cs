using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchToScene : MonoBehaviour
{
    public string sceneToLoad; // Nombre de la escena a cargar
    public float delay = 2.0f; // Retardo en segundos antes de cargar la escena

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}
