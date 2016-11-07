using UnityEngine;

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
	}
}