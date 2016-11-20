using UnityEngine;
using System;
using System.Collections;

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
			mainInput.OnUp();

		if(Input.GetButtonDown("Down"))
			mainInput.OnDown();

		if(Input.GetButton("Horizontal")){
			if(Input.GetAxis("Horizontal") > 0)
				mainInput.OnMoveRight();
			else if(Input.GetAxis("Horizontal") < 0)
				mainInput.OnMoveLeft();
		}
		else if(Input.GetButtonUp("Horizontal")){
			mainInput.OffMoveRight();
			mainInput.OffMoveLeft();
		}

		if(Input.GetButton("Sprint"))
			mainInput.OnSprint();
		else if(Input.GetButtonUp("Sprint"))
			mainInput.OffSprint();

		if(Input.GetButton("Sneak"))
			mainInput.OnSneak();
		else if(Input.GetButtonUp("Sneak"))
			mainInput.OffSneak();

		if(Input.GetButtonDown("Jump"))
			mainInput.OnJump();

		if(Input.GetButtonDown("Pause")){
			GameManager.instance.PauseGame();
		}

		if(Input.GetButtonDown("Interact"))
			secondaryInput.OnAgentInteract();

		if(Input.GetKeyDown(KeyCode.Alpha2))
			Application.Quit();

		//Debug.Log(mainInput);

		/*
		for (int i = 0;i < 20; i++) {
            if(Input.GetKeyDown("joystick button "+i)){
                print("joystick button "+i);
            }
        }
        */
	}
}