using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public int PlayerPointsGoal = 26;

    private bool gameOver;

    private Rigidbody rb;
    private PickUpSystem PickUpSystem;
    private int Points;
    private int totalAmountOfPickUp;
    private GameObject pickUpObject;

    public bool GameOver
    {
        get
        {
            return gameOver;
        }

        set
        {
            gameOver = value;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Points = 0;
        SetCountText();
        winText.text = "";
        GameOver = false;
        PickUpSystem = GameObject.FindGameObjectWithTag("Pick Ups").GetComponent<PickUpSystem>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // since we do not want ot move up, the y value is zero
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
        if (other.gameObject.CompareTag("Pick Up") && other != null)
        {
            PickUpSystem.DestroyPickUp(this, other.gameObject);
            SetCountText();
        }
    }

    public void GivePoints(int points)
    {
        this.Points += points;
    }

    void SetCountText()
    {
        countText.text = "Current Points: " + Points.ToString();
        if (Points >= PlayerPointsGoal)
        {
            winText.text = "You Win!";
            gameOver = true;
            //Debug.Log("GameOver? " + gameOver);
            PickUpSystem.EndPickUpSystem(this);
        }
    }
}
