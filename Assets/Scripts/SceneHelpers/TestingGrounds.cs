using UnityEngine;

public class TestingGrounds : MonoBehaviour{
	void Start(){
		InputManager.instance.inputController = new NullInputController();
	}
}

public class NullInputController : IInputController{
	public void OnSubmit(){}
	public void OnUpArrow(){}
	public void OnDownArrow(){}
}