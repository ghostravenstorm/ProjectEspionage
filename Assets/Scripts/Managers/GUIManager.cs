using System;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
	
	public static GUIManager instance;

	public Image detector;
	public Sprite eyeOpen;
	public Sprite eyeClosed;

	void Awake(){
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(this);
		}
	}

	public void setDetector(bool detection){
		if(detection) detector.sprite = eyeOpen;
		if(!detection) detector.sprite = eyeClosed;
	}
}

