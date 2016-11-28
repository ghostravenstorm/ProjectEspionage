using UnityEngine;
using System;
using System.Collections;

public class InputManager : MonoBehaviour{

	public static InputManager instance;

	// -- References to things that recieve input.
	// -- Set to NullInputController to inact no input.
	public IInputController mainInput;
	public IInputController secondaryInput;

	void Awake(){
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(this);
		}
	}

	void Start(){
		// -- Default: nothing to receive input at start.
		secondaryInput = new NullInputController();
	}

	// -- Any and all actions on input events occur here.
	// -- Objects that receive input must be set to one of the IInputController
	//    variables above and must also use the interface.
	// -- The code below calls the appropiate methods in the interface.
	void Update(){
		if(Input.GetButtonDown("Submit"))
			mainInput.OnSubmit();

		if(Input.GetButtonDown("Up"))
			mainInput.OnUp();

		if(Input.GetButtonDown("Down"))
			mainInput.OnDown();

		if(Input.GetButton("Vertical")){
			if(Input.GetAxis("Vertical") > 0)
				mainInput.OnMoveUp();
			else if(Input.GetAxis("Vertical") < 0)
				mainInput.OnMoveDown();
		}
		else if(Input.GetButtonUp("Vertical")){
			mainInput.OffMoveUp();
			mainInput.OffMoveDown();
		}

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

		// -- Hard-coded escape button for arcade cabnet.
		if(Input.GetKeyDown(KeyCode.Alpha3))
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
