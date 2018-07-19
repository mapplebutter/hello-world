namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class InteractableObj : VRTK_InteractableObject
    {

        [HideInInspector]
        public bool hasSecondGrab = false;
        public bool isDropped = false; //used to check if the utensil has dropped on the ground
        public bool isTouchingTable = false; //for serving cloth, dinner plate and tray only - used to check if has touched the table
        public AudioSource DingSound;

        Coroutine chairMove;
        public IEnumerator ChairMoved(Vector3 origPos, float offset)
        {
            while ((transform.position- origPos).magnitude < offset)
            {
                yield return new WaitForSeconds(Time.deltaTime / 4);
            }

            if (!hasSecondGrab)
            {
                ForceReleaseGrab();
                yield break;
            }
        }

        void OnColliderEnter(Collider col)
        {
            DingSound = GameObject.Find("SpoonDing").GetComponent<AudioSource>();
            if (this.gameObject.tag == "Utensils")
            {
                if (col.gameObject.tag == "DinnerKnife" || col.gameObject.tag == "DinnerFork" || col.gameObject.tag == "ButterKnife" || col.gameObject.tag == "SidePlate" || col.gameObject.tag == "WineGlass")
                {
                    DingSound.Play();
                }
            }
        }

        protected override void PrimaryControllerGrab(GameObject currentGrabbingObject)
        {
            base.PrimaryControllerGrab(currentGrabbingObject);
            if (gameObject.layer == (int)ObjectLayer.Heavy)
            {
                PlayerActionSound.instance.GrabChair();
                StartCoroutine(ChairMoved(transform.position, 0.4f));
            }
            else
                PlayerActionSound.instance.GrabDefault();

            GameManager.instance.StartPrepTimer();
        }

        protected override void PrimaryControllerUngrab(GameObject previousGrabbingObject, GameObject previousSecondaryGrabbingObject)
        {
            /* ---Calling base.PrimaryControllerUngrab(previousGrabbingObject, previousSecondaryGrabbingObject); ---*/

            UnpauseCollisions();
            RemoveTrackPoint();
            ResetUseState(previousGrabbingObject);
            grabbingObjects.Clear();
            if (secondaryGrabActionScript != null && previousSecondaryGrabbingObject != null)
            {
                secondaryGrabActionScript.OnDropAction();
                previousSecondaryGrabbingObject.GetComponent<VRTK_InteractGrab>().ForceRelease();
            }
            else
            {
                /*not part of base function*/
                if (gameObject.layer == (int)ObjectLayer.Tray)
                {
                    //Unsnaps all objects on the tray if the tray is ungrabbed//
                    gameObject.GetComponent<TiltChecker>().UnsnapAll();
                }
                /*not part of base function*/
            }
            LoadPreviousState();

            /*----------------------------------------End base function call---------------------------------------*/

            if (gameObject.layer == (int)ObjectLayer.Ungrabbable || gameObject.layer == (int)ObjectLayer.Tray)
            {
                gameObject.GetComponent<SphereCollider>().enabled = true;
            }
        }

        public void SecondGrabbed()
        {
            hasSecondGrab = true;
        }
        
        public void ResetSecondGrab()
        {
            hasSecondGrab = false;
        }

        protected override void OnTeleporting(object sender, DestinationMarkerEventArgs e)
        {
            base.OnTeleporting(sender, e);          
        }

        protected override void OnTeleported(object sender, DestinationMarkerEventArgs e)
        {
            if (grabAttachMechanicScript != null && grabAttachMechanicScript.IsTracked() && stayGrabbedOnTeleport && trackPoint != null)
            {
                GameObject actualController = VRTK_DeviceFinder.GetActualController(GetGrabbingObject());
                transform.position = (actualController ? actualController.transform.position : transform.position);            
            }
        }
    }
}
