using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;


public class PlayerCharacter : MonoBehaviour {

    // Use this for initialization
    [Tooltip("In ms^-1")] [SerializeField] float controlSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;
    [SerializeField] GameObject[] guns;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -20f;

    public GameObject bullet;
    public Transform pos1;
    public Transform posL;
    public Transform posR;
    public AudioClip shotSound;

    int powerUp;
    public float shotPower;
    public int hp;

    AudioSource shootAudio;

    float xThrow, yThrow;
    void Start () {
        powerUp = 2;
        shootAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        Movement();
        Shooting();

    }

    void Movement ()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
    void Shooting ()
    {
       // CrossPlatformInputManager.GetButton();
       if (Input.GetKeyDown(KeyCode.Space))
        {
            shootAudio.PlayOneShot(shotSound);
            switch(powerUp)
            {
                case 1:
                    {
                        print("ASDAS");
                        GameObject newBullet = Instantiate(bullet, pos1.position, transform.rotation);
                        newBullet.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                    }break;
                case 2:
                    {
                        GameObject Bullet1 = Instantiate(bullet, posL.position, transform.rotation);
                        Bullet1.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                        GameObject Bullet2 = Instantiate(bullet, posR.position, transform.rotation);
                        Bullet2.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                    }
                    break;
                case 3:
                    {
                        GameObject Bullet1 = Instantiate(bullet, posL.position, transform.rotation);
                        Bullet1.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                        GameObject Bullet2 = Instantiate(bullet, posR.position, transform.rotation);
                        Bullet2.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                        GameObject Bullet3 = Instantiate(bullet, pos1.position, transform.rotation);
                        Bullet3.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                    }
                    break;
                default:
                    {
                        
                    }
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "powerUp")
        {
            if (powerUp < 3)
            {
                powerUp++;
                Destroy(col.gameObject);
            }
        }
        if (col.gameObject.tag == "powerDown")
        {
            if (powerUp > 1)
            {
                powerUp--;
                Destroy(col.gameObject);
            }
        }
    }
}
