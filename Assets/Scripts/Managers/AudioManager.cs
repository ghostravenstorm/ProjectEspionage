using UnityEngine;

public class AudioManager : MonoBehaviour{
	
	public static AudioManager instance;

	void Awake(){
		instance = this;
		DontDestroyOnLoad(this);
	}
}