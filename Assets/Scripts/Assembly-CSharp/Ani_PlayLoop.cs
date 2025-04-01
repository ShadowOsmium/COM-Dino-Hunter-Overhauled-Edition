using UnityEngine;

public class Ani_PlayLoop : MonoBehaviour
{
	private void Start()
	{
		if (base.GetComponent<Animation>() != null)
		{
			base.GetComponent<Animation>().wrapMode = WrapMode.Loop;
			base.GetComponent<Animation>().Play();
		}
	}

	private void Update()
	{
	}
}
