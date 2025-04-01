using UnityEngine;

public class Popup_Sale_Item : MonoBehaviour
{
	public TUIMeshSprite img_item;

	public TUILabel label_old_price;

	public TUIMeshSprite img_old_price_unit;

	public TUILabel label_current_price;

	public TUIMeshSprite img_current_price_unit;

	public TUILabel label_off;

	public TUIMeshSprite img_off;

	private string img_off_texture01 = "sale_onsalebiaoqian";

	private string img_off_texture02 = "sale_onsalebiaoqian2";

	public TUIButtonClick btn_link;

	public TUILabel label_name;

	private TUISingleSaleInfo single_sale_info;

	private string weapon_texture_path = "TUI/Weapon/";

	private string skill_path = "TUI/Skill/";

	private string NGUI_weapon_texture_path = "Artist/Textrues/Weapon/";

	private string NGUI_weapon_altas_path = "Artist/Atlas/Weapon/";

	private string gold_texture = "title_jingbi";

	private string crystal_texture = "title_shuijing";

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
	}

	private void Update()
	{
	}

	public void DoCreate(TUISingleSaleInfo m_info, TUIRect m_rect, GameObject m_invoke_go)
	{
		single_sale_info = m_info;
		if (m_info == null)
		{
			Debug.Log("no info");
			return;
		}
		Debug.Log(string.Concat(m_info.sale_type, " ", m_info.id, " ", m_info.icon));
		OnSaleType sale_type = m_info.sale_type;
		int id = m_info.id;
		TUIPriceInfo old_price_info = m_info.old_price_info;
		float discount = m_info.discount;
		string text = m_info.name;
		if (label_name != null)
		{
			label_name.Text = text;
		}
		if (img_item != null)
		{
			string text2 = string.Empty;
			switch (sale_type)
			{
			case OnSaleType.Weapon:
				if (m_info.icon.Length < 1)
				{
					text2 = TUIMappingInfo.Instance().GetWeaponTexture(id);
				}
				text2 = TUIMappingInfo.Instance().m_sPathRootCustomTex + "/Weapon/" + text2;
				SetCustomizeTexture(img_item, text2, true);
				break;
			case OnSaleType.Armor:
				if (m_info.icon.Length > 0)
				{
					text2 = TUIMappingInfo.Instance().m_sPathRootCustomTex + "/Armor/" + m_info.icon;
					SetCustomizeTexture(img_item, text2, true);
				}
				break;
			case OnSaleType.Accessory:
				if (m_info.icon.Length > 0)
				{
					text2 = TUIMappingInfo.Instance().m_sPathRootCustomTex + "/Accessory/" + m_info.icon;
					SetCustomizeTexture(img_item, text2, true);
				}
				break;
			case OnSaleType.Skill:
				text2 = TUIMappingInfo.Instance().GetSkillTexture(id);
				SetCustomizeTexture(img_item, skill_path + text2);
				break;
			case OnSaleType.Role:
				text2 = TUIMappingInfo.Instance().GetRoleTexture(id);
				img_item.texture = text2;
				img_item.ForceUpdate();
				break;
			}
		}
		if (label_old_price != null && img_old_price_unit != null && label_current_price != null && img_current_price_unit != null && old_price_info != null)
		{
			int num = Mathf.CeilToInt((float)old_price_info.price * discount);
			label_old_price.Text = old_price_info.price.ToString();
			label_current_price.Text = num.ToString();
			if (old_price_info.unit_type == UnitType.Gold)
			{
				img_old_price_unit.texture = gold_texture;
				img_current_price_unit.texture = gold_texture;
			}
			else if (old_price_info.unit_type == UnitType.Crystal)
			{
				img_old_price_unit.texture = crystal_texture;
				img_current_price_unit.texture = crystal_texture;
			}
		}
		if (label_off != null && img_off != null)
		{
			if (discount == 0f)
			{
				label_off.Text = "Free";
				img_off.texture = img_off_texture02;
			}
			else
			{
				label_off.Text = (int)((1f - discount) * 100f + 0.5f) + "% off";
				img_off.texture = img_off_texture01;
			}
		}
		if (m_invoke_go != null && btn_link != null)
		{
			btn_link.invokeOnEvent = true;
			btn_link.invokeObject = m_invoke_go;
			btn_link.componentName = "Scene_MainMenu";
			btn_link.invokeFunctionName = "TUIEvent_ClickSaleLink";
		}
	}

	public void SetCustomizeTexture(TUIMeshSprite m_sprite, string m_path, bool m_use_NGUI = false)
	{
		m_sprite.texture = string.Empty;
		m_sprite.UseCustomize = true;
		m_sprite.CustomizeTexture = Resources.Load(m_path) as Texture;
		if (m_sprite.CustomizeTexture == null)
		{
			Debug.Log("lose texture!" + m_sprite.name);
			return;
		}
		if (!m_use_NGUI)
		{
			m_sprite.CustomizeRect = new Rect(0f, 0f, m_sprite.CustomizeTexture.width, m_sprite.CustomizeTexture.height);
			return;
		}
		Rect rect = new Rect(0f, 0f, m_sprite.CustomizeTexture.width, m_sprite.CustomizeTexture.height);
		Common.GetAtlasSpriteSize(NGUI_weapon_altas_path + m_sprite.CustomizeTexture.name, m_sprite.CustomizeTexture.name, ref rect);
		m_sprite.CustomizeRect = rect;
	}

	public TUISingleSaleInfo GetSaleInfo()
	{
		return single_sale_info;
	}
}
