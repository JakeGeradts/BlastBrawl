using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 Movement;
    private float timer;
    public float timerMax = 100;
    public float PlatformSpdX = 2;   
    public bool Reverse;


    private float timerY;
    public float timerMaxY = 100;
    public float PlatformSpdY = 0;
    public bool ReverseY;

    private float timerZ;
    public float timerMaxZ = 100;
    public float PlatformSpdZ = 0;
    public bool ReverseZ;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Control X Movement
        if (timer >= timerMax)
        {
            if (Reverse == true)
                Reverse = false;
            else
                Reverse = true;
            timer = 0;
        }
        else
        {
            timer += 1;
        }
        if (Reverse == true)
            transform.Translate(Vector3.right * PlatformSpdX * Time.deltaTime);
        else
            transform.Translate(Vector3.right * -PlatformSpdX * Time.deltaTime);

        //Control Y Movement
        if (timerY >= timerMaxY)
        {
            if (ReverseY == true)
                ReverseY = false;
            else
                ReverseY = true;
            timerY = 0;
        }
        else
        {
            timerY += 1;
        }
        if (ReverseY == true)
            transform.Translate(Vector3.forward * PlatformSpdY * Time.deltaTime);
        else
            transform.Translate(Vector3.forward * -PlatformSpdY * Time.deltaTime);
        //Control Z Movement
        if (timerZ >= timerMaxZ)
        {
            if (ReverseZ == true)
                ReverseZ = false;
            else
                ReverseZ = true;
            timerZ = 0;
        }
        else
        {
            timerZ += 1;
        }
        if (ReverseZ == true)
            transform.Translate(Vector3.up * -PlatformSpdZ * Time.deltaTime);
        else
            transform.Translate(Vector3.up * PlatformSpdZ * Time.deltaTime);
    }
}
