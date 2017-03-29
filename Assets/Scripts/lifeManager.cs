using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lifeManager : MonoBehaviour {

	private Text _text;
	public int remainLife = 3;
	const string prefix = "Life: ";
	public GameObject mask;
	public bool update = true;
	public bool end = false;

	void Start () {
		_text = this.GetComponent<Text> ();
		_text.text = prefix + remainLife;

	}

	public void resetLife(){
		remainLife = 3;
		update = true;
		end = false;
	}

	public void minusLife(){
		remainLife -= 1;
		_text.text = prefix + remainLife;
	}

	void Update () {
		
		if (remainLife == 0 && update && !end ) {
			//End Game
			mask.SetActive(true);
			update = false;
			SceneManager.LoadScene ("gameOver", LoadSceneMode.Additive);
		}

	}
}
