using UnityEngine;

public class GuardSoundController : MonoBehaviour{
	
	public AudioSource audio1;
	public AudioSource audio2;

	public AudioClip[] footsteps;
	public AudioClip[] gunshots;
	public AudioClip[] bullets;

	private System.Random rand;

	void Start(){
		rand = new System.Random();
	}

	public void PlayFootstep(){
		if(footsteps == null) return;
		audio1.clip = footsteps[rand.Next(0, footsteps.Length)];
		//audio1.Play();
	}

	public void PlayGunshot(){
		if(gunshots == null) return;
		audio1.clip = gunshots[rand.Next(0, gunshots.Length)];
		audio1.Play();
	}

	public void Playbullet(){
		if(bullets == null) return;
		audio2.clip = bullets[rand.Next(0, bullets.Length)];
		audio2.Play();
	}

}