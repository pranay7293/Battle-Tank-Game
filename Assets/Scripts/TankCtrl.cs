using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCtrl : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Rigidbody playerrb;


    private void Start()
    {
        playerrb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        TankMovement();
    }

    private void TankMovement()
    {
        float vertical = joystick.Vertical;
        float horizontal = joystick.Horizontal;

        if (vertical > 0.2 ) 
        {
            Vector3 movement = transform.forward * vertical * speed * Time.deltaTime;
            playerrb.MovePosition(transform.position + movement);
        }
        if (horizontal > 0.5f)
        {
            Vector3 vector = new Vector3(0, horizontal * rotationSpeed, 0);
            Quaternion deltaRotation = Quaternion.Euler(vector * Time.deltaTime);
            playerrb.MoveRotation(playerrb.rotation * deltaRotation);
        }

    }
}
