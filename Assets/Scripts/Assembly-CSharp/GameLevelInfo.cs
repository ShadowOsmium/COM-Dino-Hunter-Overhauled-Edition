using System.Collections.Generic;

public class GameLevelInfo
{
	public int nID;

	public string sSceneName = string.Empty;

	public bool bIsSkyScene;

	public string sLevelName = string.Empty;

	public string sLevelDesc = string.Empty;

	public string sIcon = string.Empty;

	public float fNavPlane;

	public int nBirthPos;

	public List<int> ltGameWave;

	public int nDefaultSPGround;

	public int nDefaultSPSky;

	public List<StartPointTrigger> ltSPTrigger;

	public int nDefaultHoverPoint;

	public List<StartPointTrigger> ltHPTrigger;

	public List<MonsterNumLimitInfo> ltMonsterNumLimit;

	public int nTPBeginCfg;

	public int nTPEndCfg;

	public int nTaskID;

	public int nRewardExp;

	public int nRewardGold;

	public List<CRewardMaterial> ltRewardMaterial;

	public string sCutScene = string.Empty;

	public string sCutSceneContent = string.Empty;

	public string sCutSceneAmbience = string.Empty;

	public string sBGM = string.Empty;

	public string sBGMAmbience = string.Empty;

	public int m_nRecommandType;

	public int m_nRecommandID;

	public int m_nRecommandLevel;

	public bool m_bRecommandLimit;

	public int m_nProccess;

	public bool m_bLimitMelee;

	public GameLevelInfo()
	{
		ltGameWave = new List<int>();
		ltSPTrigger = new List<StartPointTrigger>();
		ltHPTrigger = new List<StartPointTrigger>();
		ltMonsterNumLimit = new List<MonsterNumLimitInfo>();
		sCutScene = string.Empty;
		sCutSceneContent = string.Empty;
		sCutSceneAmbience = string.Empty;
		sBGM = string.Empty;
		sBGMAmbience = string.Empty;
		ltRewardMaterial = new List<CRewardMaterial>();
	}
}
