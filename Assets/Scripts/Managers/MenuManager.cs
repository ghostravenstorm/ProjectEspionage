using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour, IInputController{
	
	public GameObject menuList;
	public string[] sceneList;

	private int selection = 0;

	void Start(){
		InputManager.instance.inputController = this;
		updateStyle();
	}

	public void OnSubmit(){
		switch(selection){
			case 0 : 
				SceneManager.LoadScene(sceneList[selection]);
				GameManager.instance.setCurrentScene(sceneList[selection]);
				break;
			case 1 : 
				SceneManager.LoadScene(sceneList[selection]);
				GameManager.instance.setCurrentScene(sceneList[selection]);
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



	/*
	void Update(){

		//E35143

		//Debug.Log(selection);

		if(Input.GetButtonDown("Up")){
			selection--;
			if(selection <= -1) selection = sceneList.Length-1;
			if(selection >= sceneList.Length) selection = 0;
			updateStyle();
		}

		if(Input.GetButtonDown("Down")){
			selection++;
			if(selection <= -1) selection = sceneList.Length-1;
			if(selection >= sceneList.Length) selection = 0;
			updateStyle();
		}

		if(Input.GetButtonDown("Submit")){

			switch(selection){
				case 0 : 
					SceneManager.LoadScene(sceneList[selection]);
					GameManager.instance.setCurrentScene(sceneList[selection]);
					break;
				case 1 : 
					SceneManager.LoadScene(sceneList[selection]);
					GameManager.instance.setCurrentScene(sceneList[selection]);
					break;
				case 2 : 
					Application.Quit();
					break;					
			}

		}
	}*/

	private void updateStyle(){

		for(int i = 0; i < menuList.transform.childCount; i++){
			menuList.transform.GetChild(i).GetComponent<Text>().fontStyle = FontStyle.Normal;
			menuList.transform.GetChild(i).GetComponent<Text>().color = Color.white;
		}

		menuList.transform.GetChild(selection).GetComponent<Text>().fontStyle = FontStyle.Bold;
		menuList.transform.GetChild(selection).GetComponent<Text>().color = new Color32(0xe3, 0x51, 0x43, 0xff);
	}
}