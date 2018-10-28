using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour 
{
	private AudioSource source;

	private float currentVolume = 1f;

	[SerializeField]
	private AudioClip ballCollisionSfx = null;

	private void Awake(){
		source = GetComponent<AudioSource> ();
	}

	/// <summary>
	/// Plays the Ball collision sfx by collision impulse
	/// </summary>
	/// <param name="impulse">İmpulse.</param>
	public void BallCollision(float impulse){
		if (!source.enabled)
			return;

		source.volume = currentVolume * Mathf.Clamp (impulse, 0f, 3f) / 3f;

		playBallCollisionSfx ();	
	}

	void playBallCollisionSfx ()
	{
		if (source.volume < 0.08f)
			source.volume = 0.08f;

		source.Stop ();
		source.clip = ballCollisionSfx;
		source.Play ();
	}

	public void BallCollisionByPlayer (float ratio){
		source.volume = currentVolume * ratio;
		playBallCollisionSfx ();
	}

	public void OpenCloseVolume(bool open){
		source.enabled = open;
	}

	public void ChangeVolume(float volume){
		currentVolume = volume;
		source.volume = currentVolume;
	}
}
