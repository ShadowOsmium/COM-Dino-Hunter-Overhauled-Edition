using UnityEngine;

[RequireComponent(typeof(gyUIAnimationBump))]
public class gyUIPanelMissionMutiply_BeadmireCount : MonoBehaviour
{
	protected gyUIAnimationBump m_AnimScript;

	protected UILabel m_Label;

	protected int m_nValue;

	public int Value
	{
		get
		{
			return m_nValue;
		}
		set
		{
			m_nValue = value;
			if (m_Label != null)
			{
				m_Label.text = m_nValue.ToString();
			}
			if (m_AnimScript != null)
			{
				m_AnimScript.Go();
			}
		}
	}

	private void Awake()
	{
		m_AnimScript = GetComponent<gyUIAnimationBump>();
		m_Label = GetComponent<UILabel>();
	}
}
