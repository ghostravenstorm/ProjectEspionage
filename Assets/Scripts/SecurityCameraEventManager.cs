using UnityEngine;

public class SecurityCameraEventManager : MonoBehaviour{

	public void KillAgent(){
		float distance = Vector3.Distance(this.transform.position, Agent.instance.transform.position);
		Agent.instance.Kill(distance/100);
	}
}