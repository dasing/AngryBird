using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class birdDamage : MonoBehaviour {

	public int hitPoint = 2;
	public Sprite damagePicture;
	public float damageMinSpeed;
	public lifeManager lifemanager;
	public GameObject mask;

	private AudioSource[] audio;
	private AudioSource diedAudio;
	private AudioSource collisionAudio;
	private int currentHitPoint;
	private float damageHitSpeedSqr;
	private SpriteRenderer _spriteRenderer;

	void Start () {
		_spriteRenderer = GetComponent<SpriteRenderer> ();
		currentHitPoint = hitPoint;
		damageHitSpeedSqr = damageMinSpeed * damageMinSpeed;
		audio = GetComponents<AudioSource> ();

		diedAudio = audio [0];
		collisionAudio = audio [1];

		if (audio[0].name != "died " ) {
			diedAudio = audio [1];
			collisionAudio = audio [0];
		}

	}
	

	void Update () {
		
	}

	void OnCollisionEnter2D( Collision2D collision ){

		if (collision.collider.tag != "Damager") {
			return;
		}

		if (collision.relativeVelocity.sqrMagnitude < damageHitSpeedSqr) {
			return;
		}
			
		_spriteRenderer.sprite = damagePicture;
		currentHitPoint -= 1;
		collisionAudio.Play ();


		if (currentHitPoint <= 0) {
			Kill ();
		}
	}

	void Kill(){
		
		diedAudio.Play ();
		_spriteRenderer.enabled = false;
		GetComponent<Collider2D> ().enabled = false;
		GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Kinematic;
		GetComponent<ParticleSystem> ().Play ();

		lifemanager.end = true;
		//change scene
		StartCoroutine (WaitAndReset());

	}

	IEnumerator WaitAndReset(){
		yield return new WaitForSeconds (4);
		Scene scene = SceneManager.GetActiveScene ();

		if (scene.name == "level0")
			SceneManager.LoadScene ("level1", LoadSceneMode.Single);
		else {
			mask.SetActive (true);
			SceneManager.LoadScene ("win", LoadSceneMode.Additive);
		}

	}
}
