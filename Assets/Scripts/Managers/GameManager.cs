using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour{

	public static GameManager instance;

	private string currentScene;

	//public Scene startScreen;
	//public Scene demoEndScreen;
	public string startScreen;
	public string demoEndScreen;



	private Vector3 checkPoint;

	void Awake(){
		instance = this;
		DontDestroyOnLoad(this);
	}

	void Start(){
		
	}

	void Update(){
		if(Input.anyKeyDown && currentScene == demoEndScreen){
			//SceneManager.LoadScene(startScreen);
			//currentScene = startScreen;
		}
	}

	public void setCurrentScene(string name){
		currentScene = name;
	}

	public string getCurrentScene(string name){
		return currentScene;
	}

	public void setCheckPoint(Vector3 point){
		checkPoint = point;
	}

	public Vector3 getCheckPoint(){
		return checkPoint;
	}

	public IEnumerator runDemoEndScreen(){

		SceneManager.LoadScene(demoEndScreen);
		currentScene = demoEndScreen;

		yield return null;
	}
}