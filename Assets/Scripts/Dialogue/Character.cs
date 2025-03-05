using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
   public float speed = 0;
   public CharacterController controller;

   public Transform orientation;

   private float xRotation;
   private float zRotation;

   private void Start()
   {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
   }

   private void Update()
   {
      float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
      float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

      controller.Move(new Vector3(horizontal, 0, vertical));
   }
}


