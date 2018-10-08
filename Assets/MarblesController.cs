using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarblesController : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

        }
    }
}
