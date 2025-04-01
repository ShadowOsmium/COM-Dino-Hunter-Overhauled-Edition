using UnityEngine;

public class GoodsNeedItem : MonoBehaviour
{
	public int m_nIndex = -1;

	public TUILabel label_goods_need;

	public GoodsNeedItemBuy btn_buy;

	public GoodsNeedItemImg img_goods;

	public GameObject go_goods_effect;

	private Vector3 m_local_position;

	private void Awake()
	{
		m_local_position = base.gameObject.transform.localPosition;
		btn_buy.m_nIndex = m_nIndex;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ShowGoodsNeedItem(int nID, int nCurCount, int nMaxCount)
	{
		TUIMappingInfo.CTUIMaterialInfo material = TUIMappingInfo.Instance().GetMaterial(nID);
		if (material != null)
		{
			base.transform.localPosition = m_local_position;
			string empty = string.Empty;
			empty = ((nCurCount >= nMaxCount) ? "{color}{0}{color}/{1}" : "{color:FF0000FF}{0}{color}/{1}");
			string text = TUITool.StringFormat(empty, nCurCount, nMaxCount);
			label_goods_need.Text = text;
			if (nCurCount < nMaxCount)
			{
				TUIPriceInfo tUIPriceInfo = new TUIPriceInfo();
				tUIPriceInfo.unit_type = material.m_PurchasePrice.unit_type;
				tUIPriceInfo.price = material.m_PurchasePrice.price * (nMaxCount - nCurCount);
				btn_buy.SetInfo(tUIPriceInfo);
			}
			else
			{
				btn_buy.HideInfo();
			}
			img_goods.SetInfo(material.m_nID, material.m_nQuality, material.m_sName);
		}
	}

	public void HideGoodsNeedItem()
	{
		base.transform.localPosition = m_local_position + new Vector3(0f, -1000f, 0f);
	}

	public void SetDefaultParam()
	{
		label_goods_need.Text = string.Empty;
	}

	public void PlayGoodsEffect()
	{
		if (go_goods_effect == null || label_goods_need == null)
		{
			Debug.Log("error!");
			return;
		}
		GameObject gameObject = (GameObject)Object.Instantiate(go_goods_effect);
		gameObject.transform.parent = label_goods_need.transform;
		gameObject.transform.localPosition = new Vector3(-8f, 0f, 0.1f);
		Object.Destroy(gameObject, 1f);
	}

	public GoodsNeedItemBuy GetGoodsNeedItemBuy()
	{
		return btn_buy;
	}
}
