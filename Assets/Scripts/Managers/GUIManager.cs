using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
	
	public static GUIManager instance { get; private set; }

	public Image detector;

	void Awake(){
		instance = this;
	}

	public void setDetector(bool detection){
		if(detection) detector.color = Color.red;
		if(!detection) detector.color = Color.green;
	}
	
}