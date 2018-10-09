using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour {

    private int collectedAmount = 0;
    private int life = 5;
    private bool allCollected;
    private bool playerDies;
    private bool gameOver;

    public MarblesGenerator marblesGenerator;

    void OnCollisionEnter(Collision collision)
    {
        GameObject objectDetected = collision.gameObject;

        int tempIndex = objectDetected.transform.GetSiblingIndex();

        if (objectDetected.CompareTag("Marble"))
        {
            //Debug.Log("Index: " + objectDetected.transform.GetSiblingIndex());
            if (marblesGenerator.GetTargetIndices().Contains(tempIndex))
            {
                collectedAmount++;
                Debug.Log(collectedAmount + " targets get!");
                if (collectedAmount == marblesGenerator.GetTargetIndices().Count)
                {
                    allCollected = true;
                }
            }
            else
            {
                life--;
                if (life <= 0)
                {
                    gameOver = true;
                }
            }
        }

        if (objectDetected.CompareTag("Player"))
        {
            //Debug.Log("Player Detected");
            playerDies = true;
        }
    }

}
