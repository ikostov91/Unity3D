using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health = 10;
    [SerializeField] private int _healthDecrease = 1;

    [SerializeField] private Text _healthText;
    [SerializeField] private AudioClip _playerDamageSfx;

    private void Start()
    {
        this._healthText.text = this._health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(this._playerDamageSfx);
        this._health -= this._healthDecrease;
        this._healthText.text = this._health.ToString();
    }
}
