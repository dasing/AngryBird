using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollwer : MonoBehaviour {

	public Transform followObj;
	public Transform leftObj;
	public Transform rightObj;
	public Transform upObj;
	public Transform downObj;

	void Start () {
		
	}
	

	void Update () {
		Vector3 newPosition = transform.position; //Camera's position
		newPosition.x = followObj.position.x;
		newPosition.y = followObj.position.y;
		newPosition.x = Mathf.Clamp (newPosition.x, leftObj.position.x, rightObj.position.x ); //move between leftObj and rightObj
		newPosition.y = Mathf.Clamp (newPosition.y, leftObj.position.y, rightObj.position.y );
		transform.position = newPosition;

		if (Input.GetAxis ("Mouse ScrollWheel") <= 0 && this.GetComponent<Camera> ().orthographicSize <= 6 ) {
			this.GetComponent<Camera> ().orthographicSize = this.GetComponent<Camera> ().orthographicSize - Input.GetAxis ("Mouse ScrollWheel");
		} else if (Input.GetAxis ("Mouse ScrollWheel") >= 0 && this.GetComponent<Camera> ().orthographicSize >= 5) {
			this.GetComponent<Camera> ().orthographicSize = this.GetComponent<Camera> ().orthographicSize - Input.GetAxis ("Mouse ScrollWheel");
		}


	}


}
