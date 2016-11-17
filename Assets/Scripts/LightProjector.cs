using UnityEngine;
using System.Collections;

public class LightProjector : MonoBehaviour{
	
	[Tooltip("This options sets the appropiate tag at runtime.")]
	public LightType lightType = LightType.Semi;
	public ProjectorType projType = ProjectorType.Ceiling;
	public float lightWidth = 3f;
	
	//public bool isFlickering = false;
	//public int flickerRandMin = 2;
	//public int flickerRandMax = 5;
	//public bool isActive = true;

	private Vector3 rightPosition;
	private Vector3 leftPosition;
	private Vector3 rightScale;
	private Vector3 leftScale;

	private System.Random rand;

	void Awake(){
		rand = new System.Random();
	}

	void Start(){

		//isActive = true;

		if(projType == ProjectorType.Flashlight){
		
			rightPosition = this.transform.localPosition;
			leftPosition = this.transform.localPosition * -1;
			rightScale = this.transform.localScale;
			leftScale = this.transform.localScale * -1;
		}

		if(lightType == LightType.Semi){
			this.gameObject.tag = "SemiLight";
			this.GetComponent<SpriteRenderer>().color = new Color32(255, 240, 180, 50);
		}
		else{
			this.gameObject.tag = "FullLight";
			this.GetComponent<SpriteRenderer>().color = new Color32(255, 240, 180, 120);
		}

		if(projType == ProjectorType.Ceiling){
			this.transform.localScale = new Vector3(lightWidth, this.transform.localScale.y, this.transform.localScale.z);
		}

		//if(isFlickering)
		//	StartCoroutine(Flicker());
	}

	void Update(){
		/*
		if(projType == ProjectorType.Flashlight){
			if(this.transform.parent.GetComponent<Rigidbody>().velocity.x >= 1){
				//faceRight();
			}
			if(this.transform.parent.GetComponent<Rigidbody>().velocity.x <= -1){
				//faceLeft();
			}
		}
		*/
	}

	public void faceRight(){
		this.transform.localPosition = rightPosition;
		this.transform.localScale = rightScale;
	}

	public void faceLeft(){
		this.transform.localPosition = leftPosition;
		this.transform.localScale = leftScale;
	}

	// -- Disabled due to light states not properly updating on the agent. --
	//private IEnumerator Flicker(){

		//GetComponent<SpriteRenderer>().enabled = true;
		//isActive = true;

		//int randWhole = rand.Next(flickerRandMin, flickerRandMax);
		//int randDec = rand.Next(0, 99);
		//yield return new WaitForSeconds(GameManager.ConvertToFloat(randWhole, randDec));

		//GetComponent<SpriteRenderer>().enabled = false;
		//isActive = false;

		//randWhole = rand.Next(flickerRandMin, flickerRandMax);
		//randDec = rand.Next(0, 99);
		//yield return new WaitForSeconds(GameManager.ConvertToFloat(randWhole, randDec));
		
		//yield return StartCoroutine(Flicker());
	//}
}