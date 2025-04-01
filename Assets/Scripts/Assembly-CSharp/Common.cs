using UnityEngine;

public class Common
{
	public static bool GetAtlasSpriteSize(string sPath, string sSprite, ref Rect rect)
	{
		GameObject gameObject = Resources.Load(sPath) as GameObject;
		if (gameObject == null)
		{
			return false;
		}
		UIAtlas component = gameObject.GetComponent<UIAtlas>();
		if (component == null)
		{
			return false;
		}
		UIAtlas.Sprite sprite = component.GetSprite(sSprite);
		if (sprite == null)
		{
			return false;
		}
		rect = sprite.outer;
		return true;
	}
}
