using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reseter : MonoBehaviour {

	public Rigidbody2D GameObj;
	public float resetSpeed = 0.05f;
	public SpringJoint2D spring;
	public lifeManager lifemanager;
	public shooter rock;

	void Start () {
		spring = GameObj.GetComponent<SpringJoint2D> ();

	}

	void Update () {
		if ( rock.shoot && GameObj.velocity.sqrMagnitude < 0.025f) {
			StartCoroutine (WaitAneReset());
		}
	}

	IEnumerator WaitAneReset(){
		TrailPoint.bTrail = false;
		yield return new WaitForSeconds (1);
		lifemanager.minusLife ();
		rock.reset();
		StopAllCoroutines();
	}

	void OnTriggerExit2D(Collider2D other){
		
		if (other.GetComponent<Rigidbody2D>() == GameObj){
			StartCoroutine (WaitAneReset());
		}
	}
}
