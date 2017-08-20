using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public void EndGame()
    {
        //Quit the Game
        Application.Quit();
        Debug.Log("End Game if in a build");
    }
}
