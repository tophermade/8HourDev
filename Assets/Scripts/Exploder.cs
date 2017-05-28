using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour {

	void Explosion(){
		Vector3 temp = transform.position;
		temp.x = temp.x-4.0f;
		Collider[] colliders = Physics.OverlapSphere(transform.position, 6.0f);

		foreach (Collider hit in colliders){
			Rigidbody body = hit.GetComponent<Rigidbody>();
			if(body != null){
				body.AddExplosionForce(750.0f, temp, 10, 3.0f);
			}
		}
	}

	void Start(){
		Explosion();
	}
}
