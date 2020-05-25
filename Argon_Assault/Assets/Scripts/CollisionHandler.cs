using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ok as long as this is only script to load scenes

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField] private float _levelLoadDelay = 3f;
    [Tooltip("VFX on Player")] [SerializeField] private GameObject _deathVfx;

    private void OnTriggerEnter(Collider otherCollider)
    {
        this.StartCoroutine(this.StartDeathSequence());
    }

    private IEnumerator StartDeathSequence()
    {
        this._deathVfx.SetActive(true);
        this.SendMessage("OnPlayerDeath");

        yield return new WaitForSeconds(this._levelLoadDelay);

        this.ReloadLevel();
    }

    private void ReloadLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
