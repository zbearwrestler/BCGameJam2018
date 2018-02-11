using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextProjectile : MonoBehaviour {
    public enum Type { Angry, PassAggressive, Neutral, Positive}
    public Type ProjectileType;

    public float Speed;

    public TextMesh AttachedTextMesh;

    private Vector2 mTrajectory;

    private int spawnedBy;
    private int convoIndex;

    private Color angryColor = new Color(1f,0f,0f);
    private Color passAggressiveColor = new Color(0.282f,0f,1f);
    private Color neutralColor = new Color(0.5f, 0.5f, 0.5f);
    private Color positiveColor = new Color(0f,0.578f,1f);

    public Sprite positiveSprite;

    public GameObject particlePrefab;

    private SpriteRenderer mSpriteRenderer;

    public int SpawnedBy
    {
        get
        {
            return spawnedBy;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.Translate(mTrajectory * Speed * Time.timeScale, Space.World);
	}

    public void Initialize(Vector2 traj, int spawnerID, int line)
    {
        //rotate
        mTrajectory = traj;
        float angle = Vector2.SignedAngle(Vector2.right, mTrajectory);
        if (angle > 90) { angle -= 180; }
        else if (angle < -90) { angle += 180; }

        mSpriteRenderer = GetComponent<SpriteRenderer>();

        transform.rotation = Quaternion.Euler(0,0,angle);
        spawnedBy = spawnerID;
        convoIndex = line;
        SetUpText();
        //SetColor();
    }

    private void SetUpText()
    {
        switch (ProjectileType)
        {
            case Type.Angry:
                AttachedTextMesh.text = ArgumentText.GetLine("Angery", spawnedBy, convoIndex);
                break;
            case Type.PassAggressive:
                AttachedTextMesh.text = ArgumentText.GetLine("PassAggressive", spawnedBy, convoIndex);
                break;
            case Type.Neutral:
                AttachedTextMesh.text = ArgumentText.GetLine("Neutral", spawnedBy, convoIndex);
                break;
            case Type.Positive:
                AttachedTextMesh.text = ArgumentText.GetLine("Positive", spawnedBy, convoIndex);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);

            if (ProjectileType == Type.Positive || ProjectileType == Type.PassAggressive)
            {
                if (ProjectileType == Type.PassAggressive)
                {
                    AudioManager.Play("Bloop");
                    SpawnDestroyedParticles();
                }
                ProjectileType = Type.Positive;
                AttachedTextMesh.text = ArgumentText.GetLine("Positive", spawnedBy, convoIndex);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().angularVelocity = 0f;
                SetColor();
            }
            else
            {
                if (ProjectileType == Type.Neutral)
                {
                    //shot down neutral - notify talking head
                    AudioManager.Play("SHH");
                    TalkingHeadManager.Instance.NotifyWasInterrupted(SpawnedBy);
                    
                }
                SpawnDestroyedParticles();
                Destroy(gameObject);
                AudioManager.Play("BrainSlat");
            }
        }
    }

    private void SetColor()
    {
        switch (ProjectileType)
        {
            case Type.Angry:
                mSpriteRenderer.color = angryColor;
                break;
            case Type.PassAggressive:
                mSpriteRenderer.color = passAggressiveColor;
                break;
            case Type.Neutral:
                mSpriteRenderer.color = neutralColor;
                break;
            case Type.Positive:
                mSpriteRenderer.color = positiveColor;
                GetComponent<SpriteRenderer>().sprite = positiveSprite;
                break;
            default:
                break;
        }
    }

    private void SpawnDestroyedParticles()
    {
        GameObject particleSpawned = GameObject.Instantiate(particlePrefab, transform.position, Quaternion.identity);
        Destroy(particleSpawned, 1f);
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "PlayerBullet")
    //    {
    //        if (ProjectileType == Type.Positive || ProjectileType == Type.PassAggressive)
    //        {
    //            ProjectileType = Type.Positive;
    //            AttachedTextMesh.text = ArgumentText.GetLine("Positive", spawnedBy, convoIndex);
    //        }else
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}
}
