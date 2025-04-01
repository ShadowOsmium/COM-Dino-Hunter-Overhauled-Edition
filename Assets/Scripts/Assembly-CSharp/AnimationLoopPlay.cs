using UnityEngine;

public class AnimationLoopPlay : MonoBehaviour
{
	public Animation ani_myseft;

	private void Start()
	{
		if (ani_myseft != null)
		{
			ani_myseft.wrapMode = WrapMode.Loop;
			ani_myseft.Play();
		}
	}

	private void Update()
	{
	}
}
