namespace VRTK.SecondaryControllerGrabActions
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class HeavySecondAction : VRTK_BaseGrabAction
    {

        public float ungrabDist = 1.0f;

        public override void Initialise(VRTK_InteractableObject currentGrabbdObject, VRTK_InteractGrab currentPrimaryGrabbingObject, VRTK_InteractGrab currentSecondaryGrabbingObject, Transform primaryGrabPoint, Transform secondaryGrabPoint)
        {
            base.Initialise(currentGrabbdObject, currentPrimaryGrabbingObject, currentSecondaryGrabbingObject, primaryGrabPoint, secondaryGrabPoint);
            grabbedObject.GetComponent<InteractableObj>().SecondGrabbed();
        }

        public override void ProcessUpdate()
        {
            base.ProcessUpdate();
            CheckForceStopCollision();
        }

        public override void ResetAction()
        {
            grabbedObject.ForceStopInteracting();
            grabbedObject.GetComponent<InteractableObj>().ResetSecondGrab();
            base.ResetAction();
        }

        protected void CheckForceStopCollision()
        {
            if (initialised)
            {
                Collider[] hitColliders = Physics.OverlapSphere(secondaryGrabbingObject.transform.position, 0.04f);
                foreach (Collider col in hitColliders)
                {
                    if (col.gameObject.layer == (int)ObjectLayer.Heavy)
                    {
                        return;
                    }
                }
                grabbedObject.ForceStopInteracting();
                grabbedObject.GetComponent<InteractableObj>().ResetSecondGrab();
            }
        }
    }
}
