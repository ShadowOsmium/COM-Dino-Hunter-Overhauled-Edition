using UnityEngine;

public class AchievementRewardText : MonoBehaviour
{
	public TUILabel label_value01;

	public TUIMeshSprite img_unit01;

	public TUILabel label_value02;

	public TUIMeshSprite img_unit02;

	private string gold_texture = "title_jingbi";

	private string crystal_texture = "title_shuijing";

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Show(int m_value01, UnitType m_type01)
	{
		base.gameObject.SetActiveRecursively(true);
		if (label_value01 != null)
		{
			label_value01.gameObject.SetActiveRecursively(true);
			label_value01.Text = "x" + m_value01;
			label_value01.transform.localPosition = new Vector3(-15f, 0f, 0f);
		}
		if (img_unit01 != null)
		{
			img_unit01.gameObject.SetActiveRecursively(true);
			switch (m_type01)
			{
			case UnitType.Gold:
				img_unit01.texture = gold_texture;
				break;
			case UnitType.Crystal:
				img_unit01.texture = crystal_texture;
				break;
			}
			img_unit01.transform.localPosition = new Vector3(-19f, 0f, 0f);
		}
		if (label_value02 != null)
		{
			label_value02.gameObject.SetActiveRecursively(false);
		}
		if (img_unit02 != null)
		{
			img_unit02.gameObject.SetActiveRecursively(false);
		}
	}

	public void Show(int m_value01, UnitType m_type01, int m_value02, UnitType m_type02)
	{
		base.gameObject.SetActiveRecursively(true);
		if (label_value01 != null)
		{
			label_value01.gameObject.SetActiveRecursively(true);
			label_value01.Text = "x" + m_value01;
			label_value01.transform.localPosition = new Vector3(-37f, 0f, 0f);
		}
		if (img_unit01 != null)
		{
			img_unit01.gameObject.SetActiveRecursively(true);
			switch (m_type01)
			{
			case UnitType.Gold:
				img_unit01.texture = gold_texture;
				break;
			case UnitType.Crystal:
				img_unit01.texture = crystal_texture;
				break;
			}
			img_unit01.transform.localPosition = new Vector3(-41f, 0f, 0f);
		}
		if (label_value02 != null)
		{
			label_value02.gameObject.SetActiveRecursively(true);
			label_value02.Text = "x" + m_value02;
			label_value02.transform.localPosition = new Vector3(10f, 0f, 0f);
		}
		if (img_unit02 != null)
		{
			img_unit02.gameObject.SetActiveRecursively(true);
			switch (m_type02)
			{
			case UnitType.Gold:
				img_unit02.texture = gold_texture;
				break;
			case UnitType.Crystal:
				img_unit02.texture = crystal_texture;
				break;
			}
			img_unit02.transform.localPosition = new Vector3(6f, 0f, 0f);
		}
	}

	public void Hide()
	{
		base.gameObject.SetActiveRecursively(false);
	}
}
