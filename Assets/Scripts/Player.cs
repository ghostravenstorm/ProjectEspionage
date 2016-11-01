using UnityEngine;

public class Player : MonoBehaviour{

	public float XDistanceFromSpawn;
	public Vector3 spawnPoint;

	void Start(){
		this.spawnPoint = this.transform.position;
		//GameManager.instance.setCheckPoint(this.transform.position);
	}

	void Update(){
		XDistanceFromSpawn = (spawnPoint.x - this.transform.position.x);
	}
}