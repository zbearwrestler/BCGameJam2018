using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject brainPrefab;
    public GameObject loveBombPrefab;
    public Transform bulletSpawn;
    public Image coolDownImage;

    [Header("Brain Properties")]
    public float brainVelocity;
    public float brainCooldown;

    [Header("Love Bomb Properties")]
    public float loveBombVelocity;
    public float loveBombCooldown;

    private bool loveBombCooldownIsActive;

    private float lastBrainFireTime;
    private float lastLoveBombFireTime;


    // Use this for initialization
    void Start()
    {
        Physics.IgnoreLayerCollision(8, 9);

        loveBombCooldownIsActive = true;
        lastLoveBombFireTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1")  || Input.GetKey(KeyCode.Space)) //fire button
        {
            FireBrain();
            
        }
        if(Input.GetButtonDown("Fire2") || Input.GetKey(KeyCode.LeftShift))
        {
            FireLoveBomb();
        }

        CooldownUI();

    }

    void FireBrain()
    {

        if (Time.time > lastBrainFireTime + brainCooldown)
        {
            gameObject.GetComponent<AudioSource>().Play();

            GameObject bullet = Instantiate(brainPrefab, bulletSpawn);

            bullet.transform.parent = null;

            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, brainVelocity);

            lastBrainFireTime = Time.time;

            Destroy(bullet, 5f);
        }
    }

    void FireLoveBomb()
    {

        if (Time.time > lastLoveBombFireTime + loveBombCooldown)
        {


            GameObject bullet = Instantiate(loveBombPrefab, bulletSpawn);

            bullet.transform.parent = null;

            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, loveBombVelocity);

            lastLoveBombFireTime = Time.time;

            loveBombCooldownIsActive = true;

            coolDownImage.fillAmount = 1;

            Destroy(bullet, 10f);
        }

    }

    void CooldownUI()
    {
        if (loveBombCooldownIsActive)
        {
            coolDownImage.fillAmount -= 1 / loveBombCooldown * (Time.deltaTime);
        }

        if (Time.time > lastLoveBombFireTime + loveBombCooldown)
        {
            loveBombCooldownIsActive = false;
        }
    }
}
