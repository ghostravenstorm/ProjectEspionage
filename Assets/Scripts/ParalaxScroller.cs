using UnityEngine;

public class ParalaxScroller : MonoBehaviour{
	
	public string sortingLayer;
	//public GameObject playerRef;
	//public bool isVerticallyStatic;
	//public float verticalOffset;
	public bool isScrolling;
	public float speedMultiplierHor;
	//public float speedMultiplierVer;

	private Vector2 savedOffset;
	//private Vector3 savedPosition;

	void Awake(){
		for(int i = 0; i < SortingLayer.layers.Length; i++){
			if(SortingLayer.layers[i].name == sortingLayer)
				this.GetComponent<Renderer>().sortingLayerName = sortingLayer;
		}
	}

	void Start(){
		//savedPosition = this.transform.localPosition;
		savedOffset = this.GetComponent<Renderer>().sharedMaterial.GetTextureOffset("_MainTex");
	}

	void Update(){
		if(isScrolling){
			float playerPos = Agent.instance.GetComponent<Player>().XDistanceFromSpawn;
			float x = Mathf.Repeat( playerPos * speedMultiplierHor * -1, 2);

			/*
			if( (Agent.instance.GetComponent<MainController>().controller.state == PlayerState.ClimbingRope ||
			 Agent.instance.GetComponent<MainController>().controller.state == PlayerState.ClimbingLadder ||
			 Agent.instance.GetComponent<MainController>().controller.state == PlayerState.ClimbingLedge) &&
			 Input.GetButton("Vertical") ){
				
				float y = savedPosition.y += (Input.GetAxis("Vertical") * speedMultiplierVer);
				this.transform.localPosition = new Vector3(this.transform.localPosition.x, y, this.transform.localPosition.x);
			}*/ 

			Vector2 offset = new Vector2(savedOffset.x + x, savedOffset.y);
			this.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
		}
	}

	void OnDisable(){
		this.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
	}	
} 