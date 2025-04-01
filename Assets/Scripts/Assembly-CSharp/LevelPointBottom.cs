using System;
using UnityEngine;

public class LevelPointBottom : MonoBehaviour
{
	private TUIMeshSprite m_sprite;

	private string texture_unchoose = "zhurenwudian";

	private string texture_choose = "sucaisousuodian";

	private bool be_choose;

	private Vector3 normal_scale;

	private float m_time;

	private void Awake()
	{
		m_sprite = base.gameObject.GetComponent<TUIMeshSprite>();
		normal_scale = base.transform.localScale;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (be_choose)
		{
			m_time += Time.deltaTime;
			float num = Mathf.Sin((float)Math.PI * 4f * m_time) * 0.15f;
			base.transform.localScale = normal_scale + new Vector3(num, num, 0f);
		}
	}

	public void OpenChoose(bool m_bool)
	{
		if (m_sprite == null)
		{
			m_sprite = base.gameObject.GetComponent<TUIMeshSprite>();
			if (m_sprite == null)
			{
				Debug.Log("error! no found sprite! id:");
				return;
			}
		}
		base.gameObject.SetActiveRecursively(true);
		if (m_bool)
		{
			m_sprite.texture = texture_choose;
			be_choose = true;
			return;
		}
		m_sprite.texture = texture_unchoose;
		be_choose = false;
		base.transform.localScale = normal_scale;
		m_time = 0f;
	}

	public void Hide()
	{
		base.gameObject.SetActiveRecursively(false);
	}
}
