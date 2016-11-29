using UnityEngine;
using System.Collections;

public class LedgeGrabber : MonoBehaviour{

    void OnTriggerEnter(Collider collider) {

        if (collider.tag == "Player")
        {
            collider.GetComponent<AgentInputController>().isGrounded = false;
            collider.GetComponent<Animator>().SetBool("IsClimbingRope", true );
       }
        
    }


    public IEnumerator Animate()
    {
        bool IsDoneClimbing = false;

       if (IsDoneClimbing == false)
        {
           yield return new WaitForSeconds(5);
        }


        yield break;
    }


}

