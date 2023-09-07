using UnityEngine;

public class StartAnimation : MonoBehaviour
{
	public Animation Animation;

	public void Trigger()
	{
		Animation.Play();
	}
}