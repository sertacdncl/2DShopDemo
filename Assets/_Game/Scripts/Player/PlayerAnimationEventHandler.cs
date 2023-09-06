using UnityEngine;

public class PlayerAnimationEventHandler : MonoBehaviour
{
	private PlayerController _pController;

	private void Awake()
	{
		//Get In Parent as this is on the GameObject with the animator and the controller is at the root of the
		//character.
		_pController = GetComponentInParent<PlayerController>();
	}
}