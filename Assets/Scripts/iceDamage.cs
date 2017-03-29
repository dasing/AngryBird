using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceDamage : MonoBehaviour {

	public int hitPoint = 1;

	public float damageMinSpeed = 1.5f;


	private int currentHitPoint;
	private float damageHitSpeedSqr;
	private SpriteRenderer _spriteRenderer;


	void Start () {
		_spriteRenderer = GetComponent<SpriteRenderer> ();
		currentHitPoint = hitPoint;
		damageHitSpeedSqr = damageMinSpeed * damageMinSpeed;
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

	}
}
