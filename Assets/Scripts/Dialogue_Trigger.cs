using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class Dialogue_Trigger : MonoBehaviour {


    public dialogue_system ds;
    public string[] conversation;
    public Sprite[] animationframes;
    public AudioClip[] CharacterSound;
    public int[] AvatarStartingIndex;
    public int[] AmountofFramesPerAvatar;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter( Collider c )
    {
        if(c.tag == "Player")
        {   
            //c.GetComponent<MainController>().PauseController(AgentState.Standing);
            ds.CurrentLine = 0;
            ds.text = conversation;
            ds.frames = animationframes;
            ds.CharacterLineSwitch = AvatarStartingIndex;
            ds.framesPerCharacter = AmountofFramesPerAvatar;
            ds.soundbyte = CharacterSound;

            InputManager.instance.mainInput = ds;

            ds.StartRender();
            ds.CurrentLine++;
            this.gameObject.SetActive(false);
        }
    }
}
