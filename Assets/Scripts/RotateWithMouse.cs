using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour {

    public float speed = 100;
    
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
        if (Cursor.lockState == CursorLockMode.Locked)
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
    }
}
