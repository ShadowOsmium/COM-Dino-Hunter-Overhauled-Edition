using System.Collections.Generic;

public class TUIWeaponLevelInfo
{
	public List<TUIGoodsNeedInfo> m_ltGoodsNeed;

	public TUIPriceInfo m_Price;

	public string m_sLevelupDesc = string.Empty;

	public string m_sDesc = string.Empty;

	public int m_nDamage;

	public float m_fShootRate;

	public int m_fBlastRadius;

	public int m_nKnockBack;

	public int m_nCapcity;

	public int m_nDefence;

	public TUIWeaponLevelInfo()
	{
		m_ltGoodsNeed = new List<TUIGoodsNeedInfo>();
		m_Price = new TUIPriceInfo(0, UnitType.Gold);
		m_sLevelupDesc = string.Empty;
		m_sDesc = string.Empty;
	}
}
