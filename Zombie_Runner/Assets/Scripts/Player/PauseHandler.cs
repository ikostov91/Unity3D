using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PauseHandler : MonoBehaviour
    {
        public static bool GamePaused = false;

        [SerializeField] private Canvas _pauseCanvas;
        private bool _isPaused = false;

        void Start()
        {
            this.TogglePauseState(this._isPaused);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this._isPaused = !this._isPaused;
                this.TogglePauseState(this._isPaused);
            }
        }

        void OnDisable()
        {
            this.TogglePauseCanvas();
            this.ToggleTimeScale();
            this.ToggleMouseCursor();
            this.ToggleAudio();
        }

        public void ContinueGame()
        {
            this._isPaused = false;
            this.TogglePauseState(this._isPaused);
        }

        public void QuitGame()
        {
            this._isPaused = true;
            this.TogglePauseState(this._isPaused);
            FindObjectOfType<SceneLoader>().QuitLevel();
        }

        public void OnPlayerDead()
        {
            this._isPaused = true;
            this.ToggleTimeScale(this._isPaused);
            this.ToggleMouseCursor(this._isPaused);
            this.ToggleAudio(this._isPaused);
        }

        private void TogglePauseState(bool isPaused)
        {
            GamePaused = isPaused;
            this.TogglePauseCanvas(isPaused);
            this.ToggleTimeScale(isPaused);
            this.ToggleMouseCursor(isPaused);
            this.ToggleAudio(isPaused);
        }

        #region HelperMethods
        private void ToggleMouseCursor(bool enabled = true)
        {
            Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = enabled;
        }

        private void ToggleTimeScale(bool timeStopped = false)
        {
            Time.timeScale = timeStopped ? 0f : 1f;
        }

        private void TogglePauseCanvas(bool enabled = false)
        {
            this._pauseCanvas.enabled = enabled;
        }

        private void ToggleAudio(bool paused = false)
        {
            AudioListener.pause = paused;
        }
        #endregion
    }
}
