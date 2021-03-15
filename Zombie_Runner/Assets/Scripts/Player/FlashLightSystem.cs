using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class FlashLightSystem : MonoBehaviour
    {
        [SerializeField] private float _lightDecay = 0.06f;
        [SerializeField] private float _angleDecay = 0.7f;
        [SerializeField] private float _minimumAngle = 40f;

        private Light _myLight;

        private void Start()
        {
            this._myLight = GetComponent<Light>();
        }

        private void Update()
        {
            this.DecreaseLightAngle();
            this.DecreaseLightIntensity();
        }

        public void RestoreLightAngle(float restoreAngle)
        {
            this._myLight.spotAngle = restoreAngle;
        }

        public void AddLightIntenisty(float intensityAmount)
        {
            this._myLight.intensity += intensityAmount;
        }

        private void DecreaseLightAngle()
        {
            if (this._myLight.spotAngle > this._minimumAngle)
            {
                this._myLight.spotAngle -= this._angleDecay * Time.deltaTime;
            }
        }

        private void DecreaseLightIntensity()
        {
            this._myLight.intensity -= this._lightDecay * Time.deltaTime;
        }
    }
}
