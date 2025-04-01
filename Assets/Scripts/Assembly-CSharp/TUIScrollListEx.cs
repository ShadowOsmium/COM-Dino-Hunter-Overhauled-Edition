using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("TUI/Control/TUIScrollListEx")]
public class TUIScrollListEx : TUIControlImpl
{
	public enum Arrangement
	{
		Horizontal,
		Vertical
	}

	protected enum CommandType
	{
		Command_Begin,
		Command_Move,
		Command_End,
		Command_Free
	}

	public Arrangement arrangement;

	public List<GameObject> items_list;

	public bool open_child_event;

	public float min_x;

	public float min_y;

	public float size_scale = 0.6f;

	public float move_z;

	public float sensivity = 100f;

	public float offset_delta;

	public float scroll_back_speed = 5f;

	public bool open_fade;

	protected int finger_id = -1;

	protected Vector2 finger_position = Vector2.zero;

	protected bool move;

	protected CommandType command_type = CommandType.Command_Free;

	[SerializeField]
	private int now_index;

	private GameObject current_item;

	public static int CommandChange;

	public void Clear()
	{
		if (items_list != null)
		{
			for (int i = 0; i < items_list.Count; i++)
			{
				Object.Destroy(items_list[i]);
			}
			items_list.Clear();
		}
		now_index = 0;
		current_item = null;
	}

	public void Add(GameObject go)
	{
		items_list.Add(go);
	}

