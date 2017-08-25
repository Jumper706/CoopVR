using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject target;
    public float moveSpeed;
    public float rotationalOffset;
    public string layerToHit;
    public AudioClip splat;
    public float vanishDistance;

	// Use this for initialization
	void Start ()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera");
        transform.LookAt(target.transform);
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.x = transform.rotation.eulerAngles.x + rotationalOffset;
        transform.rotation = Quaternion.Euler(rotationVector);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (Mathf.Abs(transform.position.x) > vanishDistance || Mathf.Abs(transform.position.z) > vanishDistance)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer(layerToHit))
        {
            if (layerToHit.Equals("Enemy"))
            {
                Destroy(col.gameObject);
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(splat, transform.position);
                SpawnEnemies controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnEnemies>();
                controller.setScore(1);
            }
            else
            {
                InputController hand = col.gameObject.GetComponent<InputController>();
                if (hand.heldObject == null)
                {
                    hand.catchArrow(gameObject);
                    moveSpeed = 0;
                    layerToHit = "Enemy";
                    this.gameObject.GetComponent<Collider>().enabled = false;
                }
            }
        }
        else if (col.gameObject.layer != LayerMask.NameToLayer("Missile") && col.gameObject.layer != LayerMask.NameToLayer("Hand")
                 && col.gameObject.layer != LayerMask.NameToLayer("Body"))
            Destroy(gameObject);
        
    }
}
