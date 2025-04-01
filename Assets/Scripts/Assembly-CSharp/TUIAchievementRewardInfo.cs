public class TUIAchievementRewardInfo
{
	public bool open_reward01;

	public int reward_value01;

	public UnitType reward_unit01;

	public bool open_reward02;

	public int reward_value02;

	public UnitType reward_unit02;

	public void SetRewardInfo01(int m_value, UnitType m_type)
	{
		open_reward01 = true;
		reward_value01 = m_value;
		reward_unit01 = m_type;
	}

	public void SetRewardInfo02(int m_value, UnitType m_type)
	{
		open_reward02 = true;
		reward_value02 = m_value;
		reward_unit02 = m_type;
	}
}
