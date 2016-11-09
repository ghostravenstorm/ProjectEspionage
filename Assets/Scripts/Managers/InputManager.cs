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
			GameManager.instance.pauseGame();
		}

		if(Input.GetKeyDown(KeyCode.Alpha2))
			Application.Quit();
	}
}