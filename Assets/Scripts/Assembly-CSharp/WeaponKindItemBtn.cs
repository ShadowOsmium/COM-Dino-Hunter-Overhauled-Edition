using UnityEngine;

public class WeaponKindItemBtn : MonoBehaviour
{
	public kShopWeaponCategory m_nCategory;

	public TUIMeshSprite img_new_mark_normal;

	public TUIMeshSprite img_new_mark_press;

	private string texture_mark = "new";

	private string texture_new = "new2";

	private TUIButtonSelect btn_select;

	public NewMarkType m_Mark { get; private set; }

	private void Awake()
	{
		btn_select = base.gameObject.GetComponent<TUIButtonSelect>();
		if (btn_select == null)
		{
			Debug.Log("no btn_select!");
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetMark(NewMarkType mark)
	{
		if (!(img_new_mark_normal == null) && !(img_new_mark_press == null))
		{
			m_Mark = mark;
			switch (mark)
			{
			case NewMarkType.New:
				img_new_mark_normal.texture = texture_new;
				img_new_mark_press.texture = texture_new;
				break;
			case NewMarkType.Mark:
				img_new_mark_normal.texture = texture_mark;
				img_new_mark_press.texture = texture_mark;
				break;
			case NewMarkType.None:
				img_new_mark_normal.texture = string.Empty;
				img_new_mark_press.texture = string.Empty;
				break;
			}
		}
	}
}
