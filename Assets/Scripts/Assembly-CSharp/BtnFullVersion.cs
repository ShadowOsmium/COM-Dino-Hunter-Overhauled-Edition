using UnityEngine;

public class BtnFullVersion : MonoBehaviour
{
	public TUIMeshSprite img_myself;

	private float total_time;

	private void Start()
	{
	}

	private void Update()
	{
		if (img_myself != null)
		{
			total_time += Time.deltaTime;
			img_myself.color = new Color(1f, 1f, 1f, Mathf.Sin(total_time * 4f) * 0.7f + 0.3f);
		}
	}
}
