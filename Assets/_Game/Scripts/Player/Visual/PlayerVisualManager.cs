using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerVisualManager : Singleton<PlayerVisualManager>
{
	public SpriteRenderer[] characterParts;
	
	[SerializeField] private SpriteLibrary torsoLibrary;
	[SerializeField] private SpriteLibrary armRLibrary;
	[SerializeField] private SpriteLibrary armLLibrary;
	[SerializeField] private SpriteLibrary legRLibrary;
	[SerializeField] private SpriteLibrary legLLibrary;
	[SerializeField] private SpriteLibrary footRLibrary;
	[SerializeField] private SpriteLibrary footLLibrary;

	public void ChangeItem(SpriteLibraryAsset newSpriteLibraryAsset)
	{
		torsoLibrary.spriteLibraryAsset = newSpriteLibraryAsset;
	}
}
