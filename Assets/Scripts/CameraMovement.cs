using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public GameObject player;
	public float speed;

	public float offsetZ;
	public float offsetX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	var forw = transform.forward;
 
		//get forward in world coords
		forw = transform.InverseTransformDirection(forw);
		 
		//remove the y component (up down rotation)
		forw.y = 0;
		forw.Normalize();
		 
		forw.x = player.transform.position.x + offsetX;
		forw.z = player.transform.position.z + offsetZ;
		forw.y = 10.46f;

		//now append to global position, because forw is a unit vector, multiplying by translateZ makes the vector length = to translateZ
		transform.position = forw; // * speed * -1;
        
    }
}
