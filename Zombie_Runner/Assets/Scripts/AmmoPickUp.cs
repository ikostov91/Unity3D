using Constants;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField] private int _ammoAmmount = 5;
    [SerializeField] private AmmoType _ammoType = AmmoType.Bullets;

    public void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == TagConstants.Player)
        {
            var ammo = FindObjectOfType<Ammo>();
            ammo.IncreaseCurrentAmmo(this._ammoType, this._ammoAmmount);
            Destroy(this.gameObject);
        }
    }
}
