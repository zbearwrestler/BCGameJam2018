using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingHead : MonoBehaviour
{
    //Public Variables
    public int HeadID;

    public float SpawnSpeed;
    public GameObject AngryPrefab;
    public GameObject NeutralPrefab;
    public GameObject PassAggPrefab;

    public Transform[] SpawnPosition;
    public Transform[] CenterPosition;

    public List<Coroutine> replyCoroutines;

    public bool IsStarter;

    public TalkingHead target;

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
            if (mAnimator)
            {
                mAnimator.SetFloat("AngerLevel", Aggressiveness);
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
    private Vector2[] mSpawnDirections = new Vector2[3];
    private Animator mAnimator;
    private float mInsultSpawnTimer = 0;

    private float mAggressiveness = 50f;
    private float mCommunicativeness = 50f;

    //--------------------------------------------------
    //Unity Functions
    void Awake()
    {
        Physics2D.IgnoreLayerCollision(8, 8, true);
        Init();
        if (IsStarter)
        {
            StartCoroutine(WaitAndReply(1));
        }
    }

    void Update()
    {
        //mInsultSpawnTimer += Time.deltaTime * SpawnSpeed;
        //if (mInsultSpawnTimer >= 10)
        //{
        //    mInsultSpawnTimer = 0;
        //    SpawnTextProjectile();
        //}

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
                    case TextProjectile.Type.Positive:
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
        for (int i = 0; i < 3; ++i)
        {
            mSpawnDirections[i] = (SpawnPosition[i].position - CenterPosition[i].position).normalized;
        }
        mAnimator = GetComponent<Animator>();
    }

    private void SpawnTextProjectile()
    {
        //calculate odds
        float totalOdds = Communicativeness + 100f;
        float randomResult = Random.Range(0, totalOdds);

        GameObject prefabToSpawn = AngryPrefab;
        if (randomResult < Communicativeness) { prefabToSpawn = NeutralPrefab; }
        else if (randomResult < Communicativeness + Aggressiveness) { prefabToSpawn = PassAggPrefab; }

        int spawnLane = Random.Range(0, 3);

        //spawn
        GameObject spawnedObject = GameObject.Instantiate(prefabToSpawn, SpawnPosition[spawnLane].position, Quaternion.identity);
        spawnedObject.GetComponent<TextProjectile>().Initialize(mSpawnDirections[spawnLane], HeadID, 0);

        //Trigger animation
        if (mAnimator)
        {
            mAnimator.SetTrigger("Talk");
        }
    }

    public void TriggerWaitToReply()
    {
        float time = 1f; //calculate time
        StartCoroutine(WaitAndReply(time));
    }

    private IEnumerator WaitAndReply(float time)
    {
        //wait
        yield return new WaitForSeconds(time);
        //spawn own projectile
        SpawnTextProjectile();
        //trigger other head to wait and reply
        target.TriggerWaitToReply();
        //remove this coroutine from list

    }

}
