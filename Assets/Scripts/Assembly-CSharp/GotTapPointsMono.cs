using EventCenter;
using UnityEngine;

public class GotTapPointsMono : MonoBehaviour
{
	private void GotTapPoints(int tapPoints)
	{
		if (tapPoints <= 0)
		{
			return;
		}
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
		dataCenter.AddCrystal(tapPoints);
		iGameApp.GetInstance().SaveData(true);
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character != null)
		{
			CCharacterInfoLevel characterInfo = gameData.GetCharacterInfo(character.nID, character.nLevel);
			if (characterInfo != null)
			{
				TUIGameInfo tUIGameInfo = new TUIGameInfo();
				tUIGameInfo.player_info = new TUIPlayerInfo();
				tUIGameInfo.player_info.role_id = character.nID;
				tUIGameInfo.player_info.level = character.nLevel;
				tUIGameInfo.player_info.level_exp = characterInfo.nExp;
				tUIGameInfo.player_info.exp = character.nExp;
				tUIGameInfo.player_info.gold = dataCenter.Gold;
				tUIGameInfo.player_info.crystal = dataCenter.Crystal;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneIAP(TUIEvent.SceneIAPEventType.TUIEvent_TopBar, tUIGameInfo));
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_TopBar, tUIGameInfo));
			}
		}
	}
}
