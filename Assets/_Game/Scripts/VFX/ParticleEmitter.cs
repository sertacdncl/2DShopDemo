using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
	[SerializeField] private ParticleSystem particles;
	[SerializeField] private int particleCount = 1;
    
	public void Emit()
	{
		particles.Emit(particleCount);
	}
}
