using UnityEngine;

public class GlobalSoundEmitter : MonoBehaviour{
	
	public static GlobalSoundEmitter instance;
	public AudioSource audiosource;

	void Awake(){
		if(instance == null){
			instance = this;
		}
	}

	public void PlaySound(AudioClip clip){
		audiosource.clip = clip;
		audiosource.Play();
	}
}