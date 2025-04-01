using TNetSdk;

public class nmsg_battle_result_playerrewards : nmsg_struct
{
	public int m_nTitle;

	public int m_nCombatRatings;

	public int m_nGainCrystalInGame;

	public int m_nGainGoldInGame;

	public int m_nBeadmireCount;

	public int m_nCharLvlResult;

	public int m_nCharExpResult;

	public int m_nHunterLvlResult;

	public int m_nHunterExpResult;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("title", m_nTitle);
		sFSObject.PutInt("combatratings", m_nCombatRatings);
		sFSObject.PutInt("gaincrystalingame", m_nGainCrystalInGame);
		sFSObject.PutInt("gaingoldingame", m_nGainGoldInGame);
		sFSObject.PutInt("beadmirecount", m_nBeadmireCount);
		sFSObject.PutInt("charlvl", m_nCharLvlResult);
		sFSObject.PutInt("charexp", m_nCharExpResult);
		sFSObject.PutInt("hunterlvl", m_nHunterLvlResult);
		sFSObject.PutInt("hunterexp", m_nHunterExpResult);
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_nTitle = data.GetInt("title");
		m_nCombatRatings = data.GetInt("combatratings");
		m_nGainCrystalInGame = data.GetInt("gaincrystalingame");
		m_nGainGoldInGame = data.GetInt("gaingoldingame");
		m_nBeadmireCount = data.GetInt("beadmirecount");
		m_nCharLvlResult = data.GetInt("charlvl");
		m_nCharExpResult = data.GetInt("charexp");
		m_nHunterLvlResult = data.GetInt("hunterlvl");
		m_nHunterExpResult = data.GetInt("hunterexp");
	}
}
