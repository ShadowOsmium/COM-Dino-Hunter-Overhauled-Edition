using UnityEngine;

public class PopupReciprocal : MonoBehaviour
{
	private const float time_gap = 1f;

	public TUILabel label_reciprocal;

	private bool open_reciprocal;

	private float time_now;

	private int text_count;

	private bool reciprocal_over;

	private void Start()
	{
		StartReciprocal(false);
	}

	private void Update()
	{
		UpdateReciprocal();
	}

	public void StartReciprocal(bool m_open_reciprocal)
	{
		open_reciprocal = m_open_reciprocal;
		if (open_reciprocal)
		{
			base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
			text_count = 4;
			time_now = 0f;
			if (label_reciprocal != null)
			{
				label_reciprocal.Text = string.Empty;
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(0f, -1000f, base.transform.localPosition.z);
		}
	}

	private void UpdateReciprocal()
	{
		if (!open_reciprocal)
		{
			return;
		}
		time_now += Time.deltaTime;
		if (!(time_now > 1f))
		{
			return;
		}
		time_now = 0f;
		text_count--;
		Debug.Log("Reciprocal:" + text_count);
		if (text_count > 0)
		{
			if (label_reciprocal != null)
			{
				label_reciprocal.Text = text_count.ToString();
				if (label_reciprocal.GetComponent<Animation>() != null)
				{
					label_reciprocal.GetComponent<Animation>().Play();
				}
			}
		}
		else
		{
			reciprocal_over = true;
			open_reciprocal = false;
			base.transform.localPosition = new Vector3(0f, -1000f, base.transform.localPosition.z);
		}
	}

	public bool GetReciprocalOver()
	{
		if (reciprocal_over)
		{
			reciprocal_over = false;
			return true;
		}
		return false;
	}
}
