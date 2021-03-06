﻿namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Highlighters;

    public class NapkinSnap : VRTK_SnapDropZone
    {
        public void DisableNapkinCol()
        {
            GameObject napkin = GetCurrentSnappedObject();
            napkin.GetComponentInChildren<Collider>().isTrigger = true;
        }

        public void ResetNapkinCol()
        {
            GameObject napkin = GetCurrentSnappedObject();
            napkin.GetComponentInChildren<Collider>().isTrigger = false;
        }

        protected override void SnapObject(Collider collider)
        {
            VRTK_InteractableObject ioCheck = ValidSnapObject(collider.gameObject, false);
            //If the item is in a snappable position and this drop zone isn't snapped and the collider is a valid interactable object
            if (willSnap && !isSnapped && ioCheck != null)
            {
                //Only snap it to the drop zone if it's not already in a drop zone
                if (!ioCheck.IsInSnapDropZone())
                {
                    if (highlightObject != null)
                    {
                        //Turn off the drop zone highlighter
                        highlightObject.SetActive(false);
                    }

                    Vector3 newLocalScale = GetNewLocalScale(ioCheck);
                    if (transitionInPlace != null)
                    {
                        StopCoroutine(transitionInPlace);
                    }

                    isSnapped = true;
                    currentSnappedObject = ioCheck.gameObject;
                    if (cloneNewOnUnsnap)
                    {
                        CreatePermanentClone();
                    }
                    DisableNapkinCol();

                    transitionInPlace = StartCoroutine(UpdateTransformDimensions(ioCheck, highlightContainer, newLocalScale, snapDuration));

                    ioCheck.ToggleSnapDropZone(this, true);
                    transform.parent.GetComponent<ProceduralSnap>().UpdateSnapCount();
                }
            }

            //Force reset isSnapped if the item is grabbed but isSnapped is still true
            isSnapped = (isSnapped && ioCheck && ioCheck.IsGrabbed() ? false : isSnapped);
            wasSnapped = false;
        }

        protected override void UnsnapObject()
        {
            ResetPermanentCloneColliders(currentSnappedObject);
            transform.parent.GetComponent<ProceduralSnap>().UpdateUnsnapCount();
            ResetNapkinCol();

            isSnapped = false;
            wasSnapped = true;
            currentSnappedObject = null;
            ResetSnapDropZoneJoint();

            if (transitionInPlace != null)
            {
                StopCoroutine(transitionInPlace);
            }

            if (cloneNewOnUnsnap)
            {
                ResnapPermanentClone();
            }
        }
    }
}
