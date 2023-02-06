using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Changes scene when mouse down or action key is pressed
/// </summary>
public class OnMouseDown_SceneChange : MonoBehaviour
{
    public int scene;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            SceneManager.LoadScene(scene);
#if !UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
#endif
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene(scene);
    }
}
