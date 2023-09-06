using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
{
	public Tilemap WalkSurfaceTilemap { get; set; }
}
