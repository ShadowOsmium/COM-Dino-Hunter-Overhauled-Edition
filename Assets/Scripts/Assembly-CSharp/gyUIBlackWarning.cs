using UnityEngine;

public class gyUIBlackWarning : MonoBehaviour
{
	public UILabel[] m_arrLabel;

	public UISprite m_Icon;

	private void Awake()
	{
		Show(false);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Show(bool bShow)
	{
		base.gameObject.SetActiveRecursively(bShow);
	}

	public void SetIcon(string sName)
	{
		if (!(m_Icon == null))
		{
			GameObject gameObject = PrefabManager.Get("Artist/Atlas/Weapon/" + sName);
			if (gameObject != null)
			{
				m_Icon.atlas = gameObject.GetComponent<UIAtlas>();
			}
		}
	}
}
