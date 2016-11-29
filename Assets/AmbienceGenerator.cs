using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmbienceGenerator : MonoBehaviour {

   public AudioClip[] clips;
    public AudioSource src;

   public bool playSound = true;
    bool timer = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(playSound == false && timer == false)
        {
            timer = true;
            StartCoroutine(PlaySound());

        }
        else if (playSound && timer)
        {
            int clip = Random.Range(0, 19);
            src.clip = clips[clip];
            playSound = false;
            timer = false;

            src.Play();
            playSound = false;
        }
	}

    public IEnumerator PlaySound()
    {

        yield return new WaitForSeconds(5);
        playSound = true;
        yield break;

    }
}
