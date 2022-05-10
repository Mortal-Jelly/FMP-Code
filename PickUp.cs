using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float grabRange = 5;
    public Transform holdParent;
    private GameObject heldObject;
    public float moveForce = 250;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            if(heldObject == null)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.TransfromDirection(Vector3.forward), out hit, grabRange))
                {
                    PickupObj(hit.transform.gameObject);
                }
            }
            else{
                DropObject();
            }
        }

        if(heldObject != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if(Vector3.Distance(heldObject.transfrom.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (heldParent.position - heldObject.transform.position);
            heldObject.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);

        }
    }

    void PickupObj(GameObject pickObject)
    {
        if(pickObject.GetComponent<Rigidbody>())
        {
            Rigidbody objectRig = pickObject.GetComponent<Rigidbody>();
            objectRig.useGravity= false;
            objectRig.drag = 10;

            objectRig.transform.parent = holdParent;
            heldObject = pickObject;
        }
    }

    void DropObject()
    {
        Rigidbody heldRig = heldObject.GetComponent<Rigidbody>();
        heldRig.useGravity = true;
        heldRig.drag = 1;
        heldObject.transform.parent = null;
        heldObject = null;
    }
}
