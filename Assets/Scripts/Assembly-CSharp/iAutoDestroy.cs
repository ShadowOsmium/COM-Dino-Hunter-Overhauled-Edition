using UnityEngine;

public class iAutoDestroy : MonoBehaviour
{
	public float time;

	private void Start()
	{
		if (time > 0f)
		{
			Object.Destroy(base.gameObject, time);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void Update()
	{
	}
}
