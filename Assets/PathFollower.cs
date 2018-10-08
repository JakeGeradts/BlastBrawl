using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {
    Node[] PathNode;

    public GameObject Player; //object who is following the path
   
    public float MoveSpeed; // Path Speed

    float timer;//Default Timer

    int CurrentNode;

    static Vector3 CurrentPositionHolder; // Store Node Position
    static Quaternion CurrentRotationHolder; // Store Node Position
    static Vector3 startPosition;
    static Quaternion startRotate;




    // Use this for initialization
    void Start ()
    {
        PathNode = GetComponentsInChildren<Node> ();
        checkNode();
	}
    //Check Node and move to it
    void checkNode()
    {
        timer = 0f;
        startPosition = Player.transform.position;
        startRotate = Player.transform.rotation;
        CurrentPositionHolder = PathNode[CurrentNode].transform.position;
        CurrentRotationHolder = PathNode[CurrentNode].transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime * PathNode[CurrentNode].NodeSpeed;

        if (Player.transform.position != CurrentPositionHolder)
        {
            Player.transform.position = Vector3.Lerp(startPosition, CurrentPositionHolder, timer);
            Player.transform.rotation = Quaternion.Lerp(startRotate, CurrentRotationHolder, timer);
        }
        else
        {
            if (CurrentNode < PathNode.Length - 1)
            {
                CurrentNode++;
                checkNode();
            }
        }
	}
}
