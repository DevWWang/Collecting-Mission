using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour {

    public Vector3 ScaleDifference;
    public float speed;

    private Vector3 StartingScale;
    private float ratio;

	// Use this for initialization
	void Start () {
        StartingScale = this.transform.localScale;
        ratio = 0;
	}
	
	// Update is called once per frame
	void Update () {
        float halfRatio = (ratio > 0.5f) ? ratio : 1 - ratio;
        this.transform.localScale = this.StartingScale + ScaleDifference * (-0.5f + halfRatio);

        ratio = speed * Time.deltaTime + ratio;
        if (ratio > 1) ratio = 0;
    }
}
