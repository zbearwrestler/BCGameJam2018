using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveBomb : MonoBehaviour {

    public float Agresivnessreduction;
    public GameObject ParticleEffect;
    public float ParticleLifeTime = 2f;

    public void Begin()
    {
        StartCoroutine(Explode());
    }

    public IEnumerator Explode()
    {
        AudioManager.Play("LoveBomb");
        Animation animation  = gameObject.GetComponent<Animation>();
        animation.Play();
        yield return new WaitForSeconds(animation.clip.length);
        GameObject paricale = GameObject.Instantiate(ParticleEffect, gameObject.transform.position, Quaternion.identity);
        paricale.GetComponent<ParticleSystem>().Play();
        Destroy(paricale, ParticleLifeTime);
        foreach (GameObject Head in GameObject.FindGameObjectsWithTag("TalkingHead"))
        {
            Head.GetComponent<TalkingHead>().Aggressiveness -= Agresivnessreduction;
        }
        foreach (GameObject Projectile in GameObject.FindGameObjectsWithTag("EnemyProjectile"))
        {
            Destroy(Projectile);
        }
         Destroy(gameObject);
    }
}
