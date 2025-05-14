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
        else
        {
            Debug.LogError("GameManager.Instance no encontrado.");
        }
    }
}
