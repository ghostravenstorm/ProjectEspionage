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
	private GameObject musicPlayerRef;

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

		/*
		int count = 0;

		 GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
 			foreach(object go in allObjects)
   				count++;

   		Debug.Log(count);*/
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape) && currentScene == demoEndScreen){
			SceneManager.LoadScene(startScreen);
			currentScene = startScreen;
		}
	}

	public void PauseGame(){
		if(!isPaused){
			//Debug.Log("Game manager pause");
			isPaused = true;
			Time.timeScale = 0;
			SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
			GUIManager.instance.SetPause();
			if(musicPlayerRef == null) musicPlayerRef = GameObject.Find("MusicPlayer");
			musicPlayerRef.GetComponent<MusicPlayer>().Pause();
		}
	}

	public void UnpauseGame(){
		isPaused = false;
		Time.timeScale = 1;
		SceneManager.UnloadScene("PauseMenu");
		InputManager.instance.mainInput = new NullInputController();
		GUIManager.instance.SetUnpause();
		if(musicPlayerRef == null) musicPlayerRef = GameObject.Find("MusicPlayer");
		musicPlayerRef.GetComponent<MusicPlayer>().Unpause();
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

	public static float ConvertToFloat(int whole, int dec){
		return ((float)whole) + ((float)((float)dec/100.00f));
	}
}