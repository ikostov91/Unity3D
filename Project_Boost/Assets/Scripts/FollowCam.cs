using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Rocket _rocket;

    // Start is called before the first frame update
    void Start()
    {
        this._rocket = FindObjectOfType<Rocket>();    
    }

    // Update is called once per frame
    void Update()
    {
        float x = this._rocket.transform.position.x;
        float y = this.gameObject.transform.position.y; // 25
        float z = this.gameObject.transform.position.z; // -50
        Vector3 cameraPosition = new Vector3(x, y, z);

        this.gameObject.transform.position = cameraPosition;
    }
}
