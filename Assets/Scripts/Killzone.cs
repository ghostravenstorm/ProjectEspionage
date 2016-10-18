using UnityEngine;

public class Killzone : MonoBehaviour{

	public GameObject playerPrefab;
	
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Player"){
			RespawnPlayer(collider.gameObject);
		}
	}
	

	private void RespawnPlayer(GameObject player){
		Object.Instantiate(playerPrefab, player.GetComponent<Player>().spawnPoint, player.transform.rotation);
		Object.Destroy(player);
	}
}