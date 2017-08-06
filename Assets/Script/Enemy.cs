using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	NavMeshAgent agent;
	GameObject target;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		target = GameObject.Find ("Player");
		if (target == null)
			Debug.Log ("No target found");
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination (target.transform.position);
	}
}
