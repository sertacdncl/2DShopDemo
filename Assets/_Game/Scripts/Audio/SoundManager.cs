using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
	[SerializeField] private AudioSource sfxReferenceSource;
	private Queue<AudioSource> _sfxPool;

	[SerializeField] private AudioClip buySellSound;

	private void Awake()
	{
		const int poolLength = 16;

		_sfxPool = new Queue<AudioSource>();
        
		for (int i = 0; i < poolLength; ++i)
		{
			//I don't want to spend my time for now
			//TODO: Do it with addressables
			var obj = new GameObject("SFXPool");
			obj.transform.SetParent(transform);

			var source = Instantiate(sfxReferenceSource);

			_sfxPool.Enqueue(source);
		}
	}

	public void PlaySFXAt(Vector3 position, AudioClip clip, bool spatialized)
	{
		var source = _sfxPool.Dequeue();

		source.clip = clip;
		source.transform.position = position;

		source.spatialBlend = spatialized ? 1.0f : 0.0f;
        
		source.Play();
        
		_sfxPool.Enqueue(source);
	}

	public void PlaySfxAtOnce(AudioClip clip)
	{
		var source = _sfxPool.Dequeue();
		source.clip = clip;
		source.spatialBlend = 0.0f;
		source.Play();
		_sfxPool.Enqueue(source);
	}

	public void PlayBuySellSound()
	{
		PlaySfxAtOnce(buySellSound);
	}
}
