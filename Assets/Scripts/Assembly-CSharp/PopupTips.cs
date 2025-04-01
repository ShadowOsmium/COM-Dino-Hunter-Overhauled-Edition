using UnityEngine;

public class PopupTips : MonoBehaviour
{
	public enum TipsPivot
	{
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight
	}

	public TUILabel label_text;

	public Transform parent_bg;

	public TUIMeshSprite img_bg_left;

	public TUIMeshSprite img_bg_mid;

	public TUIMeshSprite img_bg_normal;

	public TUIMeshSprite img_bg_right;

	public Vector2 delta_vect2 = Vector2.zero;

	private float bg_width = 15f;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetInfo(string m_text, Vector3 m_pos, TipsPivot m_tip_pivot)
	{
		if (label_text == null)
		{
			Debug.Log("error!");
			return;
		}
		base.transform.position = m_pos + new Vector3(0f, 0f, -2f);
		label_text.Text = m_text;
		DoAlign(m_tip_pivot);
	}

	private void DoAlign(TipsPivot m_tip_pivot)
	{
		if (label_text == null || parent_bg == null || img_bg_left == null || img_bg_mid == null || img_bg_normal == null || img_bg_right == null)
		{
			Debug.Log("error!");
			return;
		}
		float x = label_text.CalculateBounds(label_text.Text).size.x;
		float num = ((!(x - bg_width * 2f >= 0f)) ? bg_width : (x - bg_width * 2f));
		img_bg_normal.transform.localScale = new Vector3((!(num / bg_width < 1f)) ? (num / bg_width) : 1f, 1f, 1f);
		switch (m_tip_pivot)
		{
		case TipsPivot.TopLeft:
			img_bg_right.transform.localPosition = new Vector3(0f, 0f, 0f);
			img_bg_mid.transform.localPosition = new Vector3(0f - bg_width, 0f, 0f);
			img_bg_mid.flipY = false;
			img_bg_normal.transform.localPosition = new Vector3(0f - bg_width - bg_width / 2f - num / 2f, 0f, 0f);
			img_bg_left.transform.localPosition = new Vector3((0f - bg_width) * 2f - num, 0f, 0f);
			label_text.transform.localPosition = new Vector3((img_bg_left.transform.localPosition.x + img_bg_right.transform.localPosition.x) / 2f, 3f, -1f);
			parent_bg.localPosition = new Vector3(bg_width + delta_vect2.x, bg_width * 2f + delta_vect2.y);
			break;
		case TipsPivot.TopRight:
			img_bg_left.transform.localPosition = new Vector3(0f, 0f, 0f);
			img_bg_mid.transform.localPosition = new Vector3(bg_width, 0f, 0f);
			img_bg_mid.flipY = false;
			img_bg_normal.transform.localPosition = new Vector3(bg_width + bg_width / 2f + num / 2f, 0f, 0f);
			img_bg_right.transform.localPosition = new Vector3(bg_width * 2f + num, 0f, 0f);
			label_text.transform.localPosition = new Vector3((img_bg_left.transform.localPosition.x + img_bg_right.transform.localPosition.x) / 2f, 3f, -1f);
			parent_bg.localPosition = new Vector3(0f - bg_width + delta_vect2.x, bg_width * 2f + delta_vect2.y);
			break;
		case TipsPivot.BottomLeft:
			img_bg_right.transform.localPosition = new Vector3(0f, 0f, 0f);
			img_bg_mid.transform.localPosition = new Vector3(0f - bg_width, 6.5f, 0f);
			img_bg_mid.flipY = true;
			img_bg_normal.transform.localPosition = new Vector3(0f - bg_width - bg_width / 2f - num / 2f, 0f, 0f);
			img_bg_left.transform.localPosition = new Vector3((0f - bg_width) * 2f - num, 0f, 0f);
			label_text.transform.localPosition = new Vector3((img_bg_left.transform.localPosition.x + img_bg_right.transform.localPosition.x) / 2f, 3f, -1f);
			parent_bg.localPosition = new Vector3(bg_width + delta_vect2.x, (0f - bg_width) * 2f + delta_vect2.y);
			break;
		case TipsPivot.BottomRight:
			img_bg_left.transform.localPosition = new Vector3(0f, 0f, 0f);
			img_bg_mid.transform.localPosition = new Vector3(bg_width, 6.5f, 0f);
			img_bg_mid.flipY = true;
			img_bg_normal.transform.localPosition = new Vector3(bg_width + bg_width / 2f + num / 2f, 0f, 0f);
			img_bg_right.transform.localPosition = new Vector3(bg_width * 2f + num, 0f, 0f);
			label_text.transform.localPosition = new Vector3((img_bg_left.transform.localPosition.x + img_bg_right.transform.localPosition.x) / 2f, 3f, -1f);
			parent_bg.localPosition = new Vector3(0f - bg_width + delta_vect2.x, (0f - bg_width) * 2f + delta_vect2.y);
			break;
		}
	}

	public void Hide()
	{
		base.transform.localPosition = new Vector3(0f, -1000f, 0f);
	}
}
