using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    
   public void EndGame()
   {
       if (gameHasEnded == false)
       {
           gameHasEnded = true;
           Debug.Log("Game Over");
           SceneManager.LoadScene("gameover");
       }
       
   }

   public void WinGame()
   {
       if (gameHasEnded == false)
       {
           gameHasEnded = true;
           Debug.Log("Win");
           SceneManager.LoadScene("winscreen");
       }
   }
}
