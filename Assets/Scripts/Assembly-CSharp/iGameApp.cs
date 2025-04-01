using System.Collections.Generic;
using gyAchievementSystem;
using UnityEngine;

public class iGameApp
{
	public static iGameApp m_Instance;

	public DebugGUI m_Debug;

	public iGizmos m_Gizmos;

	public iGameSceneBase m_GameScene;

	public iGameState m_GameState;

	public iGameData m_GameData;

	public iClearMemory m_ClearMemory;

	public static iGameApp GetInstance()
	{
		if (m_Instance == null)
		{
			m_Instance = new iGameApp();
			m_Instance.Initialize();
		}
		return m_Instance;
	}

	public void Initialize()
	{
		MyUtils.SimulatePlatform = PlatformEnum.Android;
		GameObject gameObject = new GameObject("_GizmosManager");
		if (gameObject != null)
		{
			gameObject.transform.position = Vector3.zero;
			gameObject.transform.rotation = Quaternion.identity;
			m_Gizmos = gameObject.AddComponent<iGizmos>();
		}
		gameObject = new GameObject("_DebugGUI");
		if (gameObject != null)
		{
			gameObject.transform.position = Vector3.zero;
			gameObject.transform.rotation = Quaternion.identity;
			m_Debug = gameObject.AddComponent<DebugGUI>();
		}
		gameObject = new GameObject("_ClearMemoryObject");
		if (gameObject != null)
		{
			gameObject.transform.position = Vector3.zero;
			gameObject.transform.rotation = Quaternion.identity;
			m_ClearMemory = gameObject.AddComponent<iClearMemory>();
		}
		PrefabManager.Initialize();
		m_GameData = new iGameData();
		m_GameData.Load();
		m_GameState = new iGameState();
		m_GameState.Initialize();
		CheckUnLock(true);
		Screen.orientation = ScreenOrientation.AutoRotation;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		CFlurryManager.GetInstance().Initialize("TWV7XMZQQKTHZ38ZH93V");
		iTrinitiDataCollect.GetInstance().SetAppSymbol(iMacroDefine.BundleID + "android");
		iTrinitiDataCollect.GetInstance().SetUserSymbol(MyUtils.GetUUID());
		CLocalNotification.GetInstance().AddLocalNotification("Grab your weapon and hunt dinosaurs for rewards!", iMacroDefine.SecondsOneDay * 3);
		CLocalNotification.GetInstance().AddLocalNotification("Grab your weapon and hunt dinosaurs for rewards!", iMacroDefine.SecondsOneDay * 7);
		CLocalNotification.GetInstance().AddLocalNotification("Your villagers need you to help them forge weapons against dinosaurs!", iMacroDefine.SecondsOneDay * 14);
		CLocalNotification.GetInstance().AddLocalNotification("Dinosaurs are running wild! Return to tame those fierce beats!", iMacroDefine.SecondsOneDay * 21);
	}

	public void Destroy()
	{
	}

	public string GetKey()
	{
		return "fuckbreakitandbethesuperman";
	}

	public void EnterScene(string sName)
	{
		Time.timeScale = 1f;
		AndroidReturnPlugin.instance.Clear();
		m_GameState.CurScene = kGameSceneEnum.OutOfGame;
		m_GameState.m_sLoadScene = sName;
		Application.LoadLevel("SceneLoad");
	}

	public void EnterScene(kGameSceneEnum gotoscene)
	{
		Time.timeScale = 1f;
		AndroidReturnPlugin.instance.Clear();
		if (m_GameState.CurScene == kGameSceneEnum.Game)
		{
			DestroyScene();
		}
		Debug.Log("play theme " + gotoscene);
		switch (gotoscene)
		{
		case kGameSceneEnum.Game:
		{
			CUISound.GetInstance().Stop("BGM_theme");
			GameLevelInfo gameLevelInfo = m_GameData.GetGameLevelInfo(m_GameState.GameLevel);
			if (gameLevelInfo != null)
			{
				m_GameState.CurScene = kGameSceneEnum.Game;
				m_GameState.m_sLoadScene = gameLevelInfo.sSceneName;
				Application.LoadLevel("SceneLoad");
			}
			break;
		}
		case kGameSceneEnum.Map:
			CUISound.GetInstance().Play("BGM_theme");
			m_GameState.CurScene = kGameSceneEnum.Map;
			m_GameState.m_sLoadScene = "Scene_Map";
			Application.LoadLevel("SceneLoad");
			break;
		case kGameSceneEnum.MutipyHome:
			CUISound.GetInstance().Play("BGM_theme");
			m_GameState.CurScene = kGameSceneEnum.MutipyHome;
			m_GameState.m_sLoadScene = "Scene_CoopMainMenu";
			Application.LoadLevel("SceneLoad");
			break;
		case kGameSceneEnum.Home:
			CUISound.GetInstance().Play("BGM_theme");
			m_GameState.CurScene = kGameSceneEnum.Home;
			m_GameState.m_sLoadScene = "Scene_MainMenu";
			Application.LoadLevel("SceneLoad");
			break;
		}
	}

	public void CreateScene()
	{
		iGameData gameData = GetInstance().m_GameData;
		if (gameData == null)
		{
			return;
		}
		iDataCenter dataCenter = gameData.GetDataCenter();
		if (dataCenter != null)
		{
			m_GameState.Clear();
			for (int i = 0; i < 3; i++)
			{
				CarryWeapon(i, dataCenter.GetSelectWeapon(i));
			}
			int gameLevel = m_GameState.GameLevel;
			switch (gameLevel)
			{
			case 0:
				m_GameScene = new iGameScene0();
				break;
			case 1:
				m_GameScene = new iGameScene1();
				break;
			case 2:
				m_GameScene = new iGameScene2();
				break;
			default:
				m_GameScene = new iGameSceneBase();
				break;
			}
			if (m_GameScene != null)
			{
				m_GameScene.Initialize();
				m_GameScene.InitializeGameLevel(gameLevel);
				m_GameScene.StartGame();
				PrefabManager.PreLoad();
			}
		}
	}

	public void DestroyScene()
	{
		if (m_GameScene != null)
		{
			m_GameScene.Destroy();
			m_GameScene = null;
			PrefabManager.DestroyPreLoad();
			PrefabManager.DestroyAll();
		}
	}

	public void ResetScene()
	{
	}

	public void Update(float deltaTime)
	{
		if (m_GameScene != null)
		{
			m_GameScene.Update(deltaTime);
		}
	}

	public void FixedUpdate(float deltaTime)
	{
		if (m_GameScene != null)
		{
			m_GameScene.FixedUpdate(deltaTime);
		}
	}

	public void LateUpdate(float deltaTime)
	{
		if (m_GameScene != null)
		{
			m_GameScene.LateUpdate(deltaTime);
		}
	}

