using UnityEngine;

public class PopupGoBuy : MonoBehaviour
{
	public enum GoBuyType
	{
		Weapon,
		WeaponInBlack,
		Skill
	}

	public GameObject go_popup;

	public TUILabel label_text;

	protected GoBuyType go_buy_type;

	public int m_nLinkID { get; private set; }

	private void Start()
	{
		m_nLinkID = -1;
	}

	private void Update()
	{
	}

	public void Show(GoBuyType m_go_buy_type, int linkid)
	{
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		if (go_popup != null && go_popup.GetComponent<Animation>() != null)
		{
			go_popup.GetComponent<Animation>().Play();
		}
		go_buy_type = m_go_buy_type;
		switch (m_go_buy_type)
		{
		case GoBuyType.Weapon:
			if (label_text != null)
			{
				label_text.Text = "GO BUY EQUIP IN THE FORGE.";
			}
			break;
		case GoBuyType.Skill:
			if (label_text != null)
			{
				label_text.Text = "GO BUY SKILL IN THE GET SKILLS.";
			}
			break;
		case GoBuyType.WeaponInBlack:
			if (label_text != null)
			{
				label_text.Text = "GO BUY EQUIP\n IN THE BLACK MARKET.";
			}
			break;
		}
		m_nLinkID = linkid;
	}

	public void Hide()
	{
		base.transform.localPosition = new Vector3(0f, -1000f, base.transform.localPosition.z);
	}

	public GoBuyType GetGoBuyType()
	{
		return go_buy_type;
	}
}
