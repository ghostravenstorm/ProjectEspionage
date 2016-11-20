using UnityEngine;
using UnityEngine.UI;

public class MasterConsole : InputController{
		
	public GameObject canvas;
	public GameObject console;

	private IInputController prevInputCont;

	void Awake(){
		DontDestroyOnLoad(this);
	}

	void Start(){
		//Instantiate(canvas);
		console = (GameObject)Instantiate(console, canvas.transform);
		console.SetActive(false);

		float width = (float) (Screen.width * 0.5) + (float) (Screen.width * 0.1);
		float height = (float) Screen.height - 20;
		//console.GetComponent<RectTransform>().rect = new Rect(10, 10, width, height);
		console.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 10, height);
		console.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 10, width);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Slash) && !console.activeSelf){
			console.SetActive(true);
			console.GetComponent<InputField>().ActivateInputField();
		}

		if(Input.GetKeyDown(KeyCode.Escape) && console.activeSelf){
			console.SetActive(false);
		}
	}

	public override void OnSubmit(){

	}
}