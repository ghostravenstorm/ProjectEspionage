using UnityEngine;

public class MusicPlayer : MonoBehaviour{

	public AudioClip defaultMusic;
	public AudioClip pauseMusic;

	private float defaultMusicPosition = 0;
	private float pauseMusicPosition = 0;

	void Start(){

	}

	public void Pause(){
		defaultMusicPosition = GetComponent<AudioSource>().time;
		GetComponent<AudioSource>().Stop();
		GetComponent<AudioSource>().clip = pauseMusic;
		GetComponent<AudioSource>().time = pauseMusicPosition;
		GetComponent<AudioSource>().Play();
	}

	public void Unpause(){
		pauseMusicPosition = GetComponent<AudioSource>().time;
		GetComponent<AudioSource>().Stop();
		GetComponent<AudioSource>().clip = defaultMusic;
		GetComponent<AudioSource>().time = defaultMusicPosition;
		GetComponent<AudioSource>().Play();
	}

	/*
	if(GameSrc == null)
        {
            GameSrc = GameObject.Find("MusicPlayer").GetComponent<AudioSource>().clip;
            PauseSrc = Resources.Load<AudioClip>("Pause");
            pauseTxt = GameObject.Find("MainGUI/Text");
        }
        if (isPaused == true && GameObject.Find("MusicPlayer").GetComponent<AudioSource>().clip != PauseSrc) { 
            Time.timeScale = 0;
            pauseTxt.SetActive(true);
            GameObject.Find("MusicPlayer").GetComponent<AudioSource>().Stop();

            GameObject.Find("MusicPlayer").GetComponent<AudioSource>().clip = PauseSrc;
            GameObject.Find("MusicPlayer").GetComponent<AudioSource>().loop = true;

            GameObject.Find("MusicPlayer").GetComponent<AudioSource>().Play();
        }
             else if( isPaused == false && GameObject.Find("MusicPlayer").GetComponent<AudioSource>().clip == PauseSrc)
        {
            Time.timeScale = 1;
            pauseTxt.SetActive(false);
            GameObject.Find("MusicPlayer").GetComponent<AudioSource>().Stop();

            GameObject.Find("MusicPlayer").GetComponent<AudioSource>().clip = GameSrc;
            GameObject.Find("MusicPlayer").GetComponent<AudioSource>().loop = true;

            GameObject.Find("MusicPlayer").GetComponent<AudioSource>().Play();

        }

        if(isPaused == false && pauseTxt != null)
        {
            pauseTxt.SetActive(false);
        }
        if (Input.GetButtonDown("Pause")){
            isPaused = !isPaused;
        }*/
}