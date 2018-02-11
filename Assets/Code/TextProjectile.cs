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

        transform.rotation = Quaternion.Euler(0,0,angle);
        spawnedBy = spawnerID;
        convoIndex = line;
        SetUpText();
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
                ProjectileType = Type.Positive;
                AttachedTextMesh.text = ArgumentText.GetLine("Positive", spawnedBy, convoIndex);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().angularVelocity = 0f;
            }
            else
            {
                if (ProjectileType == Type.Neutral)
                {
                    //shot down neutral - notify talking head
                    TalkingHeadManager.Instance.NotifyWasInterrupted(SpawnedBy);
                }
                Destroy(gameObject);
            }
        }
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
