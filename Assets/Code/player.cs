using System.Collections;
using UnityEngine;

public class player : MonoBehaviour {

    [Header("Player Movement")]
    private float yrange = 10;
    private float xrange = 5;
    private float yspeed = 0;
    [SerializeField]private float xspeed = 10;

    [Header("Health Settings")]
    public float health;
    float xPos;
    float yPos;

    private void Start()
    {
        xPos = 0;
        yPos = -4;
    }

    void FixedUpdate()
    {
       
        //float v = Input.GetAxis("Vertical");
        
       /* if (yPos * v < 0 || (yPos > -yrange && yPos < yrange))
        {
            yPos += v * yspeed * Time.deltaTime;
        }*/

        float h = Input.GetAxis("Horizontal");

        if (xPos * h < 0 || (xPos > -xrange && xPos < xrange))
        {
            xPos += h * xspeed * Time.deltaTime;
        }


        transform.position = new Vector3(xPos, yPos, 0);
    }

}
