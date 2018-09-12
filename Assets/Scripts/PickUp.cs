using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    private int points;
    public float minSize;
    public float maxSize;

    public int pointLevels = 5;

    public int Points
    {
        get
        {
            return points;
        }

        set
        {
            points = value;
        }
    }

    void Awake() {
        float size = UnityEngine.Random.Range(minSize, maxSize);
        this.gameObject.transform.localScale = new Vector3(size, size, size);
        Points = Mathf.CeilToInt(pointLevels * (size - minSize) / (maxSize - minSize));
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
