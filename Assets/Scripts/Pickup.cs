using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
   public Rigidbody rb;
   public SphereCollider collider;
   public Transform player, Container, mainCamera;

   public float pickUpRange;
   public float dropForwardForce, dropUpwardForce;

   public bool equipped;
   public static bool slotFull;

   private void Start()
   {
       if (!equipped)
       {
           rb.isKinematic = false;
           collider.isTrigger = false;
       }
       if (equipped)
       {
           rb.isKinematic = true;
           collider.isTrigger = true;
           slotFull = true;
       }
   }

   private void Update()
   {
       Vector3 distanceToPlayer = player.position - transform.position;
       if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

       if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
   }

   private void PickUp()
   {
       equipped = true;
       slotFull = true;

       transform.SetParent(Container);
       transform.localPosition = Vector3.zero;
       transform.localRotation = Quaternion.Euler(Vector3.zero);
       transform.localScale = Vector3.one;


       rb.isKinematic = true;
       collider.isTrigger = true;
   }

   private void Drop()
   {
        equipped = false;
       slotFull = false;

       transform.SetParent(null);
       rb.velocity = player.GetComponent<Rigidbody>().velocity;

       rb.AddForce(mainCamera.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(mainCamera.up * dropUpwardForce, ForceMode.Impulse);


       rb.isKinematic = false;
       collider.isTrigger = false;
   }
}