	public override bool HandleInput(TUIInput input)
	{
		if (open_child_event)
		{
			base.HandleInput(input);
		}
		switch (input.inputType)
		{
		case TUIInputType.Began:
			if (PtInControl(input.position))
			{
				if (move)
				{
					command_type = CommandType.Command_Free;
				}
				finger_id = input.fingerId;
				finger_position = input.position;
				move = false;
			}
			return false;
		case TUIInputType.Moved:
			if (input.fingerId != finger_id)
			{
				return false;
			}
			if (PtInControl(input.position))
			{
				float num = input.position.x - finger_position.x;
				float num2 = input.position.y - finger_position.y;
				if (move)
				{
					finger_position = input.position;
					command_type = CommandType.Command_Move;
					UpdateItems(num, num2);
				}
				else
				{
					float num3 = Mathf.Abs(num);
					float num4 = Mathf.Abs(num2);
					if (num3 > min_x || num4 > min_y)
					{
						move = true;
						finger_position = input.position;
						command_type = CommandType.Command_Move;
					}
				}
				return true;
			}
			return false;
		case TUIInputType.Ended:
			if (input.fingerId != finger_id)
			{
				return false;
			}
			if (move)
			{
				move = false;
				finger_id = -1;
				finger_position = Vector2.zero;
				command_type = CommandType.Command_Free;
				return true;
			}
			finger_id = -1;
			finger_position = Vector2.zero;
			return false;
		default:
			return false;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		UpdateItems(Time.deltaTime);
	}

	private void UpdateItems(float wparam, float lparam)
	{
		if (command_type != CommandType.Command_Move || items_list == null || items_list.Count <= 0)
		{
			return;
		}
		float num = 0f;
		num = ((arrangement != 0) ? lparam : wparam);
		if (num > 0f)
		{
			float num2 = 0f;
			num2 = ((arrangement != 0) ? (0f - items_list[0].transform.localPosition.y) : items_list[0].transform.localPosition.x);
			if (num2 > 0f)
			{
				float num3 = Mathf.Abs(num2) * 0.1f;
				if (num3 < 1f)
				{
					num3 = 1f;
				}
				num /= num3;
			}
		}
		else if (num < 0f)
		{
			float num4 = 0f;
			num4 = ((arrangement != 0) ? (0f - items_list[items_list.Count - 1].transform.localPosition.y) : items_list[items_list.Count - 1].transform.localPosition.x);
			if (num4 < 0f)
			{
				float num5 = Mathf.Abs(num4) * 0.1f;
				if (num5 < 1f)
				{
					num5 = 1f;
				}
				num /= num5;
			}
		}
		for (int i = 0; i < items_list.Count; i++)
		{
			GameObject gameObject = items_list[i];
			Vector2 vector = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);
			float num6 = 0f;
			if (sensivity != 0f)
			{
				num6 = 0f;
				num6 = ((arrangement != 0) ? (Mathf.Abs(vector.y + num) / sensivity * (0f - size_scale) + 1f) : (Mathf.Abs(vector.x + num) / sensivity * (0f - size_scale) + 1f));
			}
			else
			{
				Debug.Log("error!");
			}
			if (num6 < size_scale)
			{
				num6 = size_scale;
			}
			else if (num6 > 1f)
			{
				num6 = 1f;
			}
			float num7 = 0f;
			float z = 0f;
			Vector3 zero = Vector3.zero;
			if (arrangement == Arrangement.Horizontal)
			{
				if (1f - size_scale != 0f)
				{
					num7 = offset_delta * (1f - (1f - num6) / (1f - size_scale));
					z = move_z * ((num6 - size_scale) / (1f - size_scale));
				}
				zero = new Vector3(vector.x + num, num7 / 2f, z);
			}
			else
			{
				if (1f - size_scale != 0f)
				{
					num7 = offset_delta * (1f - (1f - num6) / (1f - size_scale));
					z = move_z * ((num6 - size_scale) / (1f - size_scale));
				}
				zero = new Vector3(num7 / 2f, vector.y + num, z);
			}
			if (open_fade)
			{
				TUIMeshSprite[] componentsInChildren = gameObject.GetComponentsInChildren<TUIMeshSprite>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					if (num6 >= 0.7f)
					{
						componentsInChildren[j].color = new Color(1f, 1f, 1f, 1f);
					}
					else
					{
						componentsInChildren[j].color = new Color(1f, 1f, 1f, -18.33f * num6 * num6 + 31.83f * num6 - 12.5f);
					}
				}
			}
			gameObject.transform.localScale = new Vector3(num6, num6, 1f);
			gameObject.transform.localPosition = zero;
		}
	}

	private void UpdateItems(float delta_time)
	{
		if (command_type != CommandType.Command_Free || items_list.Count <= 0)
		{
			return;
		}
		GameObject gameObject = null;
		int index = 0;
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < items_list.Count; i++)
		{
			gameObject = items_list[index];
			GameObject gameObject2 = items_list[i];
			if (arrangement == Arrangement.Horizontal)
			{
				num = Mathf.Abs(gameObject.transform.localPosition.x);
				num2 = Mathf.Abs(gameObject2.transform.localPosition.x);
			}
			else
			{
				num = Mathf.Abs(gameObject.transform.localPosition.y);
				num2 = Mathf.Abs(gameObject2.transform.localPosition.y);
			}
			if (num2 < num)
			{
				index = i;
				num = num2;
			}
		}
		now_index = index;
		gameObject = items_list[index];
		if (current_item != gameObject)
		{
			current_item = gameObject;
			PostEvent(this, CommandChange, 0f, 0f, null);
		}
		num = ((arrangement != 0) ? gameObject.transform.localPosition.y : gameObject.transform.localPosition.x);
		for (int j = 0; j < items_list.Count; j++)
		{
			GameObject gameObject3 = items_list[j];
			Vector2 vector = new Vector2(gameObject3.transform.localPosition.x, gameObject3.transform.localPosition.y);
			float num3 = 0f;
			if (sensivity != 0f)
			{
				num3 = ((arrangement != 0) ? (Mathf.Abs(vector.y - num * scroll_back_speed * delta_time) / sensivity * (0f - size_scale) + 1f) : (Mathf.Abs(vector.x - num * scroll_back_speed * delta_time) / sensivity * (0f - size_scale) + 1f));
			}
			else
			{
				Debug.Log("error!");
			}
			if (num3 < size_scale)
			{
				num3 = size_scale;
			}
			else if (num3 > 1f)
			{
				num3 = 1f;
			}
			float num4 = 0f;
			float z = 0f;
			if (1f - size_scale != 0f)
			{
				num4 = offset_delta * (1f - (1f - num3) / (1f - size_scale));
				z = move_z * ((num3 - size_scale) / (1f - size_scale));
			}
			Vector3 zero = Vector3.zero;
			zero = ((arrangement != 0) ? new Vector3(num4 / 2f, vector.y - num * scroll_back_speed * delta_time, z) : new Vector3(vector.x - num * scroll_back_speed * delta_time, num4 / 2f, z));
			if (open_fade)
			{
				TUIMeshSprite[] componentsInChildren = gameObject3.GetComponentsInChildren<TUIMeshSprite>();
				for (int k = 0; k < componentsInChildren.Length; k++)
				{
					if (num3 >= 0.7f)
					{
						componentsInChildren[k].color = new Color(1f, 1f, 1f, 1f);
					}
					else
					{
						componentsInChildren[k].color = new Color(1f, 1f, 1f, -18.33f * num3 * num3 + 31.83f * num3 - 12.5f);
					}
				}
			}
			gameObject3.transform.localScale = new Vector3(num3, num3, 1f);
			gameObject3.transform.localPosition = zero;
		}
	}

	public int GetNowIndex()
	{
		if (items_list.Count <= 0)
		{
			return -1;
		}
		return now_index;
	}

	public GameObject GetNowItem()
	{
		if (items_list.Count <= 0)
		{
			return null;
		}
		return items_list[now_index];
	}

	public List<GameObject> GetItemsList()
	{
		if (items_list.Count <= 0)
		{
			return null;
		}
		return items_list;
	}

	public void SetNowItem(GameObject m_item)
	{
		float num = 0f;
		for (int i = 0; i < items_list.Count; i++)
		{
			if (items_list[i] == m_item)
			{
				now_index = i;
				num = ((arrangement != 0) ? items_list[i].transform.localPosition.y : items_list[i].transform.localPosition.x);
			}
		}
		for (int j = 0; j < items_list.Count; j++)
		{
			GameObject gameObject = items_list[j];
			Vector2 vector = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);
			float num2 = 0f;
			if (sensivity != 0f)
			{
				num2 = ((arrangement != 0) ? (Mathf.Abs(vector.y - num) / sensivity * (0f - size_scale) + 1f) : (Mathf.Abs(vector.x - num) / sensivity * (0f - size_scale) + 1f));
			}
			else
			{
				Debug.Log("error!");
			}
			if (num2 < size_scale)
			{
				num2 = size_scale;
			}
			else if (num2 > 1f)
			{
				num2 = 1f;
			}
			float num3 = 0f;
			float z = 0f;
			if (1f - size_scale != 0f)
			{
				num3 = offset_delta * (1f - (1f - num2) / (1f - size_scale));
				z = move_z * ((num2 - size_scale) / (1f - size_scale));
			}
			Vector3 zero = Vector3.zero;
			zero = ((arrangement != 0) ? new Vector3(num3 / 2f, vector.y - num, z) : new Vector3(vector.x - num, num3 / 2f, z));
			if (open_fade)
			{
				TUIMeshSprite[] componentsInChildren = gameObject.GetComponentsInChildren<TUIMeshSprite>();
				for (int k = 0; k < componentsInChildren.Length; k++)
				{
					if (num2 >= 0.7f)
					{
						componentsInChildren[k].color = new Color(1f, 1f, 1f, 1f);
					}
					else
					{
						componentsInChildren[k].color = new Color(1f, 1f, 1f, -18.33f * num2 * num2 + 31.83f * num2 - 12.5f);
					}
				}
			}
			gameObject.transform.localScale = new Vector3(num2, num2, 1f);
			gameObject.transform.localPosition = zero;
		}
	}

	public void SetNowItem(int m_index)
	{
		if (m_index < 0 || m_index >= items_list.Count)
		{
			Debug.Log("error!");
			return;
		}
		float num = 0f;
		num = ((arrangement != 0) ? items_list[m_index].transform.localPosition.y : items_list[m_index].transform.localPosition.x);
		now_index = m_index;
		for (int i = 0; i < items_list.Count; i++)
		{
			GameObject gameObject = items_list[i];
			Vector2 vector = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);
			float num2 = 0f;
			if (sensivity != 0f)
			{
				num2 = ((arrangement != 0) ? (Mathf.Abs(vector.y - num) / sensivity * (0f - size_scale) + 1f) : (Mathf.Abs(vector.x - num) / sensivity * (0f - size_scale) + 1f));
			}
			else
			{
				Debug.Log("error!");
			}
			if (num2 < size_scale)
			{
				num2 = size_scale;
			}
			else if (num2 > 1f)
			{
				num2 = 1f;
			}
			float num3 = 0f;
			float z = 0f;
			if (1f - size_scale != 0f)
			{
				num3 = offset_delta * (1f - (1f - num2) / (1f - size_scale));
				z = move_z * ((num2 - size_scale) / (1f - size_scale));
			}
			Vector3 zero = Vector3.zero;
			zero = ((arrangement != 0) ? new Vector3(num3 / 2f, vector.y - num, z) : new Vector3(vector.x - num, num3 / 2f, z));
			if (open_fade)
			{
				TUIMeshSprite[] componentsInChildren = gameObject.GetComponentsInChildren<TUIMeshSprite>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					if (num2 >= 0.7f)
					{
						componentsInChildren[j].color = new Color(1f, 1f, 1f, 1f);
					}
					else
					{
						componentsInChildren[j].color = new Color(1f, 1f, 1f, -18.33f * num2 * num2 + 31.83f * num2 - 12.5f);
					}
				}
			}
			gameObject.transform.localScale = new Vector3(num2, num2, 1f);
			gameObject.transform.localPosition = zero;
		}
	}
}
