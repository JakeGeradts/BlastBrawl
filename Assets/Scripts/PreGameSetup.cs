using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameSetup : MonoBehaviour {

    //PreGame Setup
    public Camera Cam1;
    public Camera Cam2;
    public Camera Cam3;
    public Camera Cam4;

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;
    public int PlayerCount = 1;


    // Use this for initialization
    void Start()
    {
        // if (GameObject.Find("Player1Cam") != null)
        //{
        //it exists
        //}
        //Set Screen Size for players
        switch (PlayerCount)
        {
            //If Game is 2 Player
            case 1:
                {
                    //Camera.Player1Cam.active = false;
                    Player2.SetActive(true);
                    Player3.SetActive(false);
                    Player4.SetActive(false);
                    Cam3.enabled = false;
                    Cam4.enabled = false;
                    Cam1.enabled = true;
                    Cam2.enabled = true;
                    if (Cam1.enabled == true)
                    {
                        Cam1.rect = new Rect(0, .5f, 1f, .5f);
                    }
                    if (Cam2.enabled == true)
                    {
                        Cam2.rect = new Rect(0f, 0f,1f, .5f);
                    }
                    break; };
            //If Game is 3 Player
            case 2:
                {
                    Player2.SetActive(true);
                    Player3.SetActive(true);
                    Player4.SetActive(false);

                    Cam3.enabled = true;
                    Cam4.enabled = false;
                    Cam1.enabled = true;
                   
                    Cam2.enabled = true;
                    if (Cam1.enabled == true)
                    {
                        Cam1.rect = new Rect(0, .5f, .5f, .5f);
                    }
                    if (Cam2.enabled == true)
                    {
                        Cam2.rect = new Rect(.5f, .5f, .5f, .5f);
                    }
                    if (Cam3.enabled == true)
                    {
                        Cam3.rect = new Rect(0f, 0f, .5f, .5f);
                    }
                    break; };
            //If Game is 4 Player
            case 3:
                {
                    Player2.SetActive(true);
                    Player3.SetActive(true);
                    Player4.SetActive(true);
                    Cam3.enabled = true;
                    Cam4.enabled = true;
                    Cam1.enabled = true;
                    Cam2.enabled = true;
                    if (Cam1.enabled == true)
                    {
                        Cam1.rect = new Rect(0, .5f, .5f, .5f);
                    }
                    if (Cam2.enabled == true)
                    {
                        Cam2.rect = new Rect(.5f, .5f, .5f, .5f);
                    }
                    if (Cam3.enabled == true)
                    {
                        Cam3.rect = new Rect(0f, 0f, .5f, .5f);
                    }
                    if (Cam4.enabled == true)
                    {
                        Cam4.rect = new Rect(.5f, 0f, .5f, .5f);
                    }
                    break; };
            //If Game is forced to have one Player
            default:
                {
                    if (Cam1.enabled == true)
                    {
                        Cam1.rect = new Rect(0, 0, 1f, 1f);
                    }
                    Player2.SetActive(false);
                    Player3.SetActive(false);
                    Player4.SetActive(false);
                    Cam3.enabled = false;
                    Cam4.enabled = false;
                    Cam1.enabled = true;
                    Cam2.enabled = false;
                    break;
                }
        }


    }

    
	
	// Update is called once per frame
	void Update () {
		
	}
}
