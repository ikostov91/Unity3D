using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private Canvas _gameOverCanvas;

    private void Start()
    {
        this._gameOverCanvas.enabled = false;
    }

    public void HandleDeath()
    {
        this._gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
