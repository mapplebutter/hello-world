using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[System.Serializable]
public class SnapDropZoneOverride : VRTK_SnapDropZone
{

    public List<GameObject> thisStack = new List<GameObject>();
    private string objectTag = "";

    public void DisableUtensilCol()
    {
        GameObject utensil = GetCurrentSnappedObject();
        foreach (Collider col in utensil.GetComponentsInChildren<Collider>())
        {
            col.isTrigger = true;
        }
    }

    public void ResetUtensilCol()
    {
        GameObject utensil = GetCurrentSnappedObject();
        foreach (Collider col in utensil.GetComponentsInChildren<Collider>())
        {
            col.isTrigger = false;
        }
    }

    protected override void SnapObject(Collider collider)
    {
        if (collider.gameObject.tag == "Utensils")
        {
            VRTK_InteractableObject ioCheck = ValidSnapObject(collider.gameObject, false);

            //highlightObjectPrefab = collider.gameObject;

            if (thisStack.Count == 0)
            {
                objectTag = collider.gameObject.transform.tag.ToString();
            }

            if (thisStack.Count > 1)
            {
                if (thisStack[thisStack.Count - 2] != null)
                {
                    thisStack[thisStack.Count - 2].SetActive(false);
                }
            }

            //If the item is in a snappable position and this drop zone isn't snapped and the collider is a valid interactable object
            if (willSnap && !isSnapped && ioCheck != null && thisStack.Count < 4 && collider.gameObject.tag.ToString() == objectTag)
            {
                if (collider.gameObject.transform.Find("UtensilType") != null)
                {
                   
                        //To check if correct utensil was taken for scoring later
                        if (collider.gameObject.transform.Find("UtensilType").tag == "DinnerKnife")
                        {
                            CorrectUtensilChecker.instance.DinnerKnife = true;
                        }
                        if (collider.gameObject.transform.Find("UtensilType").tag == "DinnerFork")
                        {
                            CorrectUtensilChecker.instance.DinnerFork = true;
                        }
                        if (collider.gameObject.transform.Find("UtensilType").tag == "ButterKnife")
                        {
                            CorrectUtensilChecker.instance.ButterKnife = true;
                        }

                        //Only snap it to the drop zone if it's not already in a drop zone
                        if (!ioCheck.IsInSnapDropZone())
                        {
                            if (highlightObject != null)
                            {
                                //Turn off the drop zone highlighter
                                highlightObject.SetActive(false);
                            }

                            //collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                            Vector3 newLocalScale = GetNewLocalScale(ioCheck);
                            if (transitionInPlace != null)
                            {
                                StopCoroutine(transitionInPlace);
                            }

                            isSnapped = true;
                            currentSnappedObject = ioCheck.gameObject;
                            thisStack.Add(ioCheck.gameObject);

                            //
                            if (collider.gameObject.name == "AppForkObj(Clone)" || collider.gameObject.name == "DinnerFork Edited(Clone)")
                            {
                                collider.gameObject.GetComponent<ForkSnap>().Snapped();
                            }
                            //

                            if (cloneNewOnUnsnap)
                            {
                                CreatePermanentClone();
                            }
                            DisableUtensilCol();

                            transitionInPlace = StartCoroutine(UpdateTransformDimensions(ioCheck, highlightContainer, newLocalScale, snapDuration));

                            ioCheck.ToggleSnapDropZone(this, true);
                            isSnapped = false;
                        }
                    
                }
                else
                { //Only snap it to the drop zone if it's not already in a drop zone
                    if (!ioCheck.IsInSnapDropZone())
                    {
                        if (highlightObject != null)
                        {
                            //Turn off the drop zone highlighter
                            highlightObject.SetActive(false);
                        }

                        collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        Vector3 newLocalScale = GetNewLocalScale(ioCheck);
                        if (transitionInPlace != null)
                        {
                            StopCoroutine(transitionInPlace);
                        }

                        isSnapped = true;
                        currentSnappedObject = ioCheck.gameObject;
                        thisStack.Add(ioCheck.gameObject);

                        //
                        if (collider.gameObject.name == "AppForkObj(Clone)" || collider.gameObject.name == "DinnerFork Edited(Clone)")
                        {
                            collider.gameObject.GetComponent<ForkSnap>().Snapped();
                        }
                        //

                        if (cloneNewOnUnsnap)
                        {
                            CreatePermanentClone();
                        }
                        DisableUtensilCol();

                        transitionInPlace = StartCoroutine(UpdateTransformDimensions(ioCheck, highlightContainer, newLocalScale, snapDuration));

                        ioCheck.ToggleSnapDropZone(this, true);
                        isSnapped = false;
                    }
                }
            }

            //Force reset isSnapped if the item is grabbed but isSnapped is still true
            isSnapped = (isSnapped && ioCheck && ioCheck.IsGrabbed() ? false : isSnapped);
            wasSnapped = false;
        }
    }

    protected override void UnsnapObject()
    {
        //
        if (currentSnappedObject.name == "AppForkObj(Clone)" || currentSnappedObject.name == "DinnerFork Edited(Clone)")
        {
            currentSnappedObject.GetComponent<ForkSnap>().Unsnapped();
        }
        //
        ResetPermanentCloneColliders(currentSnappedObject);
        ResetUtensilCol();

        thisStack.RemoveAt(thisStack.Count - 1);
        if (thisStack.Count > 0)
        {
            thisStack[thisStack.Count - 1].SetActive(true);
        }

        isSnapped = false;
        wasSnapped = true;

        if (thisStack.Count > 0)
        {
            currentSnappedObject = thisStack[thisStack.Count - 1].gameObject;
        }

        if (thisStack.Count == 0)
        {
            objectTag = "";
        }

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
