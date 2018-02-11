using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TalkingHead : MonoBehaviour
{
    [Header("Head Propertys")]
    public int HeadID;
    public int LinesDiologe = 1;

    [Header("Prefabs")]
    public GameObject AngryPrefab;
    public GameObject NeutralPrefab;
    public GameObject PassAggPrefab;

    [Header("Spawning")]
    public Transform[] SpawnPosition;
    public Transform[] CenterPosition;
    public float FastestSpawnRate = 0.5f;
    public float SlowestSpawnRate = 1.5f;


    [Header("Other")]
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
                Aggressiveness = 100;
                EndGame(GameScore.EndResult.Anger);
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
                Communicativeness = 0;
                Debug.Log("Lose!!!!!");
                EndGame(GameScore.EndResult.NotTalking);
            }
        }
    }

    //Private Variables
    private Vector2[] mSpawnDirections = new Vector2[3];
    private Animator mAnimator;

    private float mAggressiveness = 50f;
    private float mCommunicativeness = 50f;
    private List<Coroutine> replyCoroutines;
    private int convoIndex;

    private float mIncrement = 3;

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

    private void Start()
    {
        convoIndex = 0;
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
                        Aggressiveness += 2*mIncrement;
                        break;
                    case TextProjectile.Type.PassAggressive:
                        Aggressiveness += mIncrement;
                        Communicativeness -= mIncrement;
                        break;
                    case TextProjectile.Type.Neutral:
                        Communicativeness += mIncrement/2f;
                        break;
                    case TextProjectile.Type.Positive:
                        Communicativeness += mIncrement;
                        Aggressiveness -= mIncrement;
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

        GameObject prefabToSpawn = PassAggPrefab;
        if (randomResult < Communicativeness) { prefabToSpawn = NeutralPrefab; }
        else if (randomResult < Communicativeness + Aggressiveness) { prefabToSpawn = AngryPrefab; }

        int spawnLane = Random.Range(0, 3);

        //spawn
        GameObject spawnedObject = GameObject.Instantiate(prefabToSpawn, SpawnPosition[spawnLane].position, Quaternion.identity);
        spawnedObject.GetComponent<TextProjectile>().Initialize(mSpawnDirections[spawnLane], HeadID, convoIndex);

        //increment spawn counter and loop it
        convoIndex++;
        if (convoIndex > LinesDiologe) { convoIndex = 0; }

        //Trigger animation
        if (mAnimator)
        {
            mAnimator.SetTrigger("Talk");
        }
    }

    public void TriggerWaitToReply()
    {
        float time = 1f;
        //calculate time

        time = Mathf.Lerp(SlowestSpawnRate, FastestSpawnRate, (Aggressiveness + (Communicativeness/2))/150f);
        //Debug.Log(gameObject.name + "Anger: "  + Aggressiveness + ", Talky: " + Communicativeness + " => " + time);
        


        StartCoroutine(WaitAndReply(time));
    }

    public void NotifyWasInterrupted()
    {
        //someone shot down your neutral statement. What meanies!
        //Debug.Log("Shot down!");
        Communicativeness -= 2*mIncrement;
        Aggressiveness += 2*mIncrement;
    }

    public void StopShooting()
    {
        foreach (Coroutine coro in replyCoroutines)
        {
            StopCoroutine(coro);
        }
        replyCoroutines.Clear();
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


    private void EndGame(GameScore.EndResult result)
    {
        GameScore.Instance.Save(Time.timeSinceLevelLoad, HeadID, result);
        //TODO delay
        SceneManager.LoadScene("EndScreen");
    }

}
