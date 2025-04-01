using System;
using System.Collections.Generic;
using System.Text;
using Boomlagoon.JSON;
using EventCenter;
using gyAchievementSystem;
using TNetSdk;
using UnityEngine;

public class TUIDataServer
{
	public delegate void OnDialogEvent();

	private static TUIDataServer instance;

	protected int m_nScrollIndexRank;

	protected int m_nScrollCountPerPageRank = 10;

	protected int m_nScrollIndexRankFriends;

	protected int m_nScrollCountPerPageRankFriends = 5;

	protected OnDialogEvent m_OnDialogOK;

	protected OnDialogEvent m_OnDialogCancel;

	protected iGameData m_GameData
	{
		get
		{
			return iGameApp.GetInstance().m_GameData;
		}
	}

	protected iDataCenter m_DataCenter
	{
		get
		{
			if (m_GameData == null)
			{
				return null;
			}
			return m_GameData.GetDataCenter();
		}
	}

	protected iDataCenterNet m_DataCenterNet
	{
		get
		{
			if (m_GameData == null)
			{
				return null;
			}
			return m_GameData.GetDataCenterNet();
		}
	}

	protected iGameState m_GameState
	{
		get
		{
			return iGameApp.GetInstance().m_GameState;
		}
	}

	public static TUIDataServer Instance()
	{
		if (instance == null)
		{
			instance = new TUIDataServer();
		}
		return instance;
	}

	public void Initialize()
	{
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.SendEvent_SceneMain>(TUIEvent_BackInfo_SceneMain);
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.SendEvent_SceneMainMenu>(TUIEvent_BackInfo_SceneMainMenu);
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.SendEvent_SceneEquip>(TUIEvent_BackInfo_SceneEquip);
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.SendEvent_SceneStash>(TUIEvent_BackInfo_SceneStash);
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.SendEvent_SceneSkill>(TUIEvent_BackInfo_SceneSkill);
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.SendEvent_SceneForge>(TUIEvent_BackInfo_SceneForge);
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.SendEvent_SceneTavern>(TUIEvent_BackInfo_SceneTavern);
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.SendEvent_SceneMap>(TUIEvent_BackInfo_SceneMap);
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.SendEvent_SceneIAP>(TUIEvent_BackInfo_SceneIAP);
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.SendEvent_SceneGold>(TUIEvent_BackInfo_SceneGold);
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.SendEvent_SceneCoopInputName>(TUIEvent_BackInfo_SceneCoopInputName);
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.SendEvent_SceneCoopMainMenu>(TUIEvent_BackInfo_SceneCoopMainMenu);
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.SendEvent_SceneCoopRoom>(TUIEvent_BackInfo_SceneCoopRoom);
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.SendEvent_SceneBlackMarket>(TUIEvent_BackInfo_SceneBlackMarket);
	}

