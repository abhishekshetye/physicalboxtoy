using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody rigidbody;

	public float m_Speed;
	public float m_Limit;

	public bool isGoingForward;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        isGoingForward = true;
    }

    // Update is called once per frame
    void Update()
    {
    	if (isGoingForward) {
	    	//transform.Translate(Vector3.forward * -Time.deltaTime * m_Speed);
	    	if (rigidbody.velocity.magnitude <= m_Limit)
		    	rigidbody.AddForce(0, 0, -m_Speed);
    	}
	    else {
	    	//transform.Translate(-Time.deltaTime * m_Speed , 0, 0);
	    	if (rigidbody.velocity.magnitude <= m_Limit)
		    	rigidbody.AddForce(-m_Speed, 0, 0);
	    }

	    Debug.Log ("Velocity is " + rigidbody.velocity.magnitude);

    	 if (Input.GetMouseButtonDown(0)) {
    		isGoingForward = !isGoingForward;
    		//Debug.Log("isGoingForward is " + isGoingForward);
    	 }
    }
}
