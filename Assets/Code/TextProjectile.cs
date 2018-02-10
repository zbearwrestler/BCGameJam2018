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
        gameObject.transform.Translate(mTrajectory * Speed * Time.timeScale);
	}

    public void Initialize(Vector2 traj, int spawnerID, int line)
    {
        mTrajectory = traj;
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
        if(collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);

            if (ProjectileType == Type.Positive || ProjectileType == Type.PassAggressive)
            {
                ProjectileType = Type.Positive;
                AttachedTextMesh.text = ArgumentText.GetLine("Positive", spawnedBy, convoIndex);
            }else
            {
                Destroy(gameObject);
            }
        }
    }
}
