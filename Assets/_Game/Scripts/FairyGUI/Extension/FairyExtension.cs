using FairyGUI;
using UnityEngine;

public static class FairyExtension
{
	/// <summary>
	/// The method retrieves a GLoader from a GObject.
	/// May return null if the GObject is not a GLoader or does not contain a GLoader child.
	/// Make sure to check for null before using the returned GLoader.
	/// Make sure the name of the child is "icon" if you are using GComponent.
	public static GLoader GetLoader(this GObject gObject)
	{
		if (gObject is GLoader loader)
		{
			return loader;
		}
		
		if (gObject is GComponent component)
		{
			return component.GetChild("icon").asLoader;
		}

		return null;
	}
	
	public static NTexture GetNTexture(this Sprite sprite)
	{
		if (sprite == null)
		{
			Debug.LogWarning("Sprite is null, returning null NTexture.");
			return null;
		}

		var texture = sprite.texture;
		if (texture == null)
		{
			Debug.LogWarning("Sprite texture is null, returning null NTexture.");
			return null;
		}

		var nTexture = new NTexture(sprite)
		{
			refCount = 1 // Set reference count to 1
		};
		return nTexture;
	}
}