using UnityEngine;

public class LightProjector : MonoBehaviour{
	
	[Tooltip("This options sets the appropiate tag at runtime.")]
	public LightType lightType = LightType.Semi;
	public ProjectorType projType = ProjectorType.Ceiling;
	public float lightWidth = 3f;

	private Vector3 rightPosition;
	private Vector3 leftPosition;
	private Vector3 rightScale;
	private Vector3 leftScale;

	void Start(){

		if(projType == ProjectorType.Flashlight){
		
			rightPosition = this.transform.localPosition;
			leftPosition = this.transform.localPosition * -1;
			rightScale = this.transform.localScale;
			leftScale = this.transform.localScale * -1;
		}

		
		
		if(lightType == LightType.Semi){
			this.gameObject.tag = "SemiLight";
			this.GetComponent<SpriteRenderer>().color = new Color(1f, 0.9f, 0.7f, 0.1f);
		}
		else{
			this.gameObject.tag = "FullLight";
			this.GetComponent<SpriteRenderer>().color = new Color(1f, 0.9f, 0.7f, 0.3f);
		}

		if(projType == ProjectorType.Ceiling){
			this.transform.localScale = new Vector3(lightWidth, this.transform.localScale.y, this.transform.localScale.z);
		}
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
}