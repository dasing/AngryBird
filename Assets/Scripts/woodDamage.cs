using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woodDamage : MonoBehaviour {

	public int hitPoint = 1;

	public float damageMinSpeed = 3;


	private int currentHitPoint;
	private float damageHitSpeedSqr;
	private SpriteRenderer _spriteRenderer;
	private AudioSource collisionAudio;


	void Start () {
		_spriteRenderer = GetComponent<SpriteRenderer> ();
		currentHitPoint = hitPoint;
		damageHitSpeedSqr = damageMinSpeed * damageMinSpeed;

		collisionAudio = GetComponent<AudioSource> ();
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


		currentHitPoint -= 1;

		if (currentHitPoint <= 0) {
			Kill ();
		}
	}

	void Kill(){
		_spriteRenderer.enabled = false;
		GetComponent<Collider2D> ().enabled = false;
		GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Kinematic;

		collisionAudio.Play ();

	}
}
