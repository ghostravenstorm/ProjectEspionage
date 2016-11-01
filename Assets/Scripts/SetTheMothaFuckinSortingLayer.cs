using UnityEngine;

public class SetTheMothaFuckinSortingLayer : MonoBehaviour{
	public string sortingLayer;

	void Start(){
		this.GetComponent<Renderer>().sortingLayerName = sortingLayer;
	}
}