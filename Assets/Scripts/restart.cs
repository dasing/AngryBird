using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour {

	private Button btn;
	public lifeManager lifemanager;

	void Start () {
		btn = GetComponent<Button> ();
		btn.onClick.AddListener (reload);
	}

	void reload () {
		SceneManager.LoadScene ("level0", LoadSceneMode.Single);

	}
}
