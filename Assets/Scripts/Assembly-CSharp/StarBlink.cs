using UnityEngine;

public class StarBlink : MonoBehaviour
{
	public bool open_blink;

	private float blink_time = 0.6f;

	private float blink_scale = 6f;

	private TUIMeshSprite m_sprite;

	private float m_time;

	private void Start()
	{
		m_sprite = base.gameObject.GetComponent<TUIMeshSprite>();
	}

	private void Update()
	{
		UpdateBlink(Time.deltaTime);
	}

	public void ShowBlink()
	{
		open_blink = true;
	}

	private void UpdateBlink(float delta_time)
	{
		if (!open_blink || m_sprite == null)
		{
			return;
		}
		if (blink_time == 0f)
		{
			Debug.Log("error!");
			return;
		}
		m_time += delta_time;
		if (m_time > blink_time)
		{
			m_time = 0f;
			open_blink = false;
			Object.Destroy(base.gameObject);
		}
		else
		{
			m_sprite.color = new Color(m_sprite.color.r, m_sprite.color.g, m_sprite.color.b, 1f * (blink_time - m_time / blink_time));
			float num = 1f + (blink_scale - 1f) * (m_time / blink_time);
			m_sprite.transform.localScale = new Vector3(num, num, 1f);
		}
	}
}
