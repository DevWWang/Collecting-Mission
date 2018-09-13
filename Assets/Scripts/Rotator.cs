using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    private Vector3 RotationDirection;
    public float speed;

    void Start()
    {
        RotationDirection = Random.onUnitSphere * 90;
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Rotate(RotationDirection * speed * Time.deltaTime);
    }
}
