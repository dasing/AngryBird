using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryRock : MonoBehaviour {

	public void killHimself(){
		Debug.Log ("here");
		Destroy(this.gameObject);
	}

}