	public void CarryWeapon(int nIndex, int nWeaponID)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		int nLevel = 0;
		if (!dataCenter.GetWeaponLevel(nWeaponID, ref nLevel) || nLevel == -1)
		{
			return;
		}
		CWeaponInfoLevel weaponInfo = m_GameData.GetWeaponInfo(nWeaponID, nLevel);
		if (weaponInfo != null)
		{
			CWeaponBase cWeaponBase = null;
			switch (weaponInfo.nAttackMode)
			{
			case 1:
				cWeaponBase = new CWeaponMelee();
				break;
			case 2:
				cWeaponBase = new CWeaponShoot();
				break;
			case 3:
				cWeaponBase = new CWeaponSpawn();
				break;
			case 4:
				cWeaponBase = new CWeaponSpawnWithHead();
				break;
			case 5:
				cWeaponBase = new CWeaponHoldy();
				break;
			case 6:
				cWeaponBase = new CWeaponShotgun();
				break;
			}
			if (cWeaponBase != null)
			{
				cWeaponBase.Initialize(nWeaponID, nLevel);
				m_GameState.CarryWeapon(nIndex, cWeaponBase);
			}
		}
	}

	public void SetGizmosPoint(string sKey, Vector3 p, Color color)
	{
		if (!(m_Gizmos == null))
		{
			m_Gizmos.SetPoint(sKey, p, color);
		}
	}

	public void SetGizmosLine(string sKey, Vector3 p1, Vector3 p2, Color color)
	{
		if (!(m_Gizmos == null))
		{
			m_Gizmos.SetLine(sKey, p1, p2, color);
		}
	}

	public void SetGizmosRay(string sKey, Vector3 p, Vector3 dir, Color color)
	{
		if (!(m_Gizmos == null))
		{
			m_Gizmos.SetRay(sKey, p, dir, color);
		}
	}

	public void ScreenLog(string str)
	{
		m_Debug.Debug(str);
	}

	public void ClearScreenLog()
	{
		if (!(m_Debug == null))
		{
			m_Debug.Clear();
		}
	}

	public void ClearMemory()
	{
		if (!(m_ClearMemory == null))
		{
			m_ClearMemory.ClearMemory();
		}
	}

	public void CheckUnLock(bool bFirst = false)
	{
		if (m_GameData == null || m_GameState == null)
		{
			return;
		}
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null || !m_GameState.isCheckUnLock)
		{
			return;
		}
		m_GameState.isCheckUnLock = false;
		bool flag = false;
		iCharacterCenter characterCenter = m_GameData.GetCharacterCenter();
		if (characterCenter != null)
		{
			Dictionary<int, CCharacterInfo> data = characterCenter.GetData();
			if (data != null)
			{
				foreach (CCharacterInfo value in data.Values)
				{
					if (dataCenter.GetCharacter(value.nID) == null && dataCenter.IsLevelPassed(value.nUnLockLevel))
					{
						dataCenter.SetCharacterSign(value.nID, 1);
						dataCenter.UnlockCharacter(value.nID);
						if (!bFirst)
						{
							dataCenter.AddUnlockSign(1, value.nID);
						}
						flag = true;
						ScreenLog("unlock character " + value.nID);
					}
				}
			}
		}
		int[] array = new int[6] { 1, 2, 3, 4, 5, 6 };
		for (int i = 0; i < array.Length; i++)
		{
			CCharSaveInfo character = dataCenter.GetCharacter(array[i]);
			if (character == null || character.nLevel == -1)
			{
				continue;
			}
			CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(array[i]);
			if (characterInfo == null || characterInfo.ltCharacterPassiveSkill == null)
			{
				continue;
			}
			foreach (int item in characterInfo.ltCharacterPassiveSkill)
			{
				CSkillInfo skillInfo = m_GameData.GetSkillInfo(item);
				if (skillInfo == null)
				{
					continue;
				}
				int nSkillLevel = 0;
				if (!dataCenter.GetPassiveSkill(skillInfo.nID, ref nSkillLevel) && character.nLevel >= skillInfo.nUnlockLevel)
				{
					dataCenter.SetSkillSign(skillInfo.nID, 1);
					dataCenter.UnlockPassiveSkill(skillInfo.nID);
					if (!bFirst)
					{
						dataCenter.AddUnlockSign(2, skillInfo.nID);
					}
					flag = true;
					ScreenLog("unlock skill " + skillInfo.nID);
				}
			}
		}
		iWeaponCenter weaponCenter = m_GameData.GetWeaponCenter();
		if (weaponCenter != null)
		{
			Dictionary<int, CWeaponInfo> data2 = weaponCenter.GetData();
			if (data2 != null)
			{
				foreach (CWeaponInfo value2 in data2.Values)
				{
					int nLevel = 0;
					if (!dataCenter.GetWeaponLevel(value2.nID, ref nLevel) && ((value2.m_nUnlockStageID > 0 && dataCenter.IsLevelPassed(value2.m_nUnlockStageID)) || (value2.m_nUnlockHunterLvl > 0 && dataCenter.HunterLvl >= value2.m_nUnlockHunterLvl)))
					{
						dataCenter.SetWeaponSign(value2.nID, 1);
						dataCenter.UnlockWeapon(value2.nID);
						if (!bFirst)
						{
							dataCenter.AddUnlockSign(3, value2.nID);
						}
						flag = true;
						ScreenLog("sign weapon " + value2.nID);
					}
				}
			}
		}
		if (m_GameData.m_AvatarCenter != null)
		{
			Dictionary<int, CAvatarInfo> data3 = m_GameData.m_AvatarCenter.GetData();
			if (data3 != null)
			{
				foreach (CAvatarInfo value3 in data3.Values)
				{
					int avatarlevel = -1;
					if (!dataCenter.GetAvatar(value3.m_nID, ref avatarlevel) && ((value3.m_nUnlockStageID > 0 && dataCenter.IsLevelPassed(value3.m_nUnlockStageID)) || (value3.m_nUnlockHunterLvl > 0 && dataCenter.HunterLvl >= value3.m_nUnlockHunterLvl)))
					{
						dataCenter.SetAvatarSign(value3.m_nID, 1);
						dataCenter.SetAvatar(value3.m_nID, -1);
						if (!bFirst)
						{
							dataCenter.AddUnlockSign(4, value3.m_nID);
						}
						flag = true;
						ScreenLog("sign avatar " + value3.m_nID);
					}
				}
			}
		}
		if (m_GameData.m_TitleCenter != null)
		{
			Dictionary<int, CTitleInfo> data4 = m_GameData.m_TitleCenter.GetData();
			if (data4 != null)
			{
				foreach (CTitleInfo value4 in data4.Values)
				{
					if (dataCenter.GetTitle(value4.nID))
					{
						continue;
					}
					bool flag2 = false;
					switch (value4.nConditionType)
					{
					case 6:
						if (dataCenter.BeAdmire >= value4.nConditionValueX)
						{
							flag2 = true;
						}
						break;
					case 2:
						if (dataCenter.CombatPower >= value4.nConditionValueX)
						{
							flag2 = true;
						}
						break;
					case 4:
						if (dataCenter.Crystal >= value4.nConditionValueX)
						{
							flag2 = true;
						}
						break;
					case 3:
						if (dataCenter.Gold >= value4.nConditionValueX)
						{
							flag2 = true;
						}
						break;
					case 1:
						if (dataCenter.HunterLvl >= value4.nConditionValueX)
						{
							flag2 = true;
						}
						break;
					case 9:
						if (dataCenter.GetKillMonster(value4.nConditionValueX) >= value4.nConditionValueY)
						{
							flag2 = true;
						}
						break;
					case 5:
						if (dataCenter.MVPCount >= value4.nConditionValueX)
						{
							flag2 = true;
						}
						break;
					case 7:
						if (dataCenter.ReviveInCoopCount >= value4.nConditionValueX)
						{
							flag2 = true;
						}
						break;
					case 8:
						if (dataCenter.SceneProccess >= (float)value4.nConditionValueX)
						{
							flag2 = true;
						}
						break;
					case 10:
						if (dataCenter.DeadInCoopCount >= value4.nConditionValueX)
						{
							flag2 = true;
						}
						break;
					}
					if (flag2)
					{
						dataCenter.AddTitle(value4.nID);
						if (!bFirst)
						{
							dataCenter.AddUnlockSign(5, value4.nID);
						}
					}
				}
			}
		}
		if (flag)
		{
			ScreenLog("===============================");
			dataCenter.Save();
		}
	}

	public bool CheckAchieveReward()
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return false;
		}
		Dictionary<int, CAchievementData> achiDataData = dataCenter.GetAchiDataData();
		if (achiDataData == null)
		{
			return false;
		}
		foreach (CAchievementData value in achiDataData.Values)
		{
			int achiStar = CAchievementManager.GetInstance().GetAchiStar(value.nID);
			if (achiStar == 0)
			{
				continue;
			}
			CAchievementInfo achievementInfo = m_GameData.GetAchievementInfo(value.nID);
			if (achievementInfo != null && !achievementInfo.isDaily)
			{
				UnityEngine.Debug.Log(value.nID + " has " + achiStar + " stars");
				if (!value.IsGotReward(achiStar - 1))
				{
					UnityEngine.Debug.Log("You has not take reward " + (achiStar - 1));
					return true;
				}
			}
		}
		return false;
	}

	public bool CheckDailyAchieveReward()
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return false;
		}
		List<int> dailyTask = dataCenter.GetDailyTask();
		if (dailyTask == null)
		{
			return false;
		}
		foreach (int item in dailyTask)
		{
			CAchievementInfo achievementInfo = m_GameData.GetAchievementInfo(item);
			if (achievementInfo == null || !achievementInfo.isDaily)
			{
				continue;
			}
			CAchievementData achiData = dataCenter.GetAchiData(item);
			if (achiData != null)
			{
				CAchievementStep step = achievementInfo.GetStep(0);
				if (step != null && !achiData.IsGotReward(0) && achiData.nCurValue >= step.nStepPurpose)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool CheckWeaponMaterialEnough(int nWeaponID = -1)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return false;
		}
		if (nWeaponID != -1)
		{
			CWeaponInfo weaponInfo = m_GameData.GetWeaponInfo(nWeaponID);
			if (weaponInfo == null)
			{
				return false;
			}
			CLevelUpWeapon cLevelUpWeapon = new CLevelUpWeapon();
			if (cLevelUpWeapon == null)
			{
				return false;
			}
			return cLevelUpWeapon.CheckCanUpgrade(nWeaponID, 1f);
		}
		iWeaponCenter weaponCenter = m_GameData.GetWeaponCenter();
		if (weaponCenter == null)
		{
			return false;
		}
		Dictionary<int, CWeaponInfo> data = weaponCenter.GetData();
		if (data == null)
		{
			return false;
		}
		CLevelUpWeapon cLevelUpWeapon2 = new CLevelUpWeapon();
		if (cLevelUpWeapon2 == null)
		{
			return false;
		}
		foreach (CWeaponInfo value in data.Values)
		{
			if (cLevelUpWeapon2.CheckCanUpgrade(value.nID, 1f))
			{
				return true;
			}
		}
		return false;
	}

	public bool CheckAvatarMaterialEnough(int nAvatarID = -1)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return false;
		}
		if (nAvatarID != -1)
		{
			CAvatarInfo cAvatarInfo = m_GameData.m_AvatarCenter.Get(nAvatarID);
			if (cAvatarInfo == null)
			{
				return false;
			}
			CLevelUpAvatar cLevelUpAvatar = new CLevelUpAvatar();
			if (cLevelUpAvatar == null)
			{
				return false;
			}
			return cLevelUpAvatar.CheckCanUpgrade(nAvatarID, 1f);
		}
		Dictionary<int, CAvatarInfo> data = m_GameData.m_AvatarCenter.GetData();
		if (data == null)
		{
			return false;
		}
		CLevelUpAvatar cLevelUpAvatar2 = new CLevelUpAvatar();
		if (cLevelUpAvatar2 == null)
		{
			return false;
		}
		foreach (CAvatarInfo value in data.Values)
		{
			if (cLevelUpAvatar2.CheckCanUpgrade(value.m_nID, 1f))
			{
				return true;
			}
		}
		return false;
	}

	public bool CheckEquipStoneMaterialEnough(int nEquip = -1)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return false;
		}
		if (nEquip != -1)
		{
			CItemInfo itemInfo = m_GameData.GetItemInfo(nEquip);
			if (itemInfo == null)
			{
				return false;
			}
			return false;
		}
		iItemCenter itemCenter = m_GameData.GetItemCenter();
		if (itemCenter == null)
		{
			return false;
		}
		Dictionary<int, CItemInfo> data = itemCenter.GetData();
		if (data == null)
		{
			return false;
		}
		return false;
	}

	public bool CheckCharacterMaterialEnough(int nCharacterID = -1)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return false;
		}
		if (nCharacterID != -1)
		{
			CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(nCharacterID);
			if (characterInfo == null)
			{
				return false;
			}
			CCharSaveInfo character = dataCenter.GetCharacter(nCharacterID);
			if (character == null || character.nLevel != -1)
			{
				return false;
			}
			if ((characterInfo.isCrystalPurchase && dataCenter.Crystal < characterInfo.nPurchasePrice) || (!characterInfo.isCrystalPurchase && dataCenter.Gold < characterInfo.nPurchasePrice))
			{
				return false;
			}
			return true;
		}
		iCharacterCenter characterCenter = m_GameData.GetCharacterCenter();
		if (characterCenter == null)
		{
			return false;
		}
		Dictionary<int, CCharacterInfo> data = characterCenter.GetData();
		if (data == null)
		{
			return false;
		}
		foreach (CCharacterInfo value in data.Values)
		{
			CCharSaveInfo character2 = dataCenter.GetCharacter(value.nID);
			if (character2 == null || character2.nLevel != -1 || ((!value.isCrystalPurchase || dataCenter.Crystal < value.nPurchasePrice) && (value.isCrystalPurchase || dataCenter.Gold < value.nPurchasePrice)))
			{
				continue;
			}
			return true;
		}
		return false;
	}

	public bool CheckSkillMaterialEnough(int nSkillID = -1)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return false;
		}
		if (nSkillID != -1)
		{
			CSkillInfo skillInfo = m_GameData.GetSkillInfo(nSkillID);
			if (skillInfo == null)
			{
				return false;
			}
			int nSkillLevel = 0;
			if (!dataCenter.GetPassiveSkill(nSkillID, ref nSkillLevel))
			{
				return false;
			}
			int nLevel = ((nSkillLevel == -1) ? 1 : (nSkillLevel + 1));
			CSkillInfoLevel cSkillInfoLevel = skillInfo.Get(nLevel);
			if (cSkillInfoLevel == null)
			{
				return false;
			}
			if ((cSkillInfoLevel.isCrystalPurchase && dataCenter.Crystal < cSkillInfoLevel.nPurchasePrice) || (!cSkillInfoLevel.isCrystalPurchase && dataCenter.Gold < cSkillInfoLevel.nPurchasePrice))
			{
				return false;
			}
			return true;
		}
		iSkillCenter skillCenter = m_GameData.GetSkillCenter();
		if (skillCenter == null)
		{
			return false;
		}
		Dictionary<int, CSkillInfo> dataSkillInfo = skillCenter.GetDataSkillInfo();
		if (dataSkillInfo == null)
		{
			return false;
		}
		foreach (CSkillInfo value in dataSkillInfo.Values)
		{
			int nSkillLevel2 = 0;
			if (dataCenter.GetPassiveSkill(value.nID, ref nSkillLevel2))
			{
				int nLevel2 = ((nSkillLevel2 == -1) ? 1 : (nSkillLevel2 + 1));
				CSkillInfoLevel cSkillInfoLevel2 = value.Get(nLevel2);
				if (cSkillInfoLevel2 != null && ((cSkillInfoLevel2.isCrystalPurchase && dataCenter.Crystal >= cSkillInfoLevel2.nPurchasePrice) || (!cSkillInfoLevel2.isCrystalPurchase && dataCenter.Gold >= cSkillInfoLevel2.nPurchasePrice)))
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool CheckWeaponSignState(int nSignState, int nWeaponID = -1)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return false;
		}
		if (nWeaponID != -1)
		{
			int nSignState2 = 0;
			if (!dataCenter.GetWeaponSign(nWeaponID, ref nSignState2) || nSignState2 != nSignState)
			{
				return false;
			}
			return true;
		}
		iWeaponCenter weaponCenter = m_GameData.GetWeaponCenter();
		if (weaponCenter == null)
		{
			return false;
		}
		Dictionary<int, CWeaponInfo> data = weaponCenter.GetData();
		if (data == null)
		{
			return false;
		}
		foreach (CWeaponInfo value in data.Values)
		{
			int nSignState3 = 0;
			if (dataCenter.GetWeaponSign(value.nID, ref nSignState3) && nSignState3 == nSignState)
			{
				return true;
			}
		}
		return false;
	}

	public bool CheckAvatarSignState(int nSignState, int nAvatarID = -1)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return false;
		}
		if (nAvatarID != -1)
		{
			int nSignState2 = 0;
			if (!dataCenter.GetAvatarSign(nAvatarID, ref nSignState2) || nSignState2 != nSignState)
			{
				return false;
			}
			return true;
		}
		if (m_GameData.m_AvatarCenter == null)
		{
			return false;
		}
		Dictionary<int, CAvatarInfo> data = m_GameData.m_AvatarCenter.GetData();
		if (data == null)
		{
			return false;
		}
		foreach (CAvatarInfo value in data.Values)
		{
			int nSignState3 = 0;
			if (dataCenter.GetAvatarSign(value.m_nID, ref nSignState3) && nSignState3 == nSignState)
			{
				return true;
			}
		}
		return false;
	}

	public bool CheckCharacterSignState(int nSignState, int nCharacterID = -1)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return false;
		}
		if (nCharacterID != -1)
		{
			int nSignState2 = 0;
			if (!dataCenter.GetCharacterSign(nCharacterID, ref nSignState2) || nSignState2 != nSignState)
			{
				return false;
			}
			return true;
		}
		iCharacterCenter characterCenter = m_GameData.GetCharacterCenter();
		if (characterCenter == null)
		{
			return false;
		}
		Dictionary<int, CCharacterInfo> data = characterCenter.GetData();
		if (data == null)
		{
			return false;
		}
		foreach (CCharacterInfo value in data.Values)
		{
			int nSignState3 = 0;
			if (dataCenter.GetCharacterSign(value.nID, ref nSignState3) && nSignState3 == nSignState)
			{
				return true;
			}
		}
		return false;
	}

	public bool CheckSkillSignState(int nSignState, int nSkillID = -1)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return false;
		}
		if (nSkillID != -1)
		{
			int nSignState2 = 0;
			if (!dataCenter.GetSkillSign(nSkillID, ref nSignState2) || nSignState2 != nSignState)
			{
				return false;
			}
			return true;
		}
		int[] array = new int[6] { 1, 2, 3, 4, 5, 6 };
		for (int i = 0; i < array.Length; i++)
		{
			CCharSaveInfo character = dataCenter.GetCharacter(array[i]);
			if (character == null)
			{
				continue;
			}
			CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(array[i]);
			if (characterInfo == null || characterInfo.ltCharacterPassiveSkill == null)
			{
				continue;
			}
			foreach (int item in characterInfo.ltCharacterPassiveSkill)
			{
				CSkillInfo skillInfo = m_GameData.GetSkillInfo(item);
				if (skillInfo == null)
				{
					continue;
				}
				CSkillInfoLevel cSkillInfoLevel = skillInfo.Get(1);
				if (cSkillInfoLevel != null && cSkillInfoLevel.nType == 1)
				{
					int nSignState3 = 0;
					if (dataCenter.GetSkillSign(skillInfo.nID, ref nSignState3) && nSignState3 == nSignState)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	public void Flurry_EnterStage(int nLevelID)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		GameLevelInfo gameLevelInfo = m_GameData.GetGameLevelInfo(nLevelID);
		if (gameLevelInfo == null)
		{
			return;
		}
		CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(dataCenter.CurCharID);
		if (characterInfo == null)
		{
			return;
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character == null || character.nLevel == -1)
		{
			return;
		}
		CCharacterInfoLevel cCharacterInfoLevel = characterInfo.Get(character.nLevel);
		if (cCharacterInfoLevel == null)
		{
			return;
		}
		CFlurryManager.CEnterStageInfo cEnterStageInfo = new CFlurryManager.CEnterStageInfo();
		if (cEnterStageInfo == null)
		{
			return;
		}
		cEnterStageInfo.sCharID = characterInfo.nID + "_" + cCharacterInfoLevel.sName;
		cEnterStageInfo.sCharLevel = character.nLevel.ToString();
		cEnterStageInfo.arrWeaponID = new string[3];
		for (int i = 0; i < 3; i++)
		{
			int selectWeapon = dataCenter.GetSelectWeapon(i);
			int nLevel = 0;
			dataCenter.GetWeaponLevel(selectWeapon, ref nLevel);
			CWeaponInfoLevel weaponInfo = m_GameData.GetWeaponInfo(selectWeapon, nLevel);
			if (weaponInfo != null)
			{
				cEnterStageInfo.arrWeaponID[i] = selectWeapon + "_" + weaponInfo.sName;
			}
			else
			{
				cEnterStageInfo.arrWeaponID[i] = "Empty";
			}
		}
		cEnterStageInfo.arrSkillID = new string[3];
		for (int j = 0; j < 3; j++)
		{
			int selectPassiveSkill = dataCenter.GetSelectPassiveSkill(characterInfo.nID, j);
			int nSkillLevel = 0;
			dataCenter.GetPassiveSkill(selectPassiveSkill, ref nSkillLevel);
			CSkillInfoLevel skillInfo = m_GameData.GetSkillInfo(selectPassiveSkill, nSkillLevel);
			if (skillInfo != null)
			{
				cEnterStageInfo.arrSkillID[j] = selectPassiveSkill + "_" + skillInfo.sName;
			}
			else
			{
				cEnterStageInfo.arrSkillID[j] = "Empty";
			}
		}
		int curEquipStone = dataCenter.CurEquipStone;
		int nItemLevel = 0;
		dataCenter.GetEquipStone(curEquipStone, ref nItemLevel);
		CItemInfoLevel itemInfo = m_GameData.GetItemInfo(curEquipStone, nItemLevel);
		if (itemInfo != null)
		{
			cEnterStageInfo.sEquipStoneID = curEquipStone + "_" + itemInfo.sName;
		}
		else
		{
			cEnterStageInfo.sEquipStoneID = "Empty";
		}
		cEnterStageInfo.sLevelID = gameLevelInfo.nID + "_" + gameLevelInfo.sLevelName;
		cEnterStageInfo.nLevelProccess = (int)dataCenter.SceneProccess;
		cEnterStageInfo.nLastStageArrive = dataCenter.LatestLevel;
		CFlurryManager.GetInstance().EnterStage(cEnterStageInfo.sLevelID, cEnterStageInfo);
		CFlurryManager.GetInstance().EnterStage("ALL Stage", cEnterStageInfo);
	}

	public void Flurry_LoseStage(int nLevelID)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		GameLevelInfo gameLevelInfo = m_GameData.GetGameLevelInfo(nLevelID);
		if (gameLevelInfo == null)
		{
			return;
		}
		CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(dataCenter.CurCharID);
		if (characterInfo == null)
		{
			return;
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character == null || character.nLevel == -1)
		{
			return;
		}
		CCharacterInfoLevel cCharacterInfoLevel = characterInfo.Get(character.nLevel);
		if (cCharacterInfoLevel == null)
		{
			return;
		}
		CFlurryManager.CEnterStageInfo cEnterStageInfo = new CFlurryManager.CEnterStageInfo();
		if (cEnterStageInfo == null)
		{
			return;
		}
		cEnterStageInfo.sCharID = characterInfo.nID + "_" + cCharacterInfoLevel.sName;
		cEnterStageInfo.sCharLevel = character.nLevel.ToString();
		cEnterStageInfo.arrWeaponID = new string[3];
		for (int i = 0; i < 3; i++)
		{
			int selectWeapon = dataCenter.GetSelectWeapon(i);
			int nLevel = 0;
			dataCenter.GetWeaponLevel(selectWeapon, ref nLevel);
			CWeaponInfoLevel weaponInfo = m_GameData.GetWeaponInfo(selectWeapon, nLevel);
			if (weaponInfo != null)
			{
				cEnterStageInfo.arrWeaponID[i] = selectWeapon + "_" + weaponInfo.sName;
			}
			else
			{
				cEnterStageInfo.arrWeaponID[i] = "Empty";
			}
		}
		cEnterStageInfo.arrSkillID = new string[3];
		for (int j = 0; j < 3; j++)
		{
			int selectPassiveSkill = dataCenter.GetSelectPassiveSkill(characterInfo.nID, j);
			int nSkillLevel = 0;
			dataCenter.GetPassiveSkill(selectPassiveSkill, ref nSkillLevel);
			CSkillInfoLevel skillInfo = m_GameData.GetSkillInfo(selectPassiveSkill, nSkillLevel);
			if (skillInfo != null)
			{
				cEnterStageInfo.arrSkillID[j] = selectPassiveSkill + "_" + skillInfo.sName;
			}
			else
			{
				cEnterStageInfo.arrSkillID[j] = "Empty";
			}
		}
		int curEquipStone = dataCenter.CurEquipStone;
		int nItemLevel = 0;
		dataCenter.GetEquipStone(curEquipStone, ref nItemLevel);
		CItemInfoLevel itemInfo = m_GameData.GetItemInfo(curEquipStone, nItemLevel);
		if (itemInfo != null)
		{
			cEnterStageInfo.sEquipStoneID = curEquipStone + "_" + itemInfo.sName;
		}
		else
		{
			cEnterStageInfo.sEquipStoneID = "Empty";
		}
		cEnterStageInfo.sLevelID = gameLevelInfo.nID + "_" + gameLevelInfo.sLevelName;
		cEnterStageInfo.nLevelProccess = (int)dataCenter.SceneProccess;
		cEnterStageInfo.nLastStageArrive = dataCenter.LatestLevel;
		CFlurryManager.GetInstance().LoseStage(cEnterStageInfo.sLevelID, cEnterStageInfo);
		CFlurryManager.GetInstance().LoseStage("ALL Stage", cEnterStageInfo);
	}

	public void Flurry_WinStage(int nLevelID)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		GameLevelInfo gameLevelInfo = m_GameData.GetGameLevelInfo(nLevelID);
		if (gameLevelInfo == null)
		{
			return;
		}
		CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(dataCenter.CurCharID);
		if (characterInfo == null)
		{
			return;
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character == null || character.nLevel == -1)
		{
			return;
		}
		CCharacterInfoLevel cCharacterInfoLevel = characterInfo.Get(character.nLevel);
		if (cCharacterInfoLevel == null)
		{
			return;
		}
		CFlurryManager.CEnterStageInfo cEnterStageInfo = new CFlurryManager.CEnterStageInfo();
		if (cEnterStageInfo == null)
		{
			return;
		}
		cEnterStageInfo.sCharID = characterInfo.nID + "_" + cCharacterInfoLevel.sName;
		cEnterStageInfo.sCharLevel = character.nLevel.ToString();
		cEnterStageInfo.arrWeaponID = new string[3];
		for (int i = 0; i < 3; i++)
		{
			int selectWeapon = dataCenter.GetSelectWeapon(i);
			int nLevel = 0;
			dataCenter.GetWeaponLevel(selectWeapon, ref nLevel);
			CWeaponInfoLevel weaponInfo = m_GameData.GetWeaponInfo(selectWeapon, nLevel);
			if (weaponInfo != null)
			{
				cEnterStageInfo.arrWeaponID[i] = selectWeapon + "_" + weaponInfo.sName;
			}
			else
			{
				cEnterStageInfo.arrWeaponID[i] = "Empty";
			}
		}
		cEnterStageInfo.arrSkillID = new string[3];
		for (int j = 0; j < 3; j++)
		{
			int selectPassiveSkill = dataCenter.GetSelectPassiveSkill(characterInfo.nID, j);
			int nSkillLevel = 0;
			dataCenter.GetPassiveSkill(selectPassiveSkill, ref nSkillLevel);
			CSkillInfoLevel skillInfo = m_GameData.GetSkillInfo(selectPassiveSkill, nSkillLevel);
			if (skillInfo != null)
			{
				cEnterStageInfo.arrSkillID[j] = selectPassiveSkill + "_" + skillInfo.sName;
			}
			else
			{
				cEnterStageInfo.arrSkillID[j] = "Empty";
			}
		}
		int curEquipStone = dataCenter.CurEquipStone;
		int nItemLevel = 0;
		dataCenter.GetEquipStone(curEquipStone, ref nItemLevel);
		CItemInfoLevel itemInfo = m_GameData.GetItemInfo(curEquipStone, nItemLevel);
		if (itemInfo != null)
		{
			cEnterStageInfo.sEquipStoneID = curEquipStone + "_" + itemInfo.sName;
		}
		else
		{
			cEnterStageInfo.sEquipStoneID = "Empty";
		}
		cEnterStageInfo.sLevelID = gameLevelInfo.nID + "_" + gameLevelInfo.sLevelName;
		cEnterStageInfo.nLevelProccess = (int)dataCenter.SceneProccess;
		cEnterStageInfo.nLastStageArrive = dataCenter.LatestLevel;
		CFlurryManager.GetInstance().WinStage(cEnterStageInfo.sLevelID, cEnterStageInfo);
		CFlurryManager.GetInstance().WinStage("ALL Stage", cEnterStageInfo);
	}

	public void Flurry_QuitStage(int nLevelID)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		GameLevelInfo gameLevelInfo = m_GameData.GetGameLevelInfo(nLevelID);
		if (gameLevelInfo == null)
		{
			return;
		}
		CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(dataCenter.CurCharID);
		if (characterInfo == null)
		{
			return;
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character == null || character.nLevel == -1)
		{
			return;
		}
		CCharacterInfoLevel cCharacterInfoLevel = characterInfo.Get(character.nLevel);
		if (cCharacterInfoLevel == null)
		{
			return;
		}
		CFlurryManager.CEnterStageInfo cEnterStageInfo = new CFlurryManager.CEnterStageInfo();
		if (cEnterStageInfo == null)
		{
			return;
		}
		cEnterStageInfo.sCharID = characterInfo.nID + "_" + cCharacterInfoLevel.sName;
		cEnterStageInfo.sCharLevel = character.nLevel.ToString();
		cEnterStageInfo.arrWeaponID = new string[3];
		for (int i = 0; i < 3; i++)
		{
			int selectWeapon = dataCenter.GetSelectWeapon(i);
			int nLevel = 0;
			dataCenter.GetWeaponLevel(selectWeapon, ref nLevel);
			CWeaponInfoLevel weaponInfo = m_GameData.GetWeaponInfo(selectWeapon, nLevel);
			if (weaponInfo != null)
			{
				cEnterStageInfo.arrWeaponID[i] = selectWeapon + "_" + weaponInfo.sName;
			}
			else
			{
				cEnterStageInfo.arrWeaponID[i] = "Empty";
			}
		}
		cEnterStageInfo.arrSkillID = new string[3];
		for (int j = 0; j < 3; j++)
		{
			int selectPassiveSkill = dataCenter.GetSelectPassiveSkill(characterInfo.nID, j);
			int nSkillLevel = 0;
			dataCenter.GetPassiveSkill(selectPassiveSkill, ref nSkillLevel);
			CSkillInfoLevel skillInfo = m_GameData.GetSkillInfo(selectPassiveSkill, nSkillLevel);
			if (skillInfo != null)
			{
				cEnterStageInfo.arrSkillID[j] = selectPassiveSkill + "_" + skillInfo.sName;
			}
			else
			{
				cEnterStageInfo.arrSkillID[j] = "Empty";
			}
		}
		int curEquipStone = dataCenter.CurEquipStone;
		int nItemLevel = 0;
		dataCenter.GetEquipStone(curEquipStone, ref nItemLevel);
		CItemInfoLevel itemInfo = m_GameData.GetItemInfo(curEquipStone, nItemLevel);
		if (itemInfo != null)
		{
			cEnterStageInfo.sEquipStoneID = curEquipStone + "_" + itemInfo.sName;
		}
		else
		{
			cEnterStageInfo.sEquipStoneID = "Empty";
		}
		cEnterStageInfo.sLevelID = gameLevelInfo.nID + "_" + gameLevelInfo.sLevelName;
		cEnterStageInfo.nLevelProccess = (int)dataCenter.SceneProccess;
		CFlurryManager.GetInstance().QuitStage(cEnterStageInfo.sLevelID, cEnterStageInfo);
		CFlurryManager.GetInstance().QuitStage("ALL Stage", cEnterStageInfo);
	}

	public void Flurry_PurchaseSkill(int nSkillID)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(dataCenter.CurCharID);
		if (characterInfo == null)
		{
			return;
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character == null || character.nLevel == -1)
		{
			return;
		}
		CCharacterInfoLevel cCharacterInfoLevel = characterInfo.Get(character.nLevel);
		if (cCharacterInfoLevel == null)
		{
			return;
		}
		int nSkillLevel = 0;
		dataCenter.GetPassiveSkill(nSkillID, ref nSkillLevel);
		CSkillInfoLevel skillInfo = m_GameData.GetSkillInfo(nSkillID, nSkillLevel);
		if (skillInfo != null)
		{
			CFlurryManager.CPurchaseSkillInfo cPurchaseSkillInfo = new CFlurryManager.CPurchaseSkillInfo();
			if (cPurchaseSkillInfo != null)
			{
				cPurchaseSkillInfo.sCharID = characterInfo.nID + "_" + cCharacterInfoLevel.sName;
				cPurchaseSkillInfo.sCharLevel = character.nLevel.ToString();
				cPurchaseSkillInfo.sSkillID = nSkillID + "_" + skillInfo.sName;
				CFlurryManager.GetInstance().PurchaseSkill(cPurchaseSkillInfo.sSkillID, cPurchaseSkillInfo);
				CFlurryManager.GetInstance().PurchaseSkill("ALL Skill", cPurchaseSkillInfo);
			}
		}
	}

	public void Flurry_UpgradeSkill(int nSkillID)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(dataCenter.CurCharID);
		if (characterInfo == null)
		{
			return;
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character == null || character.nLevel == -1)
		{
			return;
		}
		CCharacterInfoLevel cCharacterInfoLevel = characterInfo.Get(character.nLevel);
		if (cCharacterInfoLevel == null)
		{
			return;
		}
		int nSkillLevel = 0;
		dataCenter.GetPassiveSkill(nSkillID, ref nSkillLevel);
		CSkillInfoLevel skillInfo = m_GameData.GetSkillInfo(nSkillID, nSkillLevel);
		if (skillInfo != null)
		{
			CFlurryManager.CUpgradeSkillInfo cUpgradeSkillInfo = new CFlurryManager.CUpgradeSkillInfo();
			if (cUpgradeSkillInfo != null)
			{
				cUpgradeSkillInfo.sCharID = characterInfo.nID + "_" + cCharacterInfoLevel.sName;
				cUpgradeSkillInfo.sCharLevel = character.nLevel.ToString();
				cUpgradeSkillInfo.sSkillID = nSkillID + "_" + skillInfo.sName;
				cUpgradeSkillInfo.sSkillLevel = nSkillLevel.ToString();
				CFlurryManager.GetInstance().UpgradeSkill(cUpgradeSkillInfo.sSkillID, cUpgradeSkillInfo);
				CFlurryManager.GetInstance().UpgradeSkill("ALL Skill", cUpgradeSkillInfo);
			}
		}
	}

	public void Flurry_PurchaseWeapon(int nWeaponID)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(dataCenter.CurCharID);
		if (characterInfo == null)
		{
			return;
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character == null || character.nLevel == -1)
		{
			return;
		}
		CCharacterInfoLevel cCharacterInfoLevel = characterInfo.Get(character.nLevel);
		if (cCharacterInfoLevel == null)
		{
			return;
		}
		int nLevel = 0;
		dataCenter.GetWeaponLevel(nWeaponID, ref nLevel);
		CWeaponInfoLevel weaponInfo = m_GameData.GetWeaponInfo(nWeaponID, nLevel);
		if (weaponInfo != null)
		{
			CFlurryManager.CPurchaseWeaponInfo cPurchaseWeaponInfo = new CFlurryManager.CPurchaseWeaponInfo();
			if (cPurchaseWeaponInfo != null)
			{
				cPurchaseWeaponInfo.sCharID = characterInfo.nID + "_" + cCharacterInfoLevel.sName;
				cPurchaseWeaponInfo.sCharLevel = character.nLevel.ToString();
				cPurchaseWeaponInfo.sWeaponID = nWeaponID + "_" + weaponInfo.sName;
				cPurchaseWeaponInfo.nLevelProccess = (int)dataCenter.SceneProccess;
				CFlurryManager.GetInstance().PurchaseWeapon(cPurchaseWeaponInfo.sWeaponID, cPurchaseWeaponInfo);
				CFlurryManager.GetInstance().PurchaseWeapon("ALL Weapon", cPurchaseWeaponInfo);
			}
		}
	}

	public void Flurry_UpgradeWeapon(int nWeaponID)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(dataCenter.CurCharID);
		if (characterInfo == null)
		{
			return;
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character == null || character.nLevel == -1)
		{
			return;
		}
		CCharacterInfoLevel cCharacterInfoLevel = characterInfo.Get(character.nLevel);
		if (cCharacterInfoLevel == null)
		{
			return;
		}
		int nLevel = 0;
		dataCenter.GetWeaponLevel(nWeaponID, ref nLevel);
		CWeaponInfoLevel weaponInfo = m_GameData.GetWeaponInfo(nWeaponID, nLevel);
		if (weaponInfo != null)
		{
			CFlurryManager.CUpgradeWeaponInfo cUpgradeWeaponInfo = new CFlurryManager.CUpgradeWeaponInfo();
			if (cUpgradeWeaponInfo != null)
			{
				cUpgradeWeaponInfo.sCharID = characterInfo.nID + "_" + cCharacterInfoLevel.sName;
				cUpgradeWeaponInfo.sCharLevel = character.nLevel.ToString();
				cUpgradeWeaponInfo.sWeaponID = nWeaponID + "_" + weaponInfo.sName;
				cUpgradeWeaponInfo.sWeaponLevel = nLevel.ToString();
				cUpgradeWeaponInfo.nLevelProccess = (int)dataCenter.SceneProccess;
				CFlurryManager.GetInstance().UpgradeWeapon(cUpgradeWeaponInfo.sWeaponID, cUpgradeWeaponInfo);
				CFlurryManager.GetInstance().UpgradeWeapon("ALL Weapon", cUpgradeWeaponInfo);
			}
		}
	}

	public void Flurry_PurchaseStone(int nStoneID)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(dataCenter.CurCharID);
		if (characterInfo == null)
		{
			return;
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character == null || character.nLevel == -1)
		{
			return;
		}
		CCharacterInfoLevel cCharacterInfoLevel = characterInfo.Get(character.nLevel);
		if (cCharacterInfoLevel == null)
		{
			return;
		}
		int nItemLevel = 0;
		dataCenter.GetEquipStone(nStoneID, ref nItemLevel);
		CItemInfoLevel itemInfo = m_GameData.GetItemInfo(nStoneID, nItemLevel);
		if (itemInfo != null)
		{
			CFlurryManager.CPurchaseStoneInfo cPurchaseStoneInfo = new CFlurryManager.CPurchaseStoneInfo();
			if (cPurchaseStoneInfo != null)
			{
				cPurchaseStoneInfo.sCharID = characterInfo.nID + "_" + cCharacterInfoLevel.sName;
				cPurchaseStoneInfo.sCharLevel = character.nLevel.ToString();
				cPurchaseStoneInfo.sStoneID = nStoneID + "_" + itemInfo.sName;
				cPurchaseStoneInfo.nLevelProccess = (int)dataCenter.SceneProccess;
				CFlurryManager.GetInstance().PurchaseStone(cPurchaseStoneInfo.sStoneID, cPurchaseStoneInfo);
				CFlurryManager.GetInstance().PurchaseStone("ALL Stone", cPurchaseStoneInfo);
			}
		}
	}

	public void Flurry_UpgradeStone(int nStoneID)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(dataCenter.CurCharID);
		if (characterInfo == null)
		{
			return;
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character == null || character.nLevel == -1)
		{
			return;
		}
		CCharacterInfoLevel cCharacterInfoLevel = characterInfo.Get(character.nLevel);
		if (cCharacterInfoLevel == null)
		{
			return;
		}
		int nItemLevel = 0;
		dataCenter.GetEquipStone(nStoneID, ref nItemLevel);
		CItemInfoLevel itemInfo = m_GameData.GetItemInfo(nStoneID, nItemLevel);
		if (itemInfo != null)
		{
			CFlurryManager.CUpgradeStoneInfo cUpgradeStoneInfo = new CFlurryManager.CUpgradeStoneInfo();
			if (cUpgradeStoneInfo != null)
			{
				cUpgradeStoneInfo.sCharID = characterInfo.nID + "_" + cCharacterInfoLevel.sName;
				cUpgradeStoneInfo.sCharLevel = character.nLevel.ToString();
				cUpgradeStoneInfo.sStoneID = nStoneID + "_" + itemInfo.sName;
				cUpgradeStoneInfo.sStoneLevel = nItemLevel.ToString();
				cUpgradeStoneInfo.nLevelProccess = (int)dataCenter.SceneProccess;
				CFlurryManager.GetInstance().UpgradeStone(cUpgradeStoneInfo.sStoneID, cUpgradeStoneInfo);
				CFlurryManager.GetInstance().UpgradeStone("ALL Stone", cUpgradeStoneInfo);
			}
		}
	}

	public void Flurry_PurchaseChar(int nCharID)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter != null)
		{
			CCharacterInfoLevel characterInfo = m_GameData.GetCharacterInfo(nCharID, 1);
			if (characterInfo != null)
			{
				CFlurryManager.CPurchaseCharInfo cPurchaseCharInfo = new CFlurryManager.CPurchaseCharInfo();
				cPurchaseCharInfo.sCharID = nCharID + "_" + characterInfo.sName;
				cPurchaseCharInfo.nLevelProccess = (int)dataCenter.SceneProccess;
				CFlurryManager.GetInstance().PurchaseChar(cPurchaseCharInfo.sCharID, cPurchaseCharInfo);
				CFlurryManager.GetInstance().PurchaseChar("ALL Char", cPurchaseCharInfo);
			}
		}
	}

	public void Flurry_PurchaseBullet(int nLevelID)
	{
		GameLevelInfo gameLevelInfo = m_GameData.GetGameLevelInfo(nLevelID);
		if (gameLevelInfo == null)
		{
			return;
		}
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(dataCenter.CurCharID);
		if (characterInfo == null)
		{
			return;
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character != null && character.nLevel != -1)
		{
			CCharacterInfoLevel cCharacterInfoLevel = characterInfo.Get(character.nLevel);
			if (cCharacterInfoLevel != null)
			{
				CFlurryManager.CPurchaseBulletInfo cPurchaseBulletInfo = new CFlurryManager.CPurchaseBulletInfo();
				cPurchaseBulletInfo.sCharID = characterInfo.nID + "_" + cCharacterInfoLevel.sName;
				cPurchaseBulletInfo.sCharLevel = character.nLevel.ToString();
				cPurchaseBulletInfo.sLevelID = nLevelID + "_" + gameLevelInfo.sLevelName;
				CFlurryManager.GetInstance().PurchaseBullet(cPurchaseBulletInfo.sLevelID, cPurchaseBulletInfo);
				CFlurryManager.GetInstance().PurchaseBullet("ALL Level", cPurchaseBulletInfo);
			}
		}
	}

	public void Flurry_PurchaseIAP(int nIAPID)
	{
		CIAPInfo iAPInfo = m_GameData.GetIAPInfo(nIAPID);
		if (iAPInfo == null)
		{
			return;
		}
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(dataCenter.CurCharID);
		if (characterInfo == null)
		{
			return;
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character == null || character.nLevel == -1)
		{
			return;
		}
		CCharacterInfoLevel cCharacterInfoLevel = characterInfo.Get(character.nLevel);
		if (cCharacterInfoLevel != null)
		{
			GameLevelInfo gameLevelInfo = m_GameData.GetGameLevelInfo(dataCenter.LatestLevel);
			if (gameLevelInfo != null)
			{
				CFlurryManager.CPurchaseIAPInfo cPurchaseIAPInfo = new CFlurryManager.CPurchaseIAPInfo();
				cPurchaseIAPInfo.sCharID = characterInfo.nID + "_" + cCharacterInfoLevel.sName;
				cPurchaseIAPInfo.sCharLevel = character.nLevel.ToString();
				cPurchaseIAPInfo.sIAP = nIAPID + "_" + iAPInfo.sKey;
				cPurchaseIAPInfo.sLevelID = gameLevelInfo.nID + "_" + gameLevelInfo.sLevelName;
				CFlurryManager.GetInstance().PurchaseIAP(cPurchaseIAPInfo.sIAP, cPurchaseIAPInfo);
				CFlurryManager.GetInstance().PurchaseIAP("ALL IAP", cPurchaseIAPInfo);
			}
		}
	}

	public void Flurry_CharRevive(int nLevelID)
	{
		GameLevelInfo gameLevelInfo = m_GameData.GetGameLevelInfo(nLevelID);
		if (gameLevelInfo == null)
		{
			return;
		}
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(dataCenter.CurCharID);
		if (characterInfo == null)
		{
			return;
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character == null || character.nLevel == -1)
		{
			return;
		}
		CCharacterInfoLevel cCharacterInfoLevel = characterInfo.Get(character.nLevel);
		if (cCharacterInfoLevel == null)
		{
			return;
		}
		CFlurryManager.CReviveInfo cReviveInfo = new CFlurryManager.CReviveInfo();
		cReviveInfo.sCharID = characterInfo.nID + "_" + cCharacterInfoLevel.sName;
		cReviveInfo.sCharLevel = character.nLevel.ToString();
		cReviveInfo.arrWeaponID = new string[3];
		for (int i = 0; i < 3; i++)
		{
			int selectWeapon = dataCenter.GetSelectWeapon(i);
			int nLevel = 0;
			dataCenter.GetWeaponLevel(selectWeapon, ref nLevel);
			CWeaponInfoLevel weaponInfo = m_GameData.GetWeaponInfo(selectWeapon, nLevel);
			if (weaponInfo != null)
			{
				cReviveInfo.arrWeaponID[i] = selectWeapon + "_" + weaponInfo.sName;
			}
			else
			{
				cReviveInfo.arrWeaponID[i] = "Empty";
			}
		}
		cReviveInfo.arrSkillID = new string[3];
		for (int j = 0; j < 3; j++)
		{
			int selectPassiveSkill = dataCenter.GetSelectPassiveSkill(characterInfo.nID, j);
			int nSkillLevel = 0;
			dataCenter.GetPassiveSkill(selectPassiveSkill, ref nSkillLevel);
			CSkillInfoLevel skillInfo = m_GameData.GetSkillInfo(selectPassiveSkill, nSkillLevel);
			if (skillInfo != null)
			{
				cReviveInfo.arrSkillID[j] = selectPassiveSkill + "_" + skillInfo.sName;
			}
			else
			{
				cReviveInfo.arrSkillID[j] = "Empty";
			}
		}
		int curEquipStone = dataCenter.CurEquipStone;
		int nItemLevel = 0;
		dataCenter.GetEquipStone(curEquipStone, ref nItemLevel);
		CItemInfoLevel itemInfo = m_GameData.GetItemInfo(curEquipStone, nItemLevel);
		if (itemInfo != null)
		{
			cReviveInfo.sEquipStoneID = curEquipStone + "_" + itemInfo.sName;
		}
		else
		{
			cReviveInfo.sEquipStoneID = "Empty";
		}
		cReviveInfo.sLevelID = gameLevelInfo.nID + "_" + gameLevelInfo.sLevelName;
		cReviveInfo.nLevelProccess = (int)dataCenter.SceneProccess;
		CFlurryManager.GetInstance().CharRevive(cReviveInfo.sLevelID, cReviveInfo);
		CFlurryManager.GetInstance().CharRevive("ALL Level", cReviveInfo);
	}

	public void Flurry_GainAchi(int nAchiID, int nStep)
	{
		iAchievementCenter achievementCenter = m_GameData.GetAchievementCenter();
		if (achievementCenter == null)
		{
			return;
		}
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		CAchievementInfo cAchievementInfo = achievementCenter.Get(nAchiID);
		if (cAchievementInfo == null)
		{
			return;
		}
		CAchievementData achiData = dataCenter.GetAchiData(nAchiID);
		if (achiData == null)
		{
			return;
		}
		CCharacterInfo characterInfo = m_GameData.GetCharacterInfo(dataCenter.CurCharID);
		if (characterInfo == null)
		{
			return;
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character != null && character.nLevel != -1)
		{
			CCharacterInfoLevel cCharacterInfoLevel = characterInfo.Get(character.nLevel);
			if (cCharacterInfoLevel != null)
			{
				CFlurryManager.CAchiInfo cAchiInfo = new CFlurryManager.CAchiInfo();
				cAchiInfo.sCharID = characterInfo.nID + "_" + cCharacterInfoLevel.sName;
				cAchiInfo.sCharLevel = character.nLevel.ToString();
				cAchiInfo.sAchiID = nAchiID + "_" + cAchievementInfo.sName;
				cAchiInfo.sAchiLevel = nStep.ToString();
				CFlurryManager.GetInstance().GainAchi(cAchiInfo.sAchiID, cAchiInfo);
				CFlurryManager.GetInstance().GainAchi("ALL Achi", cAchiInfo);
			}
		}
	}

	public string PackSaveData()
	{
		if (m_GameData == null)
		{
			return string.Empty;
		}
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return string.Empty;
		}
		if (iServerSaveData.GetInstance().m_bCreateNewSave)
		{
			iServerSaveData.GetInstance().m_bCreateNewSave = false;
			dataCenter.LoadData(string.Empty);
		}
		try
		{
			return dataCenter.Pack();
		}
		catch
		{
			UnityEngine.Debug.LogError("pack data exception");
			return string.Empty;
		}
	}

	public bool UnPackSaveData(string sData)
	{
		if (m_GameData == null)
		{
			return false;
		}
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return false;
		}
		return dataCenter.UnPack(sData);
	}

	public void OnPurchaseIAP(string sKey, string sIdentifier, string sReceipt)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		CIAPInfo iAPInfoByKey = m_GameData.GetIAPInfoByKey(sKey);
		if (iAPInfoByKey == null)
		{
			return;
		}
		CTrinitiCollectManager.GetInstance().SendPay(dataCenter.Crystal, iAPInfoByKey.nValue, iAPInfoByKey.fMoney);
		if (iAPInfoByKey.isCrystal)
		{
			CUISound.GetInstance().Play("UI_Crystal");
			dataCenter.AddCrystal(iAPInfoByKey.nValue);
		}
		else
		{
			dataCenter.AddGold(iAPInfoByKey.nValue);
		}
		iServerVerify.CServerConfigInfo serverConfigInfo = iServerVerify.GetInstance().GetServerConfigInfo();
		iGameData gameData = GetInstance().m_GameData;
		if (gameData != null)
		{
			iWeaponCenter weaponCenter = gameData.GetWeaponCenter();
			if (weaponCenter != null)
			{
				Dictionary<int, CWeaponInfo> data = weaponCenter.GetData();
				if (data != null)
				{
					foreach (CWeaponInfo value in data.Values)
					{
						if (!dataCenter.IsFreeWeaponID(value.nID) && serverConfigInfo != null && serverConfigInfo.IsGift(value.nID))
						{
							dataCenter.AddFreeWeapon(value.nID);
						}
					}
				}
			}
		}
		GetInstance().SaveData();
	}

	public void OnPurchaseIAP_InBackground(string sKey, string sIdentifier, string sReceipt)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		CIAPInfo iAPInfoByKey = m_GameData.GetIAPInfoByKey(sKey);
		if (iAPInfoByKey == null)
		{
			return;
		}
		CTrinitiCollectManager.GetInstance().SendPay(dataCenter.Crystal, iAPInfoByKey.nValue, iAPInfoByKey.fMoney);
		if (iAPInfoByKey.isCrystal)
		{
			dataCenter.AddCrystalInBackground(iAPInfoByKey.nValue, iAPInfoByKey.fMoney, sKey + sIdentifier + sReceipt);
		}
		else
		{
			dataCenter.AddGold(iAPInfoByKey.nValue);
		}
		iServerVerify.CServerConfigInfo serverConfigInfo = iServerVerify.GetInstance().GetServerConfigInfo();
		iGameData gameData = GetInstance().m_GameData;
		if (gameData != null)
		{
			iWeaponCenter weaponCenter = gameData.GetWeaponCenter();
			if (weaponCenter != null)
			{
				Dictionary<int, CWeaponInfo> data = weaponCenter.GetData();
				if (data != null)
				{
					foreach (CWeaponInfo value in data.Values)
					{
						if (!dataCenter.IsFreeWeaponID(value.nID) && serverConfigInfo != null && serverConfigInfo.IsGift(value.nID))
						{
							dataCenter.AddFreeWeapon(value.nID);
						}
					}
				}
			}
		}
		GetInstance().SaveData();
	}

	public void SaveData(bool bImmediately = false)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter != null)
		{
			dataCenter.Save();
			iServerSaveData.GetInstance().UploadImmidately();
		}
	}

	public bool IsTutorialStage(int nStageID)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return false;
		}
		NewHelpState nTutorialVillageState = (NewHelpState)dataCenter.nTutorialVillageState;
		if (nTutorialVillageState == NewHelpState.Help_Over)
		{
			return false;
		}
		UnityEngine.Debug.Log(string.Concat("newhelpstate ", nTutorialVillageState, " ", nStageID));
		if (nStageID == 1001 && nTutorialVillageState == NewHelpState.None)
		{
			return true;
		}
		if (nStageID == 1002 && nTutorialVillageState == NewHelpState.Help11_ClickEnterSkills)
		{
			return true;
		}
		if (nStageID == 1003 && nTutorialVillageState == NewHelpState.Help21_ClickEnterForge)
		{
			return true;
		}
		return false;
	}

	public bool UpgradeVersion(string version_new)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return false;
		}
		string gameVersion = dataCenter.GameVersion;
		Debug.Log(gameVersion + " " + version_new);
		if (gameVersion == version_new)
		{
			return true;
		}
		try
		{
			dataCenter.GameVersion = version_new;
			dataCenter.isEvaluate = false;
			dataCenter.EnterAppCount = 0;
			iGameLevelCenter gameLevelCenter = m_GameData.GetGameLevelCenter();
			if (version_new[0] >= '2')
			{
				bool flag = false;
				if (gameVersion == "2.0" && gameLevelCenter != null)
				{
					if (gameLevelCenter.GetGroupID(dataCenter.LatestLevel) == -1)
					{
						flag = true;
					}
					else
					{
						float proccess = gameLevelCenter.GetProccess(dataCenter.LatestLevel);
						if (proccess >= 0f)
						{
							dataCenter.SceneProccess = proccess;
						}
					}
				}
				if (gameVersion[0] < '2' || flag)
				{
					if (gameLevelCenter != null)
					{
						List<int> levelListBySeq = gameLevelCenter.GetLevelListBySeq((int)dataCenter.SceneProccess);
						if (levelListBySeq != null && levelListBySeq.Count > 0)
						{
							dataCenter.ClearPassedLevel();
							for (int i = 0; i < levelListBySeq.Count - 1; i++)
							{
								dataCenter.SetPassedLevel(levelListBySeq[i]);
							}
							dataCenter.LatestLevel = levelListBySeq[levelListBySeq.Count - 1];
							dataCenter.LastLevel = dataCenter.LatestLevel;
							float proccess2 = gameLevelCenter.GetProccess(dataCenter.LatestLevel);
							if (proccess2 >= 0f)
							{
								dataCenter.SceneProccess = proccess2;
							}
						}
					}
					iServerVerify.CServerConfigInfo serverConfigInfo = iServerVerify.GetInstance().GetServerConfigInfo();
					if (serverConfigInfo != null && serverConfigInfo.m_ltItemIDChange.Count > 0)
					{
						for (int j = 0; j < serverConfigInfo.m_ltItemIDChange.Count; j++)
						{
							int nSrcID = serverConfigInfo.m_ltItemIDChange[j].m_nSrcID;
							int nDstID = serverConfigInfo.m_ltItemIDChange[j].m_nDstID;
							UnityEngine.Debug.Log(nSrcID + " -> " + nDstID);
							int materialNum = dataCenter.GetMaterialNum(nSrcID);
							if (materialNum > 0)
							{
								dataCenter.AddMaterialNum(nDstID, materialNum);
								dataCenter.DelMaterial(nSrcID);
							}
						}
					}
				}
			}
			Dictionary<int, int> equipStoneData = dataCenter.GetEquipStoneData();
			if (equipStoneData != null)
			{
				foreach (KeyValuePair<int, int> item in equipStoneData)
				{
					int avatarlevel = -1;
					switch (item.Key)
					{
					case 10001:
						dataCenter.GetAvatar(701, ref avatarlevel);
						if (avatarlevel < item.Value)
						{
							dataCenter.SetAvatar(701, item.Value);
						}
						break;
					case 10002:
						dataCenter.GetAvatar(702, ref avatarlevel);
						if (avatarlevel < item.Value)
						{
							dataCenter.SetAvatar(702, item.Value);
						}
						break;
					case 10003:
						dataCenter.GetAvatar(703, ref avatarlevel);
						if (avatarlevel < item.Value)
						{
							dataCenter.SetAvatar(703, item.Value);
						}
						break;
					case 10004:
						dataCenter.GetAvatar(704, ref avatarlevel);
						if (avatarlevel < item.Value)
						{
							dataCenter.SetAvatar(704, item.Value);
						}
						break;
					case 10005:
						dataCenter.GetAvatar(706, ref avatarlevel);
						if (avatarlevel < item.Value)
						{
							dataCenter.SetAvatar(706, item.Value);
						}
						break;
					case 10006:
						dataCenter.GetAvatar(707, ref avatarlevel);
						if (avatarlevel < item.Value)
						{
							dataCenter.SetAvatar(707, item.Value);
						}
						break;
					case 10007:
						dataCenter.GetAvatar(705, ref avatarlevel);
						if (avatarlevel < item.Value)
						{
							dataCenter.SetAvatar(705, item.Value);
						}
						break;
					}
				}
			}
			Dictionary<int, int> materialData = dataCenter.GetMaterialData();
			if (materialData != null)
			{
				iServerVerify.CServerConfigInfo serverConfigInfo2 = iServerVerify.GetInstance().GetServerConfigInfo();
				if (serverConfigInfo2 != null && serverConfigInfo2.m_ltItemIDChange.Count > 0)
				{
					List<int> list = new List<int>();
					foreach (KeyValuePair<int, int> item2 in materialData)
					{
						if (m_GameData.GetItemInfo(item2.Key) == null)
						{
							UnityEngine.Debug.Log(item2.Key);
							list.Add(item2.Key);
						}
					}
					if (list.Count > 0)
					{
						foreach (int item3 in list)
						{
							for (int k = 0; k < serverConfigInfo2.m_ltItemIDChange.Count; k++)
							{
								if (item3 == serverConfigInfo2.m_ltItemIDChange[k].m_nSrcID)
								{
									int nSrcID2 = serverConfigInfo2.m_ltItemIDChange[k].m_nSrcID;
									int nDstID2 = serverConfigInfo2.m_ltItemIDChange[k].m_nDstID;
									UnityEngine.Debug.Log(nSrcID2 + " -> " + nDstID2);
									int materialNum2 = dataCenter.GetMaterialNum(nSrcID2);
									if (materialNum2 > 0)
									{
										dataCenter.AddMaterialNum(nDstID2, materialNum2);
										dataCenter.DelMaterial(nSrcID2);
									}
									break;
								}
							}
						}
					}
				}
			}
			if (gameLevelCenter != null)
			{
				Dictionary<int, GameLevelGroupInfo> dataGroup = gameLevelCenter.GetDataGroup();
				if (dataGroup != null)
				{
					foreach (GameLevelGroupInfo value in dataGroup.Values)
					{
						for (int l = 0; l < value.ltLevelList.Count; l++)
						{
							if (value.ltLevelList[l] < dataCenter.LatestLevel && !dataCenter.IsLevelPassed(value.ltLevelList[l]))
							{
								dataCenter.SetPassedLevel(value.ltLevelList[l]);
							}
						}
					}
				}
			}
		}
		catch
		{
			Debug.Log("?");
			return false;
		}
		SaveData(true);
		return true;
	}

	public int CalcCombatPower()
	{
		if (m_GameData == null)
		{
			return 1;
		}
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return 1;
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character == null)
		{
			return 1;
		}
		float num = 0f;
		for (int i = 0; i < 3; i++)
		{
			int selectWeapon = dataCenter.GetSelectWeapon(i);
			if (selectWeapon == -1)
			{
				continue;
			}
			int nLevel = -1;
			if (dataCenter.GetWeaponLevel(selectWeapon, ref nLevel))
			{
				CWeaponInfoLevel weaponInfo = m_GameData.GetWeaponInfo(selectWeapon, nLevel);
				if (weaponInfo != null)
				{
					num += weaponInfo.CalcDPS();
				}
			}
		}
		float num2 = 0f;
		if (dataCenter.AvatarStone > 0)
		{
			int avatarlevel = -1;
			if (dataCenter.GetAvatar(dataCenter.AvatarStone, ref avatarlevel))
			{
				CAvatarInfoLevel cAvatarInfoLevel = m_GameData.m_AvatarCenter.Get(dataCenter.AvatarStone, avatarlevel);
				if (cAvatarInfoLevel != null)
				{
					for (int j = 0; j < 3; j++)
					{
						if (cAvatarInfoLevel.arrFunc[j] == 1)
						{
							kProEnum kProEnum2 = (kProEnum)MyUtils.Low32(cAvatarInfoLevel.arrValueX[j]);
							if (kProEnum2 == kProEnum.HPMax)
							{
								num2 = cAvatarInfoLevel.arrValueY[j];
								break;
							}
						}
					}
				}
			}
		}
		float num3 = 0f;
		for (int k = 0; k < 3; k++)
		{
			int selectPassiveSkill = dataCenter.GetSelectPassiveSkill(dataCenter.CurCharID, k);
			int nSkillLevel = -1;
			if (dataCenter.GetPassiveSkill(selectPassiveSkill, ref nSkillLevel))
			{
				num3 += (float)nSkillLevel;
			}
		}
		float num4 = 0f;
		List<int> list = new List<int>();
		list.Add(dataCenter.AvatarHead);
		list.Add(dataCenter.AvatarUpper);
		list.Add(dataCenter.AvatarLower);
		list.Add(dataCenter.AvatarWrist);
		List<int> list2 = list;
		foreach (int item in list2)
		{
			int avatarlevel2 = -1;
			if (!dataCenter.GetAvatar(item, ref avatarlevel2))
			{
				continue;
			}
			CAvatarInfoLevel cAvatarInfoLevel2 = m_GameData.m_AvatarCenter.Get(item, avatarlevel2);
			if (cAvatarInfoLevel2 == null)
			{
				continue;
			}
			for (int l = 0; l < cAvatarInfoLevel2.arrFunc.Length; l++)
			{
				if (cAvatarInfoLevel2.arrFunc[l] == 1 && MyUtils.Low32(cAvatarInfoLevel2.arrValueX[l]) == 5)
				{
					num4 += (float)MyUtils.Low32(cAvatarInfoLevel2.arrValueY[l]);
				}
			}
		}
		float num5 = 0f;
		list = new List<int>();
		list.Add(dataCenter.AvatarHeadup);
		list.Add(dataCenter.AvatarNeck);
		list.Add(dataCenter.AvatarBadge);
		List<int> list3 = list;
		foreach (int item2 in list3)
		{
			int avatarlevel3 = -1;
			if (dataCenter.GetAvatar(item2, ref avatarlevel3))
			{
				num5 += (float)avatarlevel3;
			}
		}
		float f = ((float)(character.nLevel * 10) + (num / 10f + num2 / 10f) + num4 * 5f + num5 * 500f) * (1f + num3 * 1.5f / 10f);
		int num6 = Mathf.FloorToInt(f);
		if (num6 < 1)
		{
			num6 = 1;
		}
		return num6;
	}
}
