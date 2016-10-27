using UnityEngine;

public class LightProjector : MonoBehaviour{
	
	public LightType lightType = LightType.Semi;

	void Start(){
		if(lightType == LightType.Semi) this.gameObject.tag = "SemiLight";
		else this.gameObject.tag = "FullLight";
	}
}