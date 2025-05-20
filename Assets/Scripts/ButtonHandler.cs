using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void OnResetButtonClick()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartScene();
        }
    }

    public void OnMenuButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
