using UnityEngine;

public class TestingGrounds : MonoBehaviour{
	void Start(){
		InputManager.instance.mainInput = new NullInputController();
	}
}