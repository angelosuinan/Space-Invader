using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed;
    public float changeTimer;
    public float shootPower;
    float maxTimer;
    public int hp;
    public bool directionSwitch;
    public bool canShoot;
    public Transform shootingPosition;
    public GameObject particleEffect;
    public GameObject bullet;
    float shootTimer;
    float maxShootTimer;
    Rigidbody rig;
    public MapLimits Limits;
	// Use this for initialization
	void Start () {
        shootTimer = Random.Range(1,5);
        maxShootTimer = shootTimer;
        Debug.Log(shootTimer);
        maxTimer = changeTimer;
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        switchTimer();
        if (transform.position.x == Limits.maximumX || transform.position.x == Limits.minimumX) switchDirection(directionSwitch);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, Limits.minimumX, Limits.maximumX),
            Mathf.Clamp(transform.position.y, Limits.minimumY, Limits.maximumY), 0.0f);
        shootTimer -= Time.deltaTime;
        if (canShoot)
        if(shootTimer <= 0)
        {
            
            GameObject newBullet = Instantiate(bullet, shootingPosition.position, new Quaternion(90,0,0,0 ));
            newBullet.GetComponent<Rigidbody>().velocity = Vector3.up * -shootPower;
            shootTimer = maxShootTimer;
        }
	}
    
    void Movement ()
    {
        if(directionSwitch == true)
            rig.velocity = new Vector3(speed * Time.deltaTime, -speed * Time.deltaTime, 0);
        else
            rig.velocity = new Vector3(-speed * Time.deltaTime, -speed * Time.deltaTime, 0);
    }

    void switchTimer()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer < 0)
        {
            switchDirection(directionSwitch);
            changeTimer = maxTimer;
            
                
              
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "enemyBullet")
            return;
        if (col.gameObject.tag == "friendlyBullet")
        {
            Destroy(col.gameObject);
            Instantiate(particleEffect, transform.position, transform.rotation);
            hp--;
            if (hp <= 0)
                Destroy(gameObject);

        }
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerCharacter>().hp--;
            Instantiate(particleEffect, transform.position, transform.rotation);
            hp--;
            if (hp <= 0)
                Destroy(gameObject);
        }
    }

    bool switchDirection(bool dir)
    {
        if (dir)
            directionSwitch = false;
        else
            directionSwitch = true;
        return directionSwitch;
    }
}
