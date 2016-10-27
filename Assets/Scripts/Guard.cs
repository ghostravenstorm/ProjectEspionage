using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guard : MonoBehaviour{

	public float patrolSpeed;
	public GameObject waypointRef;

	[Tooltip("Use if, and only if, waypoints are placed manually in the scene. This is where they would be referenced.")]
	public GameObject[] wayPoints;

	[Tooltip("Automatically generate waypoints. Distances for point A and B must be defined.")]	
	public bool autoGenerateWaypoints;

	//[Tooltip("Is there a point B pre defined? If not then generate a point B at the start location.")]
	//public bool usePointB;

	[Tooltip("Use if, and only if, waypoints are auto gererated.")]
	public float pointADistance;

	[Tooltip("Use if, and only if, waypoints are auto gererated. This will be ignored if there is no predefined point B.")]
	public float pointBDistance;

	private new Rigidbody rigidbody;
	private Vector3 startPoint;
	private Vector3 originalScale;
	private IEnumerator IEPatrol;

	void Start(){

		rigidbody = this.GetComponent<Rigidbody>();
		startPoint = this.transform.position;
		originalScale = this.transform.localScale;

		if(wayPoints.Length == 0)
			wayPoints = new GameObject[2];

		if(autoGenerateWaypoints){

			Vector3 pointA = new Vector3(startPoint.x + pointADistance, startPoint.y, 0);
			Vector3 pointB = new Vector3(startPoint.x - pointBDistance, startPoint.y, 0);

			wayPoints[0] = ( (GameObject)Instantiate(waypointRef, pointA, this.transform.rotation) );
			/*if(usePointB)*/ wayPoints[1] = ( (GameObject)Instantiate(waypointRef, pointB, this.transform.rotation) );
		}

		/*
		if(!usePointB) 
			wayPoints[1] = (GameObject)Instantiate(waypointRef, startPoint, this.transform.rotation); */

		if(wayPoints[0] != null && wayPoints[1] != null){
			IEPatrol = Patrol();
			StartCoroutine(IEPatrol);
		}
		else
			Debug.LogError("Waypoints for " +  this.gameObject.name + " is not set. Patroling disabled.");
	}

	void OnTriggerEnter(Collider collider){

		if(collider.gameObject.tag == "Waypoint"){
			patrolSpeed *= -1;
			this.transform.localScale = new Vector3(this.transform.localScale.x * -1, originalScale.y, originalScale.z);
		}
	}

	private IEnumerator Patrol(){

		while(true){

			rigidbody.velocity = new Vector3(patrolSpeed, rigidbody.velocity.y, 0);

			yield return null;
		}
	}
}