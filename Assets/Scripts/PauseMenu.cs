using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour, IInputController{
	
	public GameObject menuList;
	private int[] sceneList;

	private int selection = 0;

	void Start(){
		InputManager.instance.inputController = this;
		sceneList = new int[menuList.transform.childCount];
		updateStyle();
		Debug.Log("Object loaded");
	}

	public void OnSubmit(){
		switch(selection){
			case 0 :
				GameManager.instance.unpauseGame();
				break;
			case 1 :
				GameManager.instance.unpauseGame();
				GameManager.instance.restartFromCheckpoint();
				break;
			case 2 :
				Application.Quit();
				break;
		}
	}

	public void OnUpArrow(){
		selection--;
		if(selection <= -1) selection = sceneList.Length - 1;
		updateStyle();
	}

	public void OnDownArrow(){
		selection++;
		if(selection >= sceneList.Length) selection = 0;
		updateStyle();
	}

	public void OnConsole(){}
	public void OnEscape(){}

	private void updateStyle(){

		for(int i = 0; i < menuList.transform.childCount; i++){
			menuList.transform.GetChild(i).GetComponent<Text>().fontStyle = FontStyle.Normal;
			menuList.transform.GetChild(i).GetComponent<Text>().color = Color.white;
		}

		menuList.transform.GetChild(selection).GetComponent<Text>().fontStyle = FontStyle.Bold;
		menuList.transform.GetChild(selection).GetComponent<Text>().color = new Color32(0xe3, 0x51, 0x43, 0xff);
	}

}