using UnityEngine;

public class gyUIScreenTouch : MonoBehaviour
{
	private void Start()
	{
		base.transform.localScale = new Vector3(Screen.width, Screen.height, 1f);
	}

	private void Update()
	{
	}
}
