using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class WalkableSurface : MonoBehaviour
{
	private void Awake()
	{
		GameManager.Instance.WalkSurfaceTilemap = GetComponent<Tilemap>();
	}
}
