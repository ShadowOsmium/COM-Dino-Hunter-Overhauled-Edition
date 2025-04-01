using UnityEngine;

public class Popup_Sale_Blink : MonoBehaviour
{
	private bool open_blink;

	private float move_speed = 600f;

	private float max_y = 600f;

	private float min_y = -800f;

	private void Start()
	{
		Transform[] componentsInChildren = base.gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.layer = 9;
			TUIDrawSprite component = componentsInChildren[i].GetComponent<TUIDrawSprite>();
			if (component != null)
			{
				component.clippingType = TUIDrawSprite.Clipping.None;
			}
		}
		base.gameObject.SetActiveRecursively(false);
	}

	private void Update()
	{
		UpdateBlink(Time.deltaTime);
	}

	private void UpdateBlink(float delta_time)
	{
		if (open_blink)
		{
			float num = base.transform.localPosition.y - move_speed * delta_time;
			if (num <= min_y)
			{
				num = max_y;
			}
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, num, base.transform.localPosition.z);
		}
	}

	public void OpenBlink(bool m_open)
	{
		open_blink = m_open;
		if (m_open)
		{
			base.gameObject.SetActiveRecursively(true);
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, max_y, base.transform.localPosition.z);
		}
		else
		{
			base.gameObject.SetActiveRecursively(false);
		}
	}
}
