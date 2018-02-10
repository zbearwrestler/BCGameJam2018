using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{

    [Header("Prefabs")]
    public GameObject blockerPrefab;
    public GameObject destroyerPrefab;
    public GameObject loveBombPrefab;
    public Transform bulletSpawn;

    [Header("Blocker Properties")]
    public float blockerVelocity;
    public float blockerCooldown;

    [Header("Destroyer Properties")]
    public float destroyerVelocity;
    public float destroyerCooldown;

    [Header("Love Bomb Properties")]
    public float loveBombVelocity;
    public float loveBombCooldown;

    private bool fireMode; // true is blocker, false is destroyer

    private float lastBlockerFireTime;
    private float lastDestroyerFireTime;
    private float lastLoveBombFireTime;


    // Use this for initialization
    void Start()
    {

        fireMode = true;

        Physics.IgnoreLayerCollision(8, 9);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire2")) //change fire mode
        {
            ChangeMode();
        }

        if (Input.GetButtonDown("Fire1")) //fire button
        {
            Fire();
        }

        if (Input.GetButtonDown("Fire3")) //fire love bomb
        {
            FireLoveBomb();
        }

    }

    void Fire()
    {

        if (fireMode) //blocker mode
        {

            if (Time.time > lastBlockerFireTime + blockerCooldown)
            {
                GameObject bullet = (GameObject)Instantiate(blockerPrefab, bulletSpawn);

                bullet.transform.parent = null;

                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, blockerVelocity);

                lastBlockerFireTime = Time.time;

                Destroy(bullet, 5f);
            }

        }
        else // destroyer mode
        {

            if (Time.time > lastDestroyerFireTime + destroyerCooldown)
            {
                GameObject bullet = (GameObject)Instantiate(destroyerPrefab, bulletSpawn);

                bullet.transform.parent = null;

                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, destroyerVelocity);

                lastDestroyerFireTime = Time.time;

                Destroy(bullet, 5f);
            }

        }
    }

    void FireLoveBomb()
    {

        if (Time.time > lastLoveBombFireTime + loveBombCooldown)
        {

            GameObject bullet = (GameObject)Instantiate(loveBombPrefab, bulletSpawn);

            bullet.transform.parent = null;

            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, loveBombVelocity);

            lastLoveBombFireTime = Time.time;

            Destroy(bullet, 10f);

        }

    }

    void ChangeMode()
    {
        fireMode = !fireMode;
    }

}
