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
        if (Input.GetKeyDown(KeyCode.Z)) // pressing jump key also triggers next scene
            SceneManager.LoadScene(scene);
#if !UNITY_EDITOR // in Standalone build,
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit(); // pressing ESC quits the game if this UI is visible
#endif
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene(scene);
    }
}