	private void TUIEvent_BackInfo_SceneMain(object sender, TUIEvent.SendEvent_SceneMain m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneMainEventType.TUIEvent_EnterInfo)
		{
			iGameApp.GetInstance();
			iServerSaveData.GetInstance().IsBackgroundUpload = false;
			iServerSaveData.GetInstance().IsBackgroundBack = false;
			iServerSaveData.GetInstance().IsBackgroundRelogin = true;
			iLoginManager.GetInstance().StartApp(OnLoginSuccess, OnLoginFailed, OnLoginNetError);
			AndroidReturnPlugin.instance.SetBackFunc(null);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(m_event.GetEventName()));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainEventType.TUIEvent_ConnectAgain)
		{
			iLoginManager.GetInstance().StartApp(OnLoginSuccess, OnLoginFailed, OnLoginNetError);
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainEventType.TUIEvent_GotoUpdate)
		{
			Application.OpenURL(iMacroDefine.AddressForItunes);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(m_event.GetEventName()));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainEventType.TUIEvent_FetchFailed)
		{
			iLoginManager.GetInstance().StartApp(OnLoginSuccess, OnLoginFailed, OnLoginNetError);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(m_event.GetEventName()));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainEventType.TUIEvent_GMUsing)
		{
			iLoginManager.GetInstance().StartApp(OnLoginSuccess, OnLoginFailed, OnLoginNetError);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(m_event.GetEventName()));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainEventType.TUIEvent_ServerMaintain)
		{
			iLoginManager.GetInstance().StartApp(OnLoginSuccess, OnLoginFailed, OnLoginNetError);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(m_event.GetEventName()));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainEventType.TUIEvent_UnkownError)
		{
			iLoginManager.GetInstance().StartApp(OnLoginSuccess, OnLoginFailed, OnLoginNetError);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(m_event.GetEventName()));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainEventType.TUIEvent_EnterLevel)
		{
			iGameData gameData = iGameApp.GetInstance().m_GameData;
			if (gameData == null)
			{
				return;
			}
			iGameState gameState = iGameApp.GetInstance().m_GameState;
			if (gameState == null)
			{
				return;
			}
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter == null)
			{
				return;
			}
			iServerSaveData.GetInstance().IsBackgroundUpload = true;
			iServerSaveData.GetInstance().IsBackgroundBack = true;
			iServerSaveData.GetInstance().IsBackgroundRelogin = false;
			NewHelpState newHelpState = (NewHelpState)dataCenter.nTutorialVillageState;
			if (newHelpState == NewHelpState.None)
			{
				gameState.GameLevel = 1001;
				int wparam = 2;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(m_event.GetEventName(), true, wparam));
				return;
			}
			UnityEngine.Debug.Log("CanChartboost CanChartboost " + TrinitiAdAndroidPlugin.Instance().CanChartboost());
			if (TrinitiAdAndroidPlugin.Instance().CanChartboost())
			{
				UnityEngine.Debug.Log("CanChartboost Chartboost show");
				ChartBoostAndroid.showInterstitial(null);
			}
			if (OpenClikPlugin.IsAdReady())
			{
				OpenClikPlugin.Show(true);
			}
			TUISceneType wparam2 = TUISceneType.Scene_MainMenu;
			if (newHelpState != NewHelpState.Help_Over)
			{
				if (newHelpState >= NewHelpState.Help01_ClickEnterForge && newHelpState <= NewHelpState.Help03_ClickWeaponMake)
				{
					newHelpState = NewHelpState.Help01_ClickEnterForge;
				}
				else if (newHelpState >= NewHelpState.Help04_ClickJumpToCamp && newHelpState <= NewHelpState.Help06_ClickWeaponEquip)
				{
					wparam2 = TUISceneType.Scene_Equip;
					newHelpState = NewHelpState.Help05_ClickOpenWeaponEquip;
				}
				else if (newHelpState >= NewHelpState.Help07_ClickBackToVillage && newHelpState <= NewHelpState.Help11_ClickEnterSkills)
				{
					newHelpState = NewHelpState.Help08_ClickMap;
				}
				else if (newHelpState >= NewHelpState.Help12_ClickOpenSkillBuy && newHelpState <= NewHelpState.Help13_ClickSkillBuy)
				{
					newHelpState = NewHelpState.Help11_ClickEnterSkills;
				}
				else if (newHelpState >= NewHelpState.Help14_ClickJumpToCamp && newHelpState <= NewHelpState.Help16_ClickSkillEquip)
				{
					wparam2 = TUISceneType.Scene_Equip;
					newHelpState = NewHelpState.Help15_ClickOpenSkillEquip;
				}
				else if (newHelpState >= NewHelpState.Help17_ClickBackToVillage && newHelpState <= NewHelpState.Help21_ClickEnterForge)
				{
					newHelpState = NewHelpState.Help18_ClickMap;
				}
				else if (newHelpState >= NewHelpState.Help22_ClickOpenWeaponUpgrade && newHelpState <= NewHelpState.Help24_ClickWeaponUpgrade)
				{
					newHelpState = NewHelpState.Help21_ClickEnterForge;
				}
				else if (newHelpState == NewHelpState.Help25_ClickBackToVillage)
				{
					newHelpState = NewHelpState.Help_Over;
				}
				TUIMappingInfo.Instance().SetNewHelpState(newHelpState);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(m_event.GetEventName(), false, (int)wparam2));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainEventType.TUIEvent_PopupServerOK)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_HidePopupServer));
			if (m_OnDialogOK != null)
			{
				m_OnDialogOK();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainEventType.TUIEvent_PopupServerCancle)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_HidePopupServer));
			if (m_OnDialogCancel != null)
			{
				m_OnDialogCancel();
			}
		}
	}

	private void TUIEvent_BackInfo_SceneMainMenu(object sender, TUIEvent.SendEvent_SceneMainMenu m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_TopBar)
		{
			iGameData gameData = iGameApp.GetInstance().m_GameData;
			if (gameData == null)
			{
				return;
			}
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter == null)
			{
				return;
			}
			CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
			if (character == null)
			{
				return;
			}
			CCharacterInfoLevel characterInfo = gameData.GetCharacterInfo(character.nID, character.nLevel);
			if (characterInfo == null)
			{
				return;
			}
			AndroidReturnPlugin.instance.SetBackFunc(null);
			NewHelpState nTutorialVillageState = (NewHelpState)dataCenter.nTutorialVillageState;
			if (nTutorialVillageState == NewHelpState.Help_Over)
			{
				TUIMappingInfo.Instance().SetNewHelpState(NewHelpState.Help_Over);
			}
			else if (dataCenter.IsLevelPassed(dataCenter.LastLevel))
			{
				switch (dataCenter.LastLevel)
				{
				case 1001:
					if (nTutorialVillageState <= NewHelpState.Help01_ClickEnterForge)
					{
						TUIMappingInfo.Instance().SetNewHelpState(NewHelpState.Help01_ClickEnterForge);
					}
					break;
				case 1002:
					if (nTutorialVillageState <= NewHelpState.Help11_ClickEnterSkills)
					{
						TUIMappingInfo.Instance().SetNewHelpState(NewHelpState.Help11_ClickEnterSkills);
					}
					break;
				case 1003:
					if (nTutorialVillageState <= NewHelpState.Help21_ClickEnterForge)
					{
						TUIMappingInfo.Instance().SetNewHelpState(NewHelpState.Help21_ClickEnterForge);
					}
					break;
				}
			}
			List<iDataCenter.CCrystalInBackground> crystalInBackground = dataCenter.GetCrystalInBackground();
			if (crystalInBackground.Count > 0)
			{
				int num = 0;
				string text = string.Empty;
				foreach (iDataCenter.CCrystalInBackground item in crystalInBackground)
				{
					dataCenter.AddCrystal(item.m_nCrystal.Get());
					num += item.m_nCrystal.Get();
					if (text.Length > 0)
					{
						text += '\n';
					}
					string text2 = text;
					text = text2 + "$" + item.m_fMoney + " for " + item.m_nCrystal.Get() + " crystals";
				}
				dataCenter.ClearCrystalInBackground();
				iGameApp.GetInstance().SaveData();
				CUISound.GetInstance().Play("UI_Crystal");
				CMessageBoxScript.GetInstance().MessageBox("You got " + num + " crystals", text, null, null, "OK");
			}
			TUIGameInfo tUIGameInfo = new TUIGameInfo();
			tUIGameInfo.player_info = new TUIPlayerInfo();
			tUIGameInfo.player_info.role_id = character.nID;
			tUIGameInfo.player_info.level = character.nLevel;
			tUIGameInfo.player_info.level_exp = characterInfo.nExp;
			tUIGameInfo.player_info.exp = character.nExp;
			tUIGameInfo.player_info.gold = dataCenter.Gold;
			tUIGameInfo.player_info.crystal = dataCenter.Crystal;
			ClearPopWindow();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), tUIGameInfo));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_OptionInfo)
		{
			iGameData gameData2 = iGameApp.GetInstance().m_GameData;
			if (gameData2 != null)
			{
				iDataCenter dataCenter2 = gameData2.GetDataCenter();
				if (dataCenter2 != null)
				{
					TUIGameInfo tUIGameInfo2 = new TUIGameInfo();
					tUIGameInfo2.option_info = new TUIOptionInfo();
					tUIGameInfo2.option_info.music_open = dataCenter2.MusicSwitch;
					tUIGameInfo2.option_info.sfx_open = dataCenter2.SoundSwitch;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), tUIGameInfo2));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_AcheviementInfo)
		{
			iGameData gameData3 = iGameApp.GetInstance().m_GameData;
			if (gameData3 == null)
			{
				return;
			}
			iDataCenter dataCenter3 = gameData3.GetDataCenter();
			if (dataCenter3 == null)
			{
				return;
			}
			iAchievementCenter achievementCenter = gameData3.GetAchievementCenter();
			if (achievementCenter == null)
			{
				return;
			}
			Dictionary<int, CAchievementInfo> data = achievementCenter.GetData();
			if (data == null)
			{
				return;
			}
			TUIGameInfo tUIGameInfo3 = new TUIGameInfo();
			tUIGameInfo3.achievement_info = new TUIAchievementInfo();
			foreach (CAchievementInfo value2 in data.Values)
			{
				if (value2.isDaily)
				{
					continue;
				}
				CAchievementData achiData = dataCenter3.GetAchiData(value2.nID);
				Dictionary<int, string> dictionary = new Dictionary<int, string>();
				Dictionary<int, string> dictionary2 = new Dictionary<int, string>();
				Dictionary<int, TUIAchievementRewardInfo> dictionary3 = new Dictionary<int, TUIAchievementRewardInfo>();
				Dictionary<int, int> dictionary4 = new Dictionary<int, int>();
				Dictionary<int, bool> dictionary5 = new Dictionary<int, bool>();
				Dictionary<int, string> dictionary6 = new Dictionary<int, string>();
				for (int i = 0; i < 3; i++)
				{
					CAchievementStep step = value2.GetStep(i);
					if (step != null)
					{
						int key = i + 1;
						dictionary.Add(key, value2.sName);
						dictionary2.Add(key, string.Format(value2.sDesc, step.nStepPurpose));
						TUIAchievementRewardInfo tUIAchievementRewardInfo = new TUIAchievementRewardInfo();
						tUIAchievementRewardInfo.SetRewardInfo01(step.nRewardNumber, (step.nRewardType == 2) ? UnitType.Crystal : UnitType.Gold);
						dictionary3.Add(key, tUIAchievementRewardInfo);
						if (achiData != null)
						{
							dictionary4.Add(key, (int)(Mathf.Clamp01((float)achiData.nCurValue / (float)step.nStepPurpose) * 100f));
							dictionary6.Add(key, achiData.nCurValue + "/" + step.nStepPurpose);
							dictionary5.Add(key, achiData.IsGotReward(i));
						}
						else
						{
							dictionary4.Add(key, 0);
							dictionary6.Add(key, "0/" + step.nStepPurpose);
							dictionary5.Add(key, false);
						}
					}
				}
				tUIGameInfo3.achievement_info.AddAchievementInfo(new TUIOneAchievementInfo(value2.nID, dictionary, dictionary2, dictionary3, dictionary4, dictionary5, dictionary6));
			}
			if (iGameApp.GetInstance().CheckAchieveReward())
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_HadAchievementReward, true));
			}
			else
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_HadAchievementReward));
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), tUIGameInfo3));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_ChangeMusic)
		{
			iGameData gameData4 = iGameApp.GetInstance().m_GameData;
			if (gameData4 != null)
			{
				iDataCenter dataCenter4 = gameData4.GetDataCenter();
				if (dataCenter4 != null)
				{
					dataCenter4.MusicSwitch = !dataCenter4.MusicSwitch;
					dataCenter4.Save();
					TAudioManager.instance.isMusicOn = dataCenter4.MusicSwitch;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), true));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_ChangeSFX)
		{
			iGameData gameData5 = iGameApp.GetInstance().m_GameData;
			if (gameData5 != null)
			{
				iDataCenter dataCenter5 = gameData5.GetDataCenter();
				if (dataCenter5 != null)
				{
					dataCenter5.SoundSwitch = !dataCenter5.SoundSwitch;
					dataCenter5.Save();
					TAudioManager.instance.isSoundOn = dataCenter5.SoundSwitch;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), true));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_Forum)
		{
			Application.OpenURL("http://forum.trinitigame.com/forum/viewforum.php?f=124");
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_TakeAchievement)
		{
			bool flag = false;
			iGameData gameData6 = iGameApp.GetInstance().m_GameData;
			if (gameData6 != null)
			{
				iDataCenter dataCenter6 = gameData6.GetDataCenter();
				iAchievementCenter achievementCenter2 = gameData6.GetAchievementCenter();
				if (dataCenter6 != null && achievementCenter2 != null)
				{
					int wParam = m_event.GetWParam();
					int lparam = m_event.GetLparam();
					CAchievementInfo cAchievementInfo = achievementCenter2.Get(wParam);
					CAchievementData achiData2 = dataCenter6.GetAchiData(wParam);
					if (cAchievementInfo != null && achiData2 != null && !achiData2.IsGotReward(lparam - 1))
					{
						CAchievementStep step2 = cAchievementInfo.GetStep(lparam - 1);
						if (step2 != null)
						{
							if (step2.nRewardType == 2)
							{
								dataCenter6.AddCrystal(step2.nRewardNumber);
								dataCenter6.Save();
							}
							else if (step2.nRewardType == 1)
							{
								dataCenter6.AddGold(step2.nRewardNumber);
								dataCenter6.Save();
							}
							achiData2.SetGotReward(lparam - 1, true);
							dataCenter6.Save();
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				if (iGameApp.GetInstance().CheckAchieveReward())
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_HadAchievementReward, true));
				}
				else
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_HadAchievementReward));
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), flag));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterInfo)
		{
			iGameData gameData7 = iGameApp.GetInstance().m_GameData;
			if (gameData7 == null)
			{
				return;
			}
			iDataCenter dataCenter7 = gameData7.GetDataCenter();
			if (dataCenter7 == null)
			{
				return;
			}
			MyTapjoy.GetInstance();
			if (CGameNetManager.GetInstance().IsConnected())
			{
				TNetManager.GetInstance().DisConnect();
			}
			m_GameState.CurScene = kGameSceneEnum.Home;
			if (!dataCenter7.isEvaluate && dataCenter7.nTutorialVillageState == 25)
			{
				dataCenter7.EnterAppCount++;
				if (dataCenter7.EnterAppCount >= 3)
				{
					SetPopWindow(0);
				}
			}
			iGameApp.GetInstance().CheckUnLock();
			TUIGameInfo tUIGameInfo4 = new TUIGameInfo();
			tUIGameInfo4.villiage_enter_info = new TUIVilliageEnterInfo();
			tUIGameInfo4.villiage_enter_info.finished_text = dataCenter7.SceneProccess.ToString("f1") + "%";
			List<iDataCenter.CUnlockSign> unlockSignList = dataCenter7.GetUnlockSignList();
			if (unlockSignList != null && unlockSignList.Count > 0)
			{
				foreach (iDataCenter.CUnlockSign item2 in unlockSignList)
				{
					switch (item2.m_nType)
					{
					case 3:
						tUIGameInfo4.villiage_enter_info.AddUnlockItem(new TUIUnlockInfo(UnlockType.Weapon, item2.m_nID, string.Empty));
						break;
					case 4:
					{
						CAvatarInfo cAvatarInfo = m_GameData.m_AvatarCenter.Get(item2.m_nID);
						Debug.Log(string.Concat(cAvatarInfo, " ", cAvatarInfo.m_sIcon));
						if (cAvatarInfo != null && cAvatarInfo.m_sIcon.Length > 0)
						{
							string text3 = TUIMappingInfo.Instance().m_sPathRootCustomTex;
							switch (cAvatarInfo.m_nType)
							{
							case 1:
							case 3:
							case 4:
							case 5:
								text3 = text3 + "/Armor/" + cAvatarInfo.m_sIcon;
								break;
							case 0:
							case 2:
							case 6:
							case 7:
								text3 = text3 + "/Accessory/" + cAvatarInfo.m_sIcon;
								break;
							}
							tUIGameInfo4.villiage_enter_info.AddUnlockItem(new TUIUnlockInfo(UnlockType.Avatar, item2.m_nID, text3));
						}
						break;
					}
					case 2:
						tUIGameInfo4.villiage_enter_info.AddUnlockItem(new TUIUnlockInfo(UnlockType.Skill, item2.m_nID, string.Empty));
						break;
					case 1:
						tUIGameInfo4.villiage_enter_info.AddUnlockItem(new TUIUnlockInfo(UnlockType.Role, item2.m_nID, string.Empty));
						break;
					case 5:
						tUIGameInfo4.villiage_enter_info.AddUnlockItem(new TUIUnlockInfo(UnlockType.Title, item2.m_nID, string.Empty));
						break;
					}
				}
				unlockSignList.Clear();
				iGameApp.GetInstance().SaveData();
				SetPopWindow(3);
			}
			if (iGameApp.GetInstance().CheckWeaponSignState(1) || iGameApp.GetInstance().CheckAvatarSignState(1))
			{
				tUIGameInfo4.villiage_enter_info.forge_sign = NewMarkType.New;
			}
			else if (iGameApp.GetInstance().CheckWeaponMaterialEnough() || iGameApp.GetInstance().CheckEquipStoneMaterialEnough())
			{
				tUIGameInfo4.villiage_enter_info.forge_sign = NewMarkType.Mark;
			}
			else
			{
				tUIGameInfo4.villiage_enter_info.forge_sign = NewMarkType.None;
			}
			int curCharID = dataCenter7.CurCharID;
			CCharacterInfo characterInfo2 = gameData7.GetCharacterInfo(curCharID);
			if (characterInfo2 != null)
			{
				NewMarkType skill_sign = NewMarkType.None;
				if (characterInfo2.ltCharacterPassiveSkill != null)
				{
					foreach (int item3 in characterInfo2.ltCharacterPassiveSkill)
					{
						if (iGameApp.GetInstance().CheckSkillSignState(1, item3))
						{
							skill_sign = NewMarkType.New;
							break;
						}
						if (iGameApp.GetInstance().CheckSkillMaterialEnough(item3))
						{
							skill_sign = NewMarkType.Mark;
						}
					}
				}
				tUIGameInfo4.villiage_enter_info.skill_sign = skill_sign;
			}
			if (iGameApp.GetInstance().CheckCharacterSignState(1))
			{
				tUIGameInfo4.villiage_enter_info.tavern_sign = NewMarkType.New;
			}
			else if (iGameApp.GetInstance().CheckCharacterMaterialEnough())
			{
				tUIGameInfo4.villiage_enter_info.tavern_sign = NewMarkType.Mark;
			}
			else
			{
				tUIGameInfo4.villiage_enter_info.tavern_sign = NewMarkType.None;
			}
			if (iGameApp.GetInstance().CheckWeaponSignState(3) || iGameApp.GetInstance().CheckAvatarSignState(3) || iGameApp.GetInstance().CheckCharacterSignState(3))
			{
				tUIGameInfo4.villiage_enter_info.equip_sign = NewMarkType.New;
			}
			else
			{
				NewMarkType equip_sign = NewMarkType.None;
				if (characterInfo2 != null && characterInfo2.ltCharacterPassiveSkill != null)
				{
					foreach (int item4 in characterInfo2.ltCharacterPassiveSkill)
					{
						if (iGameApp.GetInstance().CheckSkillSignState(3, item4))
						{
							equip_sign = NewMarkType.New;
							break;
						}
					}
				}
				tUIGameInfo4.villiage_enter_info.equip_sign = equip_sign;
			}
			tUIGameInfo4.villiage_enter_info.stash_sign = NewMarkType.None;
			Dictionary<int, CBlackItem> data2 = gameData7.m_BlackMarketCenter.GetData();
			if (data2 != null && data2.Count > 0)
			{
				tUIGameInfo4.villiage_enter_info.blackmarket_sign = NewMarkType.New;
			}
			else
			{
				tUIGameInfo4.villiage_enter_info.blackmarket_sign = NewMarkType.None;
			}
			tUIGameInfo4.coop_title_list_info = new TUICoopTitleListInfo();
			iTitleCenter titleCenter = m_GameData.m_TitleCenter;
			if (titleCenter != null)
			{
				Dictionary<int, CTitleInfo> data3 = titleCenter.GetData();
				if (data3 != null)
				{
					foreach (CTitleInfo value3 in data3.Values)
					{
						tUIGameInfo4.coop_title_list_info.AddTitle(value3.nID, value3.sName);
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), tUIGameInfo4));
			NewHelpState nTutorialVillageState2 = (NewHelpState)dataCenter7.nTutorialVillageState;
			if (nTutorialVillageState2 == NewHelpState.Help_Over)
			{
				ShowPopWindow();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterIAP)
		{
			iGameState gameState = iGameApp.GetInstance().m_GameState;
			if (gameState != null)
			{
				gameState.m_lstScene4IAP = TUISceneType.Scene_MainMenu;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterGold)
		{
			iGameState gameState2 = iGameApp.GetInstance().m_GameState;
			if (gameState2 != null)
			{
				gameState2.m_lstScene4IAP = TUISceneType.Scene_MainMenu;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterEquip)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterForge)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterTavern)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterSkill)
		{
			iGameData gameData8 = iGameApp.GetInstance().m_GameData;
			if (gameData8 == null)
			{
				return;
			}
			iDataCenter dataCenter8 = gameData8.GetDataCenter();
			if (dataCenter8 == null)
			{
				return;
			}
			iGameState gameState3 = iGameApp.GetInstance().m_GameState;
			if (gameState3 != null)
			{
				gameState3.m_nLinkSkillRole = dataCenter8.CurCharID;
				CCharacterInfo characterInfo3 = gameData8.GetCharacterInfo(dataCenter8.CurCharID);
				if (characterInfo3 != null && characterInfo3.ltCharacterPassiveSkill != null && characterInfo3.ltCharacterPassiveSkill.Count > 0)
				{
					gameState3.m_nLinkSkill = characterInfo3.ltCharacterPassiveSkill[0];
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterStash)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterBlackMarket)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterMap)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterCoop)
		{
			if (m_DataCenter != null)
			{
				if (m_DataCenter.NickName != null && m_DataCenter.NickName.Length > 0)
				{
					Debug.Log("myname is " + m_DataCenter.NickName);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), true, 12));
				}
				else
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), true));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_OpenSupportURL)
		{
			string text4 = "http://www.trinitigame.com/support/support.html?ip=%25@&country=";
			string countryCode = DevicePlugin.GetCountryCode();
			string text5 = WWW.EscapeURL(DevicePlugin.GetDeviceModelDetail());
			string sysVersion = DevicePlugin.GetSysVersion();
			string text6 = "Call%20of%20Mini:%20Dino%20Hunter";
			string appVersion = DevicePlugin.GetAppVersion();
			string curDeviceId = iServerSaveData.GetInstance().CurDeviceId;
			text4 = text4 + countryCode + "&device=" + text5 + "&os=" + sysVersion + "&game=" + text6 + "&gamever=" + appVersion + "&code=" + curDeviceId;
			UnityEngine.Debug.Log(text4);
			Application.OpenURL(text4);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName()));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_OpenReviewURL)
		{
			Application.OpenURL(iMacroDefine.AddressForItunes);
			ShowPopWindow();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName()));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_CloseReviewURL)
		{
			Debug.Log("22222");
			ShowPopWindow();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName()));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_SaleInfo)
		{
			iGameData gameData9 = iGameApp.GetInstance().m_GameData;
			if (gameData9 == null)
			{
				return;
			}
			iServerVerify.CServerConfigInfo serverConfigInfo = iServerVerify.GetInstance().GetServerConfigInfo();
			if (serverConfigInfo == null)
			{
				return;
			}
			List<iServerVerify.CServerConfigInfo.CServerDiscount> serverDiscount = serverConfigInfo.GetServerDiscount();
			if (serverDiscount == null)
			{
				return;
			}
			TUIGameInfo tUIGameInfo5 = new TUIGameInfo();
			tUIGameInfo5.all_sale_info = new TUIAllSaleInfo();
			OnSaleType onSaleType = OnSaleType.None;
			int id = -1;
			string name = string.Empty;
			string icon = string.Empty;
			UnitType unit_type = UnitType.Crystal;
			int price = 0;
			float discount = 0f;
			foreach (iServerVerify.CServerConfigInfo.CServerDiscount item5 in serverDiscount)
			{
				switch (item5.nType)
				{
				case 1:
				{
					CWeaponInfoLevel weaponInfo = gameData9.GetWeaponInfo(item5.nID, 1);
					if (weaponInfo == null)
					{
						continue;
					}
					onSaleType = OnSaleType.Weapon;
					id = item5.nID;
					name = weaponInfo.sName;
					unit_type = (weaponInfo.isCrystalPurchase ? UnitType.Crystal : UnitType.Gold);
					price = weaponInfo.nPurchasePrice;
					discount = (float)item5.nDiscount / 100f;
					break;
				}
				case 2:
				{
					CAvatarInfo cAvatarInfo2 = gameData9.m_AvatarCenter.Get(item5.nID);
					if (cAvatarInfo2 == null)
					{
						continue;
					}
					CAvatarInfoLevel cAvatarInfoLevel = cAvatarInfo2.Get(1);
					if (cAvatarInfoLevel == null)
					{
						continue;
					}
					onSaleType = ((cAvatarInfo2.m_nType != 1 && cAvatarInfo2.m_nType != 3 && cAvatarInfo2.m_nType != 5 && cAvatarInfo2.m_nType != 4) ? OnSaleType.Accessory : OnSaleType.Armor);
					id = item5.nID;
					name = cAvatarInfo2.m_sName;
					icon = cAvatarInfo2.m_sIcon;
					unit_type = (cAvatarInfoLevel.isCrystalPurchase ? UnitType.Crystal : UnitType.Gold);
					price = cAvatarInfoLevel.nPurchasePrice;
					discount = (float)item5.nDiscount / 100f;
					break;
				}
				case 4:
				{
					CSkillInfoLevel skillInfo = gameData9.GetSkillInfo(item5.nID, 1);
					if (skillInfo == null)
					{
						continue;
					}
					onSaleType = OnSaleType.Skill;
					id = item5.nID;
					name = skillInfo.sName;
					unit_type = (skillInfo.isCrystalPurchase ? UnitType.Crystal : UnitType.Gold);
					price = skillInfo.nPurchasePrice;
					discount = (float)item5.nDiscount / 100f;
					break;
				}
				case 3:
				{
					CCharacterInfo characterInfo4 = gameData9.GetCharacterInfo(item5.nID);
					if (characterInfo4 == null)
					{
						continue;
					}
					CCharacterInfoLevel cCharacterInfoLevel = characterInfo4.Get(1);
					if (cCharacterInfoLevel == null)
					{
						continue;
					}
					onSaleType = OnSaleType.Role;
					id = item5.nID;
					name = cCharacterInfoLevel.sName;
					unit_type = (characterInfo4.isCrystalPurchase ? UnitType.Crystal : UnitType.Gold);
					price = characterInfo4.nPurchasePrice;
					discount = (float)item5.nDiscount / 100f;
					break;
				}
				default:
					onSaleType = OnSaleType.None;
					break;
				}
				if (onSaleType != 0)
				{
					TUISingleSaleInfo tUISingleSaleInfo = new TUISingleSaleInfo(onSaleType, id, name, new TUIPriceInfo(price, unit_type), discount);
					tUISingleSaleInfo.icon = icon;
					tUIGameInfo5.all_sale_info.AddItem(tUISingleSaleInfo);
				}
			}
			iWeaponCenter weaponCenter = gameData9.GetWeaponCenter();
			if (weaponCenter != null)
			{
				Dictionary<int, CWeaponInfo> data4 = weaponCenter.GetData();
				if (data4 != null)
				{
					foreach (CWeaponInfo value4 in data4.Values)
					{
						if (serverConfigInfo != null && serverConfigInfo.IsGift(value4.nID))
						{
							CWeaponInfoLevel cWeaponInfoLevel = value4.Get(1);
							if (cWeaponInfoLevel != null)
							{
								onSaleType = OnSaleType.Weapon;
								id = value4.nID;
								name = cWeaponInfoLevel.sName;
								unit_type = (cWeaponInfoLevel.isCrystalPurchase ? UnitType.Crystal : UnitType.Gold);
								price = cWeaponInfoLevel.nPurchasePrice;
								discount = 0f;
								tUIGameInfo5.all_sale_info.AddItem(new TUISingleSaleInfo(onSaleType, id, name, new TUIPriceInfo(price, unit_type), discount));
							}
						}
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), tUIGameInfo5));
			iGameState gameState4 = iGameApp.GetInstance().m_GameState;
			if (gameState4 != null && gameState4.m_bNeedAutoSaleUI && TUIMappingInfo.Instance().GetNewHelpState() == NewHelpState.Help_Over)
			{
				SetPopWindow(2);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterSale)
		{
			iGameData gameData10 = iGameApp.GetInstance().m_GameData;
			if (gameData10 == null)
			{
				return;
			}
			iGameState gameState5 = iGameApp.GetInstance().m_GameState;
			if (gameState5 == null)
			{
				return;
			}
			OnSaleType wParam2 = (OnSaleType)m_event.GetWParam();
			int lparam2 = m_event.GetLparam();
			UnityEngine.Debug.Log(string.Concat(wParam2, " ", lparam2));
			TUISceneType wparam = TUISceneType.None;
			switch (wParam2)
			{
			case OnSaleType.Role:
				wparam = TUISceneType.Scene_Tavern;
				gameState5.m_nLinkCharacter = lparam2;
				break;
			case OnSaleType.Skill:
			{
				wparam = TUISceneType.Scene_Skill;
				gameState5.m_nLinkSkill = lparam2;
				iCharacterCenter characterCenter = gameData10.GetCharacterCenter();
				if (characterCenter != null)
				{
					CCharacterInfo infoBySkillID = characterCenter.GetInfoBySkillID(lparam2);
					if (infoBySkillID != null)
					{
						gameState5.m_nLinkSkillRole = infoBySkillID.nID;
					}
				}
				break;
			}
			case OnSaleType.Weapon:
			case OnSaleType.Armor:
			case OnSaleType.Accessory:
				wparam = TUISceneType.Scene_Forge;
				gameState5.m_nLinkWeapon = lparam2;
				break;
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), true, (int)wparam));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_CloseSale)
		{
			Debug.Log("TUIEvent_CloseSale");
			ShowPopWindow();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName()));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_DailyLoginBonusInfo)
		{
			iGameData gameData11 = iGameApp.GetInstance().m_GameData;
			if (gameData11 == null)
			{
				return;
			}
			iDataCenter dataCenter9 = gameData11.GetDataCenter();
			if (dataCenter9 == null)
			{
				return;
			}
			iDailyRewardCenter dailyRewardCenter = gameData11.GetDailyRewardCenter();
			if (dailyRewardCenter == null)
			{
				return;
			}
			UnityEngine.Debug.Log(dataCenter9.DailyRewardCount + " " + dataCenter9.DailyRewardHasGot);
			if (dataCenter9.DailyRewardCount != dataCenter9.DailyRewardHasGot)
			{
				TUIGameInfo tUIGameInfo6 = new TUIGameInfo();
				tUIGameInfo6.daily_bonus_info = new TUIDailyLoginBonusInfo();
				for (int j = 1; j < 8; j++)
				{
					CDailyRewardInfo cDailyRewardInfo = dailyRewardCenter.Get(j);
					if (cDailyRewardInfo != null)
					{
						tUIGameInfo6.daily_bonus_info.AddItem(new TUIPriceInfo(cDailyRewardInfo.nValue, cDailyRewardInfo.isCrystal ? UnitType.Crystal : UnitType.Gold));
					}
					else
					{
						tUIGameInfo6.daily_bonus_info.AddItem(new TUIPriceInfo(0, UnitType.Gold));
					}
				}
				tUIGameInfo6.daily_bonus_info.SetDay(dataCenter9.DailyRewardCount);
				SetPopWindow(1);
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), tUIGameInfo6));
			}
			else
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName()));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_ClickDailyLoginBonus)
		{
			bool success = false;
			iGameData gameData12 = iGameApp.GetInstance().m_GameData;
			if (gameData12 != null)
			{
				iDataCenter dataCenter10 = gameData12.GetDataCenter();
				if (dataCenter10 != null)
				{
					iDailyRewardCenter dailyRewardCenter2 = gameData12.GetDailyRewardCenter();
					if (dailyRewardCenter2 != null)
					{
						CDailyRewardInfo cDailyRewardInfo2 = dailyRewardCenter2.Get(dataCenter10.DailyRewardCount);
						if (cDailyRewardInfo2 != null)
						{
							if (cDailyRewardInfo2.isCrystal)
							{
								dataCenter10.AddCrystal(cDailyRewardInfo2.nValue);
							}
							else
							{
								dataCenter10.AddGold(cDailyRewardInfo2.nValue);
							}
							dataCenter10.DailyRewardHasGot = dataCenter10.DailyRewardCount;
							dataCenter10.Save();
							iServerSaveData.GetInstance().UploadImmidately();
							success = true;
						}
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), success));
			ShowPopWindow();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_DailyMissionsInfo)
		{
			iGameData gameData13 = iGameApp.GetInstance().m_GameData;
			if (gameData13 == null)
			{
				return;
			}
			iDataCenter dataCenter11 = gameData13.GetDataCenter();
			if (dataCenter11 == null)
			{
				return;
			}
			List<int> dailyTask = dataCenter11.GetDailyTask();
			if (dailyTask == null)
			{
				return;
			}
			bool success2 = false;
			TUIGameInfo tUIGameInfo7 = new TUIGameInfo();
			tUIGameInfo7.daily_missions_info = new TUIDailyMissionsInfo();
			foreach (int item6 in dailyTask)
			{
				CAchievementInfo achievementInfo = gameData13.GetAchievementInfo(item6);
				if (achievementInfo == null)
				{
					continue;
				}
				CAchievementStep step3 = achievementInfo.GetStep(0);
				if (step3 == null)
				{
					continue;
				}
				UnitType type = UnitType.Gold;
				int value = 0;
				if (step3.nRewardType == 2)
				{
					type = UnitType.Crystal;
					value = step3.nRewardNumber;
				}
				else if (step3.nRewardType == 1)
				{
					type = UnitType.Gold;
					GameLevelInfo gameLevelInfo = gameData13.GetGameLevelInfo(dataCenter11.LatestLevel);
					if (gameLevelInfo == null)
					{
						gameLevelInfo = gameData13.GetGameLevelInfo(1);
					}
					if (gameLevelInfo != null)
					{
						value = gameLevelInfo.nRewardGold * step3.nRewardNumber;
					}
				}
				TUIAchievementRewardInfo tUIAchievementRewardInfo2 = new TUIAchievementRewardInfo();
				tUIAchievementRewardInfo2.SetRewardInfo01(value, type);
				bool flag2 = true;
				int num2 = 0;
				CAchievementData achiData3 = dataCenter11.GetAchiData(item6);
				if (achiData3 != null)
				{
					flag2 = achiData3.IsGotReward(0);
					num2 = achiData3.nCurValue;
				}
				if (!flag2 && num2 >= step3.nStepPurpose)
				{
					success2 = true;
				}
				tUIGameInfo7.daily_missions_info.AddDailyMissionsInfo(new TUIOneDailyMissionsInfo(item6, achievementInfo.sName, string.Format(achievementInfo.sDesc, step3.nStepPurpose), tUIAchievementRewardInfo2, flag2, (int)Mathf.Clamp((float)num2 * 100f / (float)step3.nStepPurpose, 0f, 100f), Mathf.Clamp(num2, 0, step3.nStepPurpose) + "/" + step3.nStepPurpose));
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), tUIGameInfo7));
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_HadDailyMissionsReward, success2));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_TakeDailyMissionsReward)
		{
			int wParam3 = m_event.GetWParam();
			bool flag3 = false;
			iGameData gameData14 = iGameApp.GetInstance().m_GameData;
			if (gameData14 != null)
			{
				iDataCenter dataCenter12 = gameData14.GetDataCenter();
				if (dataCenter12 != null)
				{
					CAchievementInfo achievementInfo2 = gameData14.GetAchievementInfo(wParam3);
					if (achievementInfo2 != null)
					{
						CAchievementStep step4 = achievementInfo2.GetStep(0);
						if (step4 != null)
						{
							CAchievementData achiData4 = dataCenter12.GetAchiData(wParam3);
							if (achiData4 != null && !achiData4.IsGotReward(0) && achiData4.nCurValue >= step4.nStepPurpose)
							{
								Debug.Log("take reward " + wParam3);
								if (step4.nRewardType == 2)
								{
									achiData4.SetGotReward(0, true);
									dataCenter12.AddCrystal(step4.nRewardNumber);
									dataCenter12.Save();
									iServerSaveData.GetInstance().UploadImmidately();
									flag3 = true;
								}
								else if (step4.nRewardType == 1)
								{
									GameLevelInfo gameLevelInfo2 = gameData14.GetGameLevelInfo(dataCenter12.LatestLevel);
									if (gameLevelInfo2 == null)
									{
										gameLevelInfo2 = gameData14.GetGameLevelInfo(1);
									}
									if (gameLevelInfo2 != null)
									{
										achiData4.SetGotReward(0, true);
										dataCenter12.AddGold(gameLevelInfo2.nRewardGold * step4.nRewardNumber);
										dataCenter12.Save();
										iServerSaveData.GetInstance().UploadImmidately();
										flag3 = true;
									}
								}
							}
						}
					}
				}
			}
			if (flag3)
			{
				if (iGameApp.GetInstance().CheckDailyAchieveReward())
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_HadDailyMissionsReward, true));
				}
				else
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_HadDailyMissionsReward));
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), flag3));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_ShowUDID)
		{
			iGameApp.GetInstance().ScreenLog("msg title " + iServerSaveData.GetInstance().GetDeviceInfoTitle());
			iGameApp.GetInstance().ScreenLog("msg content " + iServerSaveData.GetInstance().GetDeviceInfoDesc());
			CMessageBoxScript.GetInstance().MessageBox(iServerSaveData.GetInstance().GetDeviceInfoTitle(), iServerSaveData.GetInstance().GetDeviceInfoDesc(), null, null, "OK");
		}
		else
		{
			if (m_event.GetEventName() != TUIEvent.SceneMainMenuEventType.TUIEvent_SkipTutorial)
			{
				return;
			}
			iGameData gameData15 = iGameApp.GetInstance().m_GameData;
			if (gameData15 != null)
			{
				iDataCenter dataCenter13 = gameData15.GetDataCenter();
				if (dataCenter13 != null)
				{
					dataCenter13.nTutorialVillageState = 25;
					iGameApp.GetInstance().SaveData(true);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(m_event.GetEventName(), true));
				}
			}
		}
	}

	private void TUIEvent_BackInfo_SceneEquip(object sender, TUIEvent.SendEvent_SceneEquip m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_TopBar)
		{
			iGameData gameData = iGameApp.GetInstance().m_GameData;
			if (gameData == null)
			{
				return;
			}
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter == null)
			{
				return;
			}
			CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
			if (character == null)
			{
				return;
			}
			CCharacterInfoLevel characterInfo = gameData.GetCharacterInfo(character.nID, character.nLevel);
			if (characterInfo != null)
			{
				AndroidReturnPlugin.instance.SetBackFunc(EventBack_Equip);
				TUIGameInfo tUIGameInfo = new TUIGameInfo();
				tUIGameInfo.player_info = new TUIPlayerInfo();
				tUIGameInfo.player_info.role_id = character.nID;
				tUIGameInfo.player_info.level = character.nLevel;
				tUIGameInfo.player_info.level_exp = characterInfo.nExp;
				tUIGameInfo.player_info.exp = character.nExp;
				tUIGameInfo.player_info.gold = dataCenter.Gold;
				tUIGameInfo.player_info.crystal = dataCenter.Crystal;
				iGameState gameState = iGameApp.GetInstance().m_GameState;
				if (gameState != null)
				{
					gameState.m_curScene4Recommand = gameState.m_lstScene4Recommand;
					gameState.m_lstScene4Recommand = TUISceneType.None;
					gameState.m_curScene4Equip = gameState.m_lstScene4Equip;
					gameState.m_lstScene4Equip = TUISceneType.None;
				}
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), tUIGameInfo));
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SetBattlePower, true, iGameApp.GetInstance().CalcCombatPower()));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_RoleSign)
		{
			iGameData gameData2 = iGameApp.GetInstance().m_GameData;
			if (gameData2 == null)
			{
				return;
			}
			iDataCenter dataCenter2 = gameData2.GetDataCenter();
			if (dataCenter2 == null)
			{
				return;
			}
			iAvatarCenter avatarCenter = gameData2.m_AvatarCenter;
			if (avatarCenter == null)
			{
				return;
			}
			iShopDisplayCenter shopDisplayCenter = gameData2.m_ShopDisplayCenter;
			if (shopDisplayCenter == null)
			{
				return;
			}
			CCharSaveInfo character2 = dataCenter2.GetCharacter(dataCenter2.CurCharID);
			if (character2 == null)
			{
				return;
			}
			TUIGameInfo tUIGameInfo2 = new TUIGameInfo();
			tUIGameInfo2.equip_info = new TUIEquipInfo();
			string name = "No Name";
			string introduce = "No Desc";
			CCharacterInfoLevel cCharacterInfoLevel = null;
			int[] array = shopDisplayCenter.m_ltCharacter.ToArray();
			tUIGameInfo2.equip_info.roles_list = new List<TUIPopupInfo>();
			for (int i = 0; i < array.Length; i++)
			{
				CCharSaveInfo character3 = dataCenter2.GetCharacter(array[i]);
				if (character3 == null || character3.nLevel == -1)
				{
					continue;
				}
				cCharacterInfoLevel = gameData2.GetCharacterInfo(array[i], 1);
				if (cCharacterInfoLevel != null)
				{
					name = cCharacterInfoLevel.sName;
					introduce = cCharacterInfoLevel.sDesc;
				}
				TUIPopupInfo tUIPopupInfo = new TUIPopupInfo(array[i], name, introduce, character3.nExp, character3.nLevel);
				tUIPopupInfo.m_PopupType = PopupType.Roles;
				tUIPopupInfo.m_CharacterAttribute = new TUICharacterAttribute();
				tUIPopupInfo.m_CharacterAttribute.m_nModelID = cCharacterInfoLevel.nModel;
				tUIPopupInfo.m_CharacterAttribute.m_nAvatarHead = ((m_DataCenter.AvatarHead <= 0) ? cCharacterInfoLevel.nAvatarHead : m_DataCenter.AvatarHead);
				tUIPopupInfo.m_CharacterAttribute.m_nAvatarUpper = ((m_DataCenter.AvatarUpper <= 0) ? cCharacterInfoLevel.nAvatarUpper : m_DataCenter.AvatarUpper);
				tUIPopupInfo.m_CharacterAttribute.m_nAvatarLower = ((m_DataCenter.AvatarLower <= 0) ? cCharacterInfoLevel.nAvatarLower : m_DataCenter.AvatarLower);
				tUIPopupInfo.m_CharacterAttribute.m_nAvatarHeadup = ((m_DataCenter.AvatarHeadup <= 0) ? (-1) : m_DataCenter.AvatarHeadup);
				tUIPopupInfo.m_CharacterAttribute.m_nAvatarBracelet = ((m_DataCenter.AvatarWrist <= 0) ? (-1) : m_DataCenter.AvatarWrist);
				tUIPopupInfo.m_CharacterAttribute.m_nAvatarNeck = ((m_DataCenter.AvatarNeck <= 0) ? (-1) : m_DataCenter.AvatarNeck);
				if (iGameApp.GetInstance().CheckCharacterSignState(3, array[i]))
				{
					tUIPopupInfo.m_MarkType = NewMarkType.New;
				}
				else
				{
					tUIPopupInfo.m_MarkType = NewMarkType.None;
				}
				tUIGameInfo2.equip_info.roles_list.Add(tUIPopupInfo);
				foreach (TUIPopupInfo item in tUIGameInfo2.equip_info.roles_list)
				{
					if (item.texture_id == dataCenter2.CurCharID)
					{
						tUIGameInfo2.equip_info.role = item;
						break;
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), tUIGameInfo2));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_SkillSign)
		{
			iGameData gameData3 = iGameApp.GetInstance().m_GameData;
			if (gameData3 == null)
			{
				return;
			}
			iDataCenter dataCenter3 = gameData3.GetDataCenter();
			if (dataCenter3 == null)
			{
				return;
			}
			iSkillCenter skillCenter = gameData3.GetSkillCenter();
			if (skillCenter == null)
			{
				return;
			}
			CCharSaveInfo character4 = dataCenter3.GetCharacter(dataCenter3.CurCharID);
			if (character4 == null)
			{
				return;
			}
			CCharacterInfoLevel characterInfo2 = gameData3.GetCharacterInfo(character4.nID, character4.nLevel);
			if (characterInfo2 == null)
			{
				return;
			}
			TUIGameInfo tUIGameInfo3 = new TUIGameInfo();
			tUIGameInfo3.equip_info = new TUIEquipInfo();
			tUIGameInfo3.equip_info.skill_list = new List<TUIPopupInfo>();
			int curCharID = dataCenter3.CurCharID;
			CCharacterInfo characterInfo3 = gameData3.GetCharacterInfo(curCharID);
			if (characterInfo3 != null && characterInfo3.ltCharacterPassiveSkill != null)
			{
				for (int j = 0; j < characterInfo3.ltCharacterPassiveSkill.Count; j++)
				{
					CSkillInfo skillInfo = gameData3.GetSkillInfo(characterInfo3.ltCharacterPassiveSkill[j]);
					if (skillInfo == null)
					{
						continue;
					}
					int nSkillLevel = 0;
					if (!dataCenter3.GetPassiveSkill(skillInfo.nID, ref nSkillLevel))
					{
						continue;
					}
					CSkillInfoLevel cSkillInfoLevel = skillInfo.Get(nSkillLevel);
					if (cSkillInfoLevel != null && cSkillInfoLevel.nType == 1)
					{
						TUIPopupInfo tUIPopupInfo2 = new TUIPopupInfo(skillInfo.nID, cSkillInfoLevel.sName, cSkillInfoLevel.sDesc);
						tUIPopupInfo2.m_PopupType = PopupType.Skills;
						if (iGameApp.GetInstance().CheckSkillSignState(3, skillInfo.nID))
						{
							tUIPopupInfo2.m_MarkType = NewMarkType.New;
						}
						else
						{
							tUIPopupInfo2.m_MarkType = NewMarkType.None;
						}
						tUIGameInfo3.equip_info.skill_list.Add(tUIPopupInfo2);
					}
				}
			}
			if (characterInfo2 != null)
			{
				int nSkill = characterInfo2.nSkill;
				int nSkillLevel2 = -1;
				dataCenter3.GetSkill(nSkill, ref nSkillLevel2);
				CSkillInfoLevel skillInfo2 = gameData3.GetSkillInfo(characterInfo2.nSkill, nSkillLevel2);
				if (skillInfo2 != null)
				{
					tUIGameInfo3.equip_info.skill = new TUIPopupInfo(characterInfo2.nSkill, skillInfo2.sName, skillInfo2.sDesc);
					tUIGameInfo3.equip_info.skill.m_PopupType = PopupType.Skills01;
					tUIGameInfo3.equip_info.skill.m_MarkType = NewMarkType.None;
					tUIGameInfo3.equip_info.skill.duoren_skill = nSkillLevel2 == 2;
				}
			}
			int[] array2 = new int[3];
			int[] array3 = new int[3];
			for (int k = 0; k < 3; k++)
			{
				array2[k] = dataCenter3.GetSelectPassiveSkill(dataCenter3.CurCharID, k);
				dataCenter3.GetPassiveSkill(array2[k], ref array3[k]);
			}
			for (int l = 0; l < 3; l++)
			{
				if (array2[l] <= 0 || array3[l] <= 0)
				{
					continue;
				}
				foreach (TUIPopupInfo item2 in tUIGameInfo3.equip_info.skill_list)
				{
					if (item2.texture_id == array2[l])
					{
						tUIGameInfo3.equip_info.arrSkill[l] = item2;
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), tUIGameInfo3));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_WeaponSign)
		{
			iGameData gameData4 = iGameApp.GetInstance().m_GameData;
			if (gameData4 == null)
			{
				return;
			}
			iDataCenter dataCenter4 = gameData4.GetDataCenter();
			if (dataCenter4 == null)
			{
				return;
			}
			iWeaponCenter weaponCenter = gameData4.GetWeaponCenter();
			if (weaponCenter == null)
			{
				return;
			}
			iItemCenter itemCenter = gameData4.GetItemCenter();
			if (itemCenter == null || m_GameState == null)
			{
				return;
			}
			TUIGameInfo tUIGameInfo4 = new TUIGameInfo();
			tUIGameInfo4.equip_info = new TUIEquipInfo();
			tUIGameInfo4.equip_info.m_GotoEquip_PopupType = (PopupType)m_GameState.m_nGotoEquip_PopupType;
			m_GameState.m_nGotoEquip_PopupType = -1;
			if (m_GameData.m_WeaponCenter != null)
			{
				Dictionary<int, int> weaponData = m_DataCenter.GetWeaponData();
				foreach (KeyValuePair<int, int> item3 in weaponData)
				{
					int key = item3.Key;
					int value = item3.Value;
					CWeaponInfo cWeaponInfo = m_GameData.m_WeaponCenter.Get(key);
					if (cWeaponInfo == null)
					{
						continue;
					}
					CWeaponInfoLevel cWeaponInfoLevel = cWeaponInfo.Get(value);
					if (cWeaponInfoLevel == null)
					{
						continue;
					}
					TUIWeaponAttribute tUIWeaponAttribute = new TUIWeaponAttribute();
					tUIWeaponAttribute.ammo = cWeaponInfoLevel.nCapacity;
					tUIWeaponAttribute.damage = (int)cWeaponInfoLevel.fDamage;
					tUIWeaponAttribute.fire_rate = cWeaponInfoLevel.fShootSpeed;
					if (cWeaponInfoLevel.nType == 5)
					{
						tUIWeaponAttribute.blast_radius = 20f;
					}
					for (int m = 0; m < 3; m++)
					{
						if (cWeaponInfoLevel.arrFunc[m] == 4)
						{
							tUIWeaponAttribute.knockback = cWeaponInfoLevel.arrValueY[m];
							break;
						}
					}
					TUIPopupInfo tUIPopupInfo3 = new TUIPopupInfo(cWeaponInfo.nID, cWeaponInfoLevel.sName, string.Empty, tUIWeaponAttribute);
					if (iGameApp.GetInstance().CheckWeaponSignState(3, cWeaponInfo.nID))
					{
						tUIPopupInfo3.m_MarkType = NewMarkType.New;
						Debug.Log(cWeaponInfo.nID + " is new!");
					}
					if (cWeaponInfoLevel.nType == 1)
					{
						tUIPopupInfo3.m_PopupType = PopupType.Weapons01;
						tUIGameInfo4.equip_info.ltWeaponMelee.Add(tUIPopupInfo3);
					}
					else
					{
						tUIPopupInfo3.m_PopupType = PopupType.Weapons02;
						tUIGameInfo4.equip_info.ltWeaponRange.Add(tUIPopupInfo3);
					}
				}
			}
			if (m_GameData.m_AvatarCenter != null)
			{
				Dictionary<int, int> avatarData = m_DataCenter.GetAvatarData();
				foreach (KeyValuePair<int, int> item4 in avatarData)
				{
					int key2 = item4.Key;
					int value2 = item4.Value;
					if (value2 < 1)
					{
						continue;
					}
					CAvatarInfo cAvatarInfo = m_GameData.m_AvatarCenter.Get(key2);
					if (cAvatarInfo == null)
					{
						continue;
					}
					CAvatarInfoLevel cAvatarInfoLevel = cAvatarInfo.Get(value2);
					if (cAvatarInfoLevel == null)
					{
						continue;
					}
					TUIArmorAttribute tUIArmorAttribute = new TUIArmorAttribute();
					for (int n = 0; n < 3; n++)
					{
						if (cAvatarInfoLevel.arrFunc[n] == 1)
						{
							kProEnum kProEnum2 = (kProEnum)MyUtils.Low32(cAvatarInfoLevel.arrValueX[n]);
							if (kProEnum2 == kProEnum.Protect)
							{
								tUIArmorAttribute.def = cAvatarInfoLevel.arrValueY[n];
								break;
							}
						}
					}
					TUIPopupInfo tUIPopupInfo4 = new TUIPopupInfo(key2, cAvatarInfo.m_sName, cAvatarInfoLevel.sDesc, tUIArmorAttribute);
					if (iGameApp.GetInstance().CheckAvatarSignState(3, cAvatarInfo.m_nID))
					{
						tUIPopupInfo4.m_MarkType = NewMarkType.New;
						Debug.Log(cAvatarInfo.m_nID + " is new!");
					}
					tUIPopupInfo4.m_sIcon = cAvatarInfo.m_sIcon;
					Debug.Log(cAvatarInfo.m_nType + " " + tUIPopupInfo4.m_sIcon);
					switch (cAvatarInfo.m_nType)
					{
					case 1:
						tUIPopupInfo4.m_PopupType = PopupType.Armor_Head;
						tUIGameInfo4.equip_info.ltArmorHead.Add(tUIPopupInfo4);
						break;
					case 3:
						tUIPopupInfo4.m_PopupType = PopupType.Armor_Body;
						tUIGameInfo4.equip_info.ltArmorUpper.Add(tUIPopupInfo4);
						break;
					case 5:
						tUIPopupInfo4.m_PopupType = PopupType.Armor_Leg;
						tUIGameInfo4.equip_info.ltArmorLower.Add(tUIPopupInfo4);
						break;
					case 0:
						tUIPopupInfo4.m_PopupType = PopupType.Accessory_Halo;
						tUIGameInfo4.equip_info.ltAccessoryHeadup.Add(tUIPopupInfo4);
						break;
					case 2:
						tUIPopupInfo4.m_PopupType = PopupType.Accessory_Necklace;
						tUIGameInfo4.equip_info.ltAccessoryNeck.Add(tUIPopupInfo4);
						break;
					case 4:
						tUIPopupInfo4.m_PopupType = PopupType.Armor_Bracelet;
						tUIGameInfo4.equip_info.ltArmorBracelet.Add(tUIPopupInfo4);
						break;
					case 6:
						tUIPopupInfo4.m_PopupType = PopupType.Accessory_Badge;
						tUIGameInfo4.equip_info.ltAccessoryBadge.Add(tUIPopupInfo4);
						break;
					case 7:
						tUIPopupInfo4.m_PopupType = PopupType.Accessory_Stoneskin;
						tUIGameInfo4.equip_info.ltAccessoryStone.Add(tUIPopupInfo4);
						break;
					}
				}
			}
			if (m_GameData.m_WeaponCenter != null)
			{
				for (int num = 0; num < 3; num++)
				{
					int selectWeapon = dataCenter4.GetSelectWeapon(num);
					int nLevel = 0;
					dataCenter4.GetWeaponLevel(selectWeapon, ref nLevel);
					CWeaponInfoLevel cWeaponInfoLevel2 = m_GameData.m_WeaponCenter.Get(selectWeapon, nLevel);
					if (cWeaponInfoLevel2 == null)
					{
						continue;
					}
					TUIPopupInfo tUIPopupInfo5 = null;
					int nType = cWeaponInfoLevel2.nType;
					if (nType == 1)
					{
						foreach (TUIPopupInfo item5 in tUIGameInfo4.equip_info.ltWeaponMelee)
						{
							if (item5.texture_id == selectWeapon)
							{
								tUIPopupInfo5 = item5;
								break;
							}
						}
					}
					else
					{
						foreach (TUIPopupInfo item6 in tUIGameInfo4.equip_info.ltWeaponRange)
						{
							if (item6.texture_id == selectWeapon)
							{
								tUIPopupInfo5 = item6;
								break;
							}
						}
					}
					if (tUIPopupInfo5 != null)
					{
						tUIGameInfo4.equip_info.SetWeapon(num, tUIPopupInfo5);
					}
				}
			}
			if (m_GameData.m_AvatarCenter != null)
			{
				int[] array4 = new int[8] { m_DataCenter.AvatarHead, m_DataCenter.AvatarUpper, m_DataCenter.AvatarLower, m_DataCenter.AvatarWrist, m_DataCenter.AvatarHeadup, m_DataCenter.AvatarNeck, m_DataCenter.AvatarBadge, m_DataCenter.AvatarStone };
				for (int num2 = 0; num2 < array4.Length; num2++)
				{
					int num3 = array4[num2];
					int avatarlevel = 0;
					m_DataCenter.GetAvatar(num3, ref avatarlevel);
					CAvatarInfo cAvatarInfo2 = m_GameData.m_AvatarCenter.Get(num3);
					if (cAvatarInfo2 == null)
					{
						continue;
					}
					bool flag = false;
					int nIndex = -1;
					List<TUIPopupInfo> list = null;
					switch (cAvatarInfo2.m_nType)
					{
					case 1:
						flag = true;
						nIndex = 0;
						list = tUIGameInfo4.equip_info.ltArmorHead;
						break;
					case 3:
						flag = true;
						nIndex = 1;
						list = tUIGameInfo4.equip_info.ltArmorUpper;
						break;
					case 4:
						flag = true;
						nIndex = 2;
						list = tUIGameInfo4.equip_info.ltArmorBracelet;
						break;
					case 5:
						flag = true;
						nIndex = 3;
						list = tUIGameInfo4.equip_info.ltArmorLower;
						break;
					case 0:
						flag = false;
						nIndex = 0;
						list = tUIGameInfo4.equip_info.ltAccessoryHeadup;
						break;
					case 2:
						flag = false;
						nIndex = 1;
						list = tUIGameInfo4.equip_info.ltAccessoryNeck;
						break;
					case 6:
						flag = false;
						nIndex = 2;
						list = tUIGameInfo4.equip_info.ltAccessoryBadge;
						break;
					case 7:
						flag = false;
						nIndex = 3;
						list = tUIGameInfo4.equip_info.ltAccessoryStone;
						break;
					}
					if (list == null)
					{
						continue;
					}
					foreach (TUIPopupInfo item7 in list)
					{
						if (item7.texture_id == num3)
						{
							Debug.Log(num2 + " " + item7);
							if (flag)
							{
								tUIGameInfo4.equip_info.SetArmor(nIndex, item7);
							}
							else
							{
								tUIGameInfo4.equip_info.SetAccessory(nIndex, item7);
							}
						}
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), tUIGameInfo4));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_RoleEquip)
		{
			iGameData gameData5 = iGameApp.GetInstance().m_GameData;
			if (gameData5 == null)
			{
				return;
			}
			iDataCenter dataCenter5 = gameData5.GetDataCenter();
			if (dataCenter5 == null)
			{
				return;
			}
			int wParam = m_event.GetWParam();
			if (dataCenter5.GetCharacter(wParam) == null)
			{
				return;
			}
			dataCenter5.CurCharID = wParam;
			TUIGameInfo tUIGameInfo5 = new TUIGameInfo();
			tUIGameInfo5.player_info = new TUIPlayerInfo();
			tUIGameInfo5.equip_info = new TUIEquipInfo();
			CCharSaveInfo character5 = dataCenter5.GetCharacter(dataCenter5.CurCharID);
			CCharacterInfoLevel characterInfo4 = gameData5.GetCharacterInfo(character5.nID, character5.nLevel);
			if (character5 != null && characterInfo4 != null)
			{
				tUIGameInfo5.player_info.role_id = character5.nID;
				tUIGameInfo5.player_info.level = character5.nLevel;
				tUIGameInfo5.player_info.level_exp = characterInfo4.nExp;
				tUIGameInfo5.player_info.exp = character5.nExp;
				tUIGameInfo5.player_info.gold = dataCenter5.Gold;
				tUIGameInfo5.player_info.crystal = dataCenter5.Crystal;
				tUIGameInfo5.equip_info.skill_list = new List<TUIPopupInfo>();
				int curCharID2 = dataCenter5.CurCharID;
				CCharacterInfo characterInfo5 = gameData5.GetCharacterInfo(curCharID2);
				if (characterInfo5 != null && characterInfo5.ltCharacterPassiveSkill != null)
				{
					for (int num4 = 0; num4 < characterInfo5.ltCharacterPassiveSkill.Count; num4++)
					{
						CSkillInfo skillInfo3 = gameData5.GetSkillInfo(characterInfo5.ltCharacterPassiveSkill[num4]);
						if (skillInfo3 == null)
						{
							continue;
						}
						int nSkillLevel3 = 0;
						if (!dataCenter5.GetPassiveSkill(skillInfo3.nID, ref nSkillLevel3))
						{
							continue;
						}
						CSkillInfoLevel cSkillInfoLevel2 = skillInfo3.Get(nSkillLevel3);
						if (cSkillInfoLevel2 != null && cSkillInfoLevel2.nType == 1)
						{
							TUIPopupInfo tUIPopupInfo6 = new TUIPopupInfo(skillInfo3.nID, cSkillInfoLevel2.sName, cSkillInfoLevel2.sDesc);
							tUIPopupInfo6.m_PopupType = PopupType.Skills;
							if (iGameApp.GetInstance().CheckSkillSignState(3, skillInfo3.nID))
							{
								tUIPopupInfo6.m_MarkType = NewMarkType.New;
							}
							else
							{
								tUIPopupInfo6.m_MarkType = NewMarkType.None;
							}
							tUIGameInfo5.equip_info.skill_list.Add(tUIPopupInfo6);
						}
					}
				}
				if (characterInfo4 != null)
				{
					int nSkill2 = characterInfo4.nSkill;
					int nSkillLevel4 = -1;
					dataCenter5.GetSkill(nSkill2, ref nSkillLevel4);
					CSkillInfoLevel skillInfo4 = gameData5.GetSkillInfo(characterInfo4.nSkill, nSkillLevel4);
					if (skillInfo4 != null)
					{
						tUIGameInfo5.equip_info.skill = new TUIPopupInfo(characterInfo4.nSkill, skillInfo4.sName, skillInfo4.sDesc);
						tUIGameInfo5.equip_info.skill.m_PopupType = PopupType.Skills01;
						tUIGameInfo5.equip_info.skill.m_MarkType = NewMarkType.None;
						tUIGameInfo5.equip_info.skill.duoren_skill = nSkillLevel4 == 2;
					}
				}
				int[] array5 = new int[3];
				int[] array6 = new int[3];
				for (int num5 = 0; num5 < 3; num5++)
				{
					array5[num5] = dataCenter5.GetSelectPassiveSkill(dataCenter5.CurCharID, num5);
					dataCenter5.GetPassiveSkill(array5[num5], ref array6[num5]);
				}
				for (int num6 = 0; num6 < 3; num6++)
				{
					if (array5[num6] <= 0 || array6[num6] <= 0)
					{
						continue;
					}
					foreach (TUIPopupInfo item8 in tUIGameInfo5.equip_info.skill_list)
					{
						if (item8.texture_id == array5[num6])
						{
							tUIGameInfo5.equip_info.arrSkill[num6] = item8;
						}
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_RoleEquip, tUIGameInfo5, true));
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SkillSign, tUIGameInfo5, true));
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_TopBar, tUIGameInfo5, true));
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SetBattlePower, true, iGameApp.GetInstance().CalcCombatPower(), 1));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_SkillEquip)
		{
			iGameData gameData6 = iGameApp.GetInstance().m_GameData;
			if (gameData6 != null)
			{
				iDataCenter dataCenter6 = gameData6.GetDataCenter();
				if (dataCenter6 != null)
				{
					int wParam2 = m_event.GetWParam();
					int lparam = m_event.GetLparam();
					dataCenter6.SetSelectPassiveSkill(dataCenter6.CurCharID, wParam2, lparam);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), true));
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SetBattlePower, true, iGameApp.GetInstance().CalcCombatPower(), 1));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_SkillUnEquip)
		{
			iGameData gameData7 = iGameApp.GetInstance().m_GameData;
			if (gameData7 != null)
			{
				iDataCenter dataCenter7 = gameData7.GetDataCenter();
				if (dataCenter7 != null)
				{
					int wParam3 = m_event.GetWParam();
					int lparam2 = m_event.GetLparam();
					dataCenter7.SetSelectPassiveSkill(dataCenter7.CurCharID, wParam3, -1);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), true));
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SetBattlePower, true, iGameApp.GetInstance().CalcCombatPower(), 1));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_SkillExchange)
		{
			iGameData gameData8 = iGameApp.GetInstance().m_GameData;
			if (gameData8 != null)
			{
				iDataCenter dataCenter8 = gameData8.GetDataCenter();
				if (dataCenter8 != null)
				{
					int wParam4 = m_event.GetWParam();
					int lparam3 = m_event.GetLparam();
					int selectPassiveSkill = dataCenter8.GetSelectPassiveSkill(dataCenter8.CurCharID, wParam4);
					int selectPassiveSkill2 = dataCenter8.GetSelectPassiveSkill(dataCenter8.CurCharID, lparam3);
					dataCenter8.SetSelectPassiveSkill(dataCenter8.CurCharID, wParam4, selectPassiveSkill2);
					dataCenter8.SetSelectPassiveSkill(dataCenter8.CurCharID, lparam3, selectPassiveSkill);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), true));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_WeaponEquip)
		{
			iGameData gameData9 = iGameApp.GetInstance().m_GameData;
			if (gameData9 == null)
			{
				return;
			}
			iDataCenter dataCenter9 = gameData9.GetDataCenter();
			if (dataCenter9 == null)
			{
				return;
			}
			iAvatarCenter avatarCenter2 = gameData9.m_AvatarCenter;
			if (avatarCenter2 == null)
			{
				return;
			}
			int wParam5 = m_event.GetWParam();
			int lparam4 = m_event.GetLparam();
			PopupType type = m_event.GetType();
			int avatarlevel2 = -1;
			switch (type)
			{
			default:
				return;
			case PopupType.Weapons01:
			case PopupType.Weapons02:
				if (!dataCenter9.GetWeaponLevel(lparam4, ref avatarlevel2) || avatarlevel2 < 1)
				{
					return;
				}
				dataCenter9.SetSelectWeapon(wParam5, lparam4);
				break;
			case PopupType.Armor_Head:
				if (!dataCenter9.GetAvatar(lparam4, ref avatarlevel2) || avatarlevel2 < 1)
				{
					return;
				}
				dataCenter9.AvatarHead = lparam4;
				break;
			case PopupType.Armor_Body:
				if (!dataCenter9.GetAvatar(lparam4, ref avatarlevel2) || avatarlevel2 < 1)
				{
					return;
				}
				dataCenter9.AvatarUpper = lparam4;
				break;
			case PopupType.Armor_Leg:
				if (!dataCenter9.GetAvatar(lparam4, ref avatarlevel2) || avatarlevel2 < 1)
				{
					return;
				}
				dataCenter9.AvatarLower = lparam4;
				break;
			case PopupType.Armor_Bracelet:
				if (!dataCenter9.GetAvatar(lparam4, ref avatarlevel2) || avatarlevel2 < 1)
				{
					return;
				}
				dataCenter9.AvatarWrist = lparam4;
				break;
			case PopupType.Accessory_Badge:
				if (!dataCenter9.GetAvatar(lparam4, ref avatarlevel2) || avatarlevel2 < 1)
				{
					return;
				}
				dataCenter9.AvatarBadge = lparam4;
				break;
			case PopupType.Accessory_Halo:
				if (!dataCenter9.GetAvatar(lparam4, ref avatarlevel2) || avatarlevel2 < 1)
				{
					return;
				}
				dataCenter9.AvatarHeadup = lparam4;
				break;
			case PopupType.Accessory_Necklace:
				if (!dataCenter9.GetAvatar(lparam4, ref avatarlevel2) || avatarlevel2 < 1)
				{
					return;
				}
				dataCenter9.AvatarNeck = lparam4;
				break;
			case PopupType.Accessory_Stoneskin:
				if (!dataCenter9.GetAvatar(lparam4, ref avatarlevel2) || avatarlevel2 < 1)
				{
					return;
				}
				dataCenter9.AvatarStone = lparam4;
				break;
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), true));
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SetBattlePower, true, iGameApp.GetInstance().CalcCombatPower(), 1));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_WeaponUnEquip)
		{
			iGameData gameData10 = iGameApp.GetInstance().m_GameData;
			if (gameData10 == null)
			{
				return;
			}
			iDataCenter dataCenter10 = gameData10.GetDataCenter();
			if (dataCenter10 == null)
			{
				return;
			}
			iAvatarCenter avatarCenter3 = gameData10.m_AvatarCenter;
			if (avatarCenter3 != null)
			{
				int wParam6 = m_event.GetWParam();
				int lparam5 = m_event.GetLparam();
				switch (m_event.GetType())
				{
				default:
					return;
				case PopupType.Weapons01:
				case PopupType.Weapons02:
					return;
				case PopupType.Armor_Head:
					dataCenter10.AvatarHead = -1;
					break;
				case PopupType.Armor_Body:
					dataCenter10.AvatarUpper = -1;
					break;
				case PopupType.Armor_Leg:
					dataCenter10.AvatarLower = -1;
					break;
				case PopupType.Armor_Bracelet:
					dataCenter10.AvatarWrist = -1;
					break;
				case PopupType.Accessory_Badge:
					dataCenter10.AvatarBadge = -1;
					break;
				case PopupType.Accessory_Halo:
					dataCenter10.AvatarHeadup = -1;
					break;
				case PopupType.Accessory_Necklace:
					dataCenter10.AvatarNeck = -1;
					break;
				case PopupType.Accessory_Stoneskin:
					dataCenter10.AvatarStone = -1;
					break;
				}
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), true));
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SetBattlePower, true, iGameApp.GetInstance().CalcCombatPower(), 1));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_WeaponExchange)
		{
			iGameData gameData11 = iGameApp.GetInstance().m_GameData;
			if (gameData11 == null)
			{
				return;
			}
			iDataCenter dataCenter11 = gameData11.GetDataCenter();
			if (dataCenter11 != null)
			{
				int wParam7 = m_event.GetWParam();
				int lparam6 = m_event.GetLparam();
				PopupType type2 = m_event.GetType();
				if (type2 == PopupType.Weapons02)
				{
					int selectWeapon2 = dataCenter11.GetSelectWeapon(wParam7);
					int selectWeapon3 = dataCenter11.GetSelectWeapon(lparam6);
					dataCenter11.SetSelectWeapon(wParam7, selectWeapon3);
					dataCenter11.SetSelectWeapon(lparam6, selectWeapon2);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), true));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_Back)
		{
			iGameApp.GetInstance().SaveData();
			iGameState gameState2 = iGameApp.GetInstance().m_GameState;
			if (gameState2 != null)
			{
				if (gameState2.m_curScene4Recommand != 0)
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), true, (int)gameState2.m_curScene4Recommand));
					gameState2.m_curScene4Recommand = TUISceneType.None;
				}
				else if (gameState2.m_curScene4Equip != 0)
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), true, (int)gameState2.m_curScene4Equip));
					gameState2.m_curScene4Equip = TUISceneType.None;
				}
				else
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), true));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_RolesChoose)
		{
			int wParam8 = m_event.GetWParam();
			iGameData gameData12 = iGameApp.GetInstance().m_GameData;
			if (gameData12 == null)
			{
				return;
			}
			iDataCenter dataCenter12 = gameData12.GetDataCenter();
			if (dataCenter12 == null)
			{
				return;
			}
			int nSignState = 0;
			if (dataCenter12.GetCharacterSign(wParam8, ref nSignState))
			{
				if (nSignState == 3)
				{
					dataCenter12.SetCharacterSign(wParam8, 4);
					dataCenter12.Save();
				}
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName()));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_WeaponChoose)
		{
			int wParam9 = m_event.GetWParam();
			iGameData gameData13 = iGameApp.GetInstance().m_GameData;
			if (gameData13 == null)
			{
				return;
			}
			iDataCenter dataCenter13 = gameData13.GetDataCenter();
			if (dataCenter13 != null)
			{
				int nSignState2 = 0;
				if (dataCenter13.GetWeaponSign(wParam9, ref nSignState2) && nSignState2 == 3)
				{
					dataCenter13.SetWeaponSign(wParam9, 4);
				}
				if (dataCenter13.GetAvatarSign(wParam9, ref nSignState2) && nSignState2 == 3)
				{
					dataCenter13.SetAvatarSign(wParam9, 4);
				}
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName()));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_SkillChoose)
		{
			int wParam10 = m_event.GetWParam();
			iGameData gameData14 = iGameApp.GetInstance().m_GameData;
			if (gameData14 == null)
			{
				return;
			}
			iDataCenter dataCenter14 = gameData14.GetDataCenter();
			if (dataCenter14 == null)
			{
				return;
			}
			int nSignState3 = 0;
			if (dataCenter14.GetSkillSign(wParam10, ref nSignState3))
			{
				if (nSignState3 == 3)
				{
					dataCenter14.SetSkillSign(wParam10, 4);
					dataCenter14.Save();
				}
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName()));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_EnterIAP)
		{
			iGameState gameState3 = iGameApp.GetInstance().m_GameState;
			if (gameState3 != null)
			{
				gameState3.m_lstScene4IAP = TUISceneType.Scene_Equip;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_EnterGold)
		{
			iGameState gameState4 = iGameApp.GetInstance().m_GameState;
			if (gameState4 != null)
			{
				gameState4.m_lstScene4IAP = TUISceneType.Scene_Equip;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_EnterGoBuyWeapon)
		{
			if (m_GameState != null)
			{
				PopupType wParam11 = (PopupType)m_event.GetWParam();
				switch (wParam11)
				{
				case PopupType.Weapons01:
					foreach (int item9 in m_GameData.m_ShopDisplayCenter.m_ltWeapon_Melee)
					{
						if (!m_GameData.m_ShopDisplayCenter.IsInSellInBlack(item9))
						{
							CWeaponInfoLevel cWeaponInfoLevel4 = m_GameData.m_WeaponCenter.Get(item9, 1);
							if (cWeaponInfoLevel4 != null && cWeaponInfoLevel4.nType == 1 && wParam11 == PopupType.Weapons01)
							{
								m_GameState.m_nLinkWeapon = item9;
								break;
							}
						}
					}
					break;
				case PopupType.Weapons02:
				{
					List<int> rangeList = m_GameData.m_ShopDisplayCenter.GetRangeList();
					foreach (int item10 in rangeList)
					{
						if (!m_GameData.m_ShopDisplayCenter.IsInSellInBlack(item10))
						{
							CWeaponInfoLevel cWeaponInfoLevel3 = m_GameData.m_WeaponCenter.Get(item10, 1);
							if (cWeaponInfoLevel3 != null && cWeaponInfoLevel3.nType != 1 && wParam11 == PopupType.Weapons02)
							{
								m_GameState.m_nLinkWeapon = item10;
								break;
							}
						}
					}
					break;
				}
				case PopupType.Armor_Head:
				case PopupType.Armor_Body:
				case PopupType.Armor_Bracelet:
				case PopupType.Armor_Leg:
					foreach (int item11 in m_GameData.m_ShopDisplayCenter.m_ltAvatar_Armor)
					{
						if (!m_GameData.m_ShopDisplayCenter.IsInSellInBlack(item11))
						{
							CAvatarInfo cAvatarInfo4 = m_GameData.m_AvatarCenter.Get(item11);
							if (cAvatarInfo4 != null && ((cAvatarInfo4.m_nType == 1 && wParam11 == PopupType.Armor_Head) || (cAvatarInfo4.m_nType == 3 && wParam11 == PopupType.Armor_Body) || (cAvatarInfo4.m_nType == 5 && wParam11 == PopupType.Armor_Leg) || (cAvatarInfo4.m_nType == 4 && wParam11 == PopupType.Armor_Bracelet)))
							{
								m_GameState.m_nLinkWeapon = item11;
								break;
							}
						}
					}
					break;
				case PopupType.Accessory_Halo:
				case PopupType.Accessory_Necklace:
				case PopupType.Accessory_Badge:
				case PopupType.Accessory_Stoneskin:
					foreach (int item12 in m_GameData.m_ShopDisplayCenter.m_ltAvatar_Accessory)
					{
						if (!m_GameData.m_ShopDisplayCenter.IsInSellInBlack(item12))
						{
							CAvatarInfo cAvatarInfo3 = m_GameData.m_AvatarCenter.Get(item12);
							if (cAvatarInfo3 != null && ((cAvatarInfo3.m_nType == 0 && wParam11 == PopupType.Accessory_Halo) || (cAvatarInfo3.m_nType == 2 && wParam11 == PopupType.Accessory_Necklace) || (cAvatarInfo3.m_nType == 6 && wParam11 == PopupType.Accessory_Badge) || (cAvatarInfo3.m_nType == 7 && wParam11 == PopupType.Accessory_Stoneskin)))
							{
								m_GameState.m_nLinkWeapon = item12;
								break;
							}
						}
					}
					break;
				}
			}
			Debug.Log(m_GameState.m_nLinkWeapon);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_EnterGoBuyWeaponInBlack)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_EnterGoBuySkill)
		{
			if (m_GameState != null)
			{
				int wParam12 = m_event.GetWParam();
				m_GameState.m_nLinkSkillRole = wParam12;
				m_GameState.m_nLinkSkill = -1;
				Debug.Log(m_GameState.m_nLinkSkillRole);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), true));
		}
		else
		{
			if (m_event.GetEventName() != TUIEvent.SceneEquipEventType.TUIEvent_SkipTutorial)
			{
				return;
			}
			iGameData gameData15 = iGameApp.GetInstance().m_GameData;
			if (gameData15 != null)
			{
				iDataCenter dataCenter15 = gameData15.GetDataCenter();
				if (dataCenter15 != null)
				{
					dataCenter15.nTutorialVillageState = 25;
					iGameApp.GetInstance().SaveData(true);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(m_event.GetEventName(), true));
				}
			}
		}
	}

	private void TUIEvent_BackInfo_SceneStash(object sender, TUIEvent.SendEvent_SceneStash m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_TopBar)
		{
			iGameData gameData = iGameApp.GetInstance().m_GameData;
			if (gameData == null)
			{
				return;
			}
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter == null)
			{
				return;
			}
			CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
			if (character != null)
			{
				CCharacterInfoLevel characterInfo = gameData.GetCharacterInfo(character.nID, character.nLevel);
				if (characterInfo != null)
				{
					AndroidReturnPlugin.instance.SetBackFunc(EventBack_Stash);
					TUIGameInfo tUIGameInfo = new TUIGameInfo();
					tUIGameInfo.player_info = new TUIPlayerInfo();
					tUIGameInfo.player_info.role_id = character.nID;
					tUIGameInfo.player_info.level = character.nLevel;
					tUIGameInfo.player_info.level_exp = characterInfo.nExp;
					tUIGameInfo.player_info.exp = character.nExp;
					tUIGameInfo.player_info.gold = dataCenter.Gold;
					tUIGameInfo.player_info.crystal = dataCenter.Crystal;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneStash(m_event.GetEventName(), tUIGameInfo));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_StashInfo)
		{
			iGameData gameData2 = iGameApp.GetInstance().m_GameData;
			if (gameData2 == null)
			{
				return;
			}
			iDataCenter dataCenter2 = gameData2.GetDataCenter();
			if (dataCenter2 == null)
			{
				return;
			}
			iItemCenter itemCenter = gameData2.GetItemCenter();
			if (itemCenter == null)
			{
				return;
			}
			iStashCapacityCenter stashCapacityCenter = gameData2.GetStashCapacityCenter();
			if (stashCapacityCenter == null)
			{
				return;
			}
			TUIGameInfo tUIGameInfo2 = new TUIGameInfo();
			tUIGameInfo2.stash_info = new TUIStashInfo();
			List<TUIStashUpdateInfo> list = new List<TUIStashUpdateInfo>();
			List<CStashCapacity> data = stashCapacityCenter.GetData();
			if (data != null)
			{
				for (int i = 0; i < data.Count; i++)
				{
					string empty = string.Empty;
					int price = 0;
					bool flag = false;
					if (i == data.Count - 1)
					{
						empty = "MAX";
					}
					else
					{
						empty = "Add Stash Size to " + data[i + 1].nCapacity + "({color:1eff0000}+" + (data[i + 1].nCapacity - data[i].nCapacity) + "{color})";
						price = data[i + 1].nPrice;
						flag = data[i + 1].isCrystalPurchase;
					}
					list.Add(new TUIStashUpdateInfo(i + 1, new TUIPriceInfo(price, flag ? UnitType.Crystal : UnitType.Gold), data[i].nCapacity, empty));
				}
			}
			tUIGameInfo2.stash_info.goods_info_list = new List<TUIGoodsInfo>();
			Dictionary<int, CItemInfo> data2 = itemCenter.GetData();
			if (data2 != null)
			{
				CItemInfoLevel cItemInfoLevel = null;
				int num = 0;
				foreach (CItemInfo value in data2.Values)
				{
					cItemInfoLevel = value.Get(1);
					if (cItemInfoLevel != null && cItemInfoLevel.nType == 3)
					{
						num = dataCenter2.GetMaterialNum(value.nID);
						if (num == -1)
						{
							num = 0;
						}
						GoodsQualityType quality = GoodsQualityType.Quality01;
						switch (cItemInfoLevel.nRare)
						{
						case 1:
							quality = GoodsQualityType.Quality01;
							break;
						case 2:
							quality = GoodsQualityType.Quality02;
							break;
						case 3:
							quality = GoodsQualityType.Quality03;
							break;
						case 4:
							quality = GoodsQualityType.Quality04;
							break;
						case 5:
							quality = GoodsQualityType.Quality05;
							break;
						case 6:
							quality = GoodsQualityType.Quality06;
							break;
						}
						tUIGameInfo2.stash_info.goods_info_list.Add(new TUIGoodsInfo(value.nID, quality, cItemInfoLevel.sName, num, new TUIPriceInfo(cItemInfoLevel.nSellPrice, cItemInfoLevel.isCrystalSell ? UnitType.Crystal : UnitType.Gold)));
					}
				}
			}
			int num2 = dataCenter2.StashLevel - 1;
			if (num2 >= list.Count)
			{
				num2 = list.Count - 1;
			}
			UnityEngine.Debug.Log(num2 + " " + dataCenter2.StashLevel);
			tUIGameInfo2.stash_info = new TUIStashInfo(dataCenter2.StashLevel, list.ToArray(), tUIGameInfo2.stash_info.goods_info_list);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneStash(m_event.GetEventName(), tUIGameInfo2));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_AddCapacity)
		{
			bool success = false;
			iGameData gameData3 = iGameApp.GetInstance().m_GameData;
			if (gameData3 != null)
			{
				iDataCenter dataCenter3 = gameData3.GetDataCenter();
				if (dataCenter3 != null)
				{
					CStashCapacity stashCapacity = gameData3.GetStashCapacity(dataCenter3.StashLevel);
					CStashCapacity stashCapacity2 = gameData3.GetStashCapacity(dataCenter3.StashLevel + 1);
					if (stashCapacity != null && stashCapacity2 != null)
					{
						if (stashCapacity2.isCrystalPurchase)
						{
							if (dataCenter3.Crystal < stashCapacity2.nPrice)
							{
								global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneStash(m_event.GetEventName(), false, BackEventFalseType.NoCrystalEnough, stashCapacity2.nPrice - dataCenter3.Crystal));
								return;
							}
							dataCenter3.AddCrystal(-Mathf.Abs(stashCapacity2.nPrice));
							CAchievementManager.GetInstance().AddAchievement(13);
							CTrinitiCollectManager.GetInstance().SendConsumeCrystal(stashCapacity2.nPrice, "buystash", -1, dataCenter3.StashLevel + 1);
							dataCenter3.StashLevel++;
							dataCenter3.Save();
							iServerSaveData.GetInstance().UploadImmidately();
							success = true;
							CFlurryManager.GetInstance().ConsumeCrystal(CFlurryManager.kConsumeType.StashSize);
						}
						else
						{
							if (dataCenter3.Gold < stashCapacity2.nPrice)
							{
								global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneStash(m_event.GetEventName(), false, BackEventFalseType.NoGoldEnough, stashCapacity2.nPrice - dataCenter3.Gold));
								return;
							}
							dataCenter3.AddGold(-Mathf.Abs(stashCapacity2.nPrice));
							CTrinitiCollectManager.GetInstance().SendConsumeGold(stashCapacity2.nPrice, "buystash", -1, dataCenter3.StashLevel + 1);
							dataCenter3.StashLevel++;
							dataCenter3.Save();
							iServerSaveData.GetInstance().UploadImmidately();
							success = true;
							CFlurryManager.GetInstance().ConsumeGold(CFlurryManager.kConsumeType.StashSize);
						}
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneStash(m_event.GetEventName(), success));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_SellGoods)
		{
			iGameData gameData4 = iGameApp.GetInstance().m_GameData;
			if (gameData4 == null)
			{
				return;
			}
			iDataCenter dataCenter4 = gameData4.GetDataCenter();
			if (dataCenter4 == null)
			{
				return;
			}
			bool flag2 = false;
			int wParam = m_event.GetWParam();
			int lparam = m_event.GetLparam();
			int rparam = m_event.GetRparam();
			CItemInfoLevel itemInfo = gameData4.GetItemInfo(wParam, 1);
			if (itemInfo == null || itemInfo.nType != 3)
			{
				return;
			}
			int materialNum = dataCenter4.GetMaterialNum(wParam);
			if (materialNum != -1)
			{
				materialNum = ((rparam <= materialNum) ? (materialNum - rparam) : 0);
				dataCenter4.SetMaterialNum(wParam, materialNum);
				if (itemInfo.isCrystalSell)
				{
					dataCenter4.AddCrystal(itemInfo.nSellPrice * rparam);
				}
				else
				{
					dataCenter4.AddGold(itemInfo.nSellPrice * rparam);
				}
				flag2 = true;
				dataCenter4.Save();
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneStash(m_event.GetEventName(), flag2));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_Back)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneStash(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_SearchGoodsDrop)
		{
			int wParam2 = m_event.GetWParam();
			iGameState gameState = iGameApp.GetInstance().m_GameState;
			if (gameState != null)
			{
				gameState.m_nMaterialIDFromEquip = wParam2;
				gameState.m_lstScene4SearchMaterial = TUISceneType.Scene_Stash;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneStash(m_event.GetEventName()));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_GoldToCrystal)
		{
			iGameData gameData5 = iGameApp.GetInstance().m_GameData;
			if (gameData5 == null)
			{
				return;
			}
			iDataCenter dataCenter5 = gameData5.GetDataCenter();
			if (dataCenter5 != null)
			{
				int wParam3 = m_event.GetWParam();
				int num3 = MyUtils.Formula_Gold2Crystal(wParam3);
				if (dataCenter5.Crystal < num3)
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneStash(m_event.GetEventName(), false, BackEventFalseType.NoCrystalEnough, num3 - dataCenter5.Crystal));
					return;
				}
				dataCenter5.AddCrystal(-Mathf.Abs(num3));
				CAchievementManager.GetInstance().AddAchievement(13);
				CTrinitiCollectManager.GetInstance().SendConsumeCrystal(num3, "exgold", -1, wParam3);
				dataCenter5.AddGold(wParam3);
				dataCenter5.Save();
				iServerSaveData.GetInstance().UploadImmidately();
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneStash(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_EnterIAP)
		{
			iGameState gameState2 = iGameApp.GetInstance().m_GameState;
			if (gameState2 != null)
			{
				gameState2.m_lstScene4IAP = TUISceneType.Scene_Stash;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneStash(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_EnterGold)
		{
			iGameState gameState3 = iGameApp.GetInstance().m_GameState;
			if (gameState3 != null)
			{
				gameState3.m_lstScene4IAP = TUISceneType.Scene_Stash;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneStash(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_EnterIAPCrystalNoEnough)
		{
			iGameState gameState4 = iGameApp.GetInstance().m_GameState;
			if (gameState4 != null)
			{
				gameState4.m_lstScene4IAP = TUISceneType.Scene_Stash;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneStash(m_event.GetEventName(), true));
			}
		}
	}

	private void TUIEvent_BackInfo_SceneSkill(object sender, TUIEvent.SendEvent_SceneSkill m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_TopBar)
		{
			iGameData gameData = iGameApp.GetInstance().m_GameData;
			if (gameData == null)
			{
				return;
			}
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter == null)
			{
				return;
			}
			CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
			if (character != null)
			{
				CCharacterInfoLevel characterInfo = gameData.GetCharacterInfo(character.nID, character.nLevel);
				if (characterInfo != null)
				{
					AndroidReturnPlugin.instance.SetBackFunc(EventBack_Skill);
					TUIGameInfo tUIGameInfo = new TUIGameInfo();
					tUIGameInfo.player_info = new TUIPlayerInfo();
					tUIGameInfo.player_info.role_id = character.nID;
					tUIGameInfo.player_info.level = character.nLevel;
					tUIGameInfo.player_info.level_exp = characterInfo.nExp;
					tUIGameInfo.player_info.exp = character.nExp;
					tUIGameInfo.player_info.gold = dataCenter.Gold;
					tUIGameInfo.player_info.crystal = dataCenter.Crystal;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), tUIGameInfo));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_SkillInfo)
		{
			iGameData gameData2 = iGameApp.GetInstance().m_GameData;
			if (gameData2 == null)
			{
				return;
			}
			iDataCenter dataCenter2 = gameData2.GetDataCenter();
			if (dataCenter2 == null)
			{
				return;
			}
			CCharacterInfo cCharacterInfo = null;
			CCharacterInfoLevel cCharacterInfoLevel = null;
			CSkillInfo cSkillInfo = null;
			CSkillInfoLevel cSkillInfoLevel = null;
			int[] array = new int[6] { 1, 4, 3, 2, 5, 6 };
			TUISkillListInfo[] array2 = new TUISkillListInfo[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new TUISkillListInfo(array[i], null);
				List<TUISkillInfo> list = new List<TUISkillInfo>();
				cCharacterInfo = gameData2.GetCharacterInfo(array[i]);
				if (cCharacterInfo != null)
				{
					List<int> list2 = new List<int>(cCharacterInfo.ltCharacterPassiveSkill);
					cCharacterInfoLevel = cCharacterInfo.Get(1);
					if (cCharacterInfoLevel != null)
					{
						list2.Insert(0, cCharacterInfoLevel.nSkill);
					}
					for (int j = 0; j < list2.Count; j++)
					{
						cSkillInfo = gameData2.GetSkillInfo(list2[j]);
						if (cSkillInfo == null)
						{
							continue;
						}
						Dictionary<int, TUIPriceInfo> dictionary = new Dictionary<int, TUIPriceInfo>();
						if (j == 0)
						{
							for (int k = 1; k <= 2; k++)
							{
								cSkillInfoLevel = cSkillInfo.Get(k);
								if (cSkillInfoLevel != null)
								{
									dictionary.Add(k, new TUIPriceInfo(cSkillInfoLevel.nPurchasePrice, cSkillInfoLevel.isCrystalPurchase ? UnitType.Crystal : UnitType.Gold));
								}
								else
								{
									dictionary.Add(k, new TUIPriceInfo(999999, UnitType.Crystal));
								}
							}
						}
						else
						{
							for (int l = 1; l <= 5; l++)
							{
								cSkillInfoLevel = cSkillInfo.Get(l);
								if (cSkillInfoLevel != null)
								{
									dictionary.Add(l, new TUIPriceInfo(cSkillInfoLevel.nPurchasePrice, cSkillInfoLevel.isCrystalPurchase ? UnitType.Crystal : UnitType.Gold));
								}
								else
								{
									dictionary.Add(l, new TUIPriceInfo(999999, UnitType.Crystal));
								}
							}
						}
						Dictionary<int, string> dictionary2 = new Dictionary<int, string>();
						Dictionary<int, string> dictionary3 = new Dictionary<int, string>();
						for (int m = 1; m <= 5; m++)
						{
							cSkillInfoLevel = cSkillInfo.Get(m);
							if (cSkillInfoLevel != null)
							{
								dictionary2.Add(m, cSkillInfoLevel.sLevelUpDesc);
								dictionary3.Add(m, cSkillInfoLevel.sDesc);
							}
						}
						cSkillInfoLevel = cSkillInfo.Get(1);
						if (cSkillInfoLevel != null)
						{
							int nDiscount = 100;
							iServerVerify.CServerConfigInfo serverConfigInfo = iServerVerify.GetInstance().GetServerConfigInfo();
							if (serverConfigInfo != null)
							{
								serverConfigInfo.IsPriceOff(4, cSkillInfo.nID, ref nDiscount);
							}
							string skill_introduce_unlock = "Unlock at Lv " + cSkillInfo.nUnlockLevel;
							if (j == 0)
							{
								int nSkillLevel = 1;
								dataCenter2.GetSkill(cSkillInfo.nID, ref nSkillLevel);
								list.Add(new TUISkillInfo(cSkillInfo.nID, cSkillInfoLevel.sName, nSkillLevel, true, dictionary, dictionary2, dictionary3, true, 2, 1f));
							}
							else
							{
								int nSkillLevel2 = 0;
								dataCenter2.GetPassiveSkill(cSkillInfo.nID, ref nSkillLevel2);
								list.Add(new TUISkillInfo(cSkillInfo.nID, cSkillInfoLevel.sName, (nSkillLevel2 != -1) ? nSkillLevel2 : 0, nSkillLevel2 != 0, new TUIPriceInfo(cSkillInfo.nUnlockPrice, cSkillInfo.isCrystalUnlock ? UnitType.Crystal : UnitType.Gold), dictionary, dictionary2, dictionary3, skill_introduce_unlock, (float)nDiscount / 100f));
							}
							if (iGameApp.GetInstance().CheckSkillSignState(1, cSkillInfo.nID))
							{
								array2[i].AddNewMark(cSkillInfo.nID, NewMarkType.New);
							}
							else if (iGameApp.GetInstance().CheckSkillMaterialEnough(cSkillInfo.nID))
							{
								array2[i].AddNewMark(cSkillInfo.nID, NewMarkType.Mark);
							}
							else
							{
								array2[i].AddNewMark(cSkillInfo.nID, NewMarkType.None);
							}
						}
					}
				}
				array2[i].skill_list_info = list.ToArray();
			}
			TUIGameInfo tUIGameInfo2 = new TUIGameInfo();
			tUIGameInfo2.all_skill_info = new TUIAllSkillInfo(array2);
			iGameState gameState = iGameApp.GetInstance().m_GameState;
			if (gameState != null && gameState.m_nLinkSkillRole > 0)
			{
				Debug.Log("skill link " + gameState.m_nLinkSkillRole + " " + gameState.m_nLinkSkill);
				cCharacterInfo = gameData2.GetCharacterInfo(gameState.m_nLinkSkillRole);
				if (cCharacterInfo != null)
				{
					for (int n = 0; n < cCharacterInfo.ltCharacterPassiveSkill.Count; n++)
					{
						if (gameState.m_nLinkSkill <= 0 || gameState.m_nLinkSkill == cCharacterInfo.ltCharacterPassiveSkill[n])
						{
							Debug.Log(gameState.m_nLinkSkillRole + " " + cCharacterInfo.ltCharacterPassiveSkill[n]);
							tUIGameInfo2.all_skill_info.SetLinkInfo(gameState.m_nLinkSkillRole, cCharacterInfo.ltCharacterPassiveSkill[n]);
							break;
						}
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), tUIGameInfo2));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_SkillUnlcok)
		{
			int lparam = m_event.GetLparam();
			bool flag = false;
			iGameData gameData3 = iGameApp.GetInstance().m_GameData;
			if (gameData3 != null)
			{
				CSkillInfo skillInfo = gameData3.GetSkillInfo(lparam);
				iDataCenter dataCenter3 = gameData3.GetDataCenter();
				if (skillInfo != null && dataCenter3 != null)
				{
					int nSkillLevel3 = 0;
					if (!dataCenter3.GetPassiveSkill(skillInfo.nID, ref nSkillLevel3))
					{
						if (skillInfo.isCrystalUnlock)
						{
							if (dataCenter3.Crystal < skillInfo.nUnlockPrice)
							{
								global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), false, BackEventFalseType.NoCrystalEnough, skillInfo.nUnlockPrice - dataCenter3.Crystal));
								return;
							}
							dataCenter3.AddCrystal(-Mathf.Abs(skillInfo.nUnlockPrice));
							CAchievementManager.GetInstance().AddAchievement(13);
							CTrinitiCollectManager.GetInstance().SendConsumeCrystal(skillInfo.nUnlockPrice, "unlockskill", skillInfo.nID, -1);
							dataCenter3.UnlockPassiveSkill(skillInfo.nID);
							dataCenter3.Save();
							iServerSaveData.GetInstance().UploadImmidately();
							flag = true;
							CFlurryManager.GetInstance().ConsumeCrystal(CFlurryManager.kConsumeType.Skill);
						}
						else
						{
							if (dataCenter3.Gold < skillInfo.nUnlockPrice)
							{
								global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), false, BackEventFalseType.NoGoldEnough, skillInfo.nUnlockPrice - dataCenter3.Gold));
								return;
							}
							dataCenter3.AddGold(-Mathf.Abs(skillInfo.nUnlockPrice));
							CTrinitiCollectManager.GetInstance().SendConsumeGold(skillInfo.nUnlockPrice, "unlockskill", skillInfo.nID, -1);
							dataCenter3.UnlockPassiveSkill(skillInfo.nID);
							dataCenter3.Save();
							iServerSaveData.GetInstance().UploadImmidately();
							flag = true;
							CFlurryManager.GetInstance().ConsumeGold(CFlurryManager.kConsumeType.Skill);
						}
						if (flag)
						{
							iSkillCenter skillCenter = gameData3.GetSkillCenter();
							if (skillCenter != null)
							{
								Dictionary<int, CSkillInfo> dataSkillInfo = skillCenter.GetDataSkillInfo();
								if (dataSkillInfo != null)
								{
									int[] array3 = new int[6] { 1, 2, 3, 4, 5, 6 };
									TUISkillListInfo[] array4 = new TUISkillListInfo[array3.Length];
									for (int num = 0; num < array4.Length; num++)
									{
										array4[num] = new TUISkillListInfo(array3[num], null);
										List<TUISkillInfo> list3 = new List<TUISkillInfo>();
										CCharacterInfo characterInfo2 = gameData3.GetCharacterInfo(array3[num]);
										if (characterInfo2 == null)
										{
											continue;
										}
										for (int num2 = 0; num2 < characterInfo2.ltCharacterPassiveSkill.Count; num2++)
										{
											int num3 = characterInfo2.ltCharacterPassiveSkill[num2];
											if (!iGameApp.GetInstance().CheckSkillSignState(1, num3))
											{
												if (iGameApp.GetInstance().CheckSkillMaterialEnough(num3))
												{
													array4[num].AddNewMark(num3, NewMarkType.Mark);
												}
												else
												{
													array4[num].AddNewMark(num3, NewMarkType.None);
												}
											}
										}
									}
									TUIGameInfo tUIGameInfo3 = new TUIGameInfo();
									tUIGameInfo3.all_skill_info = new TUIAllSkillInfo(array4);
									global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_NewMarkInfo, tUIGameInfo3));
								}
							}
						}
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), flag));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_SkillBuy)
		{
			int lparam2 = m_event.GetLparam();
			bool flag2 = false;
			iGameData gameData4 = iGameApp.GetInstance().m_GameData;
			if (gameData4 != null)
			{
				CSkillInfo skillInfo2 = gameData4.GetSkillInfo(lparam2);
				iDataCenter dataCenter4 = gameData4.GetDataCenter();
				if (skillInfo2 != null && dataCenter4 != null)
				{
					int nSkillLevel4 = 0;
					if (dataCenter4.GetPassiveSkill(skillInfo2.nID, ref nSkillLevel4) && nSkillLevel4 == -1)
					{
						CSkillInfoLevel cSkillInfoLevel2 = skillInfo2.Get(1);
						if (cSkillInfoLevel2 != null)
						{
							int num4 = cSkillInfoLevel2.nPurchasePrice;
							iServerVerify.CServerConfigInfo serverConfigInfo2 = iServerVerify.GetInstance().GetServerConfigInfo();
							if (serverConfigInfo2 != null)
							{
								int nDiscount2 = 100;
								serverConfigInfo2.IsPriceOff(4, skillInfo2.nID, ref nDiscount2);
								num4 = (int)Mathf.Ceil((float)(cSkillInfoLevel2.nPurchasePrice * nDiscount2) / 100f);
							}
							if (cSkillInfoLevel2.isCrystalPurchase)
							{
								if (dataCenter4.Crystal < num4)
								{
									global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), false, BackEventFalseType.NoCrystalEnough, num4 - dataCenter4.Crystal));
									return;
								}
								dataCenter4.AddCrystal(-Mathf.Abs(num4));
								CAchievementManager.GetInstance().AddAchievement(13);
								CTrinitiCollectManager.GetInstance().SendConsumeCrystal(num4, "buyskill", skillInfo2.nID, 1);
								dataCenter4.SetPassiveSkill(skillInfo2.nID, 1);
								if (!dataCenter4.HasSelectPassiveSkill(dataCenter4.CurCharID, skillInfo2.nID))
								{
									dataCenter4.SetSkillSign(skillInfo2.nID, 3);
								}
								dataCenter4.Save();
								iServerSaveData.GetInstance().UploadImmidately();
								flag2 = true;
								iGameApp.GetInstance().Flurry_PurchaseSkill(skillInfo2.nID);
								CFlurryManager.GetInstance().ConsumeCrystal(CFlurryManager.kConsumeType.Skill);
							}
							else
							{
								if (dataCenter4.Gold < num4)
								{
									global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), false, BackEventFalseType.NoGoldEnough, num4 - dataCenter4.Gold));
									return;
								}
								dataCenter4.AddGold(-Mathf.Abs(num4));
								CTrinitiCollectManager.GetInstance().SendConsumeGold(num4, "buyskill", skillInfo2.nID, 1);
								dataCenter4.SetPassiveSkill(skillInfo2.nID, 1);
								if (!dataCenter4.HasSelectPassiveSkill(dataCenter4.CurCharID, skillInfo2.nID))
								{
									dataCenter4.SetSkillSign(skillInfo2.nID, 3);
								}
								CTrinitiCollectManager.GetInstance().SendConsumeGold(num4, "buyskill", skillInfo2.nID, 1);
								dataCenter4.Save();
								iServerSaveData.GetInstance().UploadImmidately();
								flag2 = true;
								iGameApp.GetInstance().Flurry_PurchaseSkill(skillInfo2.nID);
								CFlurryManager.GetInstance().ConsumeGold(CFlurryManager.kConsumeType.Skill);
							}
							if (flag2)
							{
								iSkillCenter skillCenter2 = gameData4.GetSkillCenter();
								if (skillCenter2 != null)
								{
									Dictionary<int, CSkillInfo> dataSkillInfo2 = skillCenter2.GetDataSkillInfo();
									if (dataSkillInfo2 != null)
									{
										int[] array5 = new int[6] { 1, 2, 3, 4, 5, 6 };
										TUISkillListInfo[] array6 = new TUISkillListInfo[array5.Length];
										for (int num5 = 0; num5 < array6.Length; num5++)
										{
											array6[num5] = new TUISkillListInfo(array5[num5], null);
											List<TUISkillInfo> list4 = new List<TUISkillInfo>();
											CCharacterInfo characterInfo3 = gameData4.GetCharacterInfo(array5[num5]);
											if (characterInfo3 == null)
											{
												continue;
											}
											for (int num6 = 0; num6 < characterInfo3.ltCharacterPassiveSkill.Count; num6++)
											{
												int num7 = characterInfo3.ltCharacterPassiveSkill[num6];
												if (!iGameApp.GetInstance().CheckSkillSignState(1, num7))
												{
													if (iGameApp.GetInstance().CheckSkillMaterialEnough(num7))
													{
														array6[num5].AddNewMark(num7, NewMarkType.Mark);
													}
													else
													{
														array6[num5].AddNewMark(num7, NewMarkType.None);
													}
												}
											}
										}
										TUIGameInfo tUIGameInfo4 = new TUIGameInfo();
										tUIGameInfo4.all_skill_info = new TUIAllSkillInfo(array6);
										global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_NewMarkInfo, tUIGameInfo4));
									}
								}
								CAchievementManager.GetInstance().AddAchievement(3, new object[1] { dataCenter4.GetPassiveSkillCount() });
							}
						}
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), flag2));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_SkillUpdate)
		{
			int lparam3 = m_event.GetLparam();
			bool flag3 = false;
			iGameData gameData5 = iGameApp.GetInstance().m_GameData;
			if (gameData5 != null)
			{
				CSkillInfo skillInfo3 = gameData5.GetSkillInfo(lparam3);
				if (skillInfo3 != null)
				{
					CSkillInfoLevel cSkillInfoLevel3 = skillInfo3.Get(1);
					if (cSkillInfoLevel3 != null && cSkillInfoLevel3.nType == 0)
					{
						int nSkillLevel5 = 1;
						m_DataCenter.GetSkill(skillInfo3.nID, ref nSkillLevel5);
						CSkillInfoLevel cSkillInfoLevel4 = skillInfo3.Get(nSkillLevel5 + 1);
						if (cSkillInfoLevel4 != null)
						{
							int nPurchasePrice = cSkillInfoLevel4.nPurchasePrice;
							if (cSkillInfoLevel4.isCrystalPurchase)
							{
								if (m_DataCenter.Crystal < nPurchasePrice)
								{
									global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), false, BackEventFalseType.NoCrystalEnough, nPurchasePrice - m_DataCenter.Crystal));
									return;
								}
								m_DataCenter.AddCrystal(-Mathf.Abs(nPurchasePrice));
								CAchievementManager.GetInstance().AddAchievement(13);
								CTrinitiCollectManager.GetInstance().SendConsumeCrystal(nPurchasePrice, "upskill", skillInfo3.nID, nSkillLevel5 + 1);
								m_DataCenter.SetSkill(skillInfo3.nID, nSkillLevel5 + 1);
								flag3 = true;
								iGameApp.GetInstance().Flurry_UpgradeSkill(skillInfo3.nID);
								CFlurryManager.GetInstance().ConsumeCrystal(CFlurryManager.kConsumeType.Skill);
								CAchievementManager.GetInstance().AddAchievement(10);
							}
							else
							{
								if (m_DataCenter.Gold < nPurchasePrice)
								{
									Debug.Log(nPurchasePrice - m_DataCenter.Gold);
									global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), false, BackEventFalseType.NoGoldEnough, nPurchasePrice - m_DataCenter.Gold));
									return;
								}
								m_DataCenter.AddGold(-Mathf.Abs(nPurchasePrice));
								CTrinitiCollectManager.GetInstance().SendConsumeGold(nPurchasePrice, "upskill", skillInfo3.nID, nSkillLevel5 + 1);
								m_DataCenter.SetSkill(skillInfo3.nID, nSkillLevel5 + 1);
								flag3 = true;
								iGameApp.GetInstance().Flurry_UpgradeSkill(skillInfo3.nID);
								CFlurryManager.GetInstance().ConsumeGold(CFlurryManager.kConsumeType.Skill);
								CAchievementManager.GetInstance().AddAchievement(10);
							}
							if (flag3)
							{
								iGameApp.GetInstance().SaveData();
							}
						}
						global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), flag3));
						return;
					}
				}
				iDataCenter dataCenter5 = gameData5.GetDataCenter();
				if (skillInfo3 != null && dataCenter5 != null)
				{
					int nSkillLevel6 = 0;
					if (dataCenter5.GetPassiveSkill(skillInfo3.nID, ref nSkillLevel6) && nSkillLevel6 > 0)
					{
						CSkillInfoLevel skillInfo4 = gameData5.GetSkillInfo(skillInfo3.nID, nSkillLevel6);
						CSkillInfoLevel skillInfo5 = gameData5.GetSkillInfo(skillInfo3.nID, nSkillLevel6 + 1);
						if (skillInfo4 != null && skillInfo5 != null)
						{
							int num8 = skillInfo5.nPurchasePrice;
							iServerVerify.CServerConfigInfo serverConfigInfo3 = iServerVerify.GetInstance().GetServerConfigInfo();
							if (serverConfigInfo3 != null)
							{
								int nDiscount3 = 100;
								serverConfigInfo3.IsPriceOff(4, skillInfo3.nID, ref nDiscount3);
								num8 = (int)Mathf.Ceil((float)(skillInfo5.nPurchasePrice * nDiscount3) / 100f);
							}
							if (skillInfo5.isCrystalPurchase)
							{
								if (dataCenter5.Crystal < num8)
								{
									global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), false, BackEventFalseType.NoCrystalEnough, num8 - dataCenter5.Crystal));
									return;
								}
								dataCenter5.AddCrystal(-Mathf.Abs(num8));
								CAchievementManager.GetInstance().AddAchievement(13);
								CTrinitiCollectManager.GetInstance().SendConsumeCrystal(num8, "upskill", skillInfo3.nID, skillInfo5.nLevel);
								dataCenter5.SetPassiveSkill(skillInfo3.nID, skillInfo5.nLevel);
								dataCenter5.Save();
								iServerSaveData.GetInstance().UploadImmidately();
								flag3 = true;
								iGameApp.GetInstance().Flurry_UpgradeSkill(skillInfo3.nID);
								CFlurryManager.GetInstance().ConsumeCrystal(CFlurryManager.kConsumeType.Skill);
								CAchievementManager.GetInstance().AddAchievement(10);
							}
							else
							{
								if (dataCenter5.Gold < num8)
								{
									Debug.Log(num8 - dataCenter5.Gold);
									global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), false, BackEventFalseType.NoGoldEnough, num8 - dataCenter5.Gold));
									return;
								}
								dataCenter5.AddGold(-Mathf.Abs(num8));
								CTrinitiCollectManager.GetInstance().SendConsumeGold(num8, "upskill", skillInfo3.nID, skillInfo5.nLevel);
								dataCenter5.SetPassiveSkill(skillInfo3.nID, skillInfo5.nLevel);
								dataCenter5.Save();
								iServerSaveData.GetInstance().UploadImmidately();
								flag3 = true;
								iGameApp.GetInstance().Flurry_UpgradeSkill(skillInfo3.nID);
								CFlurryManager.GetInstance().ConsumeGold(CFlurryManager.kConsumeType.Skill);
								CAchievementManager.GetInstance().AddAchievement(10);
							}
							if (flag3)
							{
								iSkillCenter skillCenter3 = gameData5.GetSkillCenter();
								if (skillCenter3 != null)
								{
									Dictionary<int, CSkillInfo> dataSkillInfo3 = skillCenter3.GetDataSkillInfo();
									if (dataSkillInfo3 != null)
									{
										int[] array7 = new int[6] { 1, 2, 3, 4, 5, 6 };
										TUISkillListInfo[] array8 = new TUISkillListInfo[array7.Length];
										for (int num9 = 0; num9 < array8.Length; num9++)
										{
											array8[num9] = new TUISkillListInfo(array7[num9], null);
											List<TUISkillInfo> list5 = new List<TUISkillInfo>();
											CCharacterInfo characterInfo4 = gameData5.GetCharacterInfo(array7[num9]);
											if (characterInfo4 == null)
											{
												continue;
											}
											for (int num10 = 0; num10 < characterInfo4.ltCharacterPassiveSkill.Count; num10++)
											{
												int num11 = characterInfo4.ltCharacterPassiveSkill[num10];
												if (!iGameApp.GetInstance().CheckSkillSignState(1, num11))
												{
													if (iGameApp.GetInstance().CheckSkillMaterialEnough(num11))
													{
														array8[num9].AddNewMark(num11, NewMarkType.Mark);
													}
													else
													{
														array8[num9].AddNewMark(num11, NewMarkType.None);
													}
												}
											}
										}
										TUIGameInfo tUIGameInfo5 = new TUIGameInfo();
										tUIGameInfo5.all_skill_info = new TUIAllSkillInfo(array8);
										global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_NewMarkInfo, tUIGameInfo5));
									}
								}
							}
						}
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), flag3));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_Back)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_SkillChoose)
		{
			int wParam = m_event.GetWParam();
			int lparam4 = m_event.GetLparam();
			iGameData gameData6 = iGameApp.GetInstance().m_GameData;
			if (gameData6 == null)
			{
				return;
			}
			iGameState gameState2 = iGameApp.GetInstance().m_GameState;
			if (gameState2 == null)
			{
				return;
			}
			iDataCenter dataCenter6 = gameData6.GetDataCenter();
			if (dataCenter6 == null)
			{
				return;
			}
			gameState2.m_nLinkSkillRole = wParam;
			gameState2.m_nLinkSkill = lparam4;
			int nSignState = 0;
			if (!dataCenter6.GetSkillSign(lparam4, ref nSignState))
			{
				return;
			}
			if (nSignState == 1)
			{
				Debug.Log("choose " + lparam4);
				dataCenter6.SetSkillSign(lparam4, 2);
				dataCenter6.Save();
				if (iGameApp.GetInstance().CheckSkillMaterialEnough(lparam4))
				{
					TUISkillListInfo[] array9 = new TUISkillListInfo[1]
					{
						new TUISkillListInfo(wParam, null)
					};
					array9[0].AddNewMark(lparam4, NewMarkType.Mark);
					TUIGameInfo tUIGameInfo6 = new TUIGameInfo();
					tUIGameInfo6.all_skill_info = new TUIAllSkillInfo(array9);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_NewMarkInfo, tUIGameInfo6));
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName()));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_GoldToCrystal)
		{
			iGameData gameData7 = iGameApp.GetInstance().m_GameData;
			if (gameData7 == null)
			{
				return;
			}
			iDataCenter dataCenter7 = gameData7.GetDataCenter();
			if (dataCenter7 != null)
			{
				int wParam2 = m_event.GetWParam();
				int num12 = MyUtils.Formula_Gold2Crystal(wParam2);
				if (dataCenter7.Crystal < num12)
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), false, BackEventFalseType.NoCrystalEnough, num12 - dataCenter7.Crystal));
					return;
				}
				dataCenter7.AddCrystal(-Mathf.Abs(num12));
				CAchievementManager.GetInstance().AddAchievement(13);
				CTrinitiCollectManager.GetInstance().SendConsumeCrystal(num12, "exgold", -1, wParam2);
				dataCenter7.AddGold(wParam2);
				dataCenter7.Save();
				iServerSaveData.GetInstance().UploadImmidately();
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_EnterIAP)
		{
			iGameState gameState3 = iGameApp.GetInstance().m_GameState;
			if (gameState3 != null)
			{
				gameState3.m_lstScene4IAP = TUISceneType.Scene_Skill;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_EnterGold)
		{
			iGameState gameState4 = iGameApp.GetInstance().m_GameState;
			if (gameState4 != null)
			{
				gameState4.m_lstScene4IAP = TUISceneType.Scene_Skill;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_EnterIAPCrystalNoEnough)
		{
			iGameState gameState5 = iGameApp.GetInstance().m_GameState;
			if (gameState5 != null)
			{
				gameState5.m_lstScene4IAP = TUISceneType.Scene_Skill;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_EnterGoEquip)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), true));
		}
		else
		{
			if (m_event.GetEventName() != TUIEvent.SceneSkillEventType.TUIEvent_SkipTutorial)
			{
				return;
			}
			iGameData gameData8 = iGameApp.GetInstance().m_GameData;
			if (gameData8 != null)
			{
				iDataCenter dataCenter8 = gameData8.GetDataCenter();
				if (dataCenter8 != null)
				{
					dataCenter8.nTutorialVillageState = 25;
					iGameApp.GetInstance().SaveData(true);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(m_event.GetEventName(), true));
				}
			}
		}
	}

	private void TUIEvent_BackInfo_SceneForge(object sender, TUIEvent.SendEvent_SceneForge m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_TopBar)
		{
			iGameData gameData = iGameApp.GetInstance().m_GameData;
			if (gameData == null)
			{
				return;
			}
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter == null)
			{
				return;
			}
			CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
			if (character == null)
			{
				return;
			}
			CCharacterInfoLevel characterInfo = gameData.GetCharacterInfo(character.nID, character.nLevel);
			if (characterInfo != null)
			{
				AndroidReturnPlugin.instance.SetBackFunc(EventBack_Forge);
				TUIGameInfo tUIGameInfo = new TUIGameInfo();
				tUIGameInfo.player_info = new TUIPlayerInfo();
				tUIGameInfo.player_info.role_id = character.nID;
				tUIGameInfo.player_info.level = character.nLevel;
				tUIGameInfo.player_info.level_exp = characterInfo.nExp;
				tUIGameInfo.player_info.exp = character.nExp;
				tUIGameInfo.player_info.gold = dataCenter.Gold;
				tUIGameInfo.player_info.crystal = dataCenter.Crystal;
				iGameState gameState = iGameApp.GetInstance().m_GameState;
				if (gameState != null)
				{
					gameState.m_curScene4Recommand = gameState.m_lstScene4Recommand;
					gameState.m_lstScene4Recommand = TUISceneType.None;
				}
				tUIGameInfo.player_info.avatar_model = characterInfo.nModel;
				tUIGameInfo.player_info.avatar_head = ((dataCenter.AvatarHead <= 0) ? characterInfo.nAvatarHead : dataCenter.AvatarHead);
				tUIGameInfo.player_info.avatar_body = ((dataCenter.AvatarUpper <= 0) ? characterInfo.nAvatarUpper : dataCenter.AvatarUpper);
				tUIGameInfo.player_info.avatar_leg = ((dataCenter.AvatarLower <= 0) ? characterInfo.nAvatarLower : dataCenter.AvatarLower);
				tUIGameInfo.player_info.avatar_headup = ((dataCenter.AvatarHeadup <= 0) ? (-1) : dataCenter.AvatarHeadup);
				tUIGameInfo.player_info.avatar_neck = ((dataCenter.AvatarNeck <= 0) ? (-1) : dataCenter.AvatarNeck);
				tUIGameInfo.player_info.avatar_bracelet = ((dataCenter.AvatarWrist <= 0) ? (-1) : dataCenter.AvatarWrist);
				tUIGameInfo.player_info.default_weapon = dataCenter.GetSelectWeapon(0);
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), tUIGameInfo));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_WeaponInfo)
		{
			if (m_GameData == null || m_DataCenter == null)
			{
				return;
			}
			iItemCenter itemCenter = m_GameData.GetItemCenter();
			if (itemCenter == null)
			{
				return;
			}
			iShopDisplayCenter shopDisplayCenter = m_GameData.m_ShopDisplayCenter;
			if (shopDisplayCenter == null)
			{
				return;
			}
			Dictionary<int, CItemInfo> data = itemCenter.GetData();
			if (data == null)
			{
				return;
			}
			iWeaponCenter weaponCenter = m_GameData.GetWeaponCenter();
			if (weaponCenter == null)
			{
				return;
			}
			Dictionary<int, CWeaponInfo> data2 = weaponCenter.GetData();
			if (data2 == null)
			{
				return;
			}
			if (data != null)
			{
				TUIMappingInfo.Instance().ClearMaterial();
				TUIMappingInfo.Instance().ClearMaterialCount();
				CItemInfoLevel cItemInfoLevel = null;
				foreach (CItemInfo value in data.Values)
				{
					cItemInfoLevel = m_GameData.GetItemInfo(value.nID, 1);
					if (cItemInfoLevel != null)
					{
						int num = m_DataCenter.GetMaterialNum(value.nID);
						if (num == -1)
						{
							num = 0;
						}
						GoodsQualityType nQuality = GoodsQualityType.Quality01;
						switch (cItemInfoLevel.nRare)
						{
						case 1:
							nQuality = GoodsQualityType.Quality01;
							break;
						case 2:
							nQuality = GoodsQualityType.Quality02;
							break;
						case 3:
							nQuality = GoodsQualityType.Quality03;
							break;
						case 4:
							nQuality = GoodsQualityType.Quality04;
							break;
						case 5:
							nQuality = GoodsQualityType.Quality05;
							break;
						case 6:
							nQuality = GoodsQualityType.Quality06;
							break;
						}
						TUIMappingInfo.Instance().SetMaterial(value.nID, nQuality, cItemInfoLevel.sName, new TUIPriceInfo(cItemInfoLevel.nPurchasePrice, cItemInfoLevel.isCrystalPurchase ? UnitType.Crystal : UnitType.Gold));
						TUIMappingInfo.Instance().SetMaterialCount(value.nID, num);
					}
				}
			}
			TUIGameInfo tUIGameInfo2 = new TUIGameInfo();
			tUIGameInfo2.weapon_info = new TUIWeaponInfo();
			AddWeaponAttribute(tUIGameInfo2.weapon_info, shopDisplayCenter.m_ltWeapon_Melee, WeaponType.CloseWeapon, kShopWeaponCategory.Melee);
			AddWeaponAttribute(tUIGameInfo2.weapon_info, shopDisplayCenter.m_ltWeapon_Bow, WeaponType.Crossbow, kShopWeaponCategory.Crossbow);
			AddWeaponAttribute(tUIGameInfo2.weapon_info, shopDisplayCenter.m_ltWeapon_Autorifle, WeaponType.MachineGun, kShopWeaponCategory.AutoRifle);
			AddWeaponAttribute(tUIGameInfo2.weapon_info, shopDisplayCenter.m_ltWeapon_Holdgun, WeaponType.LiquidFireGun, kShopWeaponCategory.HoldGun);
			AddWeaponAttribute(tUIGameInfo2.weapon_info, shopDisplayCenter.m_ltWeapon_Shotgun, WeaponType.ViolenceGun, kShopWeaponCategory.ShotGun);
			AddWeaponAttribute(tUIGameInfo2.weapon_info, shopDisplayCenter.m_ltWeapon_Rocket, WeaponType.RPG, kShopWeaponCategory.Rocket);
			AddAvatarAttribute(tUIGameInfo2.weapon_info, shopDisplayCenter.m_ltAvatar_Armor, kShopWeaponCategory.Armor);
			AddAvatarAttribute(tUIGameInfo2.weapon_info, shopDisplayCenter.m_ltAvatar_Accessory, kShopWeaponCategory.Accessory);
			if (TUIMappingInfo.Instance().GetNewHelpState() == NewHelpState.Help02_ClickOpenWeaponMake)
			{
				tUIGameInfo2.weapon_info.SetLinkInfo(kShopWeaponCategory.AutoRifle, 4);
			}
			else if (TUIMappingInfo.Instance().GetNewHelpState() == NewHelpState.Help22_ClickOpenWeaponUpgrade)
			{
				tUIGameInfo2.weapon_info.SetLinkInfo(kShopWeaponCategory.Crossbow, 1);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), tUIGameInfo2));
			Dictionary<int, NewMarkType> dictMarkData = null;
			if (GetMarkDataForge(out dictMarkData))
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_NewMarkInfo, dictMarkData));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_WeaponGoodsBuy)
		{
			if (m_GameData == null || m_DataCenter == null)
			{
				return;
			}
			int wParam = m_event.GetWParam();
			int rparam = m_event.GetRparam();
			int lparam = m_event.GetLparam();
			Debug.Log(wParam + " " + rparam);
			CItemInfoLevel itemInfo = m_GameData.GetItemInfo(wParam, 1);
			if (itemInfo == null || itemInfo.nType != 3)
			{
				return;
			}
			if (itemInfo.isCrystalPurchase)
			{
				int num2 = itemInfo.nPurchasePrice * rparam;
				if (m_DataCenter.Crystal < num2)
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), false, BackEventFalseType.NoCrystalEnough, num2 - m_DataCenter.Crystal));
					return;
				}
				m_DataCenter.AddCrystal(-Mathf.Abs(num2));
				CAchievementManager.GetInstance().AddAchievement(13);
				CTrinitiCollectManager.GetInstance().SendConsumeCrystal(num2, "exchange", wParam, rparam);
				m_DataCenter.AddMaterialNum(wParam, rparam);
				CAchievementManager.GetInstance().AddAchievement(14, new object[1] { rparam });
				m_DataCenter.Save();
				if (TUIMappingInfo.Instance().GetNewHelpState() == NewHelpState.Help_Over)
				{
					CFlurryManager.GetInstance().ConsumeCrystal(CFlurryManager.kConsumeType.Material);
				}
			}
			else
			{
				int num3 = itemInfo.nPurchasePrice * rparam;
				if (m_DataCenter.Gold < num3)
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), false, BackEventFalseType.NoGoldEnough, num3 - m_DataCenter.Gold));
					return;
				}
				m_DataCenter.AddGold(-Mathf.Abs(num3));
				CTrinitiCollectManager.GetInstance().SendConsumeGold(num3, "exchange", wParam, rparam);
				m_DataCenter.AddMaterialNum(wParam, rparam);
				CAchievementManager.GetInstance().AddAchievement(14, new object[1] { rparam });
				m_DataCenter.Save();
				CFlurryManager.GetInstance().ConsumeGold(CFlurryManager.kConsumeType.Material);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), true, wParam, rparam));
			Dictionary<int, NewMarkType> dictMarkData2 = null;
			if (GetMarkDataForge(out dictMarkData2))
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_NewMarkInfo, dictMarkData2));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_ClickUpgrade)
		{
			if (m_GameData == null || m_DataCenter == null)
			{
				return;
			}
			iServerVerify.CServerConfigInfo serverConfigInfo = iServerVerify.GetInstance().GetServerConfigInfo();
			int wParam2 = m_event.GetWParam();
			WeaponType lparam2 = (WeaponType)m_event.GetLparam();
			Debug.Log(wParam2 + " " + lparam2);
			switch (lparam2)
			{
			case WeaponType.CloseWeapon:
			case WeaponType.Crossbow:
			case WeaponType.LiquidFireGun:
			case WeaponType.MachineGun:
			case WeaponType.RPG:
			case WeaponType.ViolenceGun:
			{
				CLevelUpWeapon cLevelUpWeapon = new CLevelUpWeapon();
				int nDiscount2 = 100;
				serverConfigInfo.IsPriceOff(1, wParam2, ref nDiscount2);
				if (!cLevelUpWeapon.CheckCanUpgrade(wParam2, (float)nDiscount2 / 100f))
				{
					if (cLevelUpWeapon.m_ltLackofMaterial.Count < 1)
					{
						if (cLevelUpWeapon.m_nLackCount > 0)
						{
							global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), false, (!cLevelUpWeapon.m_bLackCrystal) ? BackEventFalseType.NoGoldEnough : BackEventFalseType.NoCrystalEnough, cLevelUpWeapon.m_nLackCount));
						}
						break;
					}
					TUISupplementInfo tUISupplementInfo2 = new TUISupplementInfo();
					int num6 = 0;
					int num7 = 0;
					foreach (CLevelUpWeapon.CLackofMaterial item in cLevelUpWeapon.m_ltLackofMaterial)
					{
						tUISupplementInfo2.Add(item.m_nMaterialID, item.m_nMaterialCount);
						CItemInfoLevel cItemInfoLevel3 = m_GameData.m_ItemCenter.Get(item.m_nMaterialID, 1);
						if (cItemInfoLevel3 != null)
						{
							if (cItemInfoLevel3.isCrystalPurchase)
							{
								num7 += cItemInfoLevel3.nPurchasePrice * item.m_nMaterialCount;
							}
							else
							{
								num6 += cItemInfoLevel3.nPurchasePrice * item.m_nMaterialCount;
							}
						}
					}
					if (num6 > 0)
					{
						tUISupplementInfo2.m_nTotalCrystalCost += MyUtils.Formula_Gold2Crystal(num6);
					}
					if (num7 > 0)
					{
						tUISupplementInfo2.m_nTotalCrystalCost += num7;
					}
					if (cLevelUpWeapon.m_nLackCount > 0)
					{
						if (cLevelUpWeapon.m_bLackCrystal)
						{
							tUISupplementInfo2.m_nCrystal = cLevelUpWeapon.m_nLackCount;
							tUISupplementInfo2.m_nTotalCrystalCost += cLevelUpWeapon.m_nLackCount;
						}
						else
						{
							tUISupplementInfo2.m_nGold = cLevelUpWeapon.m_nLackCount;
							tUISupplementInfo2.m_nTotalCrystalCost += MyUtils.Formula_Gold2Crystal(cLevelUpWeapon.m_nLackCount);
						}
					}
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_ShowSupplement, tUISupplementInfo2));
					break;
				}
				cLevelUpWeapon.Levelup();
				if (cLevelUpWeapon.m_nCost >= 0)
				{
					if (!m_DataCenter.HasSelectWeapon(cLevelUpWeapon.m_nWeaponID))
					{
						m_DataCenter.SetWeaponSign(cLevelUpWeapon.m_nWeaponID, 3);
					}
					iGameApp.GetInstance().SaveData();
					if (cLevelUpWeapon.m_nWeaponLevelNext == 1)
					{
						iGameApp.GetInstance().Flurry_PurchaseWeapon(cLevelUpWeapon.m_nWeaponID);
						CAchievementManager.GetInstance().AddAchievement(4, new object[1] { m_DataCenter.GetWeaponCount() });
					}
					else
					{
						iGameApp.GetInstance().Flurry_UpgradeWeapon(cLevelUpWeapon.m_nWeaponID);
						CAchievementManager.GetInstance().AddAchievement(11);
					}
					if (cLevelUpWeapon.m_bCostCrystal)
					{
						CFlurryManager.GetInstance().ConsumeCrystal(CFlurryManager.kConsumeType.Weapon);
						CAchievementManager.GetInstance().AddAchievement(13);
						CTrinitiCollectManager.GetInstance().SendConsumeCrystal(cLevelUpWeapon.m_nCost, (cLevelUpWeapon.m_nWeaponLevelNext != 1) ? "upweapon" : "buyweapon", cLevelUpWeapon.m_nWeaponID, cLevelUpWeapon.m_nWeaponLevelNext);
					}
					else
					{
						CFlurryManager.GetInstance().ConsumeGold(CFlurryManager.kConsumeType.Weapon);
						CTrinitiCollectManager.GetInstance().SendConsumeGold(cLevelUpWeapon.m_nCost, (cLevelUpWeapon.m_nWeaponLevelNext != 1) ? "upweapon" : "buyweapon", cLevelUpWeapon.m_nWeaponID, cLevelUpWeapon.m_nWeaponLevelNext);
					}
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), true, cLevelUpWeapon.m_nWeaponID, cLevelUpWeapon.m_nWeaponLevelNext));
					Dictionary<int, NewMarkType> dictMarkData4 = null;
					if (GetMarkDataForge(out dictMarkData4))
					{
						global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_NewMarkInfo, dictMarkData4));
					}
				}
				break;
			}
			case WeaponType.Armor_Head:
			case WeaponType.Armor_Body:
			case WeaponType.Armor_Bracelet:
			case WeaponType.Armor_Leg:
			case WeaponType.Accessory_Halo:
			case WeaponType.Accessory_Badge:
			case WeaponType.Accessory_Necklace:
			case WeaponType.Accessory_Stoneskin:
			{
				CLevelUpAvatar cLevelUpAvatar = new CLevelUpAvatar();
				int nDiscount = 100;
				serverConfigInfo.IsPriceOff(2, wParam2, ref nDiscount);
				if (!cLevelUpAvatar.CheckCanUpgrade(wParam2, (float)nDiscount / 100f))
				{
					if (cLevelUpAvatar.m_ltLackofMaterial.Count < 1)
					{
						if (cLevelUpAvatar.m_nLackCount > 0)
						{
							global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), false, (!cLevelUpAvatar.m_bLackCrystal) ? BackEventFalseType.NoGoldEnough : BackEventFalseType.NoCrystalEnough, cLevelUpAvatar.m_nLackCount));
						}
						break;
					}
					TUISupplementInfo tUISupplementInfo = new TUISupplementInfo();
					int num4 = 0;
					int num5 = 0;
					foreach (CLevelUpAvatar.CLackofMaterial item2 in cLevelUpAvatar.m_ltLackofMaterial)
					{
						tUISupplementInfo.Add(item2.m_nMaterialID, item2.m_nMaterialCount);
						CItemInfoLevel cItemInfoLevel2 = m_GameData.m_ItemCenter.Get(item2.m_nMaterialID, 1);
						if (cItemInfoLevel2 != null)
						{
							if (cItemInfoLevel2.isCrystalPurchase)
							{
								num5 += cItemInfoLevel2.nPurchasePrice * item2.m_nMaterialCount;
							}
							else
							{
								num4 += cItemInfoLevel2.nPurchasePrice * item2.m_nMaterialCount;
							}
						}
					}
					if (num4 > 0)
					{
						tUISupplementInfo.m_nTotalCrystalCost += MyUtils.Formula_Gold2Crystal(num4);
					}
					if (num5 > 0)
					{
						tUISupplementInfo.m_nTotalCrystalCost += num5;
					}
					if (cLevelUpAvatar.m_nLackCount > 0)
					{
						if (cLevelUpAvatar.m_bLackCrystal)
						{
							tUISupplementInfo.m_nCrystal = cLevelUpAvatar.m_nLackCount;
							tUISupplementInfo.m_nTotalCrystalCost += cLevelUpAvatar.m_nLackCount;
						}
						else
						{
							tUISupplementInfo.m_nGold = cLevelUpAvatar.m_nLackCount;
							tUISupplementInfo.m_nTotalCrystalCost += MyUtils.Formula_Gold2Crystal(cLevelUpAvatar.m_nLackCount);
						}
					}
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_ShowSupplement, tUISupplementInfo));
					break;
				}
				cLevelUpAvatar.Levelup();
				if (cLevelUpAvatar.m_nCost >= 0)
				{
					m_DataCenter.SetAvatarSign(cLevelUpAvatar.m_nAvatarID, 3);
					iGameApp.GetInstance().SaveData();
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), true, cLevelUpAvatar.m_nAvatarID, cLevelUpAvatar.m_nAvatarLevelNext));
					Dictionary<int, NewMarkType> dictMarkData3 = null;
					if (GetMarkDataForge(out dictMarkData3))
					{
						global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_NewMarkInfo, dictMarkData3));
					}
				}
				break;
			}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_GetActiveWeapon)
		{
			iGameData gameData2 = iGameApp.GetInstance().m_GameData;
			if (gameData2 == null)
			{
				return;
			}
			iDataCenter dataCenter2 = gameData2.GetDataCenter();
			if (dataCenter2 != null)
			{
				int wParam3 = m_event.GetWParam();
				CWeaponInfo weaponInfo = gameData2.GetWeaponInfo(wParam3);
				if (weaponInfo == null || !dataCenter2.IsFreeWeaponID(wParam3))
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName()));
					return;
				}
				dataCenter2.SetWeaponLevel(wParam3, 1);
				iGameApp.GetInstance().SaveData();
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), true, wParam3, 1));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_Back)
		{
			iGameState gameState2 = iGameApp.GetInstance().m_GameState;
			if (gameState2 != null)
			{
				if (gameState2.m_curScene4Recommand != 0)
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), true, (int)gameState2.m_curScene4Recommand));
					gameState2.m_curScene4Recommand = TUISceneType.None;
				}
				else
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), true));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_SearchGoodsDrop)
		{
			int wParam4 = m_event.GetWParam();
			int lparam3 = m_event.GetLparam();
			iGameState gameState3 = iGameApp.GetInstance().m_GameState;
			if (gameState3 != null)
			{
				gameState3.m_nMaterialIDFromEquip = wParam4;
				gameState3.m_lstScene4SearchMaterial = TUISceneType.Scene_Forge;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_WeaponChoose)
		{
			if (m_GameData == null || m_GameState == null || m_DataCenter == null)
			{
				return;
			}
			int wParam5 = m_event.GetWParam();
			WeaponType lparam4 = (WeaponType)m_event.GetLparam();
			if (lparam4 == WeaponType.None)
			{
				return;
			}
			int nSignState = 0;
			m_GameState.m_nLinkWeapon = wParam5;
			switch (lparam4)
			{
			case WeaponType.CloseWeapon:
			case WeaponType.Crossbow:
			case WeaponType.LiquidFireGun:
			case WeaponType.MachineGun:
			case WeaponType.RPG:
			case WeaponType.ViolenceGun:
				if (m_DataCenter.GetWeaponSign(wParam5, ref nSignState) && nSignState == 1)
				{
					m_DataCenter.SetWeaponSign(wParam5, 2);
					iGameApp.GetInstance().SaveData();
				}
				break;
			default:
				if (m_DataCenter.GetAvatarSign(wParam5, ref nSignState) && nSignState == 1)
				{
					m_DataCenter.SetAvatarSign(wParam5, 2);
					iGameApp.GetInstance().SaveData();
				}
				break;
			}
			Dictionary<int, NewMarkType> dictMarkData5 = null;
			if (GetMarkDataForge(out dictMarkData5))
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_NewMarkInfo, dictMarkData5));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_ClickSupplement)
		{
			if (m_GameData == null || m_DataCenter == null)
			{
				return;
			}
			TUISupplementInfo supplementInfo = m_event.GetSupplementInfo();
			if (supplementInfo == null)
			{
				return;
			}
			if (m_DataCenter.Crystal < supplementInfo.m_nTotalCrystalCost)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), false, BackEventFalseType.NoCrystalEnough, supplementInfo.m_nTotalCrystalCost - m_DataCenter.Crystal));
				return;
			}
			if (supplementInfo.goods_list.Count > 0)
			{
				foreach (TUISupplementInfoGoods item3 in supplementInfo.goods_list)
				{
					int num8 = m_DataCenter.GetMaterialNum(item3.m_nMaterialID);
					if (num8 < 0)
					{
						num8 = 0;
					}
					m_DataCenter.SetMaterialNum(item3.m_nMaterialID, num8 + item3.m_nMaterialCount);
					CAchievementManager.GetInstance().AddAchievement(14, new object[1] { item3.m_nMaterialCount });
					CItemInfoLevel cItemInfoLevel4 = m_GameData.m_ItemCenter.Get(item3.m_nMaterialID, 1);
					if (cItemInfoLevel4 != null && cItemInfoLevel4.isCrystalPurchase)
					{
						CAchievementManager.GetInstance().AddAchievement(13);
						CTrinitiCollectManager.GetInstance().SendConsumeCrystal(cItemInfoLevel4.nPurchasePrice * item3.m_nMaterialCount, "exchange", item3.m_nMaterialID, item3.m_nMaterialCount);
					}
				}
			}
			if (supplementInfo.m_nGold > 0)
			{
				m_DataCenter.AddGold(supplementInfo.m_nGold);
				int crystal = MyUtils.Formula_Gold2Crystal(supplementInfo.m_nGold);
				CAchievementManager.GetInstance().AddAchievement(13);
				CTrinitiCollectManager.GetInstance().SendConsumeCrystal(crystal, "exgold", -1, supplementInfo.m_nGold);
			}
			m_DataCenter.AddCrystal(-Mathf.Abs(supplementInfo.m_nTotalCrystalCost));
			iGameApp.GetInstance().SaveData();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), true));
			Dictionary<int, NewMarkType> dictMarkData6 = null;
			if (GetMarkDataForge(out dictMarkData6))
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_NewMarkInfo, dictMarkData6));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_GoldToCrystal)
		{
			iGameData gameData3 = iGameApp.GetInstance().m_GameData;
			if (gameData3 == null)
			{
				return;
			}
			iDataCenter dataCenter3 = gameData3.GetDataCenter();
			if (dataCenter3 == null)
			{
				return;
			}
			int wParam6 = m_event.GetWParam();
			int num9 = MyUtils.Formula_Gold2Crystal(wParam6);
			if (dataCenter3.Crystal < num9)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), false, BackEventFalseType.NoCrystalEnough, num9 - dataCenter3.Crystal));
				return;
			}
			dataCenter3.AddCrystal(-Mathf.Abs(num9));
			CAchievementManager.GetInstance().AddAchievement(13);
			CTrinitiCollectManager.GetInstance().SendConsumeCrystal(num9, "exgold", -1, wParam6);
			dataCenter3.AddGold(wParam6);
			dataCenter3.Save();
			iServerSaveData.GetInstance().UploadImmidately();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), true));
			Dictionary<int, NewMarkType> dictMarkData7 = null;
			if (GetMarkDataForge(out dictMarkData7))
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_NewMarkInfo, dictMarkData7));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_EnterIAP)
		{
			iGameState gameState4 = iGameApp.GetInstance().m_GameState;
			if (gameState4 != null)
			{
				gameState4.m_lstScene4IAP = TUISceneType.Scene_Forge;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_EnterGold)
		{
			iGameState gameState5 = iGameApp.GetInstance().m_GameState;
			if (gameState5 != null)
			{
				gameState5.m_lstScene4IAP = TUISceneType.Scene_Forge;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_EnterIAPCrystalNoEnough)
		{
			iGameState gameState6 = iGameApp.GetInstance().m_GameState;
			if (gameState6 != null)
			{
				gameState6.m_lstScene4IAP = TUISceneType.Scene_Forge;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_EnterGoEquip)
		{
			if (m_GameState != null)
			{
				m_GameState.m_nGotoEquip_PopupType = m_event.GetWParam();
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), true));
			}
		}
		else
		{
			if (m_event.GetEventName() != TUIEvent.SceneForgeEventType.TUIEvent_SkipTutorial)
			{
				return;
			}
			iGameData gameData4 = iGameApp.GetInstance().m_GameData;
			if (gameData4 != null)
			{
				iDataCenter dataCenter4 = gameData4.GetDataCenter();
				if (dataCenter4 != null)
				{
					dataCenter4.nTutorialVillageState = 25;
					iGameApp.GetInstance().SaveData(true);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(m_event.GetEventName(), true));
				}
			}
		}
	}

	private void TUIEvent_BackInfo_SceneTavern(object sender, TUIEvent.SendEvent_SceneTavern m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_TopBar)
		{
			iGameData gameData = iGameApp.GetInstance().m_GameData;
			if (gameData == null)
			{
				return;
			}
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter == null)
			{
				return;
			}
			CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
			if (character != null)
			{
				CCharacterInfoLevel characterInfo = gameData.GetCharacterInfo(character.nID, character.nLevel);
				if (characterInfo != null)
				{
					AndroidReturnPlugin.instance.SetBackFunc(EventBack_Tarven);
					TUIGameInfo tUIGameInfo = new TUIGameInfo();
					tUIGameInfo.player_info = new TUIPlayerInfo();
					tUIGameInfo.player_info.role_id = character.nID;
					tUIGameInfo.player_info.level = character.nLevel;
					tUIGameInfo.player_info.level_exp = characterInfo.nExp;
					tUIGameInfo.player_info.exp = character.nExp;
					tUIGameInfo.player_info.gold = dataCenter.Gold;
					tUIGameInfo.player_info.crystal = dataCenter.Crystal;
					tUIGameInfo.player_info.avatar_model = characterInfo.nModel;
					tUIGameInfo.player_info.avatar_head = ((dataCenter.AvatarHead <= 0) ? characterInfo.nAvatarHead : dataCenter.AvatarHead);
					tUIGameInfo.player_info.avatar_body = ((dataCenter.AvatarUpper <= 0) ? characterInfo.nAvatarUpper : dataCenter.AvatarUpper);
					tUIGameInfo.player_info.avatar_leg = ((dataCenter.AvatarLower <= 0) ? characterInfo.nAvatarLower : dataCenter.AvatarLower);
					tUIGameInfo.player_info.default_weapon = dataCenter.GetSelectWeapon(0);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), tUIGameInfo));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_AllRoleInfo)
		{
			iGameData gameData2 = iGameApp.GetInstance().m_GameData;
			if (gameData2 == null)
			{
				return;
			}
			iCharacterCenter characterCenter = gameData2.GetCharacterCenter();
			if (characterCenter == null)
			{
				return;
			}
			iDataCenter dataCenter2 = gameData2.GetDataCenter();
			if (dataCenter2 == null)
			{
				return;
			}
			iShopDisplayCenter shopDisplayCenter = gameData2.m_ShopDisplayCenter;
			if (shopDisplayCenter == null)
			{
				return;
			}
			TUIGameInfo tUIGameInfo2 = new TUIGameInfo();
			tUIGameInfo2.all_role_info = new TUIAllRoleInfo();
			tUIGameInfo2.all_role_info.role_list = new TUIRoleInfo[6];
			int[] array = shopDisplayCenter.m_ltCharacter.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				CCharacterInfo characterInfo2 = gameData2.GetCharacterInfo(array[i]);
				if (characterInfo2 == null)
				{
					continue;
				}
				CCharacterInfoLevel cCharacterInfoLevel = characterInfo2.Get(1);
				if (i >= tUIGameInfo2.all_role_info.role_list.Length)
				{
					break;
				}
				CCharSaveInfo character2 = dataCenter2.GetCharacter(characterInfo2.nID);
				string introduce_unlock = string.Empty;
				if (characterInfo2.nUnLockLevel > 0)
				{
					iGameLevelCenter gameLevelCenter = gameData2.GetGameLevelCenter();
					if (gameLevelCenter != null)
					{
						GameLevelGroupInfo groupInfo = gameLevelCenter.GetGroupInfo(gameLevelCenter.GetGroupID(characterInfo2.nUnLockLevel));
						if (groupInfo != null)
						{
							introduce_unlock = "Complete Stage " + groupInfo.nID + " to unlock";
						}
					}
				}
				List<TUIPopupInfo> list = new List<TUIPopupInfo>();
				CSkillInfoLevel skillInfo = gameData2.GetSkillInfo(cCharacterInfoLevel.nSkill, 1);
				if (skillInfo != null)
				{
					TUIPopupInfo tUIPopupInfo = new TUIPopupInfo(cCharacterInfoLevel.nSkill, skillInfo.sName, skillInfo.sDesc);
					tUIPopupInfo.m_PopupType = PopupType.Skills01;
					list.Add(tUIPopupInfo);
				}
				int nDiscount = 100;
				bool flag = false;
				bool flag2 = false;
				iServerVerify.CServerConfigInfo serverConfigInfo = iServerVerify.GetInstance().GetServerConfigInfo();
				if (serverConfigInfo != null)
				{
					serverConfigInfo.IsPriceOff(3, characterInfo2.nID, ref nDiscount);
					string sBundleID = string.Empty;
					if (serverConfigInfo.IsPopularize(3, characterInfo2.nID, ref sBundleID))
					{
						flag = true;
						if (sBundleID.Length > 0 && InstalledAppPlugin.Check(sBundleID))
						{
							flag2 = true;
						}
						Debug.Log("check bundle " + sBundleID + " roleid = " + characterInfo2.nID + " check = " + flag2);
					}
				}
				if (!flag)
				{
					tUIGameInfo2.all_role_info.role_list[i] = new TUIRoleInfo(characterInfo2.nID, cCharacterInfoLevel.sName, cCharacterInfoLevel.sDesc, character2 != null, new TUIPriceInfo(characterInfo2.nUnLockPrice, characterInfo2.isCrystalUnLock ? UnitType.Crystal : UnitType.Gold), character2 != null && character2.nLevel >= 1, new TUIPriceInfo(characterInfo2.nPurchasePrice, characterInfo2.isCrystalPurchase ? UnitType.Crystal : UnitType.Gold), introduce_unlock, list, (float)nDiscount / 100f);
				}
				else
				{
					tUIGameInfo2.all_role_info.role_list[i] = new TUIRoleInfo(characterInfo2.nID, cCharacterInfoLevel.sName, cCharacterInfoLevel.sDesc, character2 != null, true, flag2, list);
				}
				tUIGameInfo2.all_role_info.role_list[i].m_nModelID = cCharacterInfoLevel.nModel;
				tUIGameInfo2.all_role_info.role_list[i].m_nAvatarHead = cCharacterInfoLevel.nAvatarHead;
				tUIGameInfo2.all_role_info.role_list[i].m_nAvatarUpper = cCharacterInfoLevel.nAvatarUpper;
				tUIGameInfo2.all_role_info.role_list[i].m_nAvatarLower = cCharacterInfoLevel.nAvatarLower;
				tUIGameInfo2.all_role_info.role_list[i].m_nAvatarHeadup = -1;
				tUIGameInfo2.all_role_info.role_list[i].m_nAvatarNeck = -1;
				tUIGameInfo2.all_role_info.role_list[i].m_nAvatarBracelet = -1;
				if (iGameApp.GetInstance().CheckCharacterSignState(1, characterInfo2.nID))
				{
					tUIGameInfo2.all_role_info.AddNewMark(characterInfo2.nID, NewMarkType.New);
				}
				else if (iGameApp.GetInstance().CheckCharacterMaterialEnough(characterInfo2.nID))
				{
					tUIGameInfo2.all_role_info.AddNewMark(characterInfo2.nID, NewMarkType.Mark);
				}
				else
				{
					tUIGameInfo2.all_role_info.AddNewMark(characterInfo2.nID, NewMarkType.None);
				}
			}
			iGameState gameState = iGameApp.GetInstance().m_GameState;
			if (gameState != null && gameState.m_nLinkCharacter > 0)
			{
				tUIGameInfo2.all_role_info.SetLinkInfo(gameState.m_nLinkCharacter);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), tUIGameInfo2));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_RoleUnlock)
		{
			bool flag3 = false;
			iGameData gameData3 = iGameApp.GetInstance().m_GameData;
			if (gameData3 != null)
			{
				iDataCenter dataCenter3 = gameData3.GetDataCenter();
				if (dataCenter3 != null)
				{
					int wParam = m_event.GetWParam();
					CCharSaveInfo character3 = dataCenter3.GetCharacter(wParam);
					if (character3 == null)
					{
						CCharacterInfo characterInfo3 = gameData3.GetCharacterInfo(wParam);
						if (characterInfo3 != null)
						{
							if (characterInfo3.isCrystalUnLock)
							{
								if (dataCenter3.Crystal < characterInfo3.nUnLockPrice)
								{
									global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), false, BackEventFalseType.NoCrystalEnough, characterInfo3.nUnLockPrice - dataCenter3.Crystal));
									return;
								}
								dataCenter3.AddCrystal(-Mathf.Abs(characterInfo3.nUnLockPrice));
								CAchievementManager.GetInstance().AddAchievement(13);
								CTrinitiCollectManager.GetInstance().SendConsumeCrystal(characterInfo3.nUnLockPrice, "unlockcharacter", wParam, -1);
								dataCenter3.UnlockCharacter(wParam);
								dataCenter3.Save();
								flag3 = true;
								CFlurryManager.GetInstance().ConsumeCrystal(CFlurryManager.kConsumeType.Char);
							}
							else
							{
								if (dataCenter3.Gold < characterInfo3.nUnLockPrice)
								{
									global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), false, BackEventFalseType.NoGoldEnough, characterInfo3.nUnLockPrice - dataCenter3.Gold));
									return;
								}
								dataCenter3.AddGold(-Mathf.Abs(characterInfo3.nUnLockPrice));
								CTrinitiCollectManager.GetInstance().SendConsumeGold(characterInfo3.nUnLockPrice, "unlockcharacter", wParam, -1);
								dataCenter3.UnlockCharacter(wParam);
								dataCenter3.Save();
								flag3 = true;
								CFlurryManager.GetInstance().ConsumeGold(CFlurryManager.kConsumeType.Char);
							}
							if (flag3)
							{
								iCharacterCenter characterCenter2 = gameData3.GetCharacterCenter();
								if (characterCenter2 != null)
								{
									Dictionary<int, CCharacterInfo> data = characterCenter2.GetData();
									if (data != null)
									{
										TUIGameInfo tUIGameInfo3 = new TUIGameInfo();
										tUIGameInfo3.all_role_info = new TUIAllRoleInfo();
										foreach (CCharacterInfo value in data.Values)
										{
											if (!iGameApp.GetInstance().CheckCharacterSignState(1, value.nID))
											{
												if (iGameApp.GetInstance().CheckCharacterMaterialEnough(value.nID))
												{
													tUIGameInfo3.all_role_info.AddNewMark(value.nID, NewMarkType.Mark);
												}
												else
												{
													tUIGameInfo3.all_role_info.AddNewMark(value.nID, NewMarkType.None);
												}
											}
										}
										global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_NewMarkInfo, tUIGameInfo3));
									}
								}
							}
						}
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), flag3));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_RoleBuy)
		{
			bool flag4 = false;
			iGameData gameData4 = iGameApp.GetInstance().m_GameData;
			if (gameData4 != null)
			{
				iDataCenter dataCenter4 = gameData4.GetDataCenter();
				if (dataCenter4 != null)
				{
					int wParam2 = m_event.GetWParam();
					CCharSaveInfo character4 = dataCenter4.GetCharacter(wParam2);
					if (character4 != null && character4.nLevel < 0)
					{
						CCharacterInfo characterInfo4 = gameData4.GetCharacterInfo(wParam2);
						if (characterInfo4 != null)
						{
							int num = characterInfo4.nPurchasePrice;
							iServerVerify.CServerConfigInfo serverConfigInfo2 = iServerVerify.GetInstance().GetServerConfigInfo();
							if (serverConfigInfo2 != null)
							{
								int nDiscount2 = 100;
								serverConfigInfo2.IsPriceOff(3, characterInfo4.nID, ref nDiscount2);
								num = (int)Mathf.Ceil((float)(characterInfo4.nPurchasePrice * nDiscount2) / 100f);
							}
							if (characterInfo4.isCrystalPurchase)
							{
								if (dataCenter4.Crystal < num)
								{
									global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), false, BackEventFalseType.NoCrystalEnough, num - dataCenter4.Crystal));
									return;
								}
								dataCenter4.AddCrystal(-Mathf.Abs(num));
								CAchievementManager.GetInstance().AddAchievement(13);
								CTrinitiCollectManager.GetInstance().SendConsumeCrystal(num, "buycharacter", wParam2, 1);
								dataCenter4.SetCharacter(wParam2, 1, 0);
								if (dataCenter4.CurCharID != wParam2)
								{
									dataCenter4.SetCharacterSign(wParam2, 3);
								}
								dataCenter4.Save();
								flag4 = true;
								CFlurryManager.GetInstance().ConsumeCrystal(CFlurryManager.kConsumeType.Char);
								iGameApp.GetInstance().Flurry_PurchaseChar(wParam2);
							}
							else
							{
								if (dataCenter4.Gold < num)
								{
									global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), false, BackEventFalseType.NoGoldEnough, num - dataCenter4.Gold));
									return;
								}
								dataCenter4.AddGold(-Mathf.Abs(num));
								CTrinitiCollectManager.GetInstance().SendConsumeGold(num, "buycharacter", wParam2, 1);
								dataCenter4.SetCharacter(wParam2, 1, 0);
								if (dataCenter4.CurCharID != wParam2)
								{
									dataCenter4.SetCharacterSign(wParam2, 3);
								}
								dataCenter4.Save();
								flag4 = true;
								CFlurryManager.GetInstance().ConsumeGold(CFlurryManager.kConsumeType.Char);
								iGameApp.GetInstance().Flurry_PurchaseChar(wParam2);
							}
							if (flag4)
							{
								iCharacterCenter characterCenter3 = gameData4.GetCharacterCenter();
								if (characterCenter3 != null)
								{
									Dictionary<int, CCharacterInfo> data2 = characterCenter3.GetData();
									if (data2 != null)
									{
										TUIGameInfo tUIGameInfo4 = new TUIGameInfo();
										tUIGameInfo4.all_role_info = new TUIAllRoleInfo();
										foreach (CCharacterInfo value2 in data2.Values)
										{
											if (!iGameApp.GetInstance().CheckCharacterSignState(1, value2.nID))
											{
												if (iGameApp.GetInstance().CheckCharacterMaterialEnough(value2.nID))
												{
													tUIGameInfo4.all_role_info.AddNewMark(value2.nID, NewMarkType.Mark);
												}
												else
												{
													tUIGameInfo4.all_role_info.AddNewMark(value2.nID, NewMarkType.None);
												}
											}
										}
										global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_NewMarkInfo, tUIGameInfo4));
									}
								}
								if (iGameApp.GetInstance().m_GameState != null)
								{
									iGameApp.GetInstance().m_GameState.isCheckUnLock = true;
								}
							}
						}
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), flag4));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_GetActiveRole)
		{
			bool flag5 = false;
			iGameData gameData5 = iGameApp.GetInstance().m_GameData;
			if (gameData5 != null)
			{
				iDataCenter dataCenter5 = gameData5.GetDataCenter();
				if (dataCenter5 != null)
				{
					int wParam3 = m_event.GetWParam();
					CCharSaveInfo character5 = dataCenter5.GetCharacter(wParam3);
					if (character5 == null || character5.nLevel < 0)
					{
						CCharacterInfo characterInfo5 = gameData5.GetCharacterInfo(wParam3);
						if (characterInfo5 != null)
						{
							iServerVerify.CServerConfigInfo serverConfigInfo3 = iServerVerify.GetInstance().GetServerConfigInfo();
							if (serverConfigInfo3 != null)
							{
								string sBundleID2 = string.Empty;
								serverConfigInfo3.IsPopularize(3, characterInfo5.nID, ref sBundleID2);
								if (sBundleID2.Length > 0 && InstalledAppPlugin.Check(sBundleID2))
								{
									dataCenter5.SetCharacter(wParam3, 1, 0);
									if (dataCenter5.CurCharID != wParam3)
									{
										dataCenter5.SetCharacterSign(wParam3, 3);
									}
									dataCenter5.Save();
									flag5 = true;
									iGameApp.GetInstance().Flurry_PurchaseChar(wParam3);
								}
							}
							if (flag5 && iGameApp.GetInstance().m_GameState != null)
							{
								iGameApp.GetInstance().m_GameState.isCheckUnLock = true;
							}
						}
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), flag5));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_RoleChange)
		{
			bool success = false;
			TUIGameInfo tUIGameInfo5 = new TUIGameInfo();
			tUIGameInfo5.player_info = new TUIPlayerInfo();
			iGameData gameData6 = iGameApp.GetInstance().m_GameData;
			if (gameData6 != null)
			{
				iDataCenter dataCenter6 = gameData6.GetDataCenter();
				if (dataCenter6 != null)
				{
					int wParam4 = m_event.GetWParam();
					CCharSaveInfo character6 = dataCenter6.GetCharacter(wParam4);
					if (character6 != null && character6.nLevel >= 1)
					{
						CCharacterInfoLevel characterInfo6 = gameData6.GetCharacterInfo(character6.nID, character6.nLevel);
						if (characterInfo6 != null)
						{
							dataCenter6.CurCharID = wParam4;
							dataCenter6.Save();
							success = true;
							tUIGameInfo5.player_info.role_id = character6.nID;
							tUIGameInfo5.player_info.level = character6.nLevel;
							tUIGameInfo5.player_info.level_exp = characterInfo6.nExp;
							tUIGameInfo5.player_info.exp = character6.nExp;
							tUIGameInfo5.player_info.gold = dataCenter6.Gold;
							tUIGameInfo5.player_info.crystal = dataCenter6.Crystal;
						}
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), tUIGameInfo5, success));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_Back)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_RolesChoose)
		{
			iGameData gameData7 = iGameApp.GetInstance().m_GameData;
			if (gameData7 == null)
			{
				return;
			}
			iGameState gameState2 = iGameApp.GetInstance().m_GameState;
			if (gameState2 == null)
			{
				return;
			}
			iDataCenter dataCenter7 = gameData7.GetDataCenter();
			if (dataCenter7 == null)
			{
				return;
			}
			int nSignState = 0;
			int num2 = (gameState2.m_nLinkCharacter = m_event.GetWParam());
			if (!dataCenter7.GetCharacterSign(num2, ref nSignState))
			{
				return;
			}
			if (nSignState == 1)
			{
				dataCenter7.SetCharacterSign(num2, 2);
				dataCenter7.Save();
				if (iGameApp.GetInstance().CheckCharacterMaterialEnough(num2))
				{
					TUIGameInfo tUIGameInfo6 = new TUIGameInfo();
					tUIGameInfo6.all_role_info = new TUIAllRoleInfo();
					tUIGameInfo6.all_role_info.AddNewMark(num2, NewMarkType.Mark);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_NewMarkInfo, tUIGameInfo6));
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName()));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_GoldToCrystal)
		{
			iGameData gameData8 = iGameApp.GetInstance().m_GameData;
			if (gameData8 == null)
			{
				return;
			}
			iDataCenter dataCenter8 = gameData8.GetDataCenter();
			if (dataCenter8 != null)
			{
				int wParam5 = m_event.GetWParam();
				int num3 = MyUtils.Formula_Gold2Crystal(wParam5);
				if (dataCenter8.Crystal < num3)
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), false, BackEventFalseType.NoCrystalEnough, num3 - dataCenter8.Crystal));
					return;
				}
				dataCenter8.AddCrystal(-Mathf.Abs(num3));
				CAchievementManager.GetInstance().AddAchievement(13);
				CTrinitiCollectManager.GetInstance().SendConsumeCrystal(num3, "exgold", -1, wParam5);
				dataCenter8.AddGold(wParam5);
				dataCenter8.Save();
				iServerSaveData.GetInstance().UploadImmidately();
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_EnterIAP)
		{
			iGameState gameState3 = iGameApp.GetInstance().m_GameState;
			if (gameState3 != null)
			{
				gameState3.m_lstScene4IAP = TUISceneType.Scene_Tavern;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_EnterGold)
		{
			iGameState gameState4 = iGameApp.GetInstance().m_GameState;
			if (gameState4 != null)
			{
				gameState4.m_lstScene4IAP = TUISceneType.Scene_Tavern;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_EnterIAPCrystalNoEnough)
		{
			iGameState gameState5 = iGameApp.GetInstance().m_GameState;
			if (gameState5 != null)
			{
				gameState5.m_lstScene4IAP = TUISceneType.Scene_Tavern;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_EnterGoEquip)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(m_event.GetEventName(), true));
		}
	}

	private void TUIEvent_BackInfo_SceneMap(object sender, TUIEvent.SendEvent_SceneMap m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_TopBar)
		{
			iGameData gameData = iGameApp.GetInstance().m_GameData;
			if (gameData == null)
			{
				return;
			}
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter == null)
			{
				return;
			}
			CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
			if (character == null)
			{
				return;
			}
			CCharacterInfoLevel characterInfo = gameData.GetCharacterInfo(character.nID, character.nLevel);
			if (characterInfo != null)
			{
				AndroidReturnPlugin.instance.SetBackFunc(EventBack_Map);
				TUIGameInfo tUIGameInfo = new TUIGameInfo();
				tUIGameInfo.player_info = new TUIPlayerInfo();
				tUIGameInfo.player_info.role_id = character.nID;
				tUIGameInfo.player_info.level = character.nLevel;
				tUIGameInfo.player_info.level_exp = characterInfo.nExp;
				tUIGameInfo.player_info.exp = character.nExp;
				tUIGameInfo.player_info.gold = dataCenter.Gold;
				tUIGameInfo.player_info.crystal = dataCenter.Crystal;
				iGameState gameState = iGameApp.GetInstance().m_GameState;
				if (gameState != null)
				{
					gameState.m_curScene4SearchMaterial = gameState.m_lstScene4SearchMaterial;
					gameState.m_lstScene4SearchMaterial = TUISceneType.None;
				}
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(m_event.GetEventName(), tUIGameInfo));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_MapEnterInfo)
		{
			iGameState gameState2 = iGameApp.GetInstance().m_GameState;
			if (gameState2 == null)
			{
				return;
			}
			iGameData gameData2 = iGameApp.GetInstance().m_GameData;
			if (gameData2 == null)
			{
				return;
			}
			iDataCenter dataCenter2 = gameData2.GetDataCenter();
			if (dataCenter2 == null)
			{
				return;
			}
			iGameLevelCenter gameLevelCenter = gameData2.GetGameLevelCenter();
			if (gameLevelCenter == null)
			{
				return;
			}
			Dictionary<int, GameLevelGroupInfo> dataGroup = gameLevelCenter.GetDataGroup();
			if (dataGroup == null)
			{
				return;
			}
			int num = -1;
			int main_level_camera_stop = -1;
			int num2 = -1;
			int num3 = -1;
			Debug.Log(dataCenter2.isUnLockLevel);
			if (dataCenter2.isUnLockLevel)
			{
				int nextLevel = gameLevelCenter.GetNextLevel(dataCenter2.LatestLevel);
				Debug.Log(nextLevel);
				if (nextLevel > 1)
				{
					num2 = gameLevelCenter.GetGroupID(dataCenter2.LatestLevel);
					num3 = gameLevelCenter.GetGroupID(nextLevel);
					dataCenter2.UnlockNewLevelConfirm(nextLevel);
					dataCenter2.Save();
					Debug.Log(num2 + " " + num3);
				}
			}
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (GameLevelGroupInfo value2 in dataGroup.Values)
			{
				int value = 0;
				for (int i = 0; i < value2.ltLevelList.Count; i++)
				{
					if (dataCenter2.IsLevelPassed(value2.ltLevelList[i]))
					{
						value = i + 1;
					}
					if (dataCenter2.LatestLevel == value2.ltLevelList[i])
					{
						num = value2.nID;
					}
					else if (dataCenter2.LastLevel == value2.ltLevelList[i])
					{
						main_level_camera_stop = value2.nID;
					}
				}
				dictionary.Add(value2.nID, value);
			}
			TUIGameInfo tUIGameInfo2 = new TUIGameInfo();
			NewHelpState newHelpState = TUIMappingInfo.Instance().GetNewHelpState();
			if (newHelpState == NewHelpState.Help09_ClickLevel02 || newHelpState == NewHelpState.Help19_ClickLevel03)
			{
				tUIGameInfo2.map_info = new TUIMapInfo(MapEnterType.Normal, 1, dictionary);
			}
			else if (gameState2.m_nMaterialIDFromEquip == -1)
			{
				gameState2.m_nMaterialIDFromEquipTemp = -1;
				if (num2 > 0 && num3 > 0 && num2 != num3)
				{
					Debug.Log(num2 + " " + num3);
					tUIGameInfo2.map_info = new TUIMapInfo(MapEnterType.OpenNewLevel, num2, num3, dictionary);
				}
				else
				{
					Debug.Log("!!!!" + num);
					tUIGameInfo2.map_info = new TUIMapInfo(MapEnterType.Normal, num, dictionary, main_level_camera_stop);
				}
			}
			else
			{
				gameState2.m_nMaterialIDFromEquipTemp = gameState2.m_nMaterialIDFromEquip;
				gameState2.m_nMaterialIDFromEquip = -1;
				int nMaterialIDFromEquipTemp = gameState2.m_nMaterialIDFromEquipTemp;
				List<int> list = new List<int>();
				foreach (GameLevelGroupInfo value3 in dataGroup.Values)
				{
					if (value3.ltLevelList == null)
					{
						continue;
					}
					foreach (int ltLevel in value3.ltLevelList)
					{
						GameLevelInfo gameLevelInfo = gameData2.GetGameLevelInfo(ltLevel);
						if (gameLevelInfo == null)
						{
							continue;
						}
						bool flag = false;
						foreach (CRewardMaterial item in gameLevelInfo.ltRewardMaterial)
						{
							if (gameLevelInfo.ltRewardMaterial == null || nMaterialIDFromEquipTemp != item.nID)
							{
								continue;
							}
							list.Add(value3.nID);
							flag = true;
							break;
						}
						if (!flag)
						{
							continue;
						}
						break;
					}
				}
				bool flag2 = false;
				if (m_GameData.m_HunterLevelCenter != null)
				{
					Dictionary<int, CHunterLevelInfo> data = m_GameData.m_HunterLevelCenter.GetData();
					if (data != null)
					{
						foreach (CHunterLevelInfo value4 in data.Values)
						{
							if (flag2)
							{
								break;
							}
							foreach (int item2 in value4.m_ltGameLevel)
							{
								if (flag2)
								{
									break;
								}
								GameLevelInfo gameLevelInfo2 = gameData2.GetGameLevelInfo(item2);
								if (gameLevelInfo2 == null)
								{
									continue;
								}
								foreach (CRewardMaterial item3 in gameLevelInfo2.ltRewardMaterial)
								{
									if (gameLevelInfo2.ltRewardMaterial == null || nMaterialIDFromEquipTemp != item3.nID)
									{
										continue;
									}
									flag2 = true;
									break;
								}
							}
						}
					}
				}
				tUIGameInfo2.map_info = new TUIMapInfo(MapEnterType.SearchGoods, num, dictionary, list.ToArray(), flag2);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(m_event.GetEventName(), tUIGameInfo2));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_LevelInfo)
		{
			iGameState gameState3 = iGameApp.GetInstance().m_GameState;
			if (gameState3 == null)
			{
				return;
			}
			iGameData gameData3 = iGameApp.GetInstance().m_GameData;
			if (gameData3 == null)
			{
				return;
			}
			iDataCenter dataCenter3 = gameData3.GetDataCenter();
			if (dataCenter3 == null)
			{
				return;
			}
			int wParam = m_event.GetWParam();
			GameLevelGroupInfo gameLevelGroupInfo = gameData3.GetGameLevelGroupInfo(wParam);
			if (gameLevelGroupInfo == null || gameLevelGroupInfo.ltLevelList == null)
			{
				return;
			}
			List<int> list2 = new List<int>();
			int num4 = -1;
			switch (TUIMappingInfo.Instance().GetNewHelpState())
			{
			case NewHelpState.Help09_ClickLevel02:
				num4 = 1002;
				break;
			case NewHelpState.Help19_ClickLevel03:
				num4 = 1003;
				break;
			default:
				if (gameState3.m_nMaterialIDFromEquipTemp > 0)
				{
					int nMaterialIDFromEquipTemp2 = gameState3.m_nMaterialIDFromEquipTemp;
					foreach (int ltLevel2 in gameLevelGroupInfo.ltLevelList)
					{
						GameLevelInfo gameLevelInfo3 = gameData3.GetGameLevelInfo(ltLevel2);
						if (gameLevelInfo3 == null)
						{
							continue;
						}
						foreach (CRewardMaterial item4 in gameLevelInfo3.ltRewardMaterial)
						{
							if (gameLevelInfo3.ltRewardMaterial == null || nMaterialIDFromEquipTemp2 != item4.nID)
							{
								continue;
							}
							num4 = ltLevel2;
							list2.Add(ltLevel2);
							break;
						}
					}
					break;
				}
				foreach (int ltLevel3 in gameLevelGroupInfo.ltLevelList)
				{
					if (ltLevel3 == dataCenter3.LatestLevel)
					{
						num4 = ltLevel3;
						break;
					}
					if (ltLevel3 == dataCenter3.LastLevel)
					{
						num4 = ltLevel3;
					}
				}
				break;
			}
			bool flag3 = false;
			foreach (int ltLevel4 in gameLevelGroupInfo.ltLevelList)
			{
				if (num4 == ltLevel4)
				{
					flag3 = true;
					break;
				}
			}
			if (!flag3)
			{
				num4 = gameLevelGroupInfo.ltLevelList[gameLevelGroupInfo.ltLevelList.Count - 1];
			}
			TUIMainLevelInfo tUIMainLevelInfo = new TUIMainLevelInfo(wParam, gameLevelGroupInfo.sName, (MainLevelType)gameLevelGroupInfo.nIcon, num4, list2.ToArray());
			foreach (int ltLevel5 in gameLevelGroupInfo.ltLevelList)
			{
				GameLevelInfo gameLevelInfo4 = gameData3.GetGameLevelInfo(ltLevel5);
				if (gameLevelInfo4 == null)
				{
					return;
				}
				TUIRecommendRoleInfo tUIRecommendRoleInfo = null;
				TUIRecommendWeaponInfo tUIRecommendWeaponInfo = null;
				Debug.Log("!!!!");
				if (gameLevelInfo4.m_nRecommandType == 1)
				{
					CWeaponInfoLevel weaponInfo = gameData3.GetWeaponInfo(gameLevelInfo4.m_nRecommandID, gameLevelInfo4.m_nRecommandLevel);
					if (weaponInfo != null)
					{
						bool have_equip = false;
						int nLevel = 0;
						dataCenter3.GetWeaponLevel(gameLevelInfo4.m_nRecommandID, ref nLevel);
						if (nLevel <= 0)
						{
							nLevel = 0;
						}
						else
						{
							for (int j = 0; j < 3; j++)
							{
								if (dataCenter3.GetSelectWeapon(j) == gameLevelInfo4.m_nRecommandID)
								{
									have_equip = true;
									break;
								}
							}
						}
						tUIRecommendWeaponInfo = new TUIRecommendWeaponInfo(gameLevelInfo4.m_nRecommandID, nLevel, gameLevelInfo4.m_nRecommandLevel, have_equip, gameLevelInfo4.m_bRecommandLimit);
					}
				}
				else if (gameLevelInfo4.m_nRecommandType == 2)
				{
					CCharacterInfoLevel characterInfo2 = gameData3.GetCharacterInfo(gameLevelInfo4.m_nRecommandID, gameLevelInfo4.m_nRecommandLevel);
					if (characterInfo2 != null)
					{
						bool have_equip2 = false;
						bool have_buy = false;
						CCharSaveInfo character2 = dataCenter3.GetCharacter(gameLevelInfo4.m_nRecommandID);
						if (character2 != null)
						{
							if (character2.nLevel > 0)
							{
								have_buy = true;
							}
							if (dataCenter3.CurCharID == gameLevelInfo4.m_nRecommandID)
							{
								have_equip2 = true;
							}
						}
						tUIRecommendRoleInfo = new TUIRecommendRoleInfo(gameLevelInfo4.m_nRecommandID, have_buy, have_equip2, gameLevelInfo4.m_bRecommandLimit);
					}
				}
				else if (gameLevelInfo4.m_nRecommandType == 3)
				{
					CItemInfoLevel itemInfo = gameData3.GetItemInfo(gameLevelInfo4.m_nRecommandID, gameLevelInfo4.m_nRecommandLevel);
					if (itemInfo != null)
					{
						bool have_equip3 = false;
						int nItemLevel = 0;
						dataCenter3.GetEquipStone(gameLevelInfo4.m_nRecommandID, ref nItemLevel);
						if (nItemLevel <= 0)
						{
							nItemLevel = 0;
						}
						else if (dataCenter3.CurEquipStone == gameLevelInfo4.m_nRecommandID)
						{
							have_equip3 = true;
						}
						tUIRecommendWeaponInfo = new TUIRecommendWeaponInfo(gameLevelInfo4.m_nRecommandID, nItemLevel, gameLevelInfo4.m_nRecommandLevel, have_equip3, gameLevelInfo4.m_bRecommandLimit);
					}
				}
				Debug.Log("!!!!");
				string sLevelDesc = gameLevelInfo4.sLevelDesc;
				string text = "Exp: " + gameLevelInfo4.nRewardExp + "\nGold: " + gameLevelInfo4.nRewardGold;
				List<TUIGoodsInfo> list3 = new List<TUIGoodsInfo>();
				if (gameLevelInfo4.ltRewardMaterial != null)
				{
					foreach (CRewardMaterial item5 in gameLevelInfo4.ltRewardMaterial)
					{
						if (item5.nID == 0)
						{
							continue;
						}
						CItemInfoLevel itemInfo2 = gameData3.GetItemInfo(item5.nID, 1);
						if (itemInfo2 != null)
						{
							GoodsQualityType quality = GoodsQualityType.Quality01;
							switch (itemInfo2.nRare)
							{
							case 1:
								quality = GoodsQualityType.Quality01;
								break;
							case 2:
								quality = GoodsQualityType.Quality02;
								break;
							case 3:
								quality = GoodsQualityType.Quality03;
								break;
							case 4:
								quality = GoodsQualityType.Quality04;
								break;
							case 5:
								quality = GoodsQualityType.Quality05;
								break;
							case 6:
								quality = GoodsQualityType.Quality06;
								break;
							}
							list3.Add(new TUIGoodsInfo(item5.nID, quality, itemInfo2.sName));
						}
					}
				}
				SecondaryLevelType secondaryLevelType = SecondaryLevelType.None;
				CTaskInfo taskInfo = gameData3.GetTaskInfo(gameLevelInfo4.nTaskID);
				if (taskInfo != null)
				{
					switch (taskInfo.nType)
					{
					case 1:
						secondaryLevelType = SecondaryLevelType.Steal;
						break;
					case 2:
						secondaryLevelType = SecondaryLevelType.BOSS;
						break;
					case 3:
						secondaryLevelType = SecondaryLevelType.Defended;
						break;
					case 5:
						secondaryLevelType = SecondaryLevelType.Killing;
						break;
					case 4:
						secondaryLevelType = SecondaryLevelType.Survival;
						break;
					}
				}
				LevelPassState levelPassState = LevelPassState.Disable;
				if (dataCenter3.IsLevelPassed(ltLevel5))
				{
					levelPassState = LevelPassState.Pass;
				}
				else if (dataCenter3.LatestLevel == ltLevel5)
				{
					levelPassState = LevelPassState.Normal;
				}
				Debug.Log(string.Concat(ltLevel5, " ", levelPassState, " ", secondaryLevelType));
				tUIMainLevelInfo.AddSecondaryLevelInfo(new TUISecondaryLevelInfo(ltLevel5, gameLevelInfo4.sLevelDesc, list3, secondaryLevelType, levelPassState));
			}
			TUIGameInfo tUIGameInfo3 = new TUIGameInfo();
			tUIGameInfo3.map_info = new TUIMapInfo(tUIMainLevelInfo);
			Debug.Log("!!!!");
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(m_event.GetEventName(), tUIGameInfo3));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterLevel)
		{
			iGameState gameState4 = iGameApp.GetInstance().m_GameState;
			if (gameState4 != null)
			{
				int wParam2 = m_event.GetWParam();
				UnityEngine.Debug.Log(wParam2);
				gameState4.GameLevel = wParam2;
				iGameApp.GetInstance().EnterScene(kGameSceneEnum.Game);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_Back)
		{
			iGameState gameState5 = iGameApp.GetInstance().m_GameState;
			if (gameState5 != null)
			{
				if (gameState5.m_curScene4SearchMaterial != 0)
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(m_event.GetEventName(), true, (int)gameState5.m_curScene4SearchMaterial));
					gameState5.m_curScene4SearchMaterial = TUISceneType.None;
				}
				else
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(m_event.GetEventName(), true));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterWeaponBuy)
		{
			int wParam3 = m_event.GetWParam();
			iGameState gameState6 = iGameApp.GetInstance().m_GameState;
			if (gameState6 != null)
			{
				gameState6.m_nLinkWeapon = wParam3;
				gameState6.m_lstScene4Recommand = TUISceneType.Scene_Map;
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterRoleBuy)
		{
			int wParam4 = m_event.GetWParam();
			iGameState gameState7 = iGameApp.GetInstance().m_GameState;
			if (gameState7 != null)
			{
				gameState7.m_nLinkCharacter = wParam4;
				gameState7.m_lstScene4Recommand = TUISceneType.Scene_Map;
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterEquip)
		{
			iGameState gameState8 = iGameApp.GetInstance().m_GameState;
			if (gameState8 != null)
			{
				gameState8.m_lstScene4Recommand = TUISceneType.Scene_Map;
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterIAP)
		{
			iGameState gameState9 = iGameApp.GetInstance().m_GameState;
			if (gameState9 != null)
			{
				gameState9.m_lstScene4IAP = TUISceneType.Scene_Map;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterGold)
		{
			iGameState gameState10 = iGameApp.GetInstance().m_GameState;
			if (gameState10 != null)
			{
				gameState10.m_lstScene4IAP = TUISceneType.Scene_Map;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterVilliage)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_ClickPopularize)
		{
			PopularizeType wParam5 = (PopularizeType)m_event.GetWParam();
			Debug.Log(wParam5);
			iGameState gameState11 = iGameApp.GetInstance().m_GameState;
			if (gameState11 != null && gameState11.m_ServerAdvertInfo != null && gameState11.m_ServerAdvertInfo.dictAdvertUrl != null && gameState11.m_ServerAdvertInfo.dictAdvertUrl.ContainsKey((int)wParam5))
			{
				Application.OpenURL(gameState11.m_ServerAdvertInfo.dictAdvertUrl[(int)wParam5]);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(m_event.GetEventName()));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_SkipTutorial)
		{
			iGameData gameData4 = iGameApp.GetInstance().m_GameData;
			if (gameData4 != null)
			{
				iDataCenter dataCenter4 = gameData4.GetDataCenter();
				if (dataCenter4 != null)
				{
					dataCenter4.nTutorialVillageState = 25;
					iGameApp.GetInstance().SaveData(true);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(m_event.GetEventName(), true));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterCoop && m_DataCenter != null)
		{
			if (m_DataCenter.NickName != null && m_DataCenter.NickName.Length > 0)
			{
				UnityEngine.Debug.Log("myname is " + m_DataCenter.NickName);
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(m_event.GetEventName(), true, 12));
			}
			else
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(m_event.GetEventName(), true));
			}
		}
	}

	private void TUIEvent_BackInfo_SceneIAP(object sender, TUIEvent.SendEvent_SceneIAP m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneIAPEventType.TUIEvent_TopBar)
		{
			iGameData gameData = iGameApp.GetInstance().m_GameData;
			if (gameData == null)
			{
				return;
			}
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter == null)
			{
				return;
			}
			CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
			if (character == null)
			{
				return;
			}
			CCharacterInfoLevel characterInfo = gameData.GetCharacterInfo(character.nID, character.nLevel);
			if (characterInfo == null)
			{
				return;
			}
			AndroidReturnPlugin.instance.SetBackFunc(EventBack_IAP);
			List<iDataCenter.CCrystalInBackground> crystalInBackground = dataCenter.GetCrystalInBackground();
			if (crystalInBackground.Count > 0)
			{
				int num = 0;
				string text = string.Empty;
				foreach (iDataCenter.CCrystalInBackground item in crystalInBackground)
				{
					dataCenter.AddCrystal(item.m_nCrystal.Get());
					num += item.m_nCrystal.Get();
					if (text.Length > 0)
					{
						text += '\n';
					}
					string text2 = text;
					text = text2 + "$" + item.m_fMoney + " for " + item.m_nCrystal.Get() + " crystals";
				}
				dataCenter.ClearCrystalInBackground();
				iGameApp.GetInstance().SaveData();
				CUISound.GetInstance().Play("UI_Crystal");
				CMessageBoxScript.GetInstance().MessageBox("You got " + num + " crystals", text, null, null, "OK");
			}
			TUIGameInfo tUIGameInfo = new TUIGameInfo();
			tUIGameInfo.player_info = new TUIPlayerInfo();
			tUIGameInfo.player_info.role_id = character.nID;
			tUIGameInfo.player_info.level = character.nLevel;
			tUIGameInfo.player_info.level_exp = characterInfo.nExp;
			tUIGameInfo.player_info.exp = character.nExp;
			tUIGameInfo.player_info.gold = dataCenter.Gold;
			tUIGameInfo.player_info.crystal = dataCenter.Crystal;
			iGameState gameState = iGameApp.GetInstance().m_GameState;
			if (gameState != null)
			{
				gameState.m_curScene4IAP = gameState.m_lstScene4IAP;
				gameState.m_lstScene4IAP = TUISceneType.None;
			}
			iServerIAPVerifyBackground.GetInstance().SetActive(false);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneIAP(m_event.GetEventName(), tUIGameInfo));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneIAPEventType.TUIEvent_IAPEnterInfo)
		{
			iGameData gameData2 = iGameApp.GetInstance().m_GameData;
			if (gameData2 == null)
			{
				return;
			}
			iIAPCenter iAPCenter = gameData2.GetIAPCenter();
			if (iAPCenter == null)
			{
				return;
			}
			Dictionary<int, CIAPInfo> data = iAPCenter.GetData();
			if (data == null)
			{
				return;
			}
			TUIGameInfo tUIGameInfo2 = new TUIGameInfo();
			tUIGameInfo2.iap_info = new TUIIAPInfo();
			foreach (CIAPInfo value in data.Values)
			{
				tUIGameInfo2.iap_info.AddIAPItem(new TUISingleIAPInfo(value.nID, value.fMoney, value.nValue, value.isCrystal ? UnitType.Crystal : UnitType.Gold, value.sIcon, value.nFree));
			}
			if (m_GameState != null)
			{
				m_GameState.m_bInIAPScene = true;
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneIAP(m_event.GetEventName(), tUIGameInfo2));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneIAPEventType.TUIEvent_IAPBuy)
		{
			iGameData gameData3 = iGameApp.GetInstance().m_GameData;
			if (gameData3 != null)
			{
				int wParam = m_event.GetWParam();
				CIAPInfo iAPInfo = gameData3.GetIAPInfo(wParam);
				if (iAPInfo != null)
				{
					iServerSaveData.GetInstance().IsBackgroundUpload = false;
					iServerSaveData.GetInstance().IsBackgroundBack = false;
					iServerSaveData.GetInstance().IsBackgroundRelogin = false;
					iIAPManager.GetInstance().StartGooglePurchase(iAPInfo.sKey, OnPurchaseIAPSuccess, OnPurchaseIAPFailed, OnPurchaseIAPCancel, OnPurchaseIAPFailed);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneIAP(m_event.GetEventName()));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneIAPEventType.TUIEvent_Back)
		{
			iGameState gameState2 = iGameApp.GetInstance().m_GameState;
			if (gameState2 != null)
			{
				gameState2.m_bInIAPScene = false;
				if (gameState2.m_curScene4IAP != 0)
				{
					Debug.Log(gameState2.m_curScene4IAP);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneIAP(m_event.GetEventName(), true, (int)gameState2.m_curScene4IAP));
					gameState2.m_curScene4IAP = TUISceneType.None;
				}
				else
				{
					iServerIAPVerifyBackground.GetInstance().SetActive(true);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneIAP(m_event.GetEventName(), true));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneIAPEventType.TUIEvent_EnterGold)
		{
			iGameState gameState3 = iGameApp.GetInstance().m_GameState;
			if (gameState3 != null)
			{
				gameState3.m_lstScene4IAP = gameState3.m_curScene4IAP;
				iServerIAPVerifyBackground.GetInstance().SetActive(true);
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneIAP(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneIAPEventType.TUIEvent_TapJoy)
		{
			Debug.Log("click tapjoy");
			MyTapjoy.GetInstance().Show();
		}
	}

	private void TUIEvent_BackInfo_SceneGold(object sender, TUIEvent.SendEvent_SceneGold m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneGoldEventType.TUIEvent_TopBar)
		{
			iGameData gameData = iGameApp.GetInstance().m_GameData;
			if (gameData == null)
			{
				return;
			}
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter == null)
			{
				return;
			}
			CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
			if (character == null)
			{
				return;
			}
			CCharacterInfoLevel characterInfo = gameData.GetCharacterInfo(character.nID, character.nLevel);
			if (characterInfo != null)
			{
				AndroidReturnPlugin.instance.SetBackFunc(EventBack_Gold);
				TUIGameInfo tUIGameInfo = new TUIGameInfo();
				tUIGameInfo.player_info = new TUIPlayerInfo();
				tUIGameInfo.player_info.role_id = character.nID;
				tUIGameInfo.player_info.level = character.nLevel;
				tUIGameInfo.player_info.level_exp = characterInfo.nExp;
				tUIGameInfo.player_info.exp = character.nExp;
				tUIGameInfo.player_info.gold = dataCenter.Gold;
				tUIGameInfo.player_info.crystal = dataCenter.Crystal;
				iGameState gameState = iGameApp.GetInstance().m_GameState;
				if (gameState != null)
				{
					gameState.m_curScene4IAP = gameState.m_lstScene4IAP;
					gameState.m_lstScene4IAP = TUISceneType.None;
				}
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneGold(m_event.GetEventName(), tUIGameInfo));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneGoldEventType.TUIEvent_GoldBuy)
		{
			iGameData gameData2 = iGameApp.GetInstance().m_GameData;
			if (gameData2 == null)
			{
				return;
			}
			iDataCenter dataCenter2 = gameData2.GetDataCenter();
			if (dataCenter2 == null)
			{
				return;
			}
			int wParam = m_event.GetWParam();
			CCrystal2GoldInfo crystal2GoldInfo = gameData2.GetCrystal2GoldInfo(wParam);
			if (crystal2GoldInfo != null)
			{
				if (dataCenter2.Crystal >= crystal2GoldInfo.nCrystal)
				{
					dataCenter2.AddGold(crystal2GoldInfo.nGold);
					dataCenter2.AddCrystal(-Mathf.Abs(crystal2GoldInfo.nCrystal));
					CAchievementManager.GetInstance().AddAchievement(13);
					CTrinitiCollectManager.GetInstance().SendConsumeCrystal(crystal2GoldInfo.nCrystal, "exgold", -1, crystal2GoldInfo.nGold);
					dataCenter2.Save();
					iServerSaveData.GetInstance().UploadImmidately();
					CFlurryManager.GetInstance().ConsumeCrystal(CFlurryManager.kConsumeType.Gold);
					TUIGameInfo tUIGameInfo2 = new TUIGameInfo();
					tUIGameInfo2.player_info = new TUIPlayerInfo();
					tUIGameInfo2.player_info.gold = dataCenter2.Gold;
					tUIGameInfo2.player_info.crystal = dataCenter2.Crystal;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneGold(TUIEvent.SceneGoldEventType.TUIEvent_GoldResult, tUIGameInfo2, true));
				}
				else
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneGold(TUIEvent.SceneGoldEventType.TUIEvent_GoldResult, false, BackEventFalseType.NoCrystalEnough, crystal2GoldInfo.nCrystal - dataCenter2.Crystal));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneGoldEventType.TUIEvent_Back)
		{
			iGameState gameState2 = iGameApp.GetInstance().m_GameState;
			if (gameState2 != null)
			{
				if (gameState2.m_curScene4IAP != 0)
				{
					Debug.Log(gameState2.m_curScene4IAP);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneGold(m_event.GetEventName(), true, (int)gameState2.m_curScene4IAP));
					gameState2.m_curScene4IAP = TUISceneType.None;
				}
				else
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneGold(m_event.GetEventName(), true));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneGoldEventType.TUIEvent_EnterIAP)
		{
			iGameState gameState3 = iGameApp.GetInstance().m_GameState;
			if (gameState3 != null)
			{
				gameState3.m_lstScene4IAP = gameState3.m_curScene4IAP;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneGold(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneGoldEventType.TUIEvent_EnterIAPCrystalNoEnough)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneGold(m_event.GetEventName(), true));
		}
	}

	protected void OnPurchaseIAPSuccess(string sIAPKey, string sIdentifier, string sReceipt, string sSignature)
	{
		Debug.Log("OnPurchaseIAPSuccess");
		iServerSaveData.GetInstance().IsBackgroundUpload = true;
		iServerSaveData.GetInstance().IsBackgroundBack = true;
		iServerSaveData.GetInstance().IsBackgroundRelogin = false;
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData != null)
		{
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter != null)
			{
				dataCenter.AddIAPTransactionInfo(sIAPKey, sIdentifier, sReceipt, sSignature, string.Empty, 0, 0, 0);
				iGameApp.GetInstance().SaveData();
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneIAP(TUIEvent.SceneIAPEventType.TUIEvent_IAPResult, true));
				iServerIAPVerify.GetInstance().VerifyIAP(sIAPKey, sIdentifier, sReceipt, sSignature, OnIAPVerifySuccess, OnIAPVerifyFailed, OnIAPVerifyNetError, iGameApp.GetInstance().OnPurchaseIAP, OnIAPWriteIdentifier, OnIAPDeleteIdentifier);
			}
		}
	}

	protected void OnPurchaseIAPFailed()
	{
		iServerSaveData.GetInstance().IsBackgroundUpload = true;
		iServerSaveData.GetInstance().IsBackgroundBack = true;
		iServerSaveData.GetInstance().IsBackgroundRelogin = false;
		Debug.Log("OnPurchaseIAPFailed");
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneIAP(TUIEvent.SceneIAPEventType.TUIEvent_IAPResult, false, 2));
	}

	protected void OnPurchaseIAPCancel()
	{
		iServerSaveData.GetInstance().IsBackgroundUpload = true;
		iServerSaveData.GetInstance().IsBackgroundBack = true;
		iServerSaveData.GetInstance().IsBackgroundRelogin = false;
		Debug.Log("OnPurchaseIAPCancel");
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneIAP(TUIEvent.SceneIAPEventType.TUIEvent_IAPResult, false, 1));
	}

	protected void OnIAPVerifySuccess(string sKey, string sIdentifier, string sReceipt)
	{
		Debug.Log("OnIAPVerifySuccess");
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData != null)
		{
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter != null)
			{
				TUIGameInfo tUIGameInfo = new TUIGameInfo();
				tUIGameInfo.player_info = new TUIPlayerInfo();
				tUIGameInfo.player_info.gold = dataCenter.Gold;
				tUIGameInfo.player_info.crystal = dataCenter.Crystal;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneIAP(TUIEvent.SceneIAPEventType.TUIEvent_ServerResult, tUIGameInfo, true));
			}
		}
	}

	protected void OnIAPVerifyFailed(string sKey, string sIdentifier, string sReceipt)
	{
		Debug.Log("OnIAPVerifyFailed");
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneIAP(TUIEvent.SceneIAPEventType.TUIEvent_ServerResult, false, 3));
	}

	protected void OnIAPVerifyNetError(string sKey)
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneIAP(TUIEvent.SceneIAPEventType.TUIEvent_ServerResult, false, 3));
	}

	protected void OnIAPWriteIdentifier(string sKey, string sIdentifier, string sReceipt, string sSignature, string sRandom, int nRat, int nRatA, int nRatB)
	{
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData != null)
		{
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter != null)
			{
				dataCenter.AddIAPTransactionInfo(sKey, sIdentifier, sReceipt, sSignature, sRandom, nRat, nRatA, nRatB);
				iGameApp.GetInstance().SaveData();
			}
		}
	}

	protected void OnIAPDeleteIdentifier(string sKey, string sIdentifier, string sReceipt)
	{
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData != null)
		{
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter != null)
			{
				dataCenter.DelIAPTransactionInfo(sKey, sIdentifier, sReceipt);
				iGameApp.GetInstance().SaveData();
			}
		}
	}

	public void ShowDialogInMain(string message, OnDialogEvent onok, OnDialogEvent oncancel)
	{
		m_OnDialogOK = onok;
		m_OnDialogCancel = oncancel;
		Debug.Log("ShowDialogInMain " + message);
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ShowPopupServer, message));
	}

	protected void OnLoginSuccess()
	{
		iServerVerify.CServerConfigInfo serverConfigInfo = iServerVerify.GetInstance().GetServerConfigInfo();
		if (serverConfigInfo != null && serverConfigInfo.m_sServerMessage.Length > 0 && iServerSaveData.GetInstance().IsBackgroundRelogin)
		{
			if (!iGameApp.GetInstance().UpgradeVersion("3.1.7a"))
			{
				OnLoginFailed(iLoginManager.kFailedType.Timeout);
				return;
			}
			CMessageBoxScript.GetInstance().MessageBox(serverConfigInfo.m_sServerTitle, serverConfigInfo.m_sServerMessage, null, null, "OK");
			iTrinitiDataCollect.GetInstance().SetUserSymbol(iServerSaveData.GetInstance().CurDeviceId);
			iTrinitiDataCollect.GetInstance().SetUserName(m_DataCenter.NickName);
			if (iServerSaveData.GetInstance().m_bFirstRegister)
			{
				CTrinitiCollectManager.GetInstance().SendRegister();
			}
			CTrinitiCollectManager.GetInstance().SendLogin();
		}
		iGameState gameState = iGameApp.GetInstance().m_GameState;
		if (gameState != null)
		{
			gameState.m_bNeedAutoSaleUI = true;
		}
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ConnectResult, true));
	}

	protected void OnLoginFailed(iLoginManager.kFailedType type)
	{
		Debug.Log("OnLoginFailed " + type);
		switch (type)
		{
		case iLoginManager.kFailedType.VersionError:
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ConnectResult, false, 2));
			break;
		case iLoginManager.kFailedType.ServerMaintain:
		{
			string str = string.Empty;
			iServerVerify.CServerConfigInfo serverConfigInfo = iServerVerify.GetInstance().GetServerConfigInfo();
			if (serverConfigInfo != null)
			{
				str = serverConfigInfo.GetServerStateStr();
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ConnectResult, false, 5, str));
			break;
		}
		case iLoginManager.kFailedType.GameCenterChanged:
			CMessageBoxScript.GetInstance().MessageBox(string.Empty, "Game Center ID changed. Please re-login.", OnLoginFailedOnOK, null, "Reconnect");
			break;
		case iLoginManager.kFailedType.GMUsing:
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ConnectResult, false, 4));
			break;
		default:
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ConnectResult, false, 3));
			break;
		}
	}

	protected void OnLoginNetError()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ConnectResult, false, 1));
	}

	protected void OnLoginFailedOnOK()
	{
		iGameApp.GetInstance().EnterScene("Scene_Main");
	}

	protected void ClearPopWindow()
	{
		Debug.Log("ClearPopWindow");
		iGameState gameState = iGameApp.GetInstance().m_GameState;
		if (gameState != null)
		{
			for (int i = 0; i < gameState.m_arrMainScenePopWindow.Length; i++)
			{
				gameState.m_arrMainScenePopWindow[i] = false;
			}
		}
	}

	protected void SetPopWindow(int nIndex)
	{
		iGameState gameState = iGameApp.GetInstance().m_GameState;
		if (gameState != null && nIndex >= 0 && nIndex < gameState.m_arrMainScenePopWindow.Length)
		{
			Debug.Log("SetPopWindow " + nIndex);
			gameState.m_arrMainScenePopWindow[nIndex] = true;
		}
	}

	protected void ShowPopWindow()
	{
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData == null)
		{
			return;
		}
		iDataCenter dataCenter = gameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		iGameState gameState = iGameApp.GetInstance().m_GameState;
		if (gameState == null)
		{
			return;
		}
		int nIndex = -1;
		if (gameState.GetNextWindow(ref nIndex))
		{
			switch (nIndex)
			{
			case 0:
				dataCenter.isEvaluate = true;
				dataCenter.Save();
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_ShowReview));
				break;
			case 1:
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_ShowDailyLoginBonus, true));
				break;
			case 2:
				gameState.m_bNeedAutoSaleUI = false;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_ShowSale, true));
				break;
			case 3:
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_ShowUnlockItem));
				break;
			}
		}
	}

	private void TUIEvent_BackInfo_SceneCoopInputName(object sender, TUIEvent.SendEvent_SceneCoopInputName m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneCoopInputNameEventType.TUIEvent_TopBar)
		{
			iGameData gameData = iGameApp.GetInstance().m_GameData;
			if (gameData == null)
			{
				return;
			}
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter == null)
			{
				return;
			}
			CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
			if (character != null)
			{
				CCharacterInfoLevel characterInfo = gameData.GetCharacterInfo(character.nID, character.nLevel);
				if (characterInfo != null)
				{
					AndroidReturnPlugin.instance.SetBackFunc(EventBack_CoopInputName);
					TUIGameInfo tUIGameInfo = new TUIGameInfo();
					tUIGameInfo.player_info = new TUIPlayerInfo();
					tUIGameInfo.player_info.role_id = character.nID;
					tUIGameInfo.player_info.level = character.nLevel;
					tUIGameInfo.player_info.level_exp = characterInfo.nExp;
					tUIGameInfo.player_info.exp = character.nExp;
					tUIGameInfo.player_info.gold = dataCenter.Gold;
					tUIGameInfo.player_info.crystal = dataCenter.Crystal;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopInputName(m_event.GetEventName(), tUIGameInfo));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopInputNameEventType.TUIEvent_Continue)
		{
			string str = m_event.GetStr();
			if (m_DataCenter != null)
			{
				Debug.Log(str);
				m_DataCenter.NickName = str;
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopInputName(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopInputNameEventType.TUIEvent_Back)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopInputName(m_event.GetEventName(), true));
		}
	}

	private void TUIEvent_BackInfo_SceneCoopMainMenu(object sender, TUIEvent.SendEvent_SceneCoopMainMenu m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_TopBar)
		{
			iGameData gameData = iGameApp.GetInstance().m_GameData;
			if (gameData == null)
			{
				return;
			}
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter == null)
			{
				return;
			}
			CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
			if (character != null)
			{
				CCharacterInfoLevel characterInfo = gameData.GetCharacterInfo(character.nID, character.nLevel);
				if (characterInfo != null)
				{
					AndroidReturnPlugin.instance.SetBackFunc(EventBack_CoopMainMenu);
					TUIGameInfo tUIGameInfo = new TUIGameInfo();
					tUIGameInfo.player_info = new TUIPlayerInfo();
					tUIGameInfo.player_info.role_id = character.nID;
					tUIGameInfo.player_info.level = character.nLevel;
					tUIGameInfo.player_info.level_exp = characterInfo.nExp;
					tUIGameInfo.player_info.exp = character.nExp;
					tUIGameInfo.player_info.gold = dataCenter.Gold;
					tUIGameInfo.player_info.crystal = dataCenter.Crystal;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(m_event.GetEventName(), tUIGameInfo));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_EnterInfo)
		{
			OpenClikPlugin.Hide();
			GameCenterPlugin.LoadFriends();
			iGameApp.GetInstance().CheckUnLock();
			if (m_DataCenter == null || m_DataCenterNet == null)
			{
				return;
			}
			iHunterCenter hunterCenter = m_GameData.m_HunterCenter;
			if (hunterCenter == null)
			{
				return;
			}
			int nLevel = 1;
			int nExp = m_DataCenter.HunterExpTotal;
			hunterCenter.CalcHunterLvl(ref nLevel, ref nExp);
			m_DataCenter.HunterLvl = nLevel;
			m_DataCenter.HunterExp = nExp;
			m_DataCenter.CombatPower = iGameApp.GetInstance().CalcCombatPower();
			CNameCardInfo namecardinfo = m_DataCenterNet.GetNameCardInfo(iServerSaveData.GetInstance().CurDeviceId);
			if (namecardinfo == null)
			{
				namecardinfo = new CNameCardInfo();
			}
			if (!m_DataCenter.GenerateNameCard(ref namecardinfo) || namecardinfo == null)
			{
				return;
			}
			CGameNetManager.GetInstance().UploadNameCard(namecardinfo.m_sID, namecardinfo.m_sNickName, namecardinfo.m_nTitle, namecardinfo.m_NCPack);
			CGameNetManager.GetInstance().UploadRankData(namecardinfo.m_sID, m_DataCenter.NickName, m_DataCenter.CombatPower, m_DataCenter.HunterExpTotal, m_DataCenter.HunterLvl, m_DataCenter.Gold, m_DataCenter.Crystal, m_DataCenter.BeAdmire);
			m_DataCenterNet.SetNameCardInfo(namecardinfo.m_sID, namecardinfo);
			if (m_DataCenter.Gold >= 1000000 || m_DataCenter.Crystal >= 100000 || m_DataCenter.HunterLvl >= 999)
			{
				m_DataCenter.m_bInBlackName = true;
			}
			TUIGameInfo tUIGameInfo2 = new TUIGameInfo();
			tUIGameInfo2.coop_player_info = new TUICoopPlayerInfo();
			bool flag = true;
			if (namecardinfo.GetPhoto() != null)
			{
				tUIGameInfo2.coop_player_info.SetTexture(namecardinfo.GetPhoto());
				if (!namecardinfo.IsPhotoExpired())
				{
					flag = false;
				}
			}
			if (flag)
			{
				CGameNetManager.GetInstance().FetchPhoto(namecardinfo.m_sID, namecardinfo.m_sGCAccount);
			}
			string curDeviceId = iServerSaveData.GetInstance().CurDeviceId;
			string curGameCenterId = iServerSaveData.GetInstance().CurGameCenterId;
			tUIGameInfo2.coop_player_info.SetID(curDeviceId);
			tUIGameInfo2.coop_player_info.SetName(m_DataCenter.NickName);
			tUIGameInfo2.coop_player_info.SetTitleID(m_DataCenter.Title);
			tUIGameInfo2.coop_player_info.SetTitleIDList(m_DataCenter.GetTitleList());
			tUIGameInfo2.coop_player_info.SetLikes(m_DataCenter.BeAdmire);
			tUIGameInfo2.coop_player_info.SetHunterLV(m_DataCenter.HunterLvl);
			tUIGameInfo2.coop_player_info.SetHunterExp(m_DataCenter.HunterExp);
			if (hunterCenter != null)
			{
				CHunterInfo cHunterInfo = hunterCenter.Get(m_DataCenter.HunterLvl);
				if (cHunterInfo == null)
				{
					cHunterInfo = hunterCenter.Get(m_DataCenter.HunterLvl - 1);
				}
				if (cHunterInfo != null)
				{
					tUIGameInfo2.coop_player_info.SetHunterUpdateExp(cHunterInfo.m_nExp);
				}
			}
			tUIGameInfo2.coop_player_info.SetProgress(m_DataCenter.SceneProccess / 100f);
			tUIGameInfo2.coop_player_info.SetStatus(m_DataCenter.Signature);
			List<int> list = new List<int>();
			for (int i = 0; i < 3; i++)
			{
				int selectWeapon = m_DataCenter.GetSelectWeapon(i);
				if (selectWeapon > 0)
				{
					list.Add(selectWeapon);
				}
			}
			CCharSaveInfo character2 = m_DataCenter.GetCharacter(m_DataCenter.CurCharID);
			if (character2 != null)
			{
				CCharacterInfoLevel cCharacterInfoLevel = m_GameData.m_CharacterCenter.Get(character2.nID, character2.nLevel);
				if (cCharacterInfoLevel == null)
				{
					return;
				}
				TUICoopRoleInfo tUICoopRoleInfo = new TUICoopRoleInfo();
				tUICoopRoleInfo.Init(character2.nID, character2.nLevel, m_DataCenter.CombatPower, list, cCharacterInfoLevel.nModel, (m_DataCenter.AvatarHead <= 0) ? cCharacterInfoLevel.nAvatarHead : m_DataCenter.AvatarHead, (m_DataCenter.AvatarUpper <= 0) ? cCharacterInfoLevel.nAvatarUpper : m_DataCenter.AvatarUpper, (m_DataCenter.AvatarLower <= 0) ? cCharacterInfoLevel.nAvatarLower : m_DataCenter.AvatarLower, (m_DataCenter.AvatarHeadup <= 0) ? (-1) : m_DataCenter.AvatarHeadup, (m_DataCenter.AvatarNeck <= 0) ? (-1) : m_DataCenter.AvatarNeck, (m_DataCenter.AvatarWrist <= 0) ? (-1) : m_DataCenter.AvatarWrist);
				tUIGameInfo2.coop_player_info.SetRoleInfo(tUICoopRoleInfo);
			}
			tUIGameInfo2.coop_title_list_info = new TUICoopTitleListInfo();
			iTitleCenter titleCenter = m_GameData.m_TitleCenter;
			if (titleCenter != null)
			{
				Dictionary<int, CTitleInfo> data = titleCenter.GetData();
				if (data != null)
				{
					foreach (CTitleInfo value in data.Values)
					{
						tUIGameInfo2.coop_title_list_info.AddTitle(value.nID, value.sName);
					}
				}
			}
			List<iDataCenter.CUnlockSign> unlockSignList = m_DataCenter.GetUnlockSignList();
			if (unlockSignList != null && unlockSignList.Count > 0)
			{
				tUIGameInfo2.villiage_enter_info = new TUIVilliageEnterInfo();
				foreach (iDataCenter.CUnlockSign item in unlockSignList)
				{
					switch (item.m_nType)
					{
					case 3:
						tUIGameInfo2.villiage_enter_info.AddUnlockItem(new TUIUnlockInfo(UnlockType.Weapon, item.m_nID, string.Empty));
						break;
					case 4:
					{
						CAvatarInfo cAvatarInfo = m_GameData.m_AvatarCenter.Get(item.m_nID);
						if (cAvatarInfo != null && cAvatarInfo.m_sIcon.Length > 0)
						{
							string text = TUIMappingInfo.Instance().m_sPathRootCustomTex;
							switch (cAvatarInfo.m_nType)
							{
							case 1:
							case 3:
							case 4:
							case 5:
								text = text + "/Armor/" + cAvatarInfo.m_sIcon;
								break;
							case 0:
							case 2:
							case 6:
							case 7:
								text = text + "/Accessory/" + cAvatarInfo.m_sIcon;
								break;
							}
							tUIGameInfo2.villiage_enter_info.AddUnlockItem(new TUIUnlockInfo(UnlockType.Avatar, item.m_nID, text));
						}
						break;
					}
					case 2:
						tUIGameInfo2.villiage_enter_info.AddUnlockItem(new TUIUnlockInfo(UnlockType.Skill, item.m_nID, string.Empty));
						break;
					case 1:
						tUIGameInfo2.villiage_enter_info.AddUnlockItem(new TUIUnlockInfo(UnlockType.Role, item.m_nID, string.Empty));
						break;
					case 5:
						tUIGameInfo2.villiage_enter_info.AddUnlockItem(new TUIUnlockInfo(UnlockType.Title, item.m_nID, string.Empty));
						break;
					}
				}
				unlockSignList.Clear();
				iGameApp.GetInstance().SaveData();
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(m_event.GetEventName(), tUIGameInfo2));
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_ShowUnlockItem));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Equip)
		{
			if (m_GameState != null)
			{
				m_GameState.m_lstScene4Equip = TUISceneType.Scene_CoopMainMenu;
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AllRanking)
		{
			if (m_DataCenterNet == null)
			{
				return;
			}
			m_nScrollIndexRank = 0;
			TUIGameInfo tUIGameInfo3 = new TUIGameInfo();
			tUIGameInfo3.coop_ranking_info = new TUICoopRankingInfo();
			CRankInfo[] arrRankInfo = m_DataCenterNet.arrRankInfo;
			if (arrRankInfo != null)
			{
				TUICoopPlayerInfo tUICoopPlayerInfo = null;
				int num = 0;
				int num2 = m_nScrollIndexRank;
				while (num2 < arrRankInfo.Length && arrRankInfo[num2] != null && num != m_nScrollCountPerPageRank)
				{
					m_nScrollIndexRank = num2 + 1;
					tUICoopPlayerInfo = new TUICoopPlayerInfo();
					tUICoopPlayerInfo.SetID(arrRankInfo[num2].m_sID);
					tUICoopPlayerInfo.SetName(arrRankInfo[num2].m_sNickName);
					tUICoopPlayerInfo.SetHunterLV(arrRankInfo[num2].m_nHunterLevel);
					tUICoopPlayerInfo.SetAllRanking(num2 + 1);
					tUICoopPlayerInfo.SetRoleInfo(new TUICoopRoleInfo(0, 0, arrRankInfo[num2].m_nCombatPower, null));
					tUICoopPlayerInfo.SetLikes(arrRankInfo[num2].m_nBeAdmired);
					tUIGameInfo3.coop_ranking_info.AddRanking(tUICoopPlayerInfo);
					num2++;
					num++;
				}
			}
			TUICoopPlayerInfo tUICoopPlayerInfo2 = new TUICoopPlayerInfo();
			tUICoopPlayerInfo2.SetID(iServerSaveData.GetInstance().CurDeviceId);
			tUICoopPlayerInfo2.SetName(m_DataCenter.NickName);
			tUICoopPlayerInfo2.SetHunterLV(m_DataCenter.HunterLvl);
			tUICoopPlayerInfo2.SetTitleID(m_DataCenter.Title);
			tUICoopPlayerInfo2.SetAllRanking(m_DataCenter.Rank);
			tUICoopPlayerInfo2.SetRoleInfo(new TUICoopRoleInfo(0, 0, m_DataCenter.CombatPower, null));
			tUICoopPlayerInfo2.SetLikes(m_DataCenter.BeAdmire);
			tUIGameInfo3.coop_ranking_info.SetMyself(tUICoopPlayerInfo2);
			if (m_DataCenterNet.IsRankInfoExpired())
			{
				CGameNetManager.GetInstance().FetchRank(iServerSaveData.GetInstance().CurDeviceId, m_DataCenter.HunterExpTotal, OnFetchRank_S, null);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(m_event.GetEventName(), tUIGameInfo3));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AddAllRanking)
		{
			Debug.Log("go to this");
			TUIGameInfo tUIGameInfo4 = new TUIGameInfo();
			tUIGameInfo4.coop_ranking_info = new TUICoopRankingInfo();
			CRankInfo[] arrRankInfo2 = m_DataCenterNet.arrRankInfo;
			if (arrRankInfo2 != null)
			{
				TUICoopPlayerInfo tUICoopPlayerInfo3 = null;
				int num3 = 0;
				int num4 = m_nScrollIndexRank;
				while (num4 < arrRankInfo2.Length && arrRankInfo2[num4] != null && num3 != m_nScrollCountPerPageRank)
				{
					m_nScrollIndexRank = num4 + 1;
					tUICoopPlayerInfo3 = new TUICoopPlayerInfo();
					tUICoopPlayerInfo3.SetID(arrRankInfo2[num4].m_sID);
					tUICoopPlayerInfo3.SetName(arrRankInfo2[num4].m_sNickName);
					tUICoopPlayerInfo3.SetHunterLV(arrRankInfo2[num4].m_nHunterLevel);
					tUICoopPlayerInfo3.SetAllRanking(num4 + 1);
					tUICoopPlayerInfo3.SetRoleInfo(new TUICoopRoleInfo(0, 0, arrRankInfo2[num4].m_nCombatPower, null));
					tUICoopPlayerInfo3.SetLikes(arrRankInfo2[num4].m_nBeAdmired);
					tUIGameInfo4.coop_ranking_info.AddRanking(tUICoopPlayerInfo3);
					num4++;
					num3++;
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(m_event.GetEventName(), tUIGameInfo4));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_FriendsRanking)
		{
			if (m_DataCenter != null)
			{
				List<string> list2 = new List<string>(m_DataCenter.GetFriends());
				list2.Add(iServerSaveData.GetInstance().CurDeviceId);
				CGameNetManager.GetInstance().FetchRankByUsers(list2.ToArray(), OnFetchRankByUsers_S, null);
			}
		}
		else
		{
			if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AddFriendsRanking)
			{
				return;
			}
			if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Friends)
			{
				if (m_DataCenter == null || m_DataCenterNet == null)
				{
					return;
				}
				TUIGameInfo tUIGameInfo5 = new TUIGameInfo();
				tUIGameInfo5.coop_friends_info = new TUICoopFriendsInfo();
				List<string> friends = m_DataCenter.GetFriends();
				foreach (string item2 in friends)
				{
					if (item2 == iServerSaveData.GetInstance().CurDeviceId)
					{
						continue;
					}
					CNameCardInfo nameCardInfo = m_DataCenterNet.GetNameCardInfo(item2);
					if (nameCardInfo == null)
					{
						continue;
					}
					TUICoopPlayerInfo tUICoopPlayerInfo4 = new TUICoopPlayerInfo();
					tUICoopPlayerInfo4.SetID(item2);
					tUICoopPlayerInfo4.SetName(nameCardInfo.m_sNickName);
					tUICoopPlayerInfo4.SetStatus(nameCardInfo.m_sSignature);
					tUICoopPlayerInfo4.SetTexture(nameCardInfo.GetPhoto());
					tUICoopPlayerInfo4.SetLikes(nameCardInfo.m_nBeAdmired);
					tUICoopPlayerInfo4.SetTitleID(nameCardInfo.m_nTitle);
					tUICoopPlayerInfo4.SetAllRanking(nameCardInfo.m_nRank);
					tUICoopPlayerInfo4.SetProgress(nameCardInfo.m_fSceneProccess);
					tUICoopPlayerInfo4.SetHunterLV(nameCardInfo.m_nHunterLvl);
					tUICoopPlayerInfo4.SetHunterExp(nameCardInfo.m_nHunterExp);
					iHunterCenter hunterCenter2 = m_GameData.m_HunterCenter;
					if (hunterCenter2 != null)
					{
						CHunterInfo cHunterInfo2 = hunterCenter2.Get(nameCardInfo.m_nHunterLvl);
						if (cHunterInfo2 == null)
						{
							cHunterInfo2 = hunterCenter2.Get(nameCardInfo.m_nHunterLvl - 1);
						}
						if (cHunterInfo2 != null)
						{
							tUICoopPlayerInfo4.SetHunterUpdateExp(cHunterInfo2.m_nExp);
						}
					}
					CCharacterInfoLevel cCharacterInfoLevel2 = m_GameData.m_CharacterCenter.Get(nameCardInfo.m_nRoleID, 1);
					if (cCharacterInfoLevel2 != null)
					{
						TUICoopRoleInfo tUICoopRoleInfo2 = new TUICoopRoleInfo();
						tUICoopRoleInfo2.Init(nameCardInfo.m_nRoleID, nameCardInfo.m_nHunterLvl, nameCardInfo.m_nCombatPower, nameCardInfo.m_ltWeapon, cCharacterInfoLevel2.nModel, (nameCardInfo.m_NCPack.head <= 0) ? cCharacterInfoLevel2.nAvatarHead : nameCardInfo.m_NCPack.head, (nameCardInfo.m_NCPack.upper <= 0) ? cCharacterInfoLevel2.nAvatarUpper : nameCardInfo.m_NCPack.upper, (nameCardInfo.m_NCPack.lower <= 0) ? cCharacterInfoLevel2.nAvatarLower : nameCardInfo.m_NCPack.lower, (nameCardInfo.m_NCPack.headup <= 0) ? (-1) : nameCardInfo.m_NCPack.headup, (nameCardInfo.m_NCPack.neck <= 0) ? (-1) : nameCardInfo.m_NCPack.neck, (nameCardInfo.m_NCPack.bracelet <= 0) ? (-1) : nameCardInfo.m_NCPack.bracelet);
						tUICoopPlayerInfo4.SetRoleInfo(tUICoopRoleInfo2);
					}
					tUIGameInfo5.coop_friends_info.Add(item2, tUICoopPlayerInfo4);
				}
				bool success = true;
				if (friends.Count > 0)
				{
					CGameNetManager.GetInstance().FetchNameCards(friends.ToArray(), OnFetchNameCards_S, null);
					success = false;
				}
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(m_event.GetEventName(), tUIGameInfo5, success));
			}
			else
			{
				if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AddFriends)
				{
					return;
				}
				if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_InfoCard)
				{
					if (m_DataCenterNet == null)
					{
						return;
					}
					string str = m_event.GetStr();
					Debug.Log(str);
					TUIGameInfo tUIGameInfo6 = new TUIGameInfo();
					tUIGameInfo6.coop_player_info = new TUICoopPlayerInfo();
					CNameCardInfo nameCardInfo2 = m_DataCenterNet.GetNameCardInfo(str);
					Debug.Log(nameCardInfo2);
					bool flag2 = true;
					if (nameCardInfo2 != null)
					{
						tUIGameInfo6.coop_player_info.SetID(nameCardInfo2.m_sID);
						tUIGameInfo6.coop_player_info.SetName(nameCardInfo2.m_sNickName);
						tUIGameInfo6.coop_player_info.SetStatus(nameCardInfo2.m_sSignature);
						tUIGameInfo6.coop_player_info.SetAllRanking(nameCardInfo2.m_nRank);
						tUIGameInfo6.coop_player_info.SetHunterLV(nameCardInfo2.m_nHunterLvl);
						tUIGameInfo6.coop_player_info.SetHunterExp(nameCardInfo2.m_nHunterExp);
						tUIGameInfo6.coop_player_info.SetTitleID(nameCardInfo2.m_nTitle);
						iHunterCenter hunterCenter3 = m_GameData.m_HunterCenter;
						if (hunterCenter3 != null)
						{
							CHunterInfo cHunterInfo3 = hunterCenter3.Get(nameCardInfo2.m_nHunterLvl);
							if (cHunterInfo3 == null)
							{
								cHunterInfo3 = hunterCenter3.Get(nameCardInfo2.m_nHunterLvl - 1);
							}
							if (cHunterInfo3 != null)
							{
								tUIGameInfo6.coop_player_info.SetHunterUpdateExp(cHunterInfo3.m_nExp);
							}
						}
						tUIGameInfo6.coop_player_info.SetLikes(nameCardInfo2.m_nBeAdmired);
						tUIGameInfo6.coop_player_info.SetProgress(nameCardInfo2.m_fSceneProccess / 100f);
						CCharacterInfoLevel cCharacterInfoLevel3 = m_GameData.m_CharacterCenter.Get(nameCardInfo2.m_nRoleID, 1);
						Debug.Log(nameCardInfo2.m_nRoleID + " " + cCharacterInfoLevel3);
						if (cCharacterInfoLevel3 != null)
						{
							TUICoopRoleInfo tUICoopRoleInfo3 = new TUICoopRoleInfo();
							tUICoopRoleInfo3.Init(nameCardInfo2.m_nRoleID, nameCardInfo2.m_nHunterLvl, nameCardInfo2.m_nCombatPower, nameCardInfo2.m_ltWeapon, cCharacterInfoLevel3.nModel, (nameCardInfo2.m_NCPack.head <= 0) ? cCharacterInfoLevel3.nAvatarHead : nameCardInfo2.m_NCPack.head, (nameCardInfo2.m_NCPack.upper <= 0) ? cCharacterInfoLevel3.nAvatarUpper : nameCardInfo2.m_NCPack.upper, (nameCardInfo2.m_NCPack.lower <= 0) ? cCharacterInfoLevel3.nAvatarLower : nameCardInfo2.m_NCPack.lower, (nameCardInfo2.m_NCPack.headup <= 0) ? (-1) : nameCardInfo2.m_NCPack.headup, (nameCardInfo2.m_NCPack.neck <= 0) ? (-1) : nameCardInfo2.m_NCPack.neck, (nameCardInfo2.m_NCPack.bracelet <= 0) ? (-1) : nameCardInfo2.m_NCPack.bracelet);
							tUIGameInfo6.coop_player_info.SetRoleInfo(tUICoopRoleInfo3);
						}
						if (nameCardInfo2.GetPhoto() != null)
						{
							tUIGameInfo6.coop_player_info.SetTexture(nameCardInfo2.GetPhoto());
						}
						if (!nameCardInfo2.IsNameCardExpired())
						{
							flag2 = false;
						}
					}
					if (flag2)
					{
						CGameNetManager.GetInstance().FetchNameCard(str, OnFetchNameCard_S, null);
					}
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_InfoCard, tUIGameInfo6));
				}
				else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_TitleChange)
				{
					int wParam = m_event.GetWParam();
					Debug.Log("change title to " + wParam);
					if (m_DataCenter != null && m_DataCenterNet != null)
					{
						string curDeviceId2 = iServerSaveData.GetInstance().CurDeviceId;
						m_DataCenter.Title = wParam;
						CNameCardInfo nameCardInfo3 = m_DataCenterNet.GetNameCardInfo(curDeviceId2);
						if (nameCardInfo3 != null)
						{
							nameCardInfo3.m_nTitle = wParam;
							CGameNetManager.GetInstance().UploadNameCard(nameCardInfo3.m_sID, nameCardInfo3.m_sNickName, nameCardInfo3.m_nTitle, nameCardInfo3.m_NCPack);
						}
					}
				}
				else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Start)
				{
					if (m_DataCenter != null)
					{
						CGameNetManager.GetInstance().UserName = m_DataCenter.NickName;
					}
					if (m_DataCenterNet != null)
					{
						m_DataCenterNet.Save();
					}
					Debug.Log(" start " + CGameNetManager.GetInstance().UserName);
					CGameNetManager.GetInstance().connected = true;
					if (!CGameNetManager.GetInstance().IsConnected())
					{
						iServerVerify.CServerConfigInfo serverConfigInfo = iServerVerify.GetInstance().GetServerConfigInfo();
						if (serverConfigInfo != null)
						{
							iServerVerify.CServerConfigInfo.CGameServerInfo gameServerInfoRandom = serverConfigInfo.GetGameServerInfoRandom();
							if (gameServerInfoRandom != null)
							{
								CGameNetManager.GetInstance().Connect(gameServerInfoRandom.sUrl, gameServerInfoRandom.nPort, gameServerInfoRandom.nGroup);
							}
						}
					}
					else if (!CGameNetManager.GetInstance().IsLogin())
					{
						CGameNetManager.GetInstance().Login();
					}
					else
					{
						CGameNetManager.GetInstance().SearchRoom();
					}
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_ShowLoading, true, "Connecting..."));
					iUpdateHandleManager.GetInstance().AddEvent(OnEvent_HideLoadingUI, null, iMacroDefine.LoginGameServerTimeout);
				}
				else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Back)
				{
					if (m_DataCenterNet != null)
					{
						m_DataCenterNet.Save();
					}
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(m_event.GetEventName(), true));
				}
				else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_StatusChange)
				{
					string str2 = m_event.GetStr();
					Debug.Log(str2);
					if (m_DataCenter != null)
					{
						m_DataCenter.Signature = str2;
					}
					if (m_DataCenterNet != null)
					{
						CNameCardInfo nameCardInfo4 = m_DataCenterNet.GetNameCardInfo(iServerSaveData.GetInstance().CurDeviceId);
						if (nameCardInfo4 != null)
						{
							nameCardInfo4.m_sSignature = str2;
							CGameNetManager.GetInstance().UploadNameCard(nameCardInfo4.m_sID, nameCardInfo4.m_sNickName, nameCardInfo4.m_nTitle, nameCardInfo4.m_NCPack);
						}
					}
				}
				else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_EnterIAP)
				{
					iGameState gameState = iGameApp.GetInstance().m_GameState;
					if (gameState != null)
					{
						gameState.m_lstScene4IAP = TUISceneType.Scene_CoopMainMenu;
						global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(m_event.GetEventName(), true));
					}
				}
				else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_EnterGold)
				{
					iGameState gameState2 = iGameApp.GetInstance().m_GameState;
					if (gameState2 != null)
					{
						gameState2.m_lstScene4IAP = TUISceneType.Scene_CoopMainMenu;
						global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(m_event.GetEventName(), true));
					}
				}
			}
		}
	}

	private void TUIEvent_BackInfo_SceneCoopRoom(object sender, TUIEvent.SendEvent_SceneCoopRoom m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneCoopRoomEventType.TUIEvent_TopBar)
		{
			iGameData gameData = iGameApp.GetInstance().m_GameData;
			if (gameData == null)
			{
				return;
			}
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter == null)
			{
				return;
			}
			CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
			if (character != null)
			{
				CCharacterInfoLevel characterInfo = gameData.GetCharacterInfo(character.nID, character.nLevel);
				if (characterInfo != null)
				{
					AndroidReturnPlugin.instance.SetBackFunc(EventBack_CoopRoom);
					TUIGameInfo tUIGameInfo = new TUIGameInfo();
					tUIGameInfo.player_info = new TUIPlayerInfo();
					tUIGameInfo.player_info.role_id = character.nID;
					tUIGameInfo.player_info.level = character.nLevel;
					tUIGameInfo.player_info.level_exp = characterInfo.nExp;
					tUIGameInfo.player_info.exp = character.nExp;
					tUIGameInfo.player_info.gold = dataCenter.Gold;
					tUIGameInfo.player_info.crystal = dataCenter.Crystal;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(m_event.GetEventName(), tUIGameInfo));
				}
			}
		}
		else
		{
			if (m_event.GetEventName() == TUIEvent.SceneCoopRoomEventType.TUIEvent_EnterInfo)
			{
				TUIGameInfo tUIGameInfo2 = new TUIGameInfo();
				tUIGameInfo2.coop_title_list_info = new TUICoopTitleListInfo();
				iTitleCenter titleCenter = m_GameData.m_TitleCenter;
				if (titleCenter != null)
				{
					Dictionary<int, CTitleInfo> data = titleCenter.GetData();
					if (data != null)
					{
						foreach (CTitleInfo value in data.Values)
						{
							tUIGameInfo2.coop_title_list_info.AddTitle(value.nID, value.sName);
						}
					}
				}
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(m_event.GetEventName(), tUIGameInfo2));
				CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo();
				if (netUserInfo != null)
				{
					PlayerEnterRoom(true, netUserInfo.m_sDeivceId, netUserInfo.m_sName, netUserInfo.m_nTitle, netUserInfo.m_nHunterLvl, netUserInfo.m_nCharID, netUserInfo.m_nCharLvl, netUserInfo.m_nCombatPower, netUserInfo.m_sSignature, netUserInfo.m_arrWeapon, netUserInfo.m_nAvatarHead, netUserInfo.m_nAvatarUpper, netUserInfo.m_nAvatarLower, netUserInfo.m_nAvatarHeadup, netUserInfo.m_nAvatarNeck, netUserInfo.m_nAvatarBracelet);
					if (CGameNetManager.GetInstance().IsRoomMaster())
					{
						global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_ShowBtnStart, true));
					}
					else
					{
						global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_ShowBtnStart));
					}
				}
				Dictionary<int, CGameNetManager.CNetUserInfo> netUserInfoData = CGameNetManager.GetInstance().GetNetUserInfoData();
				if (netUserInfoData == null)
				{
					return;
				}
				{
					foreach (CGameNetManager.CNetUserInfo value2 in netUserInfoData.Values)
					{
						if (!CGameNetManager.GetInstance().IsMe(value2.m_nId))
						{
							PlayerEnterRoom(false, value2.m_sDeivceId, value2.m_sName, value2.m_nTitle, value2.m_nHunterLvl, value2.m_nCharID, value2.m_nCharLvl, value2.m_nCombatPower, value2.m_sSignature, value2.m_arrWeapon, value2.m_nAvatarHead, value2.m_nAvatarUpper, value2.m_nAvatarLower, value2.m_nAvatarHeadup, value2.m_nAvatarNeck, value2.m_nAvatarBracelet);
						}
					}
					return;
				}
			}
			if (m_event.GetEventName() == TUIEvent.SceneCoopRoomEventType.TUIEvent_GameStartBtn)
			{
				int num = CGameNetManager.GetInstance().RoomateCount();
				if (num >= 3)
				{
					CGameNetManager.GetInstance().MutiplyState = CGameNetManager.kMutiplyState.Gaming;
					TNetRoom curRoom = CGameNetManager.GetInstance().GetCurRoom();
					if (curRoom == null || !curRoom.RoomMaster.IsItMe)
					{
						return;
					}
					int num2 = CGameNetManager.GetInstance().m_nCurRoomGameLevelID;
					if (num2 == 0 && m_GameData.m_HunterLevelCenter != null)
					{
						CHunterLevelInfo cHunterLevelInfo = m_GameData.m_HunterLevelCenter.Get(m_DataCenter.HunterLvl);
						if (cHunterLevelInfo != null)
						{
							num2 = cHunterLevelInfo.GetGameLevel();
						}
					}
					if (num2 > 0)
					{
						iGameApp.GetInstance().ScreenLog("play game !!! " + num2);
						CGameNetSender.GetInstance().SendMsg_GAME_ENTER(num2, m_DataCenter.HunterLvl, curRoom);
					}
				}
				else
				{
					bool success = true;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_ShowStartWarning, success));
				}
			}
			else if (m_event.GetEventName() == TUIEvent.SceneCoopRoomEventType.TUIEvent_GameStartYes)
			{
				CGameNetManager.GetInstance().MutiplyState = CGameNetManager.kMutiplyState.Gaming;
				//TNetRoom curRoom2 = CGameNetManager.GetInstance().GetCurRoom();
				//if (curRoom2 != null && curRoom2.RoomMaster.IsItMe)
				//{
					int num3 = 0;//CGameNetManager.GetInstance().m_nCurRoomGameLevelID;
				//	if (num3 == 0 && m_GameData.m_HunterLevelCenter != null)
				//	{
						CHunterLevelInfo cHunterLevelInfo2 = m_GameData.m_HunterLevelCenter.Get(m_DataCenter.HunterLvl);
						if (cHunterLevelInfo2 != null)
						{
							num3 = cHunterLevelInfo2.GetGameLevel();
						}
					//}
					//if (num3 > 0)
					//{
						iGameApp.GetInstance().ScreenLog("play game !!! " + num3);
						m_GameState.GameLevel = num3;
						m_GameState.m_nCurHunterLevelID = m_DataCenter.HunterLvl;
						iGameApp.GetInstance().EnterScene(kGameSceneEnum.Game);
						//CGameNetSender.GetInstance().SendMsg_GAME_ENTER(num3, m_DataCenter.HunterLvl, curRoom2);
					//}
				//}
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_GameStart, true));
			}
			else if (m_event.GetEventName() == TUIEvent.SceneCoopRoomEventType.TUIEvent_GameStartCancel)
			{
				bool success2 = false;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_ShowStartWarning, success2));
			}
			else if (m_event.GetEventName() == TUIEvent.SceneCoopRoomEventType.TUIEvent_Back)
			{
				TNetManager.GetInstance().LeaveRoom();
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(m_event.GetEventName(), true));
			}
			else
			{
				if (m_event.GetEventName() != TUIEvent.SceneCoopRoomEventType.TUIEvent_LastRoleSpeedOver)
				{
					return;
				}
				TNetRoom curRoom3 = CGameNetManager.GetInstance().GetCurRoom();
				if (curRoom3 == null || !curRoom3.RoomMaster.IsItMe || curRoom3.MaxUsers != curRoom3.UserCount)
				{
					return;
				}
				int num4 = CGameNetManager.GetInstance().m_nCurRoomGameLevelID;
				if (num4 == 0 && m_GameData.m_HunterLevelCenter != null)
				{
					CHunterLevelInfo cHunterLevelInfo3 = m_GameData.m_HunterLevelCenter.Get(m_DataCenter.HunterLvl);
					if (cHunterLevelInfo3 != null)
					{
						num4 = cHunterLevelInfo3.GetGameLevel();
					}
				}
				if (num4 > 0)
				{
					iGameApp.GetInstance().ScreenLog("play game !!! " + num4);
					CGameNetSender.GetInstance().SendMsg_GAME_ENTER(num4, m_DataCenter.HunterLvl, curRoom3);
				}
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_ShowBtnStart, true));
			}
		}
	}

	private void TUIEvent_BackInfo_SceneBlackMarket(object sender, TUIEvent.SendEvent_SceneBlackMarket m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_TopBar)
		{
			iGameData gameData = iGameApp.GetInstance().m_GameData;
			if (gameData == null)
			{
				return;
			}
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter == null)
			{
				return;
			}
			CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
			if (character != null)
			{
				CCharacterInfoLevel characterInfo = gameData.GetCharacterInfo(character.nID, character.nLevel);
				if (characterInfo != null)
				{
					AndroidReturnPlugin.instance.SetBackFunc(EventBack_BlackMarket);
					TUIGameInfo tUIGameInfo = new TUIGameInfo();
					tUIGameInfo.player_info = new TUIPlayerInfo();
					tUIGameInfo.player_info.role_id = character.nID;
					tUIGameInfo.player_info.level = character.nLevel;
					tUIGameInfo.player_info.level_exp = characterInfo.nExp;
					tUIGameInfo.player_info.exp = character.nExp;
					tUIGameInfo.player_info.gold = dataCenter.Gold;
					tUIGameInfo.player_info.crystal = dataCenter.Crystal;
					tUIGameInfo.player_info.avatar_model = characterInfo.nModel;
					tUIGameInfo.player_info.avatar_head = ((dataCenter.AvatarHead <= 0) ? characterInfo.nAvatarHead : dataCenter.AvatarHead);
					tUIGameInfo.player_info.avatar_body = ((dataCenter.AvatarUpper <= 0) ? characterInfo.nAvatarUpper : dataCenter.AvatarUpper);
					tUIGameInfo.player_info.avatar_leg = ((dataCenter.AvatarLower <= 0) ? characterInfo.nAvatarLower : dataCenter.AvatarLower);
					tUIGameInfo.player_info.avatar_headup = ((dataCenter.AvatarHeadup <= 0) ? (-1) : dataCenter.AvatarHeadup);
					tUIGameInfo.player_info.avatar_neck = ((dataCenter.AvatarNeck <= 0) ? (-1) : dataCenter.AvatarNeck);
					tUIGameInfo.player_info.avatar_bracelet = ((dataCenter.AvatarWrist <= 0) ? (-1) : dataCenter.AvatarWrist);
					tUIGameInfo.player_info.default_weapon = dataCenter.GetSelectWeapon(0);
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneBlackMarket(m_event.GetEventName(), tUIGameInfo));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_GoodsInfo)
		{
			if (m_GameData == null)
			{
				return;
			}
			Dictionary<int, CBlackItem> data = m_GameData.m_BlackMarketCenter.GetData();
			if (data == null)
			{
				return;
			}
			Debug.Log(data.Count);
			TUIGameInfo tUIGameInfo2 = new TUIGameInfo();
			tUIGameInfo2.blackmarket_info = new TUIBlackMarketInfo();
			foreach (KeyValuePair<int, CBlackItem> item in data)
			{
				CBlackItem value = item.Value;
				if (value.m_curBlackItemInfo == null)
				{
					continue;
				}
				int nItemID = value.m_curBlackItemInfo.m_nItemID;
				TUIBlackMarketItem tUIBlackMarketItem = new TUIBlackMarketItem();
				if (tUIBlackMarketItem == null)
				{
					continue;
				}
				tUIBlackMarketItem.m_nBlackMarketID = item.Key;
				tUIBlackMarketItem.m_nItemID = nItemID;
				switch (value.m_curBlackItemInfo.m_nItemType)
				{
				case 1:
				{
					CWeaponInfo cWeaponInfo = m_GameData.m_WeaponCenter.Get(nItemID);
					if (cWeaponInfo == null)
					{
						continue;
					}
					CWeaponInfoLevel cWeaponInfoLevel = cWeaponInfo.Get(1);
					if (cWeaponInfoLevel == null)
					{
						continue;
					}
					CWeaponInfoLevel cWeaponInfoLevel2 = cWeaponInfo.Get(cWeaponInfo.GetLvlCount());
					if (cWeaponInfoLevel2 == null)
					{
						continue;
					}
					switch (cWeaponInfoLevel.nType)
					{
					case 1:
						tUIBlackMarketItem.m_WeaponType = WeaponType.CloseWeapon;
						break;
					case 2:
						tUIBlackMarketItem.m_WeaponType = WeaponType.MachineGun;
						break;
					case 0:
						tUIBlackMarketItem.m_WeaponType = WeaponType.Crossbow;
						break;
					case 3:
						tUIBlackMarketItem.m_WeaponType = WeaponType.ViolenceGun;
						break;
					case 4:
						tUIBlackMarketItem.m_WeaponType = WeaponType.LiquidFireGun;
						break;
					case 5:
						tUIBlackMarketItem.m_WeaponType = WeaponType.RPG;
						break;
					}
					tUIBlackMarketItem.m_sName = cWeaponInfoLevel.sName;
					tUIBlackMarketItem.m_sIcon = cWeaponInfoLevel.sIcon;
					tUIBlackMarketItem.m_sDesc = cWeaponInfoLevel.sDesc;
					tUIBlackMarketItem.m_fDamage = cWeaponInfoLevel.fDamage;
					tUIBlackMarketItem.m_fDamageMax = cWeaponInfoLevel2.fDamage;
					tUIBlackMarketItem.m_nCapcity = cWeaponInfoLevel.nCapacity;
					tUIBlackMarketItem.m_nBlastRadius = ((cWeaponInfoLevel.nType == 5) ? 20 : 0);
					for (int k = 0; k < 3; k++)
					{
						if (cWeaponInfoLevel.arrFunc[k] == 1)
						{
							if (MyUtils.Low32(cWeaponInfoLevel.arrValueX[k]) == 54)
							{
								tUIBlackMarketItem.m_nKnockBack = MyUtils.Low32(cWeaponInfoLevel.arrValueY[k]);
							}
							break;
						}
					}
					int nLevel = -1;
					if (!m_DataCenter.GetWeaponLevel(nItemID, ref nLevel) || nLevel < 1)
					{
						tUIBlackMarketItem.m_bAlreadyGain = false;
					}
					else
					{
						tUIBlackMarketItem.m_bAlreadyGain = true;
					}
					break;
				}
				case 3:
				{
					CAvatarInfo cAvatarInfo = m_GameData.m_AvatarCenter.Get(nItemID);
					if (cAvatarInfo == null)
					{
						continue;
					}
					CAvatarInfoLevel cAvatarInfoLevel = cAvatarInfo.Get(1);
					if (cAvatarInfoLevel == null)
					{
						continue;
					}
					CAvatarInfoLevel cAvatarInfoLevel2 = cAvatarInfo.Get(cAvatarInfo.GetCount());
					if (cAvatarInfoLevel2 == null)
					{
						continue;
					}
					switch (cAvatarInfo.m_nType)
					{
					case 1:
						tUIBlackMarketItem.m_WeaponType = WeaponType.Armor_Head;
						break;
					case 3:
						tUIBlackMarketItem.m_WeaponType = WeaponType.Armor_Body;
						break;
					case 5:
						tUIBlackMarketItem.m_WeaponType = WeaponType.Armor_Leg;
						break;
					case 4:
						tUIBlackMarketItem.m_WeaponType = WeaponType.Armor_Bracelet;
						break;
					case 0:
						tUIBlackMarketItem.m_WeaponType = WeaponType.Accessory_Halo;
						break;
					case 2:
						tUIBlackMarketItem.m_WeaponType = WeaponType.Accessory_Necklace;
						break;
					case 6:
						tUIBlackMarketItem.m_WeaponType = WeaponType.Accessory_Badge;
						break;
					case 7:
						tUIBlackMarketItem.m_WeaponType = WeaponType.Accessory_Stoneskin;
						break;
					}
					tUIBlackMarketItem.m_sName = cAvatarInfo.m_sName;
					tUIBlackMarketItem.m_sIcon = cAvatarInfo.m_sIcon;
					tUIBlackMarketItem.m_sDesc = cAvatarInfoLevel.sDesc;
					for (int i = 0; i < 3; i++)
					{
						if (cAvatarInfoLevel.arrFunc[i] == 1)
						{
							if (MyUtils.Low32(cAvatarInfoLevel.arrValueX[i]) == 5)
							{
								tUIBlackMarketItem.m_nDefence = MyUtils.Low32(cAvatarInfoLevel.arrValueY[i]);
							}
							break;
						}
					}
					for (int j = 0; j < 3; j++)
					{
						if (cAvatarInfoLevel2.arrFunc[j] == 1)
						{
							if (MyUtils.Low32(cAvatarInfoLevel2.arrValueX[j]) == 5)
							{
								tUIBlackMarketItem.m_nDefenceMax = MyUtils.Low32(cAvatarInfoLevel2.arrValueY[j]);
							}
							break;
						}
					}
					int avatarlevel = -1;
					if (!m_DataCenter.GetAvatar(nItemID, ref avatarlevel) || avatarlevel < 1)
					{
						tUIBlackMarketItem.m_bAlreadyGain = false;
					}
					else
					{
						tUIBlackMarketItem.m_bAlreadyGain = true;
					}
					break;
				}
				default:
					continue;
				}
				tUIBlackMarketItem.m_Price = new TUIPriceInfo(value.m_curBlackItemInfo.m_nPrice, value.m_curBlackItemInfo.m_bCrystal ? UnitType.Crystal : UnitType.Gold);
				tUIBlackMarketItem.m_fLeftTime = value.m_fTime - value.m_fTimeCount;
				if (tUIBlackMarketItem.m_fLeftTime < 0f)
				{
					tUIBlackMarketItem.m_fLeftTime = 0f;
				}
				tUIGameInfo2.blackmarket_info.Add(tUIBlackMarketItem);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneBlackMarket(m_event.GetEventName(), tUIGameInfo2));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_ClickBtnBuy)
		{
			int wParam = m_event.GetWParam();
			Debug.Log("buy black item " + wParam);
			CBlackItem cBlackItem = m_GameData.m_BlackMarketCenter.Get(wParam);
			if (cBlackItem == null || cBlackItem.m_curBlackItemInfo == null)
			{
				return;
			}
			int nItemID2 = cBlackItem.m_curBlackItemInfo.m_nItemID;
			if (cBlackItem.m_curBlackItemInfo.m_bCrystal)
			{
				if (m_DataCenter.Crystal < cBlackItem.m_curBlackItemInfo.m_nPrice)
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneBlackMarket(m_event.GetEventName(), false, BackEventFalseType.NoCrystalEnough, cBlackItem.m_curBlackItemInfo.m_nPrice - m_DataCenter.Crystal));
					return;
				}
				m_DataCenter.AddCrystal(-Mathf.Abs(cBlackItem.m_curBlackItemInfo.m_nPrice));
			}
			else
			{
				if (m_DataCenter.Gold < cBlackItem.m_curBlackItemInfo.m_nPrice)
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneBlackMarket(m_event.GetEventName(), false, BackEventFalseType.NoGoldEnough, cBlackItem.m_curBlackItemInfo.m_nPrice - m_DataCenter.Gold));
					return;
				}
				m_DataCenter.AddGold(-Mathf.Abs(cBlackItem.m_curBlackItemInfo.m_nPrice));
			}
			switch (cBlackItem.m_curBlackItemInfo.m_nItemType)
			{
			default:
				return;
			case 1:
				m_DataCenter.SetWeaponLevel(nItemID2, 1);
				m_DataCenter.SetWeaponSign(nItemID2, 3);
				iGameApp.GetInstance().Flurry_PurchaseWeapon(nItemID2);
				CAchievementManager.GetInstance().AddAchievement(4, new object[1] { m_DataCenter.GetWeaponCount() });
				if (cBlackItem.m_curBlackItemInfo.m_bCrystal)
				{
					CFlurryManager.GetInstance().ConsumeCrystal(CFlurryManager.kConsumeType.Weapon);
					CAchievementManager.GetInstance().AddAchievement(13);
					CTrinitiCollectManager.GetInstance().SendConsumeCrystal(cBlackItem.m_curBlackItemInfo.m_nPrice, "buyweapon", nItemID2, 1);
				}
				else
				{
					CFlurryManager.GetInstance().ConsumeGold(CFlurryManager.kConsumeType.Weapon);
					CTrinitiCollectManager.GetInstance().SendConsumeGold(cBlackItem.m_curBlackItemInfo.m_nPrice, "buyweapon", nItemID2, 1);
				}
				break;
			case 3:
				m_DataCenter.SetAvatar(nItemID2, 1);
				m_DataCenter.SetAvatarSign(nItemID2, 3);
				if (cBlackItem.m_curBlackItemInfo.m_bCrystal)
				{
					CFlurryManager.GetInstance().ConsumeCrystal(CFlurryManager.kConsumeType.Avatar);
					CAchievementManager.GetInstance().AddAchievement(13);
					CTrinitiCollectManager.GetInstance().SendConsumeCrystal(cBlackItem.m_curBlackItemInfo.m_nPrice, "buyavatar", nItemID2, 1);
				}
				else
				{
					CFlurryManager.GetInstance().ConsumeGold(CFlurryManager.kConsumeType.Avatar);
					CTrinitiCollectManager.GetInstance().SendConsumeGold(cBlackItem.m_curBlackItemInfo.m_nPrice, "buyavatar", nItemID2, 1);
				}
				break;
			case 2:
				return;
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneBlackMarket(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_Back)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneBlackMarket(m_event.GetEventName(), true));
		}
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_GoldToCrystal)
		{
			iGameData gameData2 = iGameApp.GetInstance().m_GameData;
			if (gameData2 == null)
			{
				return;
			}
			iDataCenter dataCenter2 = gameData2.GetDataCenter();
			if (dataCenter2 != null)
			{
				int wParam2 = m_event.GetWParam();
				int num = MyUtils.Formula_Gold2Crystal(wParam2);
				if (dataCenter2.Crystal < num)
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneBlackMarket(m_event.GetEventName(), false, BackEventFalseType.NoCrystalEnough, num - dataCenter2.Crystal));
					return;
				}
				dataCenter2.AddCrystal(-Mathf.Abs(num));
				CAchievementManager.GetInstance().AddAchievement(13);
				CTrinitiCollectManager.GetInstance().SendConsumeCrystal(num, "exgold", -1, wParam2);
				dataCenter2.AddGold(wParam2);
				dataCenter2.Save();
				iServerSaveData.GetInstance().UploadImmidately();
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneBlackMarket(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_EnterIAP)
		{
			iGameState gameState = iGameApp.GetInstance().m_GameState;
			if (gameState != null)
			{
				gameState.m_lstScene4IAP = TUISceneType.Scene_BlackMarket;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneBlackMarket(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_EnterGold)
		{
			iGameState gameState2 = iGameApp.GetInstance().m_GameState;
			if (gameState2 != null)
			{
				gameState2.m_lstScene4IAP = TUISceneType.Scene_BlackMarket;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneBlackMarket(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_EnterIAPCrystalNoEnough)
		{
			iGameState gameState3 = iGameApp.GetInstance().m_GameState;
			if (gameState3 != null)
			{
				gameState3.m_lstScene4IAP = TUISceneType.Scene_BlackMarket;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneBlackMarket(m_event.GetEventName(), true));
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_EnterGoEquip && m_GameState != null)
		{
			m_GameState.m_nGotoEquip_PopupType = m_event.GetWParam();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneBlackMarket(m_event.GetEventName(), true));
		}
	}

	protected void OnFetchNameCards_S(string response)
	{
		TUIGameInfo tUIGameInfo = new TUIGameInfo();
		tUIGameInfo.coop_friends_info = new TUICoopFriendsInfo();
		string empty = string.Empty;
		try
		{
			JSONObject jSONObject = JSONObject.Parse(response);
			empty = jSONObject.GetString("code");
			if (!(empty == "0"))
			{
				return;
			}
			m_DataCenter.ClearFriends();
			JSONArray array = jSONObject["users"].Array;
			for (int i = 0; i < array.Length; i++)
			{
				JSONObject obj = array[i].Obj;
				if (obj["nickName"] != null)
				{
					string str = obj["userId"].Str;
					string str2 = obj["nickName"].Str;
					int nTitle = -1;
					if (obj["title"] != null)
					{
						nTitle = Convert.ToInt32(obj["title"].Str);
					}
					CNameCardInfo cNameCardInfo = m_DataCenterNet.GetNameCardInfo(str);
					if (cNameCardInfo == null)
					{
						cNameCardInfo = new CNameCardInfo();
						cNameCardInfo.m_sID = str;
						m_DataCenterNet.SetNameCardInfo(cNameCardInfo.m_sID, cNameCardInfo);
					}
					cNameCardInfo.m_sNickName = str2;
					cNameCardInfo.m_nTitle = nTitle;
					m_DataCenter.AddFriend(str);
				}
			}
			m_DataCenterNet.Save();
			if (m_DataCenter == null)
			{
				return;
			}
			List<string> friends = m_DataCenter.GetFriends();
			if (friends.Count <= 0)
			{
				return;
			}
			for (int j = 0; j < friends.Count; j++)
			{
				CNameCardInfo nameCardInfo = m_DataCenterNet.GetNameCardInfo(friends[j]);
				if (nameCardInfo != null)
				{
					Debug.Log("friend " + nameCardInfo.m_sID);
					TUICoopPlayerInfo tUICoopPlayerInfo = new TUICoopPlayerInfo();
					tUICoopPlayerInfo.SetID(nameCardInfo.m_sID);
					if (nameCardInfo.GetPhoto() != null)
					{
						tUICoopPlayerInfo.SetTexture(nameCardInfo.GetPhoto());
					}
					tUICoopPlayerInfo.SetName(nameCardInfo.m_sNickName);
					tUICoopPlayerInfo.SetTitleID(nameCardInfo.m_nTitle);
					tUIGameInfo.coop_friends_info.Add(tUICoopPlayerInfo.id, tUICoopPlayerInfo);
				}
			}
		}
		catch
		{
			Debug.Log("OnFetchNameCards_S parse error");
		}
		finally
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Friends, tUIGameInfo, true));
		}
	}

	protected void OnFetchNameCard_S(string response)
	{
		string empty = string.Empty;
		try
		{
			JSONObject jSONObject = JSONObject.Parse(response);
			empty = jSONObject.GetString("code");
			if (!(empty == "0"))
			{
				return;
			}
			string str = jSONObject["userId"].Str;
			string sNickName = string.Empty;
			if (jSONObject["nickName"] != null)
			{
				sNickName = jSONObject["nickName"].Str;
			}
			int nTitle = -1;
			if (jSONObject["title"] != null)
			{
				nTitle = Convert.ToInt32(jSONObject["title"].Str);
			}
			CNameCardInfo cNameCardInfo = m_DataCenterNet.GetNameCardInfo(str);
			if (cNameCardInfo == null)
			{
				cNameCardInfo = new CNameCardInfo();
				cNameCardInfo.m_sID = str;
				m_DataCenterNet.SetNameCardInfo(cNameCardInfo.m_sID, cNameCardInfo);
			}
			cNameCardInfo.m_sNickName = sNickName;
			cNameCardInfo.m_nTitle = nTitle;
			if (jSONObject["exts"] != null)
			{
				string str2 = jSONObject["exts"].Str;
				byte[] array = Convert.FromBase64String(str2);
				string @string = Encoding.UTF8.GetString(array);
				Debug.Log(@string);
				Debug.Log("namecard npcpack length = " + str2.Length);
				cNameCardInfo.m_NCPack = MyUtils.Deserialize<CNCPack>(array);
			}
			cNameCardInfo.ResetNameCardTime();
			if (cNameCardInfo == null)
			{
				return;
			}
			TUIGameInfo tUIGameInfo = new TUIGameInfo();
			tUIGameInfo.coop_player_info = new TUICoopPlayerInfo();
			tUIGameInfo.coop_player_info.SetID(cNameCardInfo.m_sID);
			tUIGameInfo.coop_player_info.SetName(cNameCardInfo.m_sNickName);
			tUIGameInfo.coop_player_info.SetTitleID(cNameCardInfo.m_nTitle);
			tUIGameInfo.coop_player_info.SetStatus(cNameCardInfo.m_sSignature);
			tUIGameInfo.coop_player_info.SetAllRanking(cNameCardInfo.m_nRank);
			tUIGameInfo.coop_player_info.SetHunterLV(cNameCardInfo.m_nHunterLvl);
			tUIGameInfo.coop_player_info.SetHunterExp(cNameCardInfo.m_nHunterExp);
			tUIGameInfo.coop_player_info.SetLikes(cNameCardInfo.m_nBeAdmired);
			tUIGameInfo.coop_player_info.SetProgress(cNameCardInfo.m_fSceneProccess / 100f);
			if (cNameCardInfo.GetPhoto() != null)
			{
				tUIGameInfo.coop_player_info.SetTexture(cNameCardInfo.GetPhoto());
			}
			iHunterCenter hunterCenter = m_GameData.m_HunterCenter;
			if (hunterCenter != null)
			{
				CHunterInfo cHunterInfo = hunterCenter.Get(cNameCardInfo.m_nHunterLvl);
				if (cHunterInfo == null)
				{
					cHunterInfo = hunterCenter.Get(cNameCardInfo.m_nHunterLvl - 1);
				}
				if (cHunterInfo != null)
				{
					tUIGameInfo.coop_player_info.SetHunterUpdateExp(cHunterInfo.m_nExp);
				}
			}
			CCharacterInfoLevel cCharacterInfoLevel = m_GameData.m_CharacterCenter.Get(cNameCardInfo.m_nRoleID, 1);
			Debug.Log(cNameCardInfo.m_nRoleID + " " + cCharacterInfoLevel);
			if (cCharacterInfoLevel != null)
			{
				TUICoopRoleInfo tUICoopRoleInfo = new TUICoopRoleInfo();
				tUICoopRoleInfo.Init(cNameCardInfo.m_nRoleID, cNameCardInfo.m_nHunterLvl, cNameCardInfo.m_nCombatPower, cNameCardInfo.m_ltWeapon, cCharacterInfoLevel.nModel, (cNameCardInfo.m_NCPack.head <= 0) ? cCharacterInfoLevel.nAvatarHead : cNameCardInfo.m_NCPack.head, (cNameCardInfo.m_NCPack.upper <= 0) ? cCharacterInfoLevel.nAvatarUpper : cNameCardInfo.m_NCPack.upper, (cNameCardInfo.m_NCPack.lower <= 0) ? cCharacterInfoLevel.nAvatarLower : cNameCardInfo.m_NCPack.lower, (cNameCardInfo.m_NCPack.headup <= 0) ? (-1) : cNameCardInfo.m_NCPack.headup, (cNameCardInfo.m_NCPack.neck <= 0) ? (-1) : cNameCardInfo.m_NCPack.neck, (cNameCardInfo.m_NCPack.bracelet <= 0) ? (-1) : cNameCardInfo.m_NCPack.bracelet);
				tUIGameInfo.coop_player_info.SetRoleInfo(tUICoopRoleInfo);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_InfoCard, tUIGameInfo));
		}
		catch (Exception ex)
		{
			Debug.Log("OnFetchNameCard_S parse error " + ex.StackTrace);
		}
	}

	protected void OnFetchRank_S(string response)
	{
		string empty = string.Empty;
		try
		{
			JSONObject jSONObject = JSONObject.Parse(response);
			empty = jSONObject.GetString("code");
			if (!(empty == "0"))
			{
				return;
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			if (m_DataCenterNet.arrRankInfo != null)
			{
				for (int i = 0; i < m_DataCenterNet.arrRankInfo.Length; i++)
				{
					if (m_DataCenterNet.arrRankInfo[i] != null && !dictionary.ContainsKey(m_DataCenterNet.arrRankInfo[i].m_sID))
					{
						dictionary.Add(m_DataCenterNet.arrRankInfo[i].m_sID, m_DataCenterNet.arrRankInfo[i].m_nCurrRank);
					}
				}
			}
			JSONArray array = jSONObject["leaderboards"].Array;
			CRankInfo[] array2 = new CRankInfo[array.Length];
			for (int j = 0; j < array.Length; j++)
			{
				JSONObject obj = array[j].Obj;
				array2[j] = new CRankInfo();
				array2[j].m_sID = obj["userId"].Str;
				array2[j].m_nBeAdmired = int.Parse(obj["applause"].Str);
				array2[j].m_nCurrRank = j + 1;
				if (dictionary.Count > 0)
				{
					if (dictionary.ContainsKey(array2[j].m_sID))
					{
						array2[j].m_nLastRank = dictionary[array2[j].m_sID];
					}
					else
					{
						array2[j].m_nLastRank = 0;
					}
				}
				else
				{
					array2[j].m_nLastRank = array2[j].m_nCurrRank;
				}
				string[] array3 = obj["rankName"].Str.Split('|');
				if (array3.Length == 3)
				{
					array2[j].m_nHunterLevel = Convert.ToInt32(array3[0]);
					array2[j].m_nCombatPower = Convert.ToInt32(array3[1]);
					array2[j].m_sNickName = array3[2];
				}
				Debug.Log(array2[j].m_sNickName + " rank:" + (j + 1) + " applause:" + obj["applause"].Str);
				CNameCardInfo cNameCardInfo = m_DataCenterNet.GetNameCardInfo(array2[j].m_sID);
				if (cNameCardInfo == null)
				{
					cNameCardInfo = new CNameCardInfo();
					cNameCardInfo.m_sID = array2[j].m_sID;
					m_DataCenterNet.SetNameCardInfo(cNameCardInfo.m_sID, cNameCardInfo);
				}
				cNameCardInfo.m_sNickName = array2[j].m_sNickName;
				cNameCardInfo.m_nHunterLvl = array2[j].m_nHunterLevel;
				cNameCardInfo.m_nCombatPower = array2[j].m_nCombatPower;
				cNameCardInfo.m_nRank = array2[j].m_nCurrRank;
				cNameCardInfo.m_nBeAdmired = array2[j].m_nBeAdmired;
			}
			m_DataCenterNet.arrRankInfo = array2;
			m_DataCenterNet.Save();
			m_DataCenterNet.ResetRankInfoTime();
			int rank = (int)jSONObject["myrank"].Number;
			if (m_DataCenter != null)
			{
				m_DataCenter.LastRank = m_DataCenter.Rank;
				m_DataCenter.Rank = rank;
				Debug.Log(m_DataCenter.Rank);
			}
			if (array2 == null)
			{
				return;
			}
			m_nScrollIndexRank = 0;
			TUIGameInfo tUIGameInfo = new TUIGameInfo();
			tUIGameInfo.coop_ranking_info = new TUICoopRankingInfo();
			TUICoopPlayerInfo tUICoopPlayerInfo = null;
			TUICoopPlayerInfo tUICoopPlayerInfo2 = null;
			int num = 0;
			int num2 = m_nScrollIndexRank;
			while (num2 < array2.Length)
			{
				if (array2[num2] != null)
				{
					CNameCardInfo nameCardInfo = m_DataCenterNet.GetNameCardInfo(array2[num2].m_sID);
					if (nameCardInfo != null)
					{
						if (num == m_nScrollCountPerPageRank)
						{
							break;
						}
						m_nScrollIndexRank = num2 + 1;
						tUICoopPlayerInfo = new TUICoopPlayerInfo();
						tUICoopPlayerInfo.SetID(nameCardInfo.m_sID);
						tUICoopPlayerInfo.SetName(nameCardInfo.m_sNickName);
						tUICoopPlayerInfo.SetHunterLV(nameCardInfo.m_nHunterLvl);
						tUICoopPlayerInfo.SetAllRanking(num2 + 1);
						tUICoopPlayerInfo.SetLikes(nameCardInfo.m_nBeAdmired);
						TUICoopRoleInfo tUICoopRoleInfo = new TUICoopRoleInfo();
						tUICoopPlayerInfo.SetRoleInfo(new TUICoopRoleInfo(0, 0, nameCardInfo.m_nCombatPower, null));
						tUIGameInfo.coop_ranking_info.AddRanking(tUICoopPlayerInfo);
						if (array2[num2].m_sID == iServerSaveData.GetInstance().CurDeviceId)
						{
							tUICoopPlayerInfo2 = tUICoopPlayerInfo;
						}
					}
				}
				num2++;
				num++;
			}
			if (tUICoopPlayerInfo2 == null)
			{
				tUICoopPlayerInfo2 = new TUICoopPlayerInfo();
				tUICoopPlayerInfo2.SetID(iServerSaveData.GetInstance().CurDeviceId);
				tUICoopPlayerInfo2.SetName(m_DataCenter.NickName);
				tUICoopPlayerInfo2.SetHunterLV(m_DataCenter.HunterLvl);
				tUICoopPlayerInfo2.SetRoleInfo(new TUICoopRoleInfo(0, 0, m_DataCenter.CombatPower, null));
				tUICoopPlayerInfo2.SetAllRanking(m_DataCenter.Rank);
				tUICoopPlayerInfo2.SetLikes(m_DataCenter.BeAdmire);
			}
			tUIGameInfo.coop_ranking_info.SetMyself(tUICoopPlayerInfo2);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AllRanking, tUIGameInfo));
		}
		catch
		{
			Debug.Log("OnFetchRank_S parse error");
		}
	}

	protected void OnFetchRankByUsers_S(string response)
	{
		string empty = string.Empty;
		try
		{
			JSONObject jSONObject = JSONObject.Parse(response);
			empty = jSONObject.GetString("code");
			if (!(empty == "0"))
			{
				return;
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			if (m_DataCenterNet.arrRankInfoFriends != null)
			{
				for (int i = 0; i < m_DataCenterNet.arrRankInfoFriends.Length; i++)
				{
					if (m_DataCenterNet.arrRankInfoFriends[i] != null && !dictionary.ContainsKey(m_DataCenterNet.arrRankInfoFriends[i].m_sID))
					{
						dictionary.Add(m_DataCenterNet.arrRankInfoFriends[i].m_sID, m_DataCenterNet.arrRankInfoFriends[i].m_nCurrRank);
					}
				}
			}
			List<JSONObject> list = new List<JSONObject>();
			JSONArray array = jSONObject["leaderboards"].Array;
			for (int j = 0; j < array.Length; j++)
			{
				list.Add(array[j].Obj);
			}
			list.Sort(delegate(JSONObject obj1, JSONObject obj2)
			{
				int num = 0;
				if (obj1["exp"].Type == JSONValueType.Number)
				{
					num = (int)obj1["exp"].Number;
				}
				else if (obj1["exp"].Type == JSONValueType.String)
				{
					num = int.Parse(obj1["exp"].Str);
				}
				int num2 = 0;
				if (obj2["exp"].Type == JSONValueType.Number)
				{
					num2 = (int)obj2["exp"].Number;
				}
				else if (obj2["exp"].Type == JSONValueType.String)
				{
					num2 = int.Parse(obj2["exp"].Str);
				}
				return num2 - num;
			});
			CRankInfo[] array2 = new CRankInfo[array.Length];
			for (int k = 0; k < list.Count; k++)
			{
				JSONObject jSONObject2 = list[k];
				array2[k] = new CRankInfo();
				array2[k].m_sID = jSONObject2["userId"].Str;
				array2[k].m_nBeAdmired = int.Parse(jSONObject2["applause"].Str);
				array2[k].m_nCurrRank = k + 1;
				if (dictionary.Count > 0)
				{
					if (dictionary.ContainsKey(array2[k].m_sID))
					{
						array2[k].m_nLastRank = dictionary[array2[k].m_sID];
					}
					else
					{
						array2[k].m_nLastRank = 0;
					}
				}
				else
				{
					array2[k].m_nLastRank = array2[k].m_nCurrRank;
				}
				string[] array3 = jSONObject2["rankName"].Str.Split('|');
				if (array3.Length == 3)
				{
					array2[k].m_nHunterLevel = Convert.ToInt32(array3[0]);
					array2[k].m_nCombatPower = Convert.ToInt32(array3[1]);
					array2[k].m_sNickName = array3[2];
				}
				CNameCardInfo cNameCardInfo = m_DataCenterNet.GetNameCardInfo(array2[k].m_sID);
				if (cNameCardInfo == null)
				{
					cNameCardInfo = new CNameCardInfo();
					cNameCardInfo.m_sID = array2[k].m_sID;
					m_DataCenterNet.SetNameCardInfo(cNameCardInfo.m_sID, cNameCardInfo);
				}
				cNameCardInfo.m_sNickName = array2[k].m_sNickName;
				cNameCardInfo.m_nHunterLvl = array2[k].m_nHunterLevel;
				cNameCardInfo.m_nCombatPower = array2[k].m_nCombatPower;
				cNameCardInfo.m_nRank = array2[k].m_nCurrRank;
				cNameCardInfo.m_nBeAdmired = array2[k].m_nBeAdmired;
			}
			m_DataCenterNet.arrRankInfoFriends = array2;
			m_DataCenterNet.Save();
			m_DataCenterNet.ResetRankInfoFriendsTime();
			TUIGameInfo tUIGameInfo = new TUIGameInfo();
			tUIGameInfo.coop_ranking_info = new TUICoopRankingInfo();
			TUICoopPlayerInfo tUICoopPlayerInfo = null;
			int friendsRanking = 0;
			if (array2 != null)
			{
				TUICoopPlayerInfo tUICoopPlayerInfo2 = null;
				for (int l = 0; l < array2.Length && array2[l] != null; l++)
				{
					CNameCardInfo nameCardInfo = m_DataCenterNet.GetNameCardInfo(array2[l].m_sID);
					if (nameCardInfo != null)
					{
						tUICoopPlayerInfo2 = new TUICoopPlayerInfo();
						tUICoopPlayerInfo2.SetID(nameCardInfo.m_sID);
						tUICoopPlayerInfo2.SetName(nameCardInfo.m_sNickName);
						tUICoopPlayerInfo2.SetHunterLV(nameCardInfo.m_nHunterLvl);
						tUICoopPlayerInfo2.SetFriendsRanking(l + 1);
						tUICoopPlayerInfo2.SetLikes(nameCardInfo.m_nBeAdmired);
						TUICoopRoleInfo tUICoopRoleInfo = new TUICoopRoleInfo();
						tUICoopPlayerInfo2.SetRoleInfo(new TUICoopRoleInfo(0, 0, nameCardInfo.m_nCombatPower, null));
						tUIGameInfo.coop_ranking_info.AddRanking(tUICoopPlayerInfo2);
						if (array2[l].m_sID == iServerSaveData.GetInstance().CurDeviceId)
						{
							tUICoopPlayerInfo = tUICoopPlayerInfo2;
							friendsRanking = l + 1;
						}
					}
				}
			}
			if (tUICoopPlayerInfo == null)
			{
				tUICoopPlayerInfo = new TUICoopPlayerInfo();
				tUICoopPlayerInfo.SetID(iServerSaveData.GetInstance().CurDeviceId);
				tUICoopPlayerInfo.SetName(m_DataCenter.NickName);
				tUICoopPlayerInfo.SetHunterLV(m_DataCenter.HunterLvl);
				tUICoopPlayerInfo.SetRoleInfo(new TUICoopRoleInfo(0, 0, m_DataCenter.CombatPower, null));
				tUICoopPlayerInfo.SetFriendsRanking(friendsRanking);
				tUICoopPlayerInfo.SetLikes(m_DataCenter.BeAdmire);
			}
			tUIGameInfo.coop_ranking_info.SetMyself(tUICoopPlayerInfo);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_FriendsRanking, tUIGameInfo));
		}
		catch
		{
			Debug.Log("OnFetchRank_S parse error");
		}
	}

	public void PlayerEnterRoom(bool isMyself, string deviceid, string name, int title, int hunterlvl, int charid, int charlvl, int combatpower, string sign, int[] arrWeapon, int nAvatarHead, int nAvatarUpper, int nAvatarLower, int nAvatarHeadup, int nAvatarNeck, int nAvatarBracelet)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < arrWeapon.Length; i++)
		{
			list.Add(arrWeapon[i]);
		}
		TUIGameInfo tUIGameInfo = new TUIGameInfo();
		tUIGameInfo.coop_player_info = new TUICoopPlayerInfo();
		tUIGameInfo.coop_player_info.SetID(deviceid);
		tUIGameInfo.coop_player_info.SetName(name);
		tUIGameInfo.coop_player_info.SetTitleID(title);
		Debug.Log(deviceid + " " + title);
		tUIGameInfo.coop_player_info.SetHunterLV(hunterlvl);
		tUIGameInfo.coop_player_info.SetStatus(sign);
		CCharacterInfoLevel cCharacterInfoLevel = m_GameData.m_CharacterCenter.Get(charid, charlvl);
		Debug.Log("PlayerEnterRoom roleid " + charid + " " + cCharacterInfoLevel.nModel + " " + nAvatarHead + " " + nAvatarUpper + " " + nAvatarLower);
		if (cCharacterInfoLevel != null)
		{
			TUICoopRoleInfo tUICoopRoleInfo = new TUICoopRoleInfo();
			tUICoopRoleInfo.Init(charid, charlvl, combatpower, list, cCharacterInfoLevel.nModel, (nAvatarHead <= 0) ? cCharacterInfoLevel.nAvatarHead : nAvatarHead, (nAvatarUpper <= 0) ? cCharacterInfoLevel.nAvatarUpper : nAvatarUpper, (nAvatarLower <= 0) ? cCharacterInfoLevel.nAvatarLower : nAvatarLower, (nAvatarHeadup <= 0) ? (-1) : nAvatarHeadup, (nAvatarNeck <= 0) ? (-1) : nAvatarNeck, (nAvatarBracelet <= 0) ? (-1) : nAvatarBracelet);
			tUIGameInfo.coop_player_info.SetRoleInfo(tUICoopRoleInfo);
		}
		if (isMyself)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_IamEnter, tUIGameInfo));
			return;
		}
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_PlayerEnter, tUIGameInfo));
		TNetRoom curRoom = CGameNetManager.GetInstance().GetCurRoom();
		if (curRoom != null && curRoom.RoomMaster.IsItMe && curRoom.MaxUsers == curRoom.UserCount)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_ShowBtnStart));
		}
	}

	public void PlayerLeaveRoom(string deviceid)
	{
		if (deviceid != null && deviceid.Length >= 1)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_PlayerExit, deviceid));
		}
	}

	protected bool OnEvent_HideLoadingUI(List<object> ltParam)
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_ShowLoading));
		CMessageBoxScript.GetInstance().MessageBox(string.Empty, "Connection failed!", null, null, "OK");
		return true;
	}

	protected void OnFetchUUIDList_S(List<string> ltUUID)
	{
		List<string> list = new List<string>();
		foreach (string item in ltUUID)
		{
			if (!list.Contains(item))
			{
				list.Add(item);
			}
		}
		if (list.Count >= 1)
		{
			CGameNetManager.GetInstance().FetchNameCards(list.ToArray(), OnFetchNameCards_S, null);
		}
	}

	protected void AddWeaponAttribute(TUIWeaponInfo tuiweaponinfo, List<int> ltWeaponList, WeaponType nWeaponType, kShopWeaponCategory nCategory)
	{
		foreach (int ltWeapon in ltWeaponList)
		{
			CWeaponInfo weaponInfo = m_GameData.GetWeaponInfo(ltWeapon);
			if (weaponInfo == null)
			{
				break;
			}
			CWeaponInfoLevel cWeaponInfoLevel = weaponInfo.Get(1);
			if (cWeaponInfoLevel == null)
			{
				continue;
			}
			int nLevel = -1;
			m_DataCenter.GetWeaponLevel(weaponInfo.nID, ref nLevel);
			if (m_GameData.m_ShopDisplayCenter.IsInSellInBlack(weaponInfo.nID) && nLevel <= 0)
			{
				continue;
			}
			TUIWeaponAttributeInfo tUIWeaponAttributeInfo = new TUIWeaponAttributeInfo();
			tUIWeaponAttributeInfo.m_nID = weaponInfo.nID;
			tUIWeaponAttributeInfo.m_nLevel = nLevel;
			tuiweaponinfo.AddItem(nCategory, tUIWeaponAttributeInfo);
			if (tUIWeaponAttributeInfo.m_nLevel < 1)
			{
				if ((weaponInfo.m_nUnlockStageID == 0 && weaponInfo.m_nUnlockHunterLvl == 0) || (weaponInfo.m_nUnlockStageID > 0 && m_DataCenter.IsLevelPassed(weaponInfo.m_nUnlockStageID)) || (weaponInfo.m_nUnlockHunterLvl > 0 && m_DataCenter.HunterLvl >= weaponInfo.m_nUnlockHunterLvl))
				{
					tUIWeaponAttributeInfo.m_bUnlock = true;
					tUIWeaponAttributeInfo.m_sUnlockStr = string.Empty;
				}
				else
				{
					tUIWeaponAttributeInfo.m_bUnlock = false;
					tUIWeaponAttributeInfo.m_sUnlockStr = string.Empty;
					if (weaponInfo.m_nUnlockStageID > 0)
					{
						int groupID = m_GameData.m_GameLevelCenter.GetGroupID(weaponInfo.m_nUnlockStageID);
						if (groupID > 0)
						{
							tUIWeaponAttributeInfo.m_sUnlockStr = tUIWeaponAttributeInfo.m_sUnlockStr + "COMPLETE STAGE " + groupID;
						}
					}
					if (weaponInfo.m_nUnlockHunterLvl > 0)
					{
						if (tUIWeaponAttributeInfo.m_sUnlockStr.Length > 0)
						{
							tUIWeaponAttributeInfo.m_sUnlockStr += " OR ";
						}
						tUIWeaponAttributeInfo.m_sUnlockStr = tUIWeaponAttributeInfo.m_sUnlockStr + "REACH HUNTER LV " + weaponInfo.m_nUnlockHunterLvl;
					}
					if (tUIWeaponAttributeInfo.m_sUnlockStr.Length > 0)
					{
						tUIWeaponAttributeInfo.m_sUnlockStr += " TO UNLOCK";
					}
				}
			}
			else
			{
				tUIWeaponAttributeInfo.m_bUnlock = true;
				tUIWeaponAttributeInfo.m_sUnlockStr = string.Empty;
			}
			tUIWeaponAttributeInfo.m_nLevelMax = weaponInfo.GetLvlCount();
			tUIWeaponAttributeInfo.m_WeaponType = nWeaponType;
			tUIWeaponAttributeInfo.m_sName = cWeaponInfoLevel.sName;
			tUIWeaponAttributeInfo.m_bCrystalWeapon = cWeaponInfoLevel.isCrystalPurchase;
			tUIWeaponAttributeInfo.m_Mark = NewMarkType.None;
			iServerVerify.CServerConfigInfo serverConfigInfo = iServerVerify.GetInstance().GetServerConfigInfo();
			if (serverConfigInfo != null)
			{
				if (serverConfigInfo.IsGift(weaponInfo.nID))
				{
					tUIWeaponAttributeInfo.m_bActive = true;
					tUIWeaponAttributeInfo.m_sActiveStr = "Buy tCrystals for the first time, and get this weapon for free.";
					tUIWeaponAttributeInfo.m_bActiveCanGet = m_DataCenter.IsFreeWeaponID(weaponInfo.nID);
				}
				int nDiscount = 100;
				serverConfigInfo.IsPriceOff(1, weaponInfo.nID, ref nDiscount);
				tUIWeaponAttributeInfo.m_fDiscount = (float)nDiscount / 100f;
			}
			for (int i = 0; i < weaponInfo.GetLvlCount(); i++)
			{
				int num = i + 1;
				CWeaponInfoLevel cWeaponInfoLevel2 = weaponInfo.Get(num);
				if (cWeaponInfoLevel2 == null)
				{
					Debug.LogWarning("id " + weaponInfo.nID + " lvl " + num + " does not exist!");
					continue;
				}
				TUIWeaponLevelInfo tUIWeaponLevelInfo = new TUIWeaponLevelInfo();
				tUIWeaponAttributeInfo.AddWeaponInfo(num, tUIWeaponLevelInfo);
				tUIWeaponLevelInfo.m_Price = new TUIPriceInfo(cWeaponInfoLevel2.nPurchasePrice, cWeaponInfoLevel2.isCrystalPurchase ? UnitType.Crystal : UnitType.Gold);
				tUIWeaponLevelInfo.m_sLevelupDesc = cWeaponInfoLevel2.sLevelUpDesc;
				tUIWeaponLevelInfo.m_sDesc = cWeaponInfoLevel2.sDesc;
				tUIWeaponLevelInfo.m_nDamage = Mathf.FloorToInt(cWeaponInfoLevel2.fDamage);
				tUIWeaponLevelInfo.m_fShootRate = cWeaponInfoLevel2.fShootSpeed;
				tUIWeaponLevelInfo.m_fBlastRadius = ((cWeaponInfoLevel2.nType == 5) ? 20 : 0);
				tUIWeaponLevelInfo.m_nCapcity = cWeaponInfoLevel2.nCapacity;
				for (int j = 0; j < 3; j++)
				{
					if (cWeaponInfoLevel2.arrFunc[j] == 4)
					{
						tUIWeaponLevelInfo.m_nKnockBack = cWeaponInfoLevel2.arrValueY[j];
					}
				}
				for (int k = 0; k < cWeaponInfoLevel2.ltMaterials.Count && k < cWeaponInfoLevel2.ltMaterialsCount.Count; k++)
				{
					tUIWeaponLevelInfo.m_ltGoodsNeed.Add(new TUIGoodsNeedInfo(cWeaponInfoLevel2.ltMaterials[k], cWeaponInfoLevel2.ltMaterialsCount[k]));
				}
			}
			if (m_GameState != null && m_GameState.m_nLinkWeapon == weaponInfo.nID)
			{
				Debug.Log(m_GameState.m_nLinkWeapon);
				tuiweaponinfo.SetLinkInfo(nCategory, m_GameState.m_nLinkWeapon);
			}
		}
	}

	protected void AddAvatarAttribute(TUIWeaponInfo tuiweaponinfo, List<int> ltArmorList, kShopWeaponCategory nCategory)
	{
		foreach (int ltArmor in ltArmorList)
		{
			CAvatarInfo cAvatarInfo = m_GameData.m_AvatarCenter.Get(ltArmor);
			if (cAvatarInfo == null)
			{
				continue;
			}
			CAvatarInfoLevel cAvatarInfoLevel = cAvatarInfo.Get(1);
			if (cAvatarInfoLevel == null)
			{
				continue;
			}
			int avatarlevel = -1;
			m_DataCenter.GetAvatar(cAvatarInfo.m_nID, ref avatarlevel);
			if (m_GameData.m_ShopDisplayCenter.IsInSellInBlack(cAvatarInfo.m_nID) && avatarlevel <= 0)
			{
				continue;
			}
			TUIWeaponAttributeInfo tUIWeaponAttributeInfo = new TUIWeaponAttributeInfo();
			tUIWeaponAttributeInfo.m_nID = cAvatarInfo.m_nID;
			tUIWeaponAttributeInfo.m_nLevel = avatarlevel;
			tuiweaponinfo.AddItem(nCategory, tUIWeaponAttributeInfo);
			tUIWeaponAttributeInfo.m_nLevelMax = cAvatarInfo.GetCount();
			if (tUIWeaponAttributeInfo.m_nLevel < 1)
			{
				if ((cAvatarInfo.m_nUnlockStageID == 0 && cAvatarInfo.m_nUnlockHunterLvl == 0) || (cAvatarInfo.m_nUnlockStageID > 0 && m_DataCenter.IsLevelPassed(cAvatarInfo.m_nUnlockStageID)) || (cAvatarInfo.m_nUnlockHunterLvl > 0 && m_DataCenter.HunterLvl >= cAvatarInfo.m_nUnlockHunterLvl))
				{
					tUIWeaponAttributeInfo.m_bUnlock = true;
					tUIWeaponAttributeInfo.m_sUnlockStr = string.Empty;
				}
				else
				{
					tUIWeaponAttributeInfo.m_bUnlock = false;
					tUIWeaponAttributeInfo.m_sUnlockStr = string.Empty;
					if (cAvatarInfo.m_nUnlockStageID > 0)
					{
						int groupID = m_GameData.m_GameLevelCenter.GetGroupID(cAvatarInfo.m_nUnlockStageID);
						if (groupID > 0)
						{
							tUIWeaponAttributeInfo.m_sUnlockStr = tUIWeaponAttributeInfo.m_sUnlockStr + "COMPLETE STAGE  " + groupID;
						}
					}
					if (cAvatarInfo.m_nUnlockHunterLvl > 0)
					{
						if (tUIWeaponAttributeInfo.m_sUnlockStr.Length > 0)
						{
							tUIWeaponAttributeInfo.m_sUnlockStr += " OR ";
						}
						tUIWeaponAttributeInfo.m_sUnlockStr = tUIWeaponAttributeInfo.m_sUnlockStr + "REACH HUNTER LV " + cAvatarInfo.m_nUnlockHunterLvl;
					}
					if (tUIWeaponAttributeInfo.m_sUnlockStr.Length > 0)
					{
						tUIWeaponAttributeInfo.m_sUnlockStr += " TO UNLOCK";
					}
				}
			}
			else
			{
				tUIWeaponAttributeInfo.m_bUnlock = true;
				tUIWeaponAttributeInfo.m_sUnlockStr = string.Empty;
			}
			switch (cAvatarInfo.m_nType)
			{
			case 1:
				tUIWeaponAttributeInfo.m_WeaponType = WeaponType.Armor_Head;
				break;
			case 3:
				tUIWeaponAttributeInfo.m_WeaponType = WeaponType.Armor_Body;
				break;
			case 5:
				tUIWeaponAttributeInfo.m_WeaponType = WeaponType.Armor_Leg;
				break;
			case 4:
				tUIWeaponAttributeInfo.m_WeaponType = WeaponType.Armor_Bracelet;
				break;
			case 0:
				tUIWeaponAttributeInfo.m_WeaponType = WeaponType.Accessory_Halo;
				break;
			case 2:
				tUIWeaponAttributeInfo.m_WeaponType = WeaponType.Accessory_Necklace;
				break;
			case 6:
				tUIWeaponAttributeInfo.m_WeaponType = WeaponType.Accessory_Badge;
				break;
			case 7:
				tUIWeaponAttributeInfo.m_WeaponType = WeaponType.Accessory_Stoneskin;
				break;
			}
			tUIWeaponAttributeInfo.m_sName = cAvatarInfo.m_sName;
			tUIWeaponAttributeInfo.m_sIcon = cAvatarInfo.m_sIcon;
			tUIWeaponAttributeInfo.m_bCrystalWeapon = cAvatarInfoLevel.isCrystalPurchase;
			tUIWeaponAttributeInfo.m_Mark = NewMarkType.None;
			iServerVerify.CServerConfigInfo serverConfigInfo = iServerVerify.GetInstance().GetServerConfigInfo();
			if (serverConfigInfo != null)
			{
				int nDiscount = 100;
				serverConfigInfo.IsPriceOff(2, cAvatarInfo.m_nID, ref nDiscount);
				tUIWeaponAttributeInfo.m_fDiscount = (float)nDiscount / 100f;
			}
			for (int i = 0; i < cAvatarInfo.GetCount(); i++)
			{
				int nLevel = i + 1;
				CAvatarInfoLevel cAvatarInfoLevel2 = cAvatarInfo.Get(nLevel);
				if (cAvatarInfoLevel2 == null)
				{
					continue;
				}
				TUIWeaponLevelInfo tUIWeaponLevelInfo = new TUIWeaponLevelInfo();
				tUIWeaponAttributeInfo.AddWeaponInfo(nLevel, tUIWeaponLevelInfo);
				tUIWeaponLevelInfo.m_Price = new TUIPriceInfo(cAvatarInfoLevel2.nPurchasePrice, cAvatarInfoLevel2.isCrystalPurchase ? UnitType.Crystal : UnitType.Gold);
				tUIWeaponLevelInfo.m_sLevelupDesc = cAvatarInfoLevel2.sLevelUpDesc;
				tUIWeaponLevelInfo.m_sDesc = cAvatarInfoLevel2.sDesc;
				for (int j = 0; j < 3; j++)
				{
					if (cAvatarInfoLevel2.arrFunc[j] == 1)
					{
						if (MyUtils.Low32(cAvatarInfoLevel2.arrValueX[j]) == 5)
						{
							tUIWeaponLevelInfo.m_nDefence = MyUtils.Low32(cAvatarInfoLevel2.arrValueY[j]);
						}
						break;
					}
				}
				for (int k = 0; k < cAvatarInfoLevel2.ltMaterials.Count && k < cAvatarInfoLevel2.ltMaterialsCount.Count; k++)
				{
					tUIWeaponLevelInfo.m_ltGoodsNeed.Add(new TUIGoodsNeedInfo(cAvatarInfoLevel2.ltMaterials[k], cAvatarInfoLevel2.ltMaterialsCount[k]));
				}
			}
			if (m_GameState != null && m_GameState.m_nLinkWeapon == cAvatarInfo.m_nID)
			{
				tuiweaponinfo.SetLinkInfo(nCategory, m_GameState.m_nLinkWeapon);
			}
		}
	}

	public bool GetMarkDataForge(out Dictionary<int, NewMarkType> dictMarkData)
	{
		dictMarkData = new Dictionary<int, NewMarkType>();
		if (m_GameData == null)
		{
			return false;
		}
		int num = 0;
		List<int> list = new List<int>();
		list.AddRange(m_GameData.m_ShopDisplayCenter.m_ltWeapon_Autorifle);
		list.AddRange(m_GameData.m_ShopDisplayCenter.m_ltWeapon_Bow);
		list.AddRange(m_GameData.m_ShopDisplayCenter.m_ltWeapon_Holdgun);
		list.AddRange(m_GameData.m_ShopDisplayCenter.m_ltWeapon_Melee);
		list.AddRange(m_GameData.m_ShopDisplayCenter.m_ltWeapon_Rocket);
		list.AddRange(m_GameData.m_ShopDisplayCenter.m_ltWeapon_Shotgun);
		CLevelUpWeapon cLevelUpWeapon = new CLevelUpWeapon();
		foreach (int item in list)
		{
			CWeaponInfo cWeaponInfo = m_GameData.m_WeaponCenter.Get(item);
			if (cWeaponInfo == null)
			{
				continue;
			}
			num = 0;
			m_DataCenter.GetWeaponSign(cWeaponInfo.nID, ref num);
			if (num == 1)
			{
				dictMarkData.Add(cWeaponInfo.nID, NewMarkType.New);
			}
			else if (num != 1)
			{
				CWeaponInfoLevel cWeaponInfoLevel = cWeaponInfo.Get(1);
				if (cLevelUpWeapon.CheckCanUpgrade(cWeaponInfo.nID, 1f) && (cWeaponInfoLevel == null || !cWeaponInfoLevel.isCrystalPurchase))
				{
					dictMarkData.Add(cWeaponInfo.nID, NewMarkType.Mark);
				}
				else
				{
					dictMarkData.Add(cWeaponInfo.nID, NewMarkType.None);
				}
			}
		}
		List<int> list2 = new List<int>();
		list2.AddRange(m_GameData.m_ShopDisplayCenter.m_ltAvatar_Armor);
		list2.AddRange(m_GameData.m_ShopDisplayCenter.m_ltAvatar_Accessory);
		CLevelUpAvatar cLevelUpAvatar = new CLevelUpAvatar();
		foreach (int item2 in list2)
		{
			CAvatarInfo cAvatarInfo = m_GameData.m_AvatarCenter.Get(item2);
			if (cAvatarInfo == null)
			{
				continue;
			}
			num = 0;
			m_DataCenter.GetAvatarSign(cAvatarInfo.m_nID, ref num);
			if (num == 1)
			{
				dictMarkData.Add(cAvatarInfo.m_nID, NewMarkType.New);
			}
			else if (num != 1)
			{
				CAvatarInfoLevel cAvatarInfoLevel = cAvatarInfo.Get(1);
				if (cLevelUpAvatar.CheckCanUpgrade(cAvatarInfo.m_nID, 1f) && (cAvatarInfoLevel == null || !cAvatarInfoLevel.isCrystalPurchase))
				{
					dictMarkData.Add(cAvatarInfo.m_nID, NewMarkType.Mark);
				}
				else
				{
					dictMarkData.Add(cAvatarInfo.m_nID, NewMarkType.None);
				}
			}
		}
		return true;
	}

	protected void EventBack_BlackMarket()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneBlackMarket(TUIEvent.SceneBlackMarketEventType.TUIEvent_Back, true));
	}

	protected void EventBack_CoopInputName()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopInputName(TUIEvent.SceneCoopInputNameEventType.TUIEvent_Back, true));
	}

	protected void EventBack_CoopMainMenu()
	{
		if (m_DataCenterNet != null)
		{
			m_DataCenterNet.Save();
		}
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Back, true));
	}

	protected void EventBack_CoopRoom()
	{
		TNetManager.GetInstance().LeaveRoom();
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_Back, true));
	}

	protected void EventBack_Equip()
	{
		iGameApp.GetInstance().SaveData();
		iGameState gameState = iGameApp.GetInstance().m_GameState;
		if (gameState != null)
		{
			if (gameState.m_curScene4Recommand != 0)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_Back, true, (int)gameState.m_curScene4Recommand));
				gameState.m_curScene4Recommand = TUISceneType.None;
			}
			else if (gameState.m_curScene4Equip != 0)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_Back, true, (int)gameState.m_curScene4Equip));
				gameState.m_curScene4Equip = TUISceneType.None;
			}
			else
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_Back, true));
			}
		}
	}

	protected void EventBack_Forge()
	{
		iGameState gameState = iGameApp.GetInstance().m_GameState;
		if (gameState != null)
		{
			if (gameState.m_curScene4Recommand != 0)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_Back, true, (int)gameState.m_curScene4Recommand));
				gameState.m_curScene4Recommand = TUISceneType.None;
			}
			else
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_Back, true));
			}
		}
	}

	protected void EventBack_Gold()
	{
		iGameState gameState = iGameApp.GetInstance().m_GameState;
		if (gameState != null)
		{
			if (gameState.m_curScene4IAP != 0)
			{
				Debug.Log(gameState.m_curScene4IAP);
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneGold(TUIEvent.SceneGoldEventType.TUIEvent_Back, true, (int)gameState.m_curScene4IAP));
				gameState.m_curScene4IAP = TUISceneType.None;
			}
			else
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneGold(TUIEvent.SceneGoldEventType.TUIEvent_Back, true));
			}
		}
	}

	protected void EventBack_IAP()
	{
		iGameState gameState = iGameApp.GetInstance().m_GameState;
		if (gameState != null)
		{
			gameState.m_bInIAPScene = false;
			if (gameState.m_curScene4IAP != 0)
			{
				Debug.Log(gameState.m_curScene4IAP);
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneIAP(TUIEvent.SceneIAPEventType.TUIEvent_Back, true, (int)gameState.m_curScene4IAP));
				gameState.m_curScene4IAP = TUISceneType.None;
			}
			else
			{
				iServerIAPVerifyBackground.GetInstance().SetActive(true);
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneIAP(TUIEvent.SceneIAPEventType.TUIEvent_Back, true));
			}
		}
	}

	protected void EventBack_Map()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_EnterVilliage, true));
	}

	protected void EventBack_Skill()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_Back, true));
	}

	protected void EventBack_Stash()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneStash(TUIEvent.SceneStashEventType.TUIEvent_Back, true));
	}

	protected void EventBack_Tarven()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_Back, true));
	}
}
