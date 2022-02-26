using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
     public int maxHealth = 3;
     public int currentHealth;

     public PlayerMovement playerMovement;

     
    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = maxHealth;
    }

    void Update()
    {
       
    }

   public void HurtPlayer(int damage)
   {
       currentHealth -= damage;
   }
}
