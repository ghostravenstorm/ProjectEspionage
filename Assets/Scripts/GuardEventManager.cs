using UnityEngine;

public class GuardEventManager : MonoBehaviour{
	
	public void KillAgent(){
		float distance = Vector3.Distance(this.transform.position, Agent.instance.transform.position);
		StartCoroutine(Agent.instance.GetComponent<SpyAnimationController>().playDeath(distance/100));
	}
}