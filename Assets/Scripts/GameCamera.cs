using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCamera : MonoBehaviour
{
    public bool HideCursor;
    public Transform target;
    public GameObject Player;

    public Transform Respawntarget;
    public GameObject RespawnPlayer;
    public float sensivityX = 4.0f;
    public float sensivityY = 1.0f;
    public float distance = 5.0f;
    public string CameraX = "Mouse XP1";
    public string CameraY = "Mouse YP1";

    public float RespawnTimer = 0;
    public float RespawnTimerMax = 15;

    public float rotationSmoothTime = 1.2f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    private Camera cam;

    public Vector2 Y_ANGLE_MIN = new Vector2(-50,50);
    public Vector2 X_ANGLE_MIN = new Vector2(-50,50);


    private float Pitch = 0.0f;
    private float Yaw = 0.0f;
    GameObject[] spawnPoints;
    GameObject currentPoint;
    PlayerMovement PlayermovementScript;

    int index;

    private void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
        if (HideCursor == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    private void LateUpdate()
    {
        //If the player that this camera follows exists
        if (Player != null)
        { 
            Yaw += Input.GetAxis(CameraX) * sensivityX;
            Pitch -= Input.GetAxis(CameraY) * sensivityY;
            Pitch = Mathf.Clamp(Pitch, Y_ANGLE_MIN.x, Y_ANGLE_MIN.y);

            currentRotation = Vector3.SmoothDamp((currentRotation), new Vector3(Pitch, Yaw), ref rotationSmoothVelocity, rotationSmoothTime);
            transform.eulerAngles = currentRotation;

            transform.position = target.position - transform.forward * distance;
        }
        else
        {
            //Respawn Timer
            if (RespawnTimer < RespawnTimerMax)
            {
                RespawnTimer += 1 * Time.deltaTime; ;
            }
            else
            {
                //Get Random Respawn Point on the map
                index = Random.Range(0, spawnPoints.Length);
                currentPoint = spawnPoints[index];

                //Reset Respawn Timer and create the player object again
                RespawnTimer = 0;
                Player=Instantiate(RespawnPlayer, currentPoint.transform.position, currentPoint.transform.rotation);
                //Player.transform.y = currentPoint.transform.y + 32;

                //Set variables for the player and Camera
                PlayermovementScript = Player.GetComponent<PlayerMovement>();
                PlayermovementScript.CameraT = transform;
                target = Player.GetComponentInChildren<Transform>().Find("TargetLook");
            }
        }
    }

    private void EndMatch()
    {
        if (HideCursor == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
