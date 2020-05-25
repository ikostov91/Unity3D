using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _deathFx;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _scoreHit = 12;

    [SerializeField] private int _hits = 10;

    private ScoreBoard _scoreBoard;

    void Start()
    {
        this.AddNonTriggerBoxCollider();

        this._scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider boxCollider = this.gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject otherCollider)
    {
        this.ProcessHit();

        if (this._hits <= 0)
        {
            this.KillEnemy();
        }
    }

    private void ProcessHit()
    {
        this._scoreBoard.ScoreHit(this._scoreHit);
        this._hits--;
    }

    private void KillEnemy()
    {
        Vector3 position = this.transform.position;
        GameObject explosion = Instantiate(this._deathFx, position, Quaternion.identity);
        explosion.transform.parent = this._parent;

        Destroy(this.gameObject);
    }
}
