using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class DisplayDamage : MonoBehaviour
    {
        [SerializeField] private Canvas _damageCanvas;
        [SerializeField] private float _onTime = 0.3f;

        void Start()
        {
            this._damageCanvas.enabled = false;
        }

        public void ShowDamageCanvas()
        {
            StartCoroutine(this.ShowDamage());
        }

        private IEnumerator ShowDamage()
        {
            this._damageCanvas.enabled = true;
            yield return new WaitForSeconds(this._onTime);
            this._damageCanvas.enabled = false;
        }
    }
}
