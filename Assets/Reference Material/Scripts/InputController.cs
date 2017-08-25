using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour {

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public bool gripButtonDown = false;
    public bool gripButtonUp = false;
    public bool gripButtonPressed = false; //Holding

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonDown = false;
    public bool triggerButtonUp = false;
    public bool triggerButtonPressed = false; //Holding

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private GameObject pickup; //Change from GameObject to Weapon?

    public GameObject heldObject;
    public GameObject highlightedObject;
    public GameObject model;

	// Use this for initialization
	void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (controller == null) {
            Debug.Log("Controller not found!");
            return;
        }

        gripButtonDown = controller.GetPressDown(gripButton);
        gripButtonUp = controller.GetPressUp(gripButton);
        gripButtonPressed = controller.GetPress(gripButton);

        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);

        /* if (gripButtonDown && pickup != null)
         {
             pickup.transform.parent = this.transform;
             //pickup.GetComponent<Rigidbody>().isKinematic = true;
         }*/
        if (gripButtonUp)
        {
            Debug.Log("Starting Game!");
            SpawnEnemies controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnEnemies>();
            if (controller.gameStarted == false)
            {
                controller.spawnMore = true;
                controller.gameStarted = true;
                controller.score = 0;
                //SceneManager.LoadScene("EndlessMode", LoadSceneMode.Additive);
            }
        }

        if (triggerButtonUp)
        {
            //Debug.Log("Throwing!");
            if (heldObject != null)
            {
                model.SetActive(true);
                heldObject.transform.parent = null;
                heldObject.gameObject.GetComponent<Collider>().enabled = true;
                heldObject = null;
            }
            //if sword is over sheath then sheathe it
        }
        
        if (triggerButtonDown)
        {
            if (highlightedObject != null && highlightedObject.GetComponent<Ground>().grabbable)
            {
                model.SetActive(false);
                highlightedObject.transform.parent = this.transform;
                highlightedObject.gameObject.GetComponent<Collider>().enabled = false;
                heldObject = highlightedObject;
            }
        }

        /*if (triggerButtonDown)
            Debug.Log("Trigger pressed");
        if (triggerButtonDown)
            Debug.Log("Trigger released");*/
    }

    private void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("TriggerEnter!");
        var ground = collider.GetComponent<Ground>();
        if (ground != null && ground.grabbable)
            highlightedObject = collider.gameObject;
    }

    private void OnTriggerExit(Collider collider)
    {
        //Debug.Log("TriggerExit!");
        var ground = collider.GetComponent<Ground>();
        if (ground != null && ground.grabbable)
            highlightedObject = null;
    }

    public void catchArrow(GameObject arrow)
    {
        //Debug.Log("Caught!");
        arrow.transform.position = this.transform.position;
        arrow.transform.rotation = this.transform.rotation;
        arrow.transform.parent = this.transform;
        heldObject = arrow;
    }

    public void highlightObject(GameObject obj)
    {
        highlightedObject = obj;
        //TODO - add a visible highlight to the object
    }
}
