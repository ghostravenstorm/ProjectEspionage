using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : InputController{
	
	public GameObject menuList;
	private int[] sceneList;

	private int selection = 0;

	void Start(){
		InputManager.instance.mainInput = this;
		sceneList = new int[menuList.transform.childCount];
		UpdateStyle();
		Debug.Log("Object loaded");
	}

	public override void OnSubmit(){
		switch(selection){
			case 0 :
				GameManager.instance.UnpauseGame();
				break;
			case 1 :
				GameManager.instance.UnpauseGame();
				GameManager.instance.restartFromCheckpoint();
				break;
			case 2 :
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