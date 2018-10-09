using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour {

    private int collectedAmount = 0;
    private int life = 5;
    private bool allCollected = false;
    private bool gameOver = false;

    public MarblesGenerator marblesGenerator;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Marble"))
        {
            //Debug.Log("Index: " + objectDetected.transform.GetSiblingIndex());
            if (marblesGenerator.GetTargetIndices().Contains(collision.gameObject.transform.GetSiblingIndex()))
            {
                collectedAmount++;
                Debug.Log(collectedAmount + " targets get!");
                if (collectedAmount == marblesGenerator.GetTargetIndices().Count)
                {
                    allCollected = true;
                    Debug.Log("All Collected " + allCollected);
                }
            }
            else
            {
                life--;
                Debug.Log(life + " lives left");
                if (life <= 0)
                {
                    Debug.Log("Dead");
                    gameOver = true;
                }
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Detected");
            gameOver = true;
        }
    }

    public bool GetAllTargets()
    {
        return allCollected;
    }

    public bool EndGame()
    {
        return (gameOver || allCollected);
    }
}
