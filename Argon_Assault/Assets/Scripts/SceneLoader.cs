using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        this.StartCoroutine(this.LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3f);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    // Standard assets note on compile error for GUITexture obsolete
    // in code, add "using UnityEngine.UI;"
    // and change the GUITexture to 'Image'
    // chnage .text to .name
}
