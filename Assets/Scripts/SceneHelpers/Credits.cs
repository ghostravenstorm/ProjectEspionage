using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : InputController{

	void Start(){
		InputManager.instance.mainInput = this;
	}
	
	public override void OnSubmit(){
		SceneManager.LoadScene("MenuScreen");
	}
}