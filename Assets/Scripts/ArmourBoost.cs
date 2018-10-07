using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourBoost : MonoBehaviour {

    public float ArmourGain = 25;
    public float RespawnTimeMax = 32;
    public bool Heavy = false;

    private float RespawnTime = 12;
    private Vector3 StartPosition;
    // Use this for initialization
    void Start()
    {
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (RespawnTime < RespawnTimeMax)
        {
            RespawnTime += 1 * Time.deltaTime;
        }
        else
        {
            transform.position = StartPosition;
            RespawnTime = RespawnTimeMax;
        }
    }
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.position = new Vector3(9990.0f, 9999.0f, 9990.0f);
            RespawnTime = 0;
        }
    }
}
