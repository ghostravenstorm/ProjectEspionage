using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : InputController{
	
	public GameObject menuList;
	public string[] sceneList;

	private int selection = 0;

	void Start(){
		InputManager.instance.mainInput = this;
		UpdateStyle();
	}

	public override void OnSubmit(){
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
				SceneManager.LoadScene(sceneList[selection]);
				GameManager.instance.setCurrentScene(sceneList[selection]);
				break;
			case 3 :
				Application.Quit();
				break;
		}
	}

	public override void OnUp(){
		selection--;
		if(selection <= -1) selection = sceneList.Length - 1;
		UpdateStyle();
	}

	public override void OnDown(){
		selection++;
		if(selection >= sceneList.Length) selection = 0;
		UpdateStyle();
	}

	private void UpdateStyle(){

		for(int i = 0; i < menuList.transform.childCount; i++){
			menuList.transform.GetChild(i).GetComponent<Text>().fontStyle = FontStyle.Normal;
			menuList.transform.GetChild(i).GetComponent<Text>().color = Color.white;
		}

		menuList.transform.GetChild(selection).GetComponent<Text>().fontStyle = FontStyle.Bold;
		menuList.transform.GetChild(selection).GetComponent<Text>().color = new Color32(0xe3, 0x51, 0x43, 0xff);
	}
}