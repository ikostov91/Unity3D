using Assets.Scripts.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _menuTheme;
    [SerializeField] private AudioClip[] _gameplayThemes;
    
    private AudioSource _audioSource;

    private void Start()
    {
        this._audioSource = GetComponent<AudioSource>();
        this.PlayTheme();
    }

    private void PlayTheme()
    {
        if (SceneManager.GetActiveScene().name == LevelConstants.MainMenu)
        {
            this._audioSource.PlayOneShot(this._menuTheme);
        }
        else
        {
            int randomIndex = Random.Range(0, this._gameplayThemes.Length);
            this._audioSource.PlayOneShot(this._gameplayThemes[randomIndex]);
        }
    }
}
