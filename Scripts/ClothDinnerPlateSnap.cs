namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Highlighters;

    public class ClothDinnerPlateSnap : VRTK_SnapDropZone
    {
        public void DisableClothCol()
        {
            GameObject cloth = GetCurrentSnappedObject();
            cloth.GetComponent<Collider>().isTrigger = true;
        }

        public void ResetClothCol()
        {
            GameObject cloth = GetCurrentSnappedObject();
            cloth.GetComponent<Collider>().isTrigger = false;
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
                    DisableClothCol();

                    transitionInPlace = StartCoroutine(UpdateTransformDimensions(ioCheck, highlightContainer, newLocalScale, snapDuration));

                    ioCheck.ToggleSnapDropZone(this, true);
                    transform.parent.GetComponent<ProceduralSnap>().UpdateSnapCount();
                    //ioCheck.GetComponent<ClothUtensilCounter>().isSnapped = true;
                }
            }

            //Force reset isSnapped if the item is grabbed but isSnapped is still true
            isSnapped = (isSnapped && ioCheck && ioCheck.IsGrabbed() ? false : isSnapped);
            wasSnapped = false;
        }

        protected override void UnsnapObject()
        {
            ResetPermanentCloneColliders(currentSnappedObject);
            //currentSnappedObject.GetComponent<ClothUtensilCounter>().isSnapped = false;
            transform.parent.GetComponent<ProceduralSnap>().UpdateUnsnapCount();
            ResetClothCol();

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
