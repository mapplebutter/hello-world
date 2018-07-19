using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ObjectBreaking : MonoBehaviour
{
    //One for spawning, the other for referencing the already spawned prefab
    public GameObject brokenVersionPrefab;
    public GameObject originalObj;
    [HideInInspector]
    public GameObject spawnedPrefab;
    private VRTK_ControllerReference controllerReference;
    private float collisionForce = 0.0f;
    private float maxCollisionForce = 6.0f;

    private GameObject brokenObj = null;
    private List<BrokenObjChildInfo> brokenObjChildInfoList;
    private Vector3 brokenObjOrigPos;
    private Quaternion brokenObjOrigRot;


    public Vector3 vel;
    public float ObjectSpeed;
    private static int brokenCount = 0;

    //Timer to decide when to remove the broken plate
    bool destroySwitch = false;
    bool initialized = false;
    float destroyTimer = 0.0f;
    Rigidbody rb;

    public AudioSource glassBreak;

    public enum TablewareType
    {
        DinnerPlate,
        Sideplate,
        Glass
    }

    public TablewareType type;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        brokenObjChildInfoList = new List<BrokenObjChildInfo>();
        glassBreak = GameObject.Find("GlassBreak").GetComponent<AudioSource>();
        initialized = true;
    }

    void Update()
    {
        ObjectSpeed = rb.velocity.magnitude;

        if (destroySwitch == true)
        {
            destroyTimer += Time.deltaTime;
        }
        if (destroyTimer > 1.5f)
        {
            ResetBrokenObj();
            destroyTimer = 0.0f;
            destroySwitch = false;

        }
    }

    protected void ResetBrokenObj()
    {
        brokenObj.SetActive(false);
        brokenObj.transform.position = brokenObjOrigPos;
        brokenObj.transform.rotation = brokenObjOrigRot;
        for (int i = 0; i < brokenObj.transform.childCount; i++)
        {
            GameObject temp = brokenObj.transform.GetChild(i).gameObject;
            temp.GetComponent<Rigidbody>().isKinematic = true;
            temp.GetComponent<Rigidbody>().useGravity = false;
            temp.transform.position = brokenObjChildInfoList[i].ObjPos;
            temp.transform.rotation = brokenObjChildInfoList[i].ObjRot;
        }

        brokenObj = null;
        brokenObjOrigPos = new Vector3();
        brokenObjOrigRot = new Quaternion();
        brokenObjChildInfoList.Clear();

    }

    void OnCollisionEnter(Collision col)
    {
        if (initialized)
        {
            if ((rb.velocity.magnitude >= 1.8f) || (col.gameObject.layer == (int)ObjectLayer.Room && rb.velocity.magnitude >= 1.0f))
            {
                Break();
            }

            controllerReference = VRTK_ControllerReference.GetControllerReference(gameObject.GetComponent<VRTK_InteractableObject>().GetGrabbingObject());
            if (VRTK_ControllerReference.IsValid(controllerReference) && gameObject.GetComponent<VRTK_InteractableObject>().IsGrabbed())
            {
                collisionForce = VRTK_DeviceFinder.GetControllerVelocity(controllerReference).magnitude;
                if (collisionForce >= 2.0f)
                {
                    float hapticStrength = collisionForce / maxCollisionForce;
                    VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, hapticStrength, 0.5f, 0.01f);
                    Break();
                }
            }
        }
    }

    void Break()
    {
        if (!destroySwitch)
        {
            glassBreak.Play();
            ScoreCheck.instance.Deductscore(10);
            if (gameObject.tag == "Plate")
            {
                gameObject.GetComponent<TiltChecker>().UnsnapAll();
            }
            destroySwitch = true;
            originalObj.SetActive(false);
            brokenObj = PreloadObjects.instance.GetPooledObj(type);
            StoreBrokenObjInfo(brokenObj);
            for (int i = 0; i < brokenObj.transform.childCount; i++)
            {
                brokenObj.transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
                brokenObj.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
            }
            brokenObj.transform.position = originalObj.transform.position;
            brokenObj.transform.rotation = originalObj.transform.rotation;
            brokenObj.SetActive(true);
            this.gameObject.GetComponent<Collider>().isTrigger = false;

            if (gameObject.tag != "Plate")
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.angularVelocity = new Vector3(0, 0, 0);
                gameObject.GetComponent<Collider>().isTrigger = true;
                gameObject.transform.position = GameManager.instance.objectRespawnZones[brokenCount].position;
                gameObject.transform.rotation = GameManager.instance.objectRespawnZones[brokenCount].rotation;
                brokenCount++;
                //originalObj.SetActive(true);

                if (brokenCount == 4)
                {
                    brokenCount = 0;
                }
            }
            else
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.angularVelocity = new Vector3(0, 0, 0);
                gameObject.GetComponent<Collider>().isTrigger = true;
                gameObject.transform.position = GameManager.instance.objectRespawnZones[5].position;
                gameObject.transform.rotation = GameManager.instance.objectRespawnZones[5].rotation;
                //originalObj.SetActive(true);
            }

        }
    }

    protected void StoreBrokenObjInfo(GameObject brokenObject)
    {
        brokenObjOrigPos = brokenObject.transform.position;
        brokenObjOrigRot = brokenObject.transform.rotation;
        for (int i = 0; i < brokenObject.transform.childCount; i++)
        {
            GameObject temp = brokenObject.transform.GetChild(i).gameObject;
            brokenObjChildInfoList.Add(new BrokenObjChildInfo(temp.transform.position, temp.transform.rotation));
        }
    }

    public struct BrokenObjChildInfo
    {
        public Vector3 ObjPos;
        public Quaternion ObjRot;

        public BrokenObjChildInfo(Vector3 objPos, Quaternion objRot)
        {
            this.ObjPos = objPos;
            this.ObjRot = objRot;
        }
    }
}


