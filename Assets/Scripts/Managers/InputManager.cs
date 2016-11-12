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

	public IInputController inputController;
	public IInputController secondaryInputController;

	void Awake(){
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(this);
		}
	}

	void Start(){
		secondaryInputController = new NullInputController();
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

		if(Input.GetButtonDown("Interact"))
			secondaryInputController.OnAgentInteract();

		if(Input.GetKeyDown(KeyCode.Alpha2))
			Application.Quit();

		/*
		for (int i = 0;i < 20; i++) {
            if(Input.GetKeyDown("joystick button "+i)){
                print("joystick button "+i);
            }
        }*/
	}
}