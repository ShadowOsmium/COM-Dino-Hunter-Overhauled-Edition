using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("TUI/Control/TUIScrollListEx")]
public class TUIScrollListCircle : TUIControlImpl
{
	protected enum CommandType
	{
		Command_Begin,
		Command_Move,
		Command_End,
		Command_Free
	}

	public List<GameObject> items_list;

	public bool open_child_event;

	public float min_x;

	public float min_y;

	public float size_scale = 0.6f;

	public float move_z;

	public float offset_y;

	public bool open_fade;

	public float circle_r = 100f;

	private List<Vector3> normal_pos_list;

	private float scroll_back_speed = 5f;

	protected int finger_id = -1;

	protected Vector2 finger_position = Vector2.zero;

	protected bool move;

	protected CommandType command_type = CommandType.Command_Free;

	public static int CommandChange;

	[SerializeField]
	private int now_index;

	public void Clear()
	{
		if (items_list != null)
		{
			for (int i = 0; i < items_list.Count; i++)
			{
				UnityEngine.Object.Destroy(items_list[i]);
			}
			items_list.Clear();
		}
		if (normal_pos_list != null)
		{
			normal_pos_list.Clear();
		}
		now_index = 0;
	}

	public void Add(GameObject go)
	{
		if (items_list == null)
		{
			items_list = new List<GameObject>();
		}
		items_list.Add(go);
		if (normal_pos_list == null)
		{
			normal_pos_list = new List<Vector3>();
		}
		normal_pos_list.Add(go.transform.localPosition);
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

	private void Awake()
	{
		if (normal_pos_list != null)
		{
			return;
		}
		normal_pos_list = new List<Vector3>();
		if (items_list == null)
		{
			return;
		}
		for (int i = 0; i < items_list.Count; i++)
		{
			GameObject gameObject = items_list[i];
			if (gameObject != null)
			{
				normal_pos_list.Add(gameObject.transform.localPosition);
			}
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
		if (command_type != CommandType.Command_Move || items_list == null || items_list.Count <= 0 || normal_pos_list == null || normal_pos_list.Count <= 0)
		{
			return;
		}
		if (wparam > 0f)
		{
			float x = items_list[0].transform.localPosition.x;
			if (x > 0f)
			{
				float num = Mathf.Abs(x) * 0.1f;
				if (num < 1f)
				{
					num = 1f;
				}
				wparam /= num;
			}
		}
		else if (wparam < 0f)
		{
			float x2 = items_list[items_list.Count - 1].transform.localPosition.x;
			if (x2 < 0f)
			{
				float num2 = Mathf.Abs(x2) * 0.1f;
				if (num2 < 1f)
				{
					num2 = 1f;
				}
				wparam /= num2;
			}
		}
		float num3 = -(float)Math.PI / 2f * circle_r;
		float num4 = (float)Math.PI / 2f * circle_r;
		for (int i = 0; i < normal_pos_list.Count; i++)
		{
			normal_pos_list[i] += new Vector3(wparam, 0f, 0f);
			if (!(items_list[i] != null) || circle_r == 0f)
			{
				continue;
			}
			float num5 = normal_pos_list[i].x;
			if (num5 < num3)
			{
				num5 = num3;
			}
			else if (num5 > num4)
			{
				num5 = num4;
			}
			float num6 = Mathf.Abs(num5) / circle_r;
			float num7 = circle_r * Mathf.Sin(num6);
			if (num5 <= 0f)
			{
				num7 = 0f - num7;
			}
			float num8 = size_scale + (1f - size_scale) * ((float)Math.PI / 2f - num6) / ((float)Math.PI / 2f);
			float num9 = offset_y * (1f - (1f - num8) / (1f - size_scale));
			float z = move_z * ((num8 - size_scale) / (1f - size_scale));
			if (open_fade)
			{
				TUIMeshSprite[] componentsInChildren = items_list[i].GetComponentsInChildren<TUIMeshSprite>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					componentsInChildren[j].color = new Color(1f, 1f, 1f, (num8 - size_scale) / (1f - size_scale));
				}
			}
			items_list[i].transform.localPosition = new Vector3(num7, num9 / 2f, z);
			items_list[i].transform.localScale = new Vector3(num8, num8, 1f);
		}
	}

	private void UpdateItems(float delta_time)
	{
		if (command_type != CommandType.Command_Free || items_list == null || items_list.Count <= 0 || normal_pos_list == null || normal_pos_list.Count <= 0)
		{
			return;
		}
		int num = 0;
		float num2 = Mathf.Abs(normal_pos_list[num].x);
		for (int i = 0; i < normal_pos_list.Count; i++)
		{
			float num3 = Mathf.Abs(normal_pos_list[i].x);
			if (num3 < num2)
			{
				num = i;
				num2 = num3;
			}
		}
		if (now_index != num)
		{
			now_index = num;
			PostEvent(this, CommandChange, 0f, 0f, null);
		}
		num2 = normal_pos_list[num].x;
		float num4 = -(float)Math.PI / 2f * circle_r;
		float num5 = (float)Math.PI / 2f * circle_r;
		for (int j = 0; j < normal_pos_list.Count; j++)
		{
			normal_pos_list[j] += new Vector3((0f - num2) * scroll_back_speed * delta_time, 0f, 0f);
			if (!(items_list[j] != null) || circle_r == 0f)
			{
				continue;
			}
			float num6 = normal_pos_list[j].x;
			if (num6 < num4)
			{
				num6 = num4;
			}
			else if (num6 > num5)
			{
				num6 = num5;
			}
			float num7 = Mathf.Abs(num6) / circle_r;
			float num8 = circle_r * Mathf.Sin(num7);
			if (num6 <= 0f)
			{
				num8 = 0f - num8;
			}
			float num9 = size_scale + (1f - size_scale) * ((float)Math.PI / 2f - num7) / ((float)Math.PI / 2f);
			float num10 = offset_y * (1f - (1f - num9) / (1f - size_scale));
			float z = move_z * ((num9 - size_scale) / (1f - size_scale));
			if (open_fade)
			{
				TUIMeshSprite[] componentsInChildren = items_list[j].GetComponentsInChildren<TUIMeshSprite>();
				for (int k = 0; k < componentsInChildren.Length; k++)
				{
					componentsInChildren[k].color = new Color(1f, 1f, 1f, (num9 - size_scale) / (1f - size_scale));
				}
			}
			items_list[j].transform.localPosition = new Vector3(num8, num10 / 2f, z);
			items_list[j].transform.localScale = new Vector3(num9, num9, 1f);
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
		if (items_list == null)
		{
			Debug.Log("error!");
		}
		else
		{
			if (items_list[now_index] == m_item)
			{
				return;
			}
			if (normal_pos_list == null || normal_pos_list.Count <= 0)
			{
				Debug.Log("error!");
				return;
			}
			float num = 0f;
			for (int i = 0; i < items_list.Count; i++)
			{
				if (items_list[i] == m_item)
				{
					now_index = i;
					num = items_list[i].transform.localPosition.x;
				}
			}
			float num2 = -(float)Math.PI / 2f * circle_r;
			float num3 = (float)Math.PI / 2f * circle_r;
			for (int j = 0; j < normal_pos_list.Count; j++)
			{
				normal_pos_list[j] += new Vector3(0f - num, 0f, 0f);
				if (!(items_list[j] != null) || circle_r == 0f)
				{
					continue;
				}
				float num4 = normal_pos_list[j].x;
				if (num4 < num2)
				{
					num4 = num2;
				}
				else if (num4 > num3)
				{
					num4 = num3;
				}
				float num5 = Mathf.Abs(num4) / circle_r;
				float num6 = circle_r * Mathf.Sin(num5);
				if (num4 <= 0f)
				{
					num6 = 0f - num6;
				}
				float num7 = size_scale + (1f - size_scale) * ((float)Math.PI / 2f - num5) / ((float)Math.PI / 2f);
				float num8 = offset_y * (1f - (1f - num7) / (1f - size_scale));
				float z = move_z * ((num7 - size_scale) / (1f - size_scale));
				if (open_fade)
				{
					TUIMeshSprite[] componentsInChildren = items_list[j].GetComponentsInChildren<TUIMeshSprite>();
					for (int k = 0; k < componentsInChildren.Length; k++)
					{
						componentsInChildren[k].color = new Color(1f, 1f, 1f, (num7 - size_scale) / (1f - size_scale));
					}
				}
				items_list[j].transform.localPosition = new Vector3(num6, num8 / 2f, z);
				items_list[j].transform.localScale = new Vector3(num7, num7, 1f);
			}
			PostEvent(this, CommandChange, 0f, 0f, null);
		}
	}

	public void SetNowItem(int m_index)
	{
		if (now_index == m_index)
		{
			return;
		}
		if (items_list == null || m_index < 0 || m_index >= items_list.Count)
		{
			Debug.Log("error!");
			return;
		}
		if (normal_pos_list == null || normal_pos_list.Count <= 0)
		{
			Debug.Log("error!");
			return;
		}
		float num = 0f;
		num = items_list[m_index].transform.localPosition.x;
		now_index = m_index;
		float num2 = -(float)Math.PI / 2f * circle_r;
		float num3 = (float)Math.PI / 2f * circle_r;
		for (int i = 0; i < normal_pos_list.Count; i++)
		{
			normal_pos_list[i] += new Vector3(0f - num, 0f, 0f);
			if (!(items_list[i] != null) || circle_r == 0f)
			{
				continue;
			}
			float num4 = normal_pos_list[i].x;
			if (num4 < num2)
			{
				num4 = num2;
			}
			else if (num4 > num3)
			{
				num4 = num3;
			}
			float num5 = Mathf.Abs(num4) / circle_r;
			float num6 = circle_r * Mathf.Sin(num5);
			if (num4 <= 0f)
			{
				num6 = 0f - num6;
			}
			float num7 = size_scale + (1f - size_scale) * ((float)Math.PI / 2f - num5) / ((float)Math.PI / 2f);
			float num8 = offset_y * (1f - (1f - num7) / (1f - size_scale));
			float z = move_z * ((num7 - size_scale) / (1f - size_scale));
			if (open_fade)
			{
				TUIMeshSprite[] componentsInChildren = items_list[i].GetComponentsInChildren<TUIMeshSprite>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					componentsInChildren[j].color = new Color(1f, 1f, 1f, (num7 - size_scale) / (1f - size_scale));
				}
			}
			items_list[i].transform.localPosition = new Vector3(num6, num8 / 2f, z);
			items_list[i].transform.localScale = new Vector3(num7, num7, 1f);
		}
		PostEvent(this, CommandChange, 0f, 0f, null);
	}

	public void SetItemList()
	{
		if (items_list != null && normal_pos_list != null && items_list.Count == normal_pos_list.Count)
		{
			for (int i = 0; i < items_list.Count; i++)
			{
				normal_pos_list[i] = items_list[i].transform.localPosition;
			}
		}
	}

	public void ResetGrid()
	{
		TUIGrid componentInChildren = base.transform.GetComponentInChildren<TUIGrid>();
		if (componentInChildren != null)
		{
			componentInChildren.Reposition();
		}
	}
}
