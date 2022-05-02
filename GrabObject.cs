using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GrabObject : MonoBehaviour
{
    // Start is called before the first frame update

    public SteamVR_Input_Sources hands;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean grab;

    private GameObject collideObject;
    private GameObject holding;

    private void SetCollidingObject(Collider col)
    {
        if (collideObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        collideObject = col.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(grab.GetLastStateDown(hands))
        {
            if(collideObject)
            {
                GrabObjects();
            }
        }   
        if(grab.GetLastStateUp(hands))
        {
            if(holding)
            {
                ReleaseObject();
            }
        }
    }

    public void OnTriggerEnter(Collider other) 
    {
        SetCollidingObject(other);
        //print("impact");
    }

    public void OnTriggerStay(Collider other) 
    {
        SetCollidingObject(other);
        //print("holding");

        
    }

    public void OnTriggerExit(Collider other) 
    {
        if(!collideObject)
        {
            return;
        }
        collideObject = null;
    }

    private void GrabObjects()
    {
        holding = collideObject;
        
        collideObject = null;
        var joint = AddFixedJoint();
        joint.connectedBody = holding.GetComponent<Rigidbody>();
        print(holding);
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            holding.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            holding.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
        }
        holding = null;
    }

}
