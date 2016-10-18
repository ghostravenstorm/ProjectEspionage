using UnityEngine;

public class Player : MonoBehaviour{

	public Vector3 spawnPoint;

	void Start(){
		this.spawnPoint = this.transform.position;
	}
}