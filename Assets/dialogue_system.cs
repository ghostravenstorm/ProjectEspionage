using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dialogue_system : MonoBehaviour {

    public GameObject canvas;
    public Text textbox;
    public Image avatar;
    public AudioSource audioSrc;
    string text;
    AudioClip soundbyte;
    Sprite[] frames;

	// Use this for initialization
	void Start () {
        Sprite one = Resources.Load<Sprite>("pacopen");
        Sprite two = Resources.Load<Sprite>("pacclosed");

        Sprite[] set =  new Sprite[] { one, two };
        SetSound(Resources.Load<AudioClip>("pactalk2"));
        frames = set;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.N))
        {
            text = "The quick brown dog jumped over the lazy fox";

               StartCoroutine( RenderText());
            }

    }

    public void SetupAvatar(Sprite[] img)
    {
        frames = img;
    }

    public void SetText(string newtext)
    {
        text = newtext;
    }

    public void SetSound(AudioClip sound)
    {
        audioSrc.clip = sound;
    }

    public IEnumerator RenderText()
    {
        canvas.SetActive(true);
        int len = text.Length;
        textbox.text = "";
        char iterator = text[0];
        int animationIterator = 0;
        int avatarlength = frames.Length;

        for (int i = 0; i < len; i++)
        {
            {
                AnimateAvatar(animationIterator);
                iterator = text[i];
                if (!audioSrc.isPlaying)
                {
                    audioSrc.pitch = Random.Range(0, 3) + 1;
                    audioSrc.Play();

                }


                textbox.text += iterator;
                animationIterator++;
                if (animationIterator == avatarlength)
                    animationIterator = 0;
                yield return new WaitForSeconds(0.15f);
            }
        }
        avatar.sprite = frames[0];
        canvas.SetActive(false);
        yield break;
        
    }

    public void  AnimateAvatar(int iter)
    {
                //avatar.sprite = frames[iter];
    }

    public void StartRender()
    {
        StartCoroutine(RenderText());
    }
}
