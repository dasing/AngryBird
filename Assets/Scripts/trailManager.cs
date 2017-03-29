using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailManager : MonoBehaviour {


	private float lifeTime = 0.5f;

	void Start () {
		
	}
	

	void Update () {
		if (lifeTime >= 0) {
			lifeTime -= Time.deltaTime;
		} else {
			Destroy (this.gameObject);
		}
	}
}
