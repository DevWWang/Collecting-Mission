using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarblesGenerator : MonoBehaviour {

    public GameObject pickupMarblesPrefab;
    public Transform spawnPoint;

    private GameObject marbles;
    private int marblesAmount = 25;
    private int targetAmount;
    public int targetAmountLimit = 1;
    private Color targetColor = Color.red;

    private List<int> randomTargetIndices = new List<int>();
    [HideInInspector]public List<GameObject> randomTarget = new List<GameObject>();

    void Awake()
    {
        targetAmount = Mathf.FloorToInt(Random.Range(1, targetAmountLimit));
    }
    // Use this for initialization
    void Start ()
    {
        //Debug.Log("Total targets = " + targetAmount);
        GenerateMarbles(targetAmount);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void GenerateMarbles(int n)
    {
        marbles = Instantiate(pickupMarblesPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
        while (randomTargetIndices.Count != targetAmount)
        {
            int newIndex = Random.Range(0, marblesAmount - 1);
            if (randomTargetIndices.Contains(newIndex))
            {
                newIndex = Random.Range(0, marblesAmount - 1);
                //Debug.Log( "Re-generate again");
            }
            else
            {
                randomTargetIndices.Add(newIndex);
                //Debug.Log("index " + newIndex + " added");
            }
            randomTarget.Add(marbles.transform.GetChild(newIndex).gameObject);
        }
        foreach (GameObject targetMarble in randomTarget)
        {
            targetMarble.GetComponent<Renderer>().material.color = targetColor;
        }
        //Debug.Log(randomTarget.transform.position);
    }
}
