using UnityEngine;

public class iEntityDrop : MonoBehaviour
{
	protected bool m_bActive;

	private void Start()
	{
	}

	private void Update()
	{
		if (m_bActive && base.transform.parent != null)
		{
			Vector3 localPosition = base.transform.localPosition;
			localPosition.y -= Time.deltaTime * 10f;
			if (localPosition.y < 0f)
			{
				localPosition.y = 0f;
				m_bActive = false;
			}
			base.transform.localPosition = localPosition;
		}
	}

	public void iEntityDrop_Go()
	{
		m_bActive = true;
	}
}
