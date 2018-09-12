using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour {

    public GameObject PickUpPrefab;
    public int NumberOfPickUpsAtStart = 10;
    [Range(1, 25)] public int MaxNumberOfPickUps = 20;

    public float SpawnFrequency = 0.5f; // how many per second
    public float SpawnPeriod { get { return 1f / SpawnFrequency; } }

    public float TimeAtLastSpawn;

    private int NumberOfPickUps;

    //List<GameObject> PickUps;

	// Use this for initialization
	void Start () {
        NumberOfPickUps = 0;
        //pickups = new list<gameobject>();
        TimeAtLastSpawn = Time.time;
        for (int i = 0; i < NumberOfPickUpsAtStart; i++)
        {
            SpawnPickUp();
        }
    }

    private void SpawnPickUp()
    {
        Vector3 pos = GetRandomPos();
        GameObject pickUp = GameObject.Instantiate(PickUpPrefab, this.transform);
        pickUp.transform.position += pos;
        TimeAtLastSpawn = Time.time;
        NumberOfPickUps++;
        //PickUps.Add(pickUp);
    }

    public void DestroyPickUp(PlayerController player, GameObject pickUp) {
        PickUp PickUp = pickUp.GetComponent<PickUp>();
        player.givePoints(PickUp.Points);
        Destroy(pickUp);
        NumberOfPickUps--;
    }

    public Vector3 GetRandomPos() {
        GameObject groundObject = GameObject.FindWithTag("Ground");
        GameObject wallsObject = GameObject.FindWithTag("Walls");
        Mesh planeMesh = groundObject.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;

        float rangeX = groundObject.transform.position.x - groundObject.transform.localScale.x * (bounds.size.x - wallsObject.transform.localScale.x) * 0.5f;
        float rangeZ = groundObject.transform.position.z - groundObject.transform.localScale.z * (bounds.size.z - wallsObject.transform.localScale.z) * 0.5f;

        Debug.Log("Bounds Size: x = " + bounds.size.x + ", z = " + bounds.size.z);
        Debug.Log("Wall Scale: x = " + wallsObject.transform.localScale.x + ", z = " + wallsObject.transform.localScale.z);
        Debug.Log("rangeX: " + rangeX + ", rangeZ: " + rangeZ);

        return new Vector3(Random.Range(rangeX, -rangeX), 0f, Random.Range(rangeZ, -rangeZ));
    }

    // Update is called once per frame
    void Update () {
        float time = Time.time;
        if (time - TimeAtLastSpawn > SpawnPeriod && NumberOfPickUps < MaxNumberOfPickUps) {
            SpawnPickUp();
        }
	}
}
