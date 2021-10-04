﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    public GameObject holdGameObjectPosition;
    public GameObject equippedObject;
    private CharacterController controller;
    IInteractable interactableObj;
    public bool canUseObject = true;
    bool canMove = true;
    private Vector3 playerVelocity;

    [Range(2.0f, 5.0f)]
    public float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;

    int dontMoveCount = 0;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    IEnumerator PickUpHelper()
    {
        canUseObject = false;
        yield return new WaitForSeconds(.1f);
        canUseObject = true;
    }
    public void ToggleMovementFalseDelay()
    {
        StartCoroutine("PickUpHelper");
    }
    public void SetMovement(bool canMove, string classname)
    {
        this.canMove = canMove;
    }
    void FixedUpdate()
    {

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position - transform.lossyScale.y * .75f * Vector3.up, transform.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position - transform.lossyScale.y * .75f * Vector3.up, transform.forward * hit.distance, Color.yellow);
            IInteractable tempInteractableObj = hit.collider.GetComponent<IInteractable>();
            Debug.Log(hit.collider.transform.gameObject.name + "detected");
            if (tempInteractableObj != null)
            {
                if (interactableObj == null)
                {
                    interactableObj = tempInteractableObj;
                    interactableObj.CharacterEnter(this);
                }
                else if (tempInteractableObj != interactableObj)
                {
                    interactableObj = tempInteractableObj;
                    interactableObj.CharacterExit(this);

                }
            }
        }
        else
        {
            if (interactableObj != null)
                interactableObj.CharacterExit(this);
            interactableObj = null;
        }
    }
    public void Drop()
    {
        equippedObject.transform.parent = null;
        equippedObject.transform.position = transform.position + transform.forward;
        equippedObject.GetComponent<Rigidbody>().isKinematic = false;
        equippedObject = null;
    }
    //this goes over players head
    public void PickUp(GameObject gameObject)
    {
        canUseObject = false;
        equippedObject = gameObject;
        equippedObject.transform.parent = holdGameObjectPosition.transform;
        equippedObject.transform.localPosition = Vector3.zero;
        Debug.Log("Picked up:" + equippedObject);
        StartCoroutine("PickUpHelper");
    }
    void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    void Update()
    {
        if (canMove)
        {
            Move();

            if (Input.GetKeyDown(KeyCode.E) && interactableObj != null)
            {
                interactableObj.Interact(this);
            }

            if (equippedObject != null && Input.GetKeyDown(KeyCode.E) && canUseObject)
            {
                equippedObject.GetComponent<IInteractable>().EquippedAction(this);
            }
        }


    }
}