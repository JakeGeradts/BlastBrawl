using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateItem : MonoBehaviour {
    public float RotSpeed = 1;
    public bool Reverse = false;
    private float drop = 0;
    public float DropSpeed = 0.1f;
    public float maxDrop =1;
    private bool dropReverse = false;
    // Use this for initialization
    void Start () {
		if (Reverse == true)
        {
            RotSpeed = -RotSpeed;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(Vector3.up, RotSpeed);
        if (drop >= maxDrop)
        {
            dropReverse = false;
        }
        if (drop <= -maxDrop)
        {
            dropReverse = true;
        }
        if (dropReverse == false)
        {
            drop -= DropSpeed;
        }
        else
        {
            drop += DropSpeed;
        }
        transform.Translate(Vector3.up* (drop * Time.deltaTime));
    }
}
