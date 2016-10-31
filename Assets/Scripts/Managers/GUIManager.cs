using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
	
	public static GUIManager instance { get; private set; }

	public Image detector;
	public Sprite eyeOpen;
	public Sprite eyeClosed;

	void Awake(){
		instance = this;
	}

	public void setDetector(bool detection){
		if(detection) detector.sprite = eyeOpen;
		if(!detection) detector.sprite = eyeClosed;
	}
	
}