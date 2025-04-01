using UnityEngine;

public class gyUIPlayerRewards : MonoBehaviour
{
	public gyUIButton m_Admire;

	public gyUIResultExpBar m_CharExp;

	public gyUIResultExpBar m_HunterExp;

	public UILabel m_PlayerName;

	public UILabel m_PlayerTitle;

	public UITexture m_PlayerPhoto;

	public UISprite m_PlayerDeathFlag;

	public gyUIPanelMissionMutiply_HopNumber m_Gold;

	public gyUIPanelMissionMutiply_HopNumber m_Crystal;

	public gyUIPanelMissionMutiply_HopNumber m_BattleRate;

	public gyUIPanelMissionMutiply_BeadmireCount m_AdmireCount;

	public bool IsAnim
	{
		get
		{
			if (m_CharExp != null && m_CharExp.IsAnim)
			{
				return true;
			}
			if (m_HunterExp != null && m_HunterExp.IsAnim)
			{
				return true;
			}
			if (m_Gold != null && m_Gold.IsAnim)
			{
				return true;
			}
			if (m_Crystal != null && m_Crystal.IsAnim)
			{
				return true;
			}
			if (m_BattleRate != null && m_BattleRate.IsAnim)
			{
				return true;
			}
			return false;
		}
	}

	public void Show(bool bShow)
	{
		base.gameObject.SetActiveRecursively(bShow);
	}

	public bool IsShow()
	{
		return base.gameObject.active;
	}
}
