using UnityEngine;
using System.Collections;

/// <summary>
/// Should be put onto an object with a collider, which is set to be a trigger.
/// As long as the target GameObject is in the area, a message displays.
/// When the target GameObject enters this trigger, the given objects spawn.
/// </summary>
public class TriggerArea : ShowMessage {

	public GameObject waitingFor;

    [System.Serializable]
    public class TriggeredObject {
    	public GameObject whatToCreate;
    	public Transform whereToCreate;
    	public bool onlyCreateOnce = true;
        public void Create()
        {
            if(whatToCreate != null)
            {
                GameObject go = (GameObject)Instantiate(whatToCreate);
                if(whereToCreate != null) {
                    go.transform.position = whereToCreate.transform.position;
					go.transform.rotation = whereToCreate.transform.rotation;
                }
                if(onlyCreateOnce) {
                    whatToCreate = null;
                }
            }
        }
    }
    public TriggeredObject[] triggeredObjects;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject == waitingFor) {
			showMessage = true;
            if(triggeredObjects != null)
            {
                for(int i = 0; i < triggeredObjects.Length; ++i)
                {
                    triggeredObjects[i].Create();
                }
            }
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == waitingFor) {
			showMessage = false;
		}
	}

	void Start()
	{
		if(collider == null || !collider.isTrigger)
			print (this+" needs a trigger collider!");
	}
}
