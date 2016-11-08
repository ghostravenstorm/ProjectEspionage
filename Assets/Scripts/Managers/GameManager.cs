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

	public bool isPaused;

	private Vector3 checkPoint;

	void Awake(){
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(this);
		}
	}

	void Start(){
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		isPaused = false;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape) && currentScene == demoEndScreen){
			SceneManager.LoadScene(startScreen);
			currentScene = startScreen;
		}
	}

	public void pauseGame(){
		if(!isPaused){
			Debug.Log("Game manager pause");
			isPaused = true;
			Time.timeScale = 0;
			SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
			GUIManager.instance.SetPause();
		}
	}

	public void unpauseGame(){
		isPaused = false;
		Time.timeScale = 1;
		SceneManager.UnloadScene("PauseMenu");
		InputManager.instance.inputController = new NullInputController();
		GUIManager.instance.SetUnpause();
	}

	public void restartFromCheckpoint(){
		GameObject.Find("Character").transform.position = getCheckPoint();
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