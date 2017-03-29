using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour {

	private SpringJoint2D spring;
	private bool bClick;
	public Rigidbody2D ball;
	private Transform catapult;
	private Ray rayToMouse;
	private Vector2 preVelocity;
	public float lineLength;
	public LineRenderer catapult_0;
	public LineRenderer catapult_1;
	private Ray catapult_1ToAngrybird;
	private float circleRadius;
	private Vector3 oriPos;
	public bool shoot;
	private AudioSource audio;
	public Camera camera;

	public void reset(){
		
		camera.transform.position = new Vector3 ( 0, 0, -10 );
		this.transform.position = oriPos;
	
		ball.velocity = Vector2.zero;
		ball.angularVelocity = 0;


		ball.bodyType = RigidbodyType2D.Kinematic;

		LineRendererSet ();

		TrailPoint.bTrail = false;
		shoot = false;

		catapult_0.enabled = true;
		catapult_1.enabled = true;

	}

	void Awake()
	{
		spring = GetComponent<SpringJoint2D> ();
		ball = GetComponent<Rigidbody2D>();
		catapult = spring.connectedBody.transform;
	}


	void Start () {
		shoot = false;
		rayToMouse = new Ray (catapult.position, Vector3.zero );
		LineRendererSet ();
		catapult_1ToAngrybird = new Ray ( catapult_1.transform.position, Vector3.zero ); 
		CircleCollider2D circle = GetComponent<Collider2D> () as CircleCollider2D;
		circleRadius = circle.radius;
		lineLength = 2.5f;
		oriPos = this.transform.position;

		audio = GetComponent<AudioSource> ();

	}

	void Update () {

		if (bClick) {
			Dragging ();
		}

		if (!shoot) {
			if (GetComponent<Rigidbody2D> ().bodyType != RigidbodyType2D.Kinematic && preVelocity.sqrMagnitude > GetComponent<Rigidbody2D> ().velocity.sqrMagnitude) {
				GetComponent<Rigidbody2D> ().velocity = preVelocity;
				spring.enabled = false;
				shoot = true;
				audio.Play ();
				TrailPoint.bTrail = true; //start Trailpoint
			}

			if (!bClick) {
				preVelocity = GetComponent<Rigidbody2D> ().velocity;
			}

			LineRendererUpdate ();

		} else {
			catapult_0.enabled = false;
			catapult_1.enabled = false;
		}
			
	}

	void LineRendererSet(){

		catapult_0.SetPosition (0, catapult_0.transform.position); //set start point
		catapult_0.sortingLayerName = "background";
		catapult_0.sortingOrder = 2;

		catapult_1.SetPosition (0, catapult_1.transform.position);
		catapult_1.sortingLayerName = "background";
		catapult_1.sortingOrder = 4;

	}

	void LineRendererUpdate(){
		
		Vector2 catapultToAngrybird = transform.position - catapult_1.transform.position;
		catapult_1ToAngrybird.direction = catapultToAngrybird;
		Vector3 thePoint = catapult_1ToAngrybird.GetPoint (catapultToAngrybird.magnitude + circleRadius);

		catapult_0.SetPosition ( 1, thePoint ); //set end point
		catapult_1.SetPosition ( 1, thePoint );
	}

	void OnMouseDown(){
		
		spring.enabled = false;
		bClick = true;

	}

	void OnMouseUp(){
		spring.enabled = true;
		bClick = false;
		ball.bodyType = RigidbodyType2D.Dynamic;

	}

	void Dragging(){
		
		Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 catapultToMouse = mouseWorldPoint - catapult.position;

		if (catapultToMouse.sqrMagnitude > lineLength*lineLength ) {
			rayToMouse.direction = catapultToMouse;
			mouseWorldPoint = rayToMouse.GetPoint ( lineLength );

		}
		mouseWorldPoint.z = 0;
		transform.position = mouseWorldPoint;
	}
}
