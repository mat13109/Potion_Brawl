using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ScoreManager 
{


    public static void ResetScores()
    {
        PlayerPrefs.SetInt("P1", 3);
        PlayerPrefs.SetInt("P2", 3);
    }

    public static void RemoveOneLifeTo(int p)
    {
        switch (p)
        {
            case 1:
                PlayerPrefs.SetInt("P1", PlayerPrefs.GetInt("P1")-1);
                break;
            case 2:
                PlayerPrefs.SetInt("P2", PlayerPrefs.GetInt("P2") - 1);
                break;
            default:
                Debug.Log("Wrong use of ScoreManager.RemoveLifeTo()");
                break;
        }
    }
}
