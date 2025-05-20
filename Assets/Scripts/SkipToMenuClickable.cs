using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipToMenuClickable : MonoBehaviour
{
    public string menuSceneName = "Menu";

    private void OnMouseDown()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void SkipToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
