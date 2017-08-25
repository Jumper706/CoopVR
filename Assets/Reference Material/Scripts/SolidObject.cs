using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidObject : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Hand"))
        {
            InputController hand = col.gameObject.GetComponent<InputController>();
            hand.highlightObject(gameObject);
        }

        //Debug.Log("Touch!");
    }

    //TODO - on trigger leave, unhighlight
}
