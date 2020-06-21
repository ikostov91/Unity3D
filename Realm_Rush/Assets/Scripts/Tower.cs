using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform _objectToMove;
    [SerializeField] private Transform _targetEnemy;

    // Update is called once per frame
    void Update()
    {
        this.LookAtEnemy();
    }

    private void LookAtEnemy()
    {
        this._objectToMove.LookAt(this._targetEnemy);
    }
}
