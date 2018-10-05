using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private bool isLaunched;

    public float movingSpeed;

    public float minLaunchDistance = 5f;
    public float maxLaunchDistance = 15f;
    public float maxChargeTime = 0.75f;
    private float currentLaunchDistance;
    private float chargeRatio;
    private Vector3 currentPosition;

    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();

        currentLaunchDistance = minLaunchDistance;
        chargeRatio = (maxLaunchDistance - minLaunchDistance) / maxChargeTime;
    }
	
	void Update ()
    {
        LaunchAction();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Move() * movingSpeed);
    }

    Vector3 Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        bool left = moveHorizontal < 0 ? true : false;
        bool right = moveHorizontal > 0 ? true : false;
        bool down = moveVertical < 0 ? true : false;
        bool up = moveVertical > 0 ? true : false;

        string direction = "";

        if (left && !right && !down && !up)
        {
            direction = "Left";
        }
        else if (!left && right && !down && !up)
        {
            direction = "Right";
        }
        else if (!left && !right && down && !up)
        {
            direction = "Down";
        }
        else if (!left && !right && !down && up)
        {
            direction = "Up";
        }
        Debug.Log(direction);

        // since we do not want ot move up, the y value is zero
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        return movement;
    }

    void LaunchAction()
    {
        currentPosition = transform.position;

        bool down = Input.GetKeyDown(KeyCode.Space);
        bool held = Input.GetKey(KeyCode.Space);
        bool up = Input.GetKeyUp(KeyCode.Space);

        if (currentLaunchDistance >= maxLaunchDistance && !isLaunched)
        {
            currentLaunchDistance = maxLaunchDistance;
            Launch();
        }
        else if (down)
        {
            isLaunched = false;
            currentLaunchDistance = minLaunchDistance;
            Debug.Log("Down");
        }
        else if (held && !isLaunched)
        {
            currentLaunchDistance += chargeRatio * Time.deltaTime;
            Debug.Log(currentLaunchDistance);
        }
        else if (up && !isLaunched)
        {
            Launch();
            Debug.Log("up");
        }
        //Debug.Log(currentLaunchDistance);
    }

    void Launch()
    {
        isLaunched = true;
        Debug.Log("Launch from " + currentPosition.ToString("F4"));
        rb.velocity = (Vector3.zero - currentPosition).normalized * currentLaunchDistance;
        currentLaunchDistance = minLaunchDistance;
    }
}
