using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingHead : MonoBehaviour {
    //Public Variables
    public int HeadID;

    public float SpawnSpeed;
    public GameObject AngryPrefab;
    public GameObject NeutralPrefab;
    public GameObject PassAggPrefab;

    public Transform SpawnPosition;
    public Transform CenterPosition;


    public float Aggressiveness
    {
        get
        {
            return mAggressiveness;
        }
        set
        {
            mAggressiveness = value;
            if (mAggressiveness < 0) { mAggressiveness = 0; }
            if (mAggressiveness > 100)
            {
                Debug.Log("Lose!!!!!");
            }
        }
    }

    public float Communicativeness
    {
        get
        {
            return mCommunicativeness;
        }
        set
        {
            mCommunicativeness = value;
            if (mCommunicativeness > 100) { mCommunicativeness = 100; }
            if (mCommunicativeness < 0)
            {
                Debug.Log("Lose!!!!!");
            }
        }
    }

    //Private Variables
    private Vector2 mSpawnDirection;
    private float mInsultSpawnTimer = 0;
    private float mNeutralSpawnTimer = 0;

    private float mAggressiveness = 50f;
    private float mCommunicativeness = 50f;

    //--------------------------------------------------
    //Unity Functions
    void Awake()
    {
        Physics2D.IgnoreLayerCollision(8, 8, true);
        Init();
    }

    void Update()
    {
        mInsultSpawnTimer += Time.deltaTime*SpawnSpeed;
        if (mInsultSpawnTimer >= 10)
        {
            mInsultSpawnTimer = 0;
            SpawnInsult();
        }

        mNeutralSpawnTimer += Time.deltaTime * SpawnSpeed*(mCommunicativeness/100f);
        if (mNeutralSpawnTimer >= 5)
        {
            mNeutralSpawnTimer = 0;
            SpawnNeutral();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TextProjectile textProj = collision.gameObject.GetComponent<TextProjectile>();
        if (textProj != null)
        {
            if (textProj.SpawnedBy != HeadID)
            {

                //run effects of what kind of projectile it is
                switch (textProj.ProjectileType)
                {
                    case TextProjectile.Type.Angry:
                        Aggressiveness += 2;
                        break;
                    case TextProjectile.Type.PassAggressive:
                        ++Aggressiveness;
                        --Communicativeness;
                        break;
                    case TextProjectile.Type.Neutral:
                        ++Communicativeness;
                        break;
                    case TextProjectile.Type.Friendly:
                        ++Communicativeness;
                        --Aggressiveness;
                        break;
                    default:
                        break;
                }
                Destroy(collision.gameObject);
                
            }
        }
    }



    //--------------------------------------------------
    //Custom Functions
    private void Init()
    {
        mSpawnDirection = (SpawnPosition.position - CenterPosition.position).normalized;
    }

    private void SpawnInsult()
    {
        GameObject spawnedObject = GameObject.Instantiate((Random.Range(0,100)>mAggressiveness) ? PassAggPrefab : AngryPrefab, SpawnPosition.position, Quaternion.identity);
        spawnedObject.GetComponent<TextProjectile>().Initialize(mSpawnDirection, "You suck!!!", HeadID);
    }

    private void SpawnNeutral()
    {
        GameObject spawnedObject = GameObject.Instantiate(NeutralPrefab, SpawnPosition.position, Quaternion.identity);
        spawnedObject.GetComponent<TextProjectile>().Initialize(mSpawnDirection, "I like cake", HeadID);
    }

}
