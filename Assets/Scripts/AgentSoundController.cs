using UnityEngine;

public class AgentSoundController : MonoBehaviour{
	
	public new AudioSource audio;
	public AudioClip[] sneakingFootsteps;
	public AudioClip[] walkingFootsteps;
	public AudioClip[] jumping;
	public AudioClip[] landing;
	public AudioClip[] dying;

	private System.Random rand;

	void Start(){
		rand = new System.Random();
	}

	public void PlayWalkingFootstep(){
		if(walkingFootsteps == null) return;
		audio.volume = 1f;
		audio.clip = walkingFootsteps[rand.Next(0, walkingFootsteps.Length)];
		audio.Play();
	}

	public void PlaySprintingFootstep(){
		if(walkingFootsteps == null) return;
		audio.volume = 2f;
		audio.clip = walkingFootsteps[rand.Next(0, walkingFootsteps.Length)];
		audio.Play();
		Debug.Log(audio.volume);
	}

	public void PlaySneakingFootstep(){
		if(sneakingFootsteps == null) return;
		audio.volume = 1f;
		audio.clip = sneakingFootsteps[rand.Next(0, sneakingFootsteps.Length)];
		audio.Play();
	}

	public void PlayJumping(){
		if(jumping == null) return;
		audio.volume = 0.3f;
		audio.clip = jumping[rand.Next(0, jumping.Length)];
		audio.Play();
	}

	public void PlayLanding(){
		if(jumping == null) return;
		audio.volume = 0.3f;
		audio.clip = landing[rand.Next(0, landing.Length)];
		audio.Play();
	}

	public void PlayDying(){
		if(dying == null) return;
		audio.volume = 1f;
		audio.clip = dying[rand.Next(0, dying.Length)];
		audio.Play();
	}
}