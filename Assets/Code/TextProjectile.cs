using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextProjectile : MonoBehaviour {
    public enum Type { Angry, PassAggressive, Neutral, Friendly}
    public Type ProjectileType;

    public float Speed;

    public TextMesh AttachedTextMesh;

    private Vector2 mTrajectory;

    private int spawnedBy;
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

    public void Initialize(Vector2 traj, string content, int spawnerID)
    {
        mTrajectory = traj;
        AttachedTextMesh.text = content;
        spawnedBy = spawnerID;
    }
}
