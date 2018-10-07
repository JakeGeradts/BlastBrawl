using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int P1_Score;
    public int P2_Score;
    public int P3_Score;
    public int P4_Score;

    public int Score_Modifier = 1;
    public int SuicidePenalties = 2;
    public int FraggedPenalties = 1;
    public int WinScore = 5;


    PreGameSetup playerCount;

    // Use this for initialization
    void Start ()
    {
        playerCount = GetComponent<PreGameSetup>();
    }
	
    //Update the Score. First to get to the score limit wins
    public void UpdateScore(int PlayerNum,int Multiplier)
    {
        switch(PlayerNum)
        {
            case 1:
                {
                    P1_Score += 1*(Multiplier);
                    break;
                }
            case 2:
                {
                    P2_Score += 1 * (Multiplier);
                    break;
                }
            case 3:
                {
                    P3_Score += 1 * (Multiplier);
                    break;
                }
            case 4:
                {
                    P4_Score += 1 * (Multiplier);
                    break;
                }
            default:
                {
                    break;
                }
        }

        if (P1_Score >= WinScore)
        {

        }
        else if (P2_Score >= WinScore)
        {

        }
        else if (P3_Score >= WinScore)
        {

        }
        else if (P4_Score >= WinScore)
        {

        }
        else
        {
            print("No winner");
        }
    }
}
