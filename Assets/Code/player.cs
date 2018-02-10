using System.Collections;
using UnityEngine;

public class player : MonoBehaviour {

    [Header("Player Movement")]
    public float yrange;
    public float xrange;
    public float yspeed;
    public float xspeed;

    [Header("Health Settings")]
    public float health;
    float xPos;
    float yPos;

    private void Start()
    {
        xPos = 0;
        yPos = 0;
    }

    void FixedUpdate()
    {
       
        float v = Input.GetAxis("Vertical");
        
        if (yPos * v < 0 || (yPos > -yrange && yPos < yrange))
        {
            yPos += v * yspeed * Time.deltaTime;
        }

        float h = Input.GetAxis("Horizontal");

        if (xPos * h < 0 || (xPos > -xrange && xPos < xrange))
        {
            xPos += h * xspeed * Time.deltaTime;
        }


        transform.position = new Vector3(xPos, yPos, 0);
    }

}
