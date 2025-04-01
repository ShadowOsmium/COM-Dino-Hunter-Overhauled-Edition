using UnityEngine;

public class gyUITutorialsPanel : MonoBehaviour
{
	public GameObject mMask;

	public GameObject[] arrTutorials;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Show(bool bShow)
	{
		base.gameObject.SetActiveRecursively(bShow);
		if (arrTutorials == null)
		{
			return;
		}
		for (int i = 0; i < arrTutorials.Length; i++)
		{
			if (arrTutorials[i] != null)
			{
				arrTutorials[i].SetActiveRecursively(bShow);
			}
		}
	}
}
