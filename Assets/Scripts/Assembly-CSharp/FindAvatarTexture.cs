using UnityEngine;

public class FindAvatarTexture : MonoBehaviour
{
	public new Camera camera;

	private void Start()
	{
		TUIMeshSprite component = base.gameObject.GetComponent<TUIMeshSprite>();
		RenderTexture renderTexture = FindTexture();
		component.CustomizeRect = new Rect(0f, 0f, renderTexture.width, renderTexture.height);
		component.CustomizeTexture = renderTexture;
		component.ForceUpdate();
	}

	public RenderTexture FindTexture()
	{
		if (camera == null && camera.targetTexture == null)
		{
			return null;
		}
		return camera.targetTexture;
	}
}
