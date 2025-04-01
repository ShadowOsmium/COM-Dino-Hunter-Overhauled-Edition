using UnityEngine;

public class LevelPointCoop : MonoBehaviour
{
	public GameObject drop_sign;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ShowDropSign(bool m_show)
	{
		if (drop_sign == null)
		{
			Debug.Log("error!");
		}
		else
		{
			drop_sign.SetActiveRecursively(m_show);
		}
	}
}
