using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed;
    public float changeTimer;
    float maxTimer;
    public int hp;
    public bool directionSwitch;
    public GameObject particleEffect;
    Rigidbody rig;
	// Use this for initialization
	void Start () {
        maxTimer = changeTimer;
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        switchTimer();
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
            if (directionSwitch)
                directionSwitch = false;
            else
            {
                directionSwitch = true;
                changeTimer = maxTimer;
            }
                
              
        }
    }

    private void OnTriggerEnter(Collider col)
    {
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
}
