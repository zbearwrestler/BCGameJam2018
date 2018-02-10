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

    public void Initialize(Vector2 traj, int spawnerID)
    {
        mTrajectory = traj;
        spawnedBy = spawnerID;
        SetUpText();
    }

    private void SetUpText()
    {
        switch (ProjectileType)
        {
            case Type.Angry:
                AttachedTextMesh.text = ArgumentText.NextLine("Angery");
                break;
            case Type.PassAggressive:
                AttachedTextMesh.text = ArgumentText.NextLine("PassAggressive");
                break;
            case Type.Neutral:
                AttachedTextMesh.text = ArgumentText.NextLine("Neutral");
                break;
            case Type.Positive:
                AttachedTextMesh.text = ArgumentText.NextLine("Positive");
                break;
        }
        convoIndex = ArgumentText.convoCounter;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            if (ProjectileType == Type.Positive || ProjectileType == Type.PassAggressive)
            {
                ProjectileType = Type.Positive;
                AttachedTextMesh.text = ArgumentText.GetLine("Positive", convoIndex);
            }else
            {
                Destroy(gameObject);
            }
        }
    }
}
