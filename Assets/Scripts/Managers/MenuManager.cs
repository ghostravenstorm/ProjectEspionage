using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour{
	
	public GameObject menuList;
	public string[] sceneList;

	private int selection = 0;

	void Start(){
		updateStyle();
	}

	void Update(){

		//E35143

		Debug.Log(selection);

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
			SceneManager.LoadScene(sceneList[selection]);
			GameManager.instance.setCurrentScene(sceneList[selection]);
		}
	}

	private void updateStyle(){

		for(int i = 0; i < menuList.transform.childCount; i++){
			menuList.transform.GetChild(i).GetComponent<Text>().fontStyle = FontStyle.Normal;
			menuList.transform.GetChild(i).GetComponent<Text>().color = Color.white;
		}

		menuList.transform.GetChild(selection).GetComponent<Text>().fontStyle = FontStyle.Bold;
		menuList.transform.GetChild(selection).GetComponent<Text>().color = new Color32(0xe3, 0x51, 0x43, 0xff);
	}
}