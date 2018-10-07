using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour {
    //Who Shot the Projectile. I shouldn't hit the caster regardless if they touch it.
    public GameObject Shooter;
    public int ShooterNum = 0;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject != Shooter)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }
}
