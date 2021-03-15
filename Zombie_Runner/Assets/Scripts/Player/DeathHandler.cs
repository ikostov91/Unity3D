using UnityEngine;
using Assets.Scripts.WeaponScripts;
using Assets.Scripts.Player;

namespace Assets.Scripts.PlayerScripts
{
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
            FindObjectOfType<PauseHandler>().OnPlayerDead();
        }
    }
}
