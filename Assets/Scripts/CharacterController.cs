using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float moveSpeed, jumpPower, floorDist;
    private Rigidbody rb;
    //private bool isGrounded;
    
    // Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        bool isGrounded = false;
        Vector3 targetPosition = transform.position + Vector3.down * floorDist;
        RaycastHit hitInfo;
        if (Physics.Linecast(transform.position, targetPosition, out hitInfo) && hitInfo.collider.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        if (Input.GetButton("Up"))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (Input.GetButton("Down"))
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        if (Input.GetButton("Left"))
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        if (Input.GetButton("Right"))
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        if (Input.GetButton("Jump") && isGrounded)
            rb.velocity = new Vector3(0, jumpPower, 0);
    }


}
