using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarblesGenerator : MonoBehaviour {

    public GameObject pickupMarblesPrefab;
    public Transform spawnPoint;

    private GameObject marbles;
    private int marblesAmount = 25;
    private int targetAmount;
    public int targetAmountLimit;
    private Color targetColor = Color.red;

    [HideInInspector]public List<int> randomTargetIndices = new List<int>();
    [HideInInspector]public List<GameObject> randomTarget = new List<GameObject>();

    void Awake()
    {
        targetAmount = Mathf.FloorToInt(Random.Range(1, targetAmountLimit));
    }

    public void GenerateMarbles()
    {
        marbles = Instantiate(pickupMarblesPrefab, spawnPoint.position, spawnPoint.rotation);
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
                Debug.Log("index " + newIndex + " added");
            }
            randomTarget.Add(marbles.transform.GetChild(newIndex).gameObject);
        }
        foreach (GameObject targetMarble in randomTarget)
        {
            targetMarble.GetComponent<Renderer>().material.color = targetColor;
        }
        //Debug.Log(randomTarget.transform.position);
    }

    public List<GameObject> GetTargetMarbles()
    {
        return randomTarget;
    }

    public List<int> GetTargetIndices()
    {
        return randomTargetIndices;
    }

    public void DisableAllMarbles()
    {
        Destroy(marbles);
    }
}
