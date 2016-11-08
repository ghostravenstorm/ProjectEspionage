using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour, IInputController{

	void Start(){
		InputManager.instance.inputController = this;
	}
	
	public void OnSubmit(){
		SceneManager.LoadScene("MenuScreen");
	}

	public void OnUpArrow(){}
	public void OnDownArrow(){}
}