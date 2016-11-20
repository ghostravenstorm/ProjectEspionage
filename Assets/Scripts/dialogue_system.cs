using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dialogue_system : InputController {

    public GameObject canvas;
    public Text textbox;
    public Image avatar;
    public AudioSource audioSrc;


    public string[] text;
    public AudioClip[] soundbyte;
    public Sprite[] frames;
    public int[] CharacterLineSwitch;
    public int[] framesPerCharacter;

    int CurrentCharacter = 0;
    public int CurrentLine = 0;
    public int CurrentAvy = 0;
    //public string ButtonToContinueText = "Submit";


    private bool isTyping = false;
    private bool CancelTyping = false;
    public float typingSpeed = 1.2f;

    public override void OnSubmit(){
        if (!isTyping){
            if(CurrentLine >= text.Length){   
                //Agent.instance.GetComponent<MainController>().ResumeController();
                //InputManager.instance.mainInput = new NullInputController();
                canvas.SetActive(false);
                //Debug.Log("Dialogue Complete.");
                return;
            }
            else
                StartRender();
        }
        else if(isTyping){
            StopAllCoroutines();

            textbox.text = text[CurrentLine-1];
            avatar.sprite = frames[CharacterLineSwitch[CurrentCharacter]];
            isTyping = false;
            return;
        }

        CurrentLine++;
    }

	void Update () {

        // -- Moved to the OnSubmit method from IInputController.
        //if(Input.GetButtonDown(ButtonToContinueText)){

        //    if (!isTyping){
        //        if(CurrentLine >= text.Length){

                    // -- Caused issues whenever submit was pressed when no dialogue is active.
                    //Agent.instance.GetComponent<MainController>().resumeController();
        //            canvas.SetActive(false);
        //            Debug.Log("Dialogue");
        //            return;
        //        }
        //        else
        //            StartRender();
        //    }
        //    else if(isTyping){
        //        StopAllCoroutines();

        //        textbox.text = text[CurrentLine-1];
        //        avatar.sprite = frames[CharacterLineSwitch[CurrentCharacter]];
        //        isTyping = false;
        //        return;
        //    }
        //    CurrentLine++;
        //}
    }

    public void SetupAvatar(Sprite[] img){
        frames = img;
    }

    public void SetText(string[] newtext){
        text = newtext;
    }

    public void SetSound(AudioClip[] sound){
        soundbyte = sound;
    }

    public IEnumerator RenderText(string CurLine){
        isTyping = true;
        canvas.SetActive(true);
        int len = CurLine.Length;
        textbox.text = "";
        char iterator = CurLine[0];
        int animationIterator = 0;

        if(CharacterLineSwitch.Length > 0){
            if(CurrentCharacter+1 < CharacterLineSwitch.Length ){
                if(CharacterLineSwitch[CurrentCharacter+1] <= CurrentLine){
                    CurrentCharacter++;
                }
            }
        }

        animationIterator = CharacterLineSwitch[CurrentCharacter];
        int avatarlength = framesPerCharacter[CurrentCharacter];

        for (int i = 0; i < len; i++){
            {  
                AnimateAvatar(animationIterator);
                iterator = CurLine[i];

                if(!audioSrc.isPlaying){
                    audioSrc.clip = soundbyte[CurrentCharacter];
                    audioSrc.pitch = Random.Range(0, 3) + 1;
                    audioSrc.Play();
                }

                textbox.text += iterator;
                animationIterator++;

                if (animationIterator == CharacterLineSwitch[CurrentCharacter] + avatarlength)
                    animationIterator = CharacterLineSwitch[CurrentCharacter];

                yield return new WaitForSeconds(typingSpeed);
            }
        }

        avatar.sprite = frames[CharacterLineSwitch[CurrentCharacter]];
        isTyping = false;

        yield break;
    }

    public void AnimateAvatar(int iter){
        avatar.sprite = frames[iter];
    }

    public void StartRender(){
        StartCoroutine(RenderText(text[CurrentLine]));
    }
}
