using UnityEngine;

public class MusicPlayerTrackSwitcher : MonoBehaviour{
	
	public AudioClip track;

	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag == "Player"){
			GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>().SwitchTrack(track);
			Destroy(this.gameObject);
		}
	}
}