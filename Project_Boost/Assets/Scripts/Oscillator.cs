using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 _movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] private float _period = 2f;

    // TODO remove from inspector later
    [Range(0, 1)]
    [SerializeField]
    private float _movementFactor; // 0 (not moved) to 1 (fully moved)

    private Vector3 _startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        this._startingPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (this._period <= Mathf.Epsilon) // protect against period is zero
        {
            return;
        }

        float cycles = Time.time / this._period; // grows continually from zero
        const float tau = Mathf.PI * 2f; // about 6.28
        float rawSineWave = Mathf.Sin(cycles * tau); // goes from -1 to +1

        this._movementFactor = (rawSineWave / 2f) + 0.5f;

        Vector3 offset = this._movementVector * this._movementFactor;
        this.transform.position = this._startingPosition + offset;
    }
}
