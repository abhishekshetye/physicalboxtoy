using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

	public float m_Speed;
	public float m_Limit;
	public bool isGoingForward;


	private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown, tapRequested, isDragging = false ;
	private Vector2 startTouch, swipeDelta;
	private Rigidbody rigidbody;

	private float fallMultiplier = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody> ();
        isGoingForward = true;
    }

    // Update is called once per frame
    void Update()
    {
    	JumpIfSwiped();

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

	    //Debug.Log ("Velocity is " + rigidbody.velocity.magnitude);
    }

    void JumpIfSwiped () {
         tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
 
         #region Standalone Inputs
         if (Input.GetMouseButtonDown(0))
         {
             tapRequested = true;
             isDragging = true;
             startTouch = Input.mousePosition;
         }
         else if (Input.GetMouseButtonUp(0))
         {
             if (tapRequested) { tap = true;}
             isDragging = false;
             Reset();
         }
         #endregion
 
         #region Mobile Inputs
         if(Input.touchCount > 0)
         {
             if(Input.touches[0].phase == TouchPhase.Began)
             {
                 tapRequested = true;
                 isDragging = true;
                 startTouch = Input.touches[0].position;
             }
             else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
             {
                 if (tapRequested) { tap = true; }
                 isDragging = false;
                 Reset();
             }
         }
         #endregion
 
         //Calculate the distance
         swipeDelta = Vector2.zero;
         if (isDragging)
         {
             if(Input.touchCount > 0) { swipeDelta = Input.touches[0].position - startTouch; }
             else if (Input.GetMouseButton(0)) { swipeDelta = (Vector2)Input.mousePosition - startTouch; }
         }
 
         //Did we cross the dead zone?
         if(swipeDelta.magnitude > 100)
         {
             tapRequested = false;
             //Which direction are we swiping?
             float x = swipeDelta.x;
             float y = swipeDelta.y;
             if(Mathf.Abs(x) > Mathf.Abs(y))
             {
                 //Left or right?
                 if (x > 0) { swipeRight = true; }
                 else { swipeLeft = true; }
                 //x > 0 ? swipeRight = true : swipeLeft = true;
             }
             else
             {
                 //Up or down?
                 if (y > 0) { swipeUp = true; }
                 else { swipeDown = true; }
                 // y > 0 ? swipeUp = true : swipeDown = true;
             }
             Reset();
         }


         if (tap) {
         	Turn();
         }

         else if (swipeUp) {
         	Jump();
         }
    }



	private void Reset() {
		startTouch = swipeDelta = Vector2.zero;
		isDragging = false;
	}

	public Vector2 SwipeDelta { get { return swipeDelta; } }
	public bool SwipeLeft { get { return swipeLeft; } }
	public bool SwipeRight { get { return swipeRight; } }
	public bool SwipeUp { get { return swipeUp; } }
	public bool SwipeDown { get { return swipeDown; } }
	public bool Tap { get { return tap; } }
	




//-----GAME FUNCTIONS----------------------------------------------------------------------------------------

    private void Jump() {

    	rigidbody.AddForce(0, 200, 0);
    	
    	if (rigidbody.velocity.y < 0) {
    		rigidbody.velocity += Vector3.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    	}

    }


    private void TurnRight() {
    	isGoingForward = false;
    }

    private void TurnLeft() {
    	isGoingForward = true;
    }

     private void Turn() {
		//if (Input.GetMouseButtonDown(0)) 
    	isGoingForward = !isGoingForward;
    }
}
