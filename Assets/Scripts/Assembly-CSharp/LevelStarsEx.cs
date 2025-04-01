using UnityEngine;

public class LevelStarsEx : MonoBehaviour
{
	public TUIMeshSprite img_star;

	public TUILabel label_value;

	private string texture_empty = "shenjixingxing2";

	private string texture_full = "shenjixingxing1";

	public GameObject prefab_star_blink;

	private void Start()
	{
		if (texture_empty == string.Empty || texture_full == string.Empty)
		{
			Debug.Log("no texture!");
		}
	}

	private void Update()
	{
	}

	public void SetStars(int m_count, Vector3 m_position, bool m_blink = false)
	{
		if (texture_empty == string.Empty || texture_full == string.Empty)
		{
			Debug.Log("no texture!");
			return;
		}
		base.gameObject.SetActiveRecursively(true);
		base.gameObject.transform.localPosition = m_position;
		SetStarsEx(m_count);
		if (m_blink && prefab_star_blink != null && img_star != null)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(prefab_star_blink);
			gameObject.transform.parent = img_star.transform;
			gameObject.transform.localPosition = new Vector3(0f, 0f, -1f);
			gameObject.GetComponent<StarBlink>().ShowBlink();
		}
	}

	public void SetStarsEx(int m_count)
	{
		base.gameObject.SetActiveRecursively(true);
		if (label_value != null)
		{
			label_value.Text = "x " + m_count;
		}
	}

	public void SetStarsDisable()
	{
		base.gameObject.SetActiveRecursively(false);
	}
}
