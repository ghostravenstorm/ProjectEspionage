using System;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
	
	public static GUIManager instance;

	public Image detector;
	public Image sprintMeter;
	public Sprite eyeOpen;
	public Sprite eyeClosed;
	public Text paused;

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

	public void Pause(){
		if(GameManager.instance.isPaused)
			paused.enabled = true;
		else
			paused.enabled = false;
	}

	public void SetPause(){
		paused.enabled = true;
	}

	public void SetUnpause(){
		paused.enabled = false;
	}

	public void UpdateSprintMeter(float n){
		sprintMeter.fillAmount = n;
	}
}

