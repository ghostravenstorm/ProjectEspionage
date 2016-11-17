using UnityEngine;
using System;
using System.Collections;

public interface IInputController{

	void OnSubmit();
	void OnUpArrow();
	void OnDownArrow();

	void OnAgentInteract();
}

public class InputManager : MonoBehaviour{
	
	public static InputManager instance;

	public IInputController mainInput;
	public IInputController secondaryInput;

	void Awake(){
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(this);
		}
	}

	void Start(){
		secondaryInput = new NullInputController();
	}

	void Update(){
		if(Input.GetButtonDown("Submit"))
			mainInput.OnSubmit();

		if(Input.GetButtonDown("Up"))
			mainInput.OnUpArrow();

		if(Input.GetButtonDown("Down"))
			mainInput.OnDownArrow();

		if(Input.GetButtonDown("Pause")){
			GameManager.instance.PauseGame();
		}

		if(Input.GetButtonDown("Interact"))
			secondaryInput.OnAgentInteract();

		if(Input.GetKeyDown(KeyCode.Alpha2))
			Application.Quit();

		Debug.Log(mainInput);

		/*
		for (int i = 0;i < 20; i++) {
            if(Input.GetKeyDown("joystick button "+i)){
                print("joystick button "+i);
            }
        }
        */
	}
}