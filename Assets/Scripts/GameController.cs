using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    // Use this for initialization
    public MapLimits Limits;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public float spawnTimer;
    float maxSpawnTimer;
    public Camera firstPersonCamera;
    public Camera overheadCamera;

    void Start () {
        SpawnEnemy();
        maxSpawnTimer = spawnTimer;
        firstPersonCamera.gameObject.SetActive(false);
        overheadCamera.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = maxSpawnTimer;
        }
	}

    void SpawnEnemy()
    {
        int randomNumber = Random.Range(0, 100);
        
        if(randomNumber < 40)
            {
                Instantiate(enemy1, new Vector3(Random.Range(Limits.minimumX, Limits.maximumX),
        Random.Range(Limits.minimumY, Limits.maximumY), 0), enemy1.transform.rotation);
            }
        if (randomNumber >= 40 && randomNumber <=70)
        {
                Instantiate(enemy2, new Vector3(Random.Range(Limits.minimumX, Limits.maximumX),
        Random.Range(Limits.minimumY, Limits.maximumY), 0), enemy2.transform.rotation);
            }
        if (randomNumber >= 70 && randomNumber <=90)
            {
                Instantiate(enemy3, new Vector3(Random.Range(Limits.minimumX, Limits.maximumX),
        Random.Range(Limits.minimumY, Limits.maximumY), 0), enemy3.transform.rotation);
            }
        if (randomNumber >= 90 && randomNumber <=100)
        {

        }
    }
}
