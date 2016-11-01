using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Dialogue_Trigger : MonoBehaviour {

    public Sprite[] frames;
    public string Text_to_say;
    public AudioClip SoundEffect;
    public dialogue_system DialogueSystem;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        {
            DialogueSystem.SetText(Text_to_say);
            DialogueSystem.SetSound(SoundEffect);
            DialogueSystem.SetupAvatar(frames);
            DialogueSystem.StartRender();
            this.gameObject.SetActive(false);
        }
    }
}
