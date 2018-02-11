using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject brainPrefab;
    public GameObject loveBombPrefab;
    public Image coolDownImage;

    [Header("Spawns")]
    public Transform bulletSpawn;
    public Transform loveBombSpawn;


    [Header("Brain Properties")]
    public float brainVelocity;
    public float brainCooldown;

    [Header("Love Bomb Properties")]
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
            AudioManager.Play("BrainShot");
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

            GameObject bomb = Instantiate(loveBombPrefab, loveBombSpawn);

            bomb.GetComponent<LoveBomb>().Begin();

            bomb.transform.parent = null;

            lastLoveBombFireTime = Time.time;

            loveBombCooldownIsActive = true;

            coolDownImage.fillAmount = 1;
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
