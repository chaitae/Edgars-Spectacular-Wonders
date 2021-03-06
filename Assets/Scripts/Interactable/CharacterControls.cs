using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    public GameObject holdGameObjectPosition;
    public GameObject equippedObject;
    [SerializeField]
    private float raycastDistance = 2f;
    public Animator animator;
    public delegate void GeneralFunction();
    public static GeneralFunction OnNearInteractable;
    public static GeneralFunction OnLeaveInteractable;
    private CharacterController controller;
    public IInteractable interactableObj;
    public bool canUseObject = true;
    bool canMove = true;
    private Vector3 playerVelocity;

    [Range(2.0f, 5.0f)]
    public float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;
    float frontOffset = 2f;

    float raycastRatio = .5f;
    public bool flipDirection = false;
    public bool thingInFrontOfPlayer = false;


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
        if (canMove == false)
        {
            animator.SetBool("isRunning", false);
        }
    }

    void FixedUpdate()
    {

        RaycastHit hit;
        bool rayHit = false;
        thingInFrontOfPlayer = false;
        // Does the ray intersect any objects excluding the player layer
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(transform.position - transform.lossyScale.y * i / 3 * Vector3.up, transform.forward, out hit, raycastDistance))
            {
                if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Ground"))
                    thingInFrontOfPlayer = true;
                rayHit = true;
                Debug.DrawRay(transform.position - transform.lossyScale.y * i / 3 * Vector3.up, transform.forward * hit.distance, Color.yellow);
                IInteractable tempInteractableObj = hit.collider.GetComponent<IInteractable>();
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
//                        interactableObj.CharacterExit(this);

                    }
                    else
                    {
                        interactableObj.CharacterStay(this);
                        // interactableObj
                    }
                    if (OnNearInteractable != null) OnNearInteractable();
                    break;
                }
            }
        }
        if (rayHit == false)
        {

            if (OnLeaveInteractable != null) OnLeaveInteractable();
            if (interactableObj != null)
                interactableObj.CharacterExit(this);
            interactableObj = null;
        }
    }
    Vector3 ReturnGroundInFront(GameObject gameObject)
    {
        Vector3 temp = this.transform.position + transform.forward;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Collider col = equippedObject.gameObject.GetComponent<Collider>();
        if (Physics.Raycast(transform.position + transform.forward * frontOffset + Vector3.up * 1, -transform.up, out hit))
        {

            Debug.DrawRay(transform.position + transform.forward * frontOffset, -transform.up * hit.distance, Color.yellow);
            temp = new Vector3(hit.point.x, hit.point.y + col.bounds.size.y / 2, hit.point.z);
        }
        return temp;
    }
    public bool CanDrop()
    {
        if (thingInFrontOfPlayer)
        {
            return false;
        }
        return true;
    }
    public void ConsumeItem()
    {
        UIManager._instance.HideSpecialInteraction();
        equippedObject.transform.parent = null;
        equippedObject.transform.position = ReturnGroundInFront(equippedObject);
        if (equippedObject.GetComponent<Rigidbody>() != null)
            equippedObject.GetComponent<Rigidbody>().isKinematic = false;
        equippedObject.SetActive(false);
        equippedObject = null;
    }
    //Used for pedastle
    public void Drop()
    {
        if (!thingInFrontOfPlayer)
        {
            equippedObject.transform.parent = null;
            equippedObject.transform.position = ReturnGroundInFront(equippedObject);
            if (equippedObject.GetComponent<Rigidbody>() != null)
                equippedObject.GetComponent<Rigidbody>().isKinematic = false;
            equippedObject = null;
        }
    }
    //this goes over players head
    public void PickUp(GameObject gameObject)
    {
        canUseObject = false;
        equippedObject = gameObject;
        equippedObject.transform.parent = holdGameObjectPosition.transform;
        equippedObject.transform.localPosition = Vector3.zero;
        if (equippedObject.GetComponent<SpecialNPC>() != null) equippedObject.GetComponent<SpecialNPC>().HideSpecial();
        Debug.Log("Picked up:" + equippedObject);
        StartCoroutine("PickUpHelper");
    }
    void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (flipDirection)
        {
            move = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
        }
        controller.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            animator.SetBool("isRunning", true);
        }
        else
        {

            animator.SetBool("isRunning", false);
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
                Debug.Log("calling interactableobj interact");
                interactableObj.Interact(this, KeyCode.E);
            }
            else if (Input.GetKeyDown(KeyCode.T) && interactableObj != null)
            {
                interactableObj.Interact(this, KeyCode.T);
            }

            if (equippedObject != null && Input.GetKeyDown(KeyCode.E) && canUseObject)
            {
                equippedObject.GetComponent<IInteractable>().EquippedAction(this);
            }
            if (equippedObject != null)
            {

                Vector3 temp = this.transform.position + transform.forward;

                Collider col = equippedObject.gameObject.GetComponent<Collider>();
                RaycastHit hit;

                if (Physics.Raycast(transform.position + transform.forward * frontOffset + Vector3.up * 1, -transform.up, out hit))
                {

                    Debug.DrawRay(transform.position + transform.forward * frontOffset + Vector3.up * 1, -transform.up * hit.distance, Color.yellow);
                    Debug.Log(hit.transform.gameObject.name);
                    // temp = hit.point + Vector3.up * col.bounds.min.y;
                }
            }
        }


    }
}