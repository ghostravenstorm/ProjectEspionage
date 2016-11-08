using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

public interface IInputController{

	void OnSubmit();
	void OnUpArrow();
	void OnDownArrow();
}

public class InputManager : MonoBehaviour{
	
	public static InputManager instance;

	public IInputController inputController;

	private GameObject musicPlayer;

	void Awake(){
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(this);
		}
	}

	void Update(){
		if(Input.GetButtonDown("Submit"))
			inputController.OnSubmit();

		if(Input.GetButtonDown("Up"))
			inputController.OnUpArrow();

		if(Input.GetButtonDown("Down"))
			inputController.OnDownArrow();

		if(Input.GetButtonDown("Pause")){
			if(musicPlayer == null)
				musicPlayer = GameObject.Find("MusicPlayer");
			GameManager.instance.pauseGame();
			musicPlayer.GetComponent<MusicPlayer>().Pause();
		}
	}
}