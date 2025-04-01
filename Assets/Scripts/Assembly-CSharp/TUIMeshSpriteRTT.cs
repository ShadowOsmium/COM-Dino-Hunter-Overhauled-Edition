using UnityEngine;

[AddComponentMenu("TUI/Control/Mesh Sprite RTT")]
public class TUIMeshSpriteRTT : TUIMeshSprite
{
	protected override Material CreateUITextureMaterial()
	{
		return new Material(Shader.Find("Triniti/Particle/AA_COL_DO_RT"));
	}
}
