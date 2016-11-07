using UnityEngine;

public class ParalaxScroller : MonoBehaviour{
	
	public string sortingLayer;
	public GameObject playerRef;
	public bool isScrolling;
	public float speedMultiplier;

	private Vector2 savedOffset;

	void Awake(){
		for(int i = 0; i < SortingLayer.layers.Length; i++){
			if(SortingLayer.layers[i].name == sortingLayer)
				this.GetComponent<Renderer>().sortingLayerName = sortingLayer;
		}
	}

	void Start(){
		savedOffset = this.GetComponent<Renderer>().sharedMaterial.GetTextureOffset("_MainTex");
	}

	void Update(){
		if(!isScrolling){
			float playerPos = playerRef.GetComponent<Player>().XDistanceFromSpawn;
			float x = Mathf.Repeat( playerPos * speedMultiplier * -1, 2);
			Vector2 offset = new Vector2(x, savedOffset.y);
			this.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
		}
	}

	void OnDisable(){
		this.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
	}

	public void getPlayer(){
		playerRef = GameObject.Find("Character");
	}	
}