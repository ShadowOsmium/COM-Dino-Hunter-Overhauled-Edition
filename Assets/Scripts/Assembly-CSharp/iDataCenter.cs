using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;

public class iDataCenter
{
    public void ClampToLimits()
    {
        
        if (m_nGold.Get() > 35000)
            m_nGold.Set(35000);

        if (m_nCrystal.Get() > 650)
            m_nCrystal.Set(650);
    }

    public class CCrystalInBackground
    {
        public float m_fMoney;

        public string m_sCombineKey;

        public SafeInteger m_nCrystal;

        public CCrystalInBackground()
        {
            m_nCrystal = new SafeInteger();
            m_nCrystal.Set(0);
        }
    }

    public class CUnlockSign
    {
        public int m_nType;

        public int m_nID;

        public CUnlockSign()
        {
        }

        public CUnlockSign(int type, int id)
        {
            m_nType = type;
            m_nID = id;
        }
    }

    protected string m_sSaveVersion = "1.0.0";

    protected string m_sGameVersion = "1.7.0";

    protected SafeInteger m_nGold;

    protected SafeInteger m_nCrystal;

    protected List<CCrystalInBackground> m_ltCrystalInBackground;

    protected SafeInteger m_nStashLevel;

    protected int m_nCrystalTotalGain;

    protected int m_nCrystalTotalConsume;

    public Dictionary<int, int> m_dictMaterials;

    public Dictionary<int, int> GetMaterials;

    protected Dictionary<int, int> m_dictWeapon;

    protected Dictionary<int, int> m_dictEquipStone;

    protected Dictionary<int, int> m_dictSkill;

    protected Dictionary<int, int> m_dictPassiveSkill;

    protected Dictionary<int, int> m_dictAvatar;

    protected Dictionary<int, CCharSaveInfo> m_dictCharSaveInfo;

    protected List<CLevelSaveInfo> m_ltLevelSaveInfo;

    protected List<CIAPTransactionInfo> m_ltIAPTransactionInfo;

    protected List<int> m_ltFreeWeapon;

    protected Dictionary<int, CAchievementData> m_dictAchievementData;

    protected DateTime m_lastLoginTime;

    protected int m_nDailyRewardCount;

    protected int m_nDailyRewardHasGot;

    protected List<int> m_ltDailyTask;

    protected bool m_bMusic;

    protected bool m_bSound;

    protected bool m_bAutoAim;

    protected List<CUnlockSign> m_ltUnlockSign;

    protected float m_fSceneProccess;

    protected bool m_bTutorial;

    protected int m_nTutorialVillageState;

    protected bool m_bEvaluate;

    protected int m_nEnterAppCount;

    protected Dictionary<int, int> m_dictWeaponSign;

    protected Dictionary<int, int> m_dictEquipStoneSign;

    protected Dictionary<int, int> m_dictSkillSign;

    protected Dictionary<int, int> m_dictCharacterSign;

    protected Dictionary<int, int> m_dictAvatarSign;

    protected int m_nCurCharID;

    protected int[] m_arrSelectWeapon;

    protected Dictionary<int, int[]> m_dictSelectPassiveSkill;

    protected int m_nCurEquipStone;

    protected SafeInteger m_nAvatarHead;

    protected SafeInteger m_nAvatarUpper;

    protected SafeInteger m_nAvatarLower;

    protected SafeInteger m_nAvatarHeadup;

    protected SafeInteger m_nAvatarNeck;

    protected SafeInteger m_nAvatarWrist;

    protected SafeInteger m_nAvatarBadge;

    protected SafeInteger m_nAvatarStone;

    protected int m_nLatestLevel;

    protected int m_nLastLevel;

    protected bool m_bUnLockLevel;

    protected List<int> m_ltLevelList;

    protected bool m_bFirstTimePlay;

    protected Dictionary<int, int> m_dictWorldMonsterKill;

    protected List<int> m_ltTitle;

    protected Dictionary<int, int> m_dictKillMonster;

    protected SafeInteger m_MVPCount;

    protected SafeInteger m_ReviveInCoopCount;

    protected SafeInteger m_DeadInCoopCount;

    protected string m_sNickName = string.Empty;

    protected SafeInteger m_nHunterLvl;

    protected SafeInteger m_nHunterExp;

    protected SafeInteger m_nHunterExpTotal;

    protected SafeInteger m_nCombatPower;

    protected int m_nRank;

    protected int m_nLastRank;

    protected SafeInteger m_nBeAdmired;

    protected SafeInteger m_nTitle;

    protected string m_sSignature = "Let's not go hunting!";

    protected List<string> m_ltFriends;

    public byte[] m_Photo;

    public bool isFirstTimePlay
    {
        get
        {
            return m_bFirstTimePlay;
        }
    }

    public bool m_bInBlackName { get; set; }

    public bool m_bInWhiteName { get; set; }

    public string GameVersion
    {
        get
        {
            return m_sGameVersion;
        }
        set
        {
            m_sGameVersion = value;
        }
    }

    public bool isTutorial
    {
        get
        {
            return m_bTutorial;
        }
        set
        {
            m_bTutorial = value;
        }
    }

    public int nTutorialVillageState
    {
        get
        {
            return m_nTutorialVillageState;
        }
        set
        {
            m_nTutorialVillageState = value;
        }
    }

    public bool MusicSwitch
    {
        get
        {
            return m_bMusic;
        }
        set
        {
            m_bMusic = value;
        }
    }

    public bool SoundSwitch
    {
        get
        {
            return m_bSound;
        }
        set
        {
            m_bSound = value;
        }
    }

    public bool AutoAimSwitch
    {
        get
        {
            return m_bAutoAim;
        }
        set
        {
            m_bAutoAim = value;
        }
    }

    public int Gold
    {
        get
        {
            return m_nGold.Get();
        }
    }

    public int Crystal
    {
        get
        {
            return m_nCrystal.Get();
        }
    }

    public int CurCharID
    {
        get
        {
            return m_nCurCharID;
        }
        set
        {
            m_nCurCharID = value;
        }
    }

    public int CurEquipStone
    {
        get
        {
            return m_nCurEquipStone;
        }
        set
        {
            m_nCurEquipStone = value;
        }
    }

    public int LatestLevel
    {
        get
        {
            return m_nLatestLevel;
        }
        set
        {
            m_nLatestLevel = value;
        }
    }

    public int LastLevel
    {
        get
        {
            return m_nLastLevel;
        }
        set
        {
            m_nLastLevel = value;
        }
    }

    public float SceneProccess
    {
        get
        {
            return m_fSceneProccess;
        }
        set
        {
            m_fSceneProccess = value;
        }
    }

    public int StashLevel
    {
        get
        {
            return m_nStashLevel.Get();
        }
        set
        {
            m_nStashLevel.Set(value);
        }
    }

    public int StashCount
    {
        get
        {
            int num = 0;
            foreach (int value in m_dictMaterials.Values)
            {
                num += value;
            }
            return num;
        }
    }

    public int StashCountMax
    {
        get
        {
            iGameData gameData = iGameApp.GetInstance().m_GameData;
            if (gameData == null)
            {
                return 0;
            }
            CStashCapacity stashCapacity = gameData.GetStashCapacity(StashLevel);
            if (stashCapacity == null)
            {
                return 0;
            }
            return stashCapacity.nCapacity;
        }
    }

    public int HighestCharLevel
    {
        get
        {
            int num = 0;
            foreach (CCharSaveInfo value in m_dictCharSaveInfo.Values)
            {
                if (num == 0 || num < value.nLevel)
                {
                    num = value.nLevel;
                }
            }
            return num;
        }
    }

    public DateTime LastLoginTime
    {
        get
        {
            return m_lastLoginTime;
        }
        set
        {
            m_lastLoginTime = value;
        }
    }

    public int DailyRewardCount
    {
        get
        {
            return m_nDailyRewardCount;
        }
        set
        {
            m_nDailyRewardCount = value;
        }
    }

    public int DailyRewardHasGot
    {
        get
        {
            return m_nDailyRewardHasGot;
        }
        set
        {
            m_nDailyRewardHasGot = value;
        }
    }

    public bool isEvaluate
    {
        get
        {
            return m_bEvaluate;
        }
        set
        {
            m_bEvaluate = value;
        }
    }

    public int EnterAppCount
    {
        get
        {
            return m_nEnterAppCount;
        }
        set
        {
            m_nEnterAppCount = value;
        }
    }

    public int AvatarHead
    {
        get
        {
            return m_nAvatarHead.Get();
        }
        set
        {
            m_nAvatarHead.Set(value);
        }
    }

    public int AvatarUpper
    {
        get
        {
            return m_nAvatarUpper.Get();
        }
        set
        {
            m_nAvatarUpper.Set(value);
        }
    }

    public int AvatarLower
    {
        get
        {
            return m_nAvatarLower.Get();
        }
        set
        {
            m_nAvatarLower.Set(value);
        }
    }

    public int AvatarWrist
    {
        get
        {
            return m_nAvatarWrist.Get();
        }
        set
        {
            m_nAvatarWrist.Set(value);
        }
    }

    public int AvatarHeadup
    {
        get
        {
            return m_nAvatarHeadup.Get();
        }
        set
        {
            m_nAvatarHeadup.Set(value);
        }
    }

    public int AvatarNeck
    {
        get
        {
            return m_nAvatarNeck.Get();
        }
        set
        {
            m_nAvatarNeck.Set(value);
        }
    }

    public int AvatarBadge
    {
        get
        {
            return m_nAvatarBadge.Get();
        }
        set
        {
            m_nAvatarBadge.Set(value);
        }
    }

    public int AvatarStone
    {
        get
        {
            return m_nAvatarStone.Get();
        }
        set
        {
            m_nAvatarStone.Set(value);
        }
    }

    public bool isUnLockLevel
    {
        get
        {
            return m_bUnLockLevel;
        }
    }

    public string NickName
    {
        get
        {
            return m_sNickName;
        }
        set
        {
            m_sNickName = value;
        }
    }

    public int HunterLvl
    {
        get
        {
            return m_nHunterLvl.Get();
        }
        set
        {
            m_nHunterLvl.Set(value);
        }
    }

    public int HunterExp
    {
        get
        {
            return m_nHunterExp.Get();
        }
        set
        {
            m_nHunterExp.Set(value);
        }
    }

    public int HunterExpTotal
    {
        get
        {
            return m_nHunterExpTotal.Get();
        }
        set
        {
            m_nHunterExpTotal.Set(value);
        }
    }

    public int CombatPower
    {
        get
        {
            return m_nCombatPower.Get();
        }
        set
        {
            m_nCombatPower.Set(value);
        }
    }

    public int Rank
    {
        get
        {
            return m_nRank;
        }
        set
        {
            m_nRank = value;
        }
    }

    public int LastRank
    {
        get
        {
            return m_nLastRank;
        }
        set
        {
            m_nLastRank = value;
        }
    }

    public int BeAdmire
    {
        get
        {
            return m_nBeAdmired.Get();
        }
        set
        {
            m_nBeAdmired.Set(value);
        }
    }

    public int Title
    {
        get
        {
            return m_nTitle.Get();
        }
        set
        {
            m_nTitle.Set(value);
        }
    }

    public string Signature
    {
        get
        {
            return m_sSignature;
        }
        set
        {
            m_sSignature = value;
        }
    }

    public int MVPCount
    {
        get
        {
            return m_MVPCount.Get();
        }
        set
        {
            m_MVPCount.Set(value);
        }
    }

    public int ReviveInCoopCount
    {
        get
        {
            return m_ReviveInCoopCount.Get();
        }
        set
        {
            m_ReviveInCoopCount.Set(value);
        }
    }

    public int DeadInCoopCount
    {
        get
        {
            return m_DeadInCoopCount.Get();
        }
        set
        {
            m_DeadInCoopCount.Set(value);
        }
    }

    public iDataCenter()
    {
        m_nGold = new SafeInteger();
        m_nCrystal = new SafeInteger();
        m_ltCrystalInBackground = new List<CCrystalInBackground>();
        m_nStashLevel = new SafeInteger();
        m_dictMaterials = new Dictionary<int, int>();
        m_dictWeapon = new Dictionary<int, int>();
        m_dictEquipStone = new Dictionary<int, int>();
        m_dictPassiveSkill = new Dictionary<int, int>();
        m_dictCharSaveInfo = new Dictionary<int, CCharSaveInfo>();
        m_dictSkill = new Dictionary<int, int>();
        m_dictAvatar = new Dictionary<int, int>();
        m_nAvatarHead = new SafeInteger();
        m_nAvatarUpper = new SafeInteger();
        m_nAvatarLower = new SafeInteger();
        m_nAvatarHeadup = new SafeInteger();
        m_nAvatarNeck = new SafeInteger();
        m_nAvatarWrist = new SafeInteger();
        m_nAvatarBadge = new SafeInteger();
        m_nAvatarStone = new SafeInteger();
        m_dictWeaponSign = new Dictionary<int, int>();
        m_dictEquipStoneSign = new Dictionary<int, int>();
        m_dictSkillSign = new Dictionary<int, int>();
        m_dictCharacterSign = new Dictionary<int, int>();
        m_dictAvatarSign = new Dictionary<int, int>();
        m_arrSelectWeapon = new int[3] { 2, 1, -1 };
        m_dictSelectPassiveSkill = new Dictionary<int, int[]>();
        m_ltLevelSaveInfo = new List<CLevelSaveInfo>();
        m_ltLevelList = new List<int>();
        for (int i = 1001; i <= 1024; i++)
        {
            m_ltLevelList.Add(i);
        }
        m_ltIAPTransactionInfo = new List<CIAPTransactionInfo>();
        m_ltUnlockSign = new List<CUnlockSign>();
        m_dictAchievementData = new Dictionary<int, CAchievementData>();
        m_ltFreeWeapon = new List<int>();
        m_ltDailyTask = new List<int>();
        m_dictWorldMonsterKill = new Dictionary<int, int>();
        m_nHunterLvl = new SafeInteger();
        m_nHunterExp = new SafeInteger();
        m_nHunterExpTotal = new SafeInteger();
        m_nCombatPower = new SafeInteger();
        m_nBeAdmired = new SafeInteger();
        m_nTitle = new SafeInteger();
        m_ltFriends = new List<string>();
        m_ltTitle = new List<int>();
        m_dictKillMonster = new Dictionary<int, int>();
        m_MVPCount = new SafeInteger();
        m_ReviveInCoopCount = new SafeInteger();
        m_DeadInCoopCount = new SafeInteger();
        Clear();
    }

    public void Clear()
    {
        m_bMusic = true;
        m_bSound = true;
        m_bAutoAim = true;
        m_nGold.Set(1500);
        m_nCrystal.Set(25);
        m_ltCrystalInBackground.Clear();
        m_nStashLevel.Set(1);
        m_nCrystalTotalGain = 0;
        m_nCrystalTotalConsume = 0;
        m_dictMaterials.Clear();
        m_dictWeapon.Clear();
        m_dictEquipStone.Clear();
        m_dictPassiveSkill.Clear();
        m_dictCharSaveInfo.Clear();
        m_dictSkill.Clear();
        m_dictAvatar.Clear();
        m_nAvatarHead.Set(-1);
        m_nAvatarUpper.Set(-1);
        m_nAvatarLower.Set(-1);
        m_nAvatarHeadup.Set(-1);
        m_nAvatarNeck.Set(-1);
        m_nAvatarWrist.Set(-1);
        m_nAvatarBadge.Set(-1);
        m_nAvatarStone.Set(-1);
        m_ltLevelSaveInfo.Clear();
        m_ltIAPTransactionInfo.Clear();
        m_ltFreeWeapon.Clear();
        m_dictAchievementData.Clear();
        m_dictWeaponSign.Clear();
        m_dictEquipStoneSign.Clear();
        m_dictSkillSign.Clear();
        m_dictCharacterSign.Clear();
        m_dictAvatarSign.Clear();
        m_nCurCharID = 1;
        m_arrSelectWeapon = new int[3] { 2, 1, -1 };
        m_dictSelectPassiveSkill.Clear();
        m_nCurEquipStone = 0;
        m_nLatestLevel = 1001;
        m_bUnLockLevel = false;
        m_ltLevelSaveInfo.Clear();
        m_bFirstTimePlay = true;
        m_fSceneProccess = 0f;
        m_bTutorial = true;
        m_nTutorialVillageState = 0;
        m_bEvaluate = false;
        m_nEnterAppCount = 0;
        m_ltIAPTransactionInfo.Clear();
        m_ltUnlockSign.Clear();
        m_dictAchievementData.Clear();
        m_ltFreeWeapon.Clear();
        m_ltDailyTask.Clear();
        m_nDailyRewardCount = 1;
        m_nDailyRewardHasGot = 0;
        m_lastLoginTime = new DateTime(1970, 1, 1);
        m_dictWorldMonsterKill.Clear();
        m_sNickName = string.Empty;
        m_nHunterLvl.Set(1);
        m_nHunterExp.Set(0);
        m_nHunterExpTotal.Set(0);
        m_nBeAdmired.Set(0);
        m_nRank = 0;
        m_nLastRank = 0;
        m_nBeAdmired.Set(999);
        m_sSignature = "Let's not go hunting!";
        m_ltFriends.Clear();
        m_ltTitle.Clear();
        m_ltTitle.Add(1);
        m_nTitle.Set(1);
        m_dictKillMonster.Clear();
        m_MVPCount.Set(0);
        m_ReviveInCoopCount.Set(0);
        m_DeadInCoopCount.Set(0);
        m_bInBlackName = false;
        m_bInWhiteName = false;
    }

    public bool Load()
    {
        string content = string.Empty;
        if (!Utils.FileGetString("gamedata.xml", ref content))
        {
            LoadData(content);
            return false;
        }
        MyUtils.UnZipString(content, ref content);
        content = XXTEAUtils.Decrypt(content, iServerConfigData.GetInstance().m_sServerInfoKey);
        LoadData(content);
        return true;
    }

    public void Save()
    {
        XmlDocument xmlDocument = new XmlDocument();
        SaveData(xmlDocument);
        string outerXML = xmlDocument.OuterXml;
        outerXML = XXTEAUtils.Encrypt(outerXML, iServerConfigData.GetInstance().m_sServerInfoKey);
        MyUtils.ZipString(outerXML, ref outerXML);
        string filename = Utils.SavePath() + "/gamedata.xml";
        File.WriteAllText(filename, outerXML);
    }

    protected void SaveData(XmlDocument doc)
    {
        XmlNode newChild = doc.CreateXmlDeclaration("1.0", "UTF-8", "no");
        doc.AppendChild(newChild);
        string empty = string.Empty;
        XmlElement xmlElement = doc.CreateElement("gamedata");
        doc.AppendChild(xmlElement);
        xmlElement.SetAttribute("version", m_sSaveVersion);
        xmlElement.SetAttribute("gameversion", "1.7.0");
        xmlElement.SetAttribute("gold", m_nGold.Get().ToString());
        xmlElement.SetAttribute("crystal", m_nCrystal.Get().ToString());
        xmlElement.SetAttribute("stashlevel", m_nStashLevel.Get().ToString());
        xmlElement.SetAttribute("latestlevel", m_nLatestLevel.ToString());
        xmlElement.SetAttribute("lastlevel", m_nLastLevel.ToString());
        xmlElement.SetAttribute("isunlocklevel", m_bUnLockLevel.ToString());
        xmlElement.SetAttribute("proccess", m_fSceneProccess.ToString());
        xmlElement.SetAttribute("crystaltotalgain", m_nCrystalTotalGain.ToString());
        xmlElement.SetAttribute("crystaltotalconsume", m_nCrystalTotalConsume.ToString());
        xmlElement.SetAttribute("isMusic", m_bMusic.ToString());
        xmlElement.SetAttribute("isSound", m_bSound.ToString());
        xmlElement.SetAttribute("isTutorial", m_bTutorial.ToString());
        xmlElement.SetAttribute("tutorialVillageState", m_nTutorialVillageState.ToString());
        xmlElement.SetAttribute("isEvaluate", m_bEvaluate.ToString());
        xmlElement.SetAttribute("enterappcount", m_nEnterAppCount.ToString());
        xmlElement.SetAttribute("dailyrewardcount", m_nDailyRewardCount.ToString());
        xmlElement.SetAttribute("dailyrewardhasgot", m_nDailyRewardHasGot.ToString());
        xmlElement.SetAttribute("nickname", m_sNickName);
        xmlElement.SetAttribute("hunterlvl", m_nHunterLvl.Get().ToString());
        xmlElement.SetAttribute("hunterexp", m_nHunterExp.Get().ToString());
        xmlElement.SetAttribute("hunterexptotal", m_nHunterExpTotal.Get().ToString());
        xmlElement.SetAttribute("combatpower", m_nCombatPower.Get().ToString());
        xmlElement.SetAttribute("rank", m_nRank.ToString());
        xmlElement.SetAttribute("beadmired", m_nBeAdmired.Get().ToString());
        xmlElement.SetAttribute("title", m_nTitle.Get().ToString());
        xmlElement.SetAttribute("signature", m_sSignature);
        xmlElement.SetAttribute("deadincoop", m_DeadInCoopCount.Get().ToString());
        xmlElement.SetAttribute("reviveincoop", m_ReviveInCoopCount.Get().ToString());
        xmlElement.SetAttribute("mvpincoop", m_MVPCount.Get().ToString());
        xmlElement.SetAttribute("isinblackname", m_bInBlackName.ToString());
        xmlElement.SetAttribute("isinwhitename", m_bInWhiteName.ToString());
        if (m_Photo != null)
        {
            xmlElement.SetAttribute("photo", Convert.ToBase64String(m_Photo));
        }
        empty = string.Empty;
        foreach (KeyValuePair<int, int> item in m_dictKillMonster)
        {
            empty = ((empty.Length >= 1) ? (empty + "," + item.Key + "," + item.Value) : (item.Key + "," + item.Value));
        }
        xmlElement.SetAttribute("killmonster", empty);
        empty = string.Empty;
        foreach (KeyValuePair<int, int> item2 in m_dictWorldMonsterKill)
        {
            empty = ((empty.Length >= 1) ? (empty + "," + item2.Key + "," + item2.Value) : (item2.Key + "," + item2.Value));
        }
        xmlElement.SetAttribute("worldmonsterkill", empty);
        empty = m_lastLoginTime.Year + "," + m_lastLoginTime.Month + "," + m_lastLoginTime.Day + "," + m_lastLoginTime.Hour + "," + m_lastLoginTime.Minute + "," + m_lastLoginTime.Second;
        xmlElement.SetAttribute("lastlogintime", empty);
        empty = string.Empty;
        for (int i = 0; i < m_ltDailyTask.Count; i++)
        {
            empty = ((i != 0) ? (empty + "," + m_ltDailyTask[i]) : m_ltDailyTask[i].ToString());
        }
        xmlElement.SetAttribute("dailytask", empty);
        XmlElement xmlElement2 = doc.CreateElement("passedlevel");
        xmlElement.AppendChild(xmlElement2);
        foreach (CLevelSaveInfo item3 in m_ltLevelSaveInfo)
        {
            XmlElement xmlElement3 = doc.CreateElement("node");
            xmlElement2.AppendChild(xmlElement3);
            xmlElement3.SetAttribute("id", item3.nID.ToString());
            xmlElement3.SetAttribute("isignorecg", item3.isIgnoreCG.ToString());
        }
        XmlElement xmlElement4 = doc.CreateElement("character");
        xmlElement.AppendChild(xmlElement4);
        xmlElement4.SetAttribute("select", m_nCurCharID.ToString());
        foreach (CCharSaveInfo value2 in m_dictCharSaveInfo.Values)
        {
            XmlElement xmlElement5 = doc.CreateElement("node");
            xmlElement4.AppendChild(xmlElement5);
            xmlElement5.SetAttribute("id", value2.nID.ToString());
            xmlElement5.SetAttribute("level", value2.nLevel.ToString());
            xmlElement5.SetAttribute("exp", value2.nExp.ToString());
        }
        XmlElement xmlElement6 = doc.CreateElement("weapon");
        xmlElement.AppendChild(xmlElement6);
        empty = string.Empty;
        int[] arrSelectWeapon = m_arrSelectWeapon;
        for (int j = 0; j < arrSelectWeapon.Length; j++)
        {
            int num = arrSelectWeapon[j];
            empty = ((empty.Length >= 1) ? (empty + "," + num) : num.ToString());
        }
        xmlElement6.SetAttribute("select", empty);
        foreach (KeyValuePair<int, int> item4 in m_dictWeapon)
        {
            XmlElement xmlElement7 = doc.CreateElement("node");
            xmlElement6.AppendChild(xmlElement7);
            xmlElement7.SetAttribute("id", item4.Key.ToString());
            xmlElement7.SetAttribute("level", item4.Value.ToString());
        }
        XmlElement xmlElement8 = doc.CreateElement("avatar");
        xmlElement.AppendChild(xmlElement8);
        xmlElement8.SetAttribute("avatarhead", AvatarHead.ToString());
        xmlElement8.SetAttribute("avatarupper", AvatarUpper.ToString());
        xmlElement8.SetAttribute("avatarlower", AvatarLower.ToString());
        xmlElement8.SetAttribute("avatarheadup", AvatarHeadup.ToString());
        xmlElement8.SetAttribute("avatarneck", AvatarNeck.ToString());
        xmlElement8.SetAttribute("avatarwrist", AvatarWrist.ToString());
        xmlElement8.SetAttribute("avatarbadge", AvatarBadge.ToString());
        xmlElement8.SetAttribute("avatarstone", AvatarStone.ToString());
        foreach (KeyValuePair<int, int> item5 in m_dictAvatar)
        {
            XmlElement xmlElement9 = doc.CreateElement("node");
            xmlElement8.AppendChild(xmlElement9);
            xmlElement9.SetAttribute("id", item5.Key.ToString());
            xmlElement9.SetAttribute("level", item5.Value.ToString());
        }
        XmlElement xmlElement10 = doc.CreateElement("skill");
        xmlElement.AppendChild(xmlElement10);
        foreach (KeyValuePair<int, int[]> item6 in m_dictSelectPassiveSkill)
        {
            XmlElement xmlElement11 = doc.CreateElement("selectnode");
            xmlElement10.AppendChild(xmlElement11);
            xmlElement11.SetAttribute("charid", item6.Key.ToString());
            empty = string.Empty;
            int[] value = item6.Value;
            for (int k = 0; k < value.Length; k++)
            {
                int num2 = value[k];
                empty = ((empty.Length >= 1) ? (empty + "," + num2) : num2.ToString());
            }
            xmlElement11.SetAttribute("select", empty);
        }
        foreach (KeyValuePair<int, int> item7 in m_dictPassiveSkill)
        {
            XmlElement xmlElement12 = doc.CreateElement("node");
            xmlElement10.AppendChild(xmlElement12);
            xmlElement12.SetAttribute("id", item7.Key.ToString());
            xmlElement12.SetAttribute("level", item7.Value.ToString());
        }
        foreach (KeyValuePair<int, int> item8 in m_dictSkill)
        {
            XmlElement xmlElement13 = doc.CreateElement("node2");
            xmlElement10.AppendChild(xmlElement13);
            xmlElement13.SetAttribute("id", item8.Key.ToString());
            xmlElement13.SetAttribute("level", item8.Value.ToString());
        }
        XmlElement xmlElement14 = doc.CreateElement("equipstone");
        xmlElement.AppendChild(xmlElement14);
        xmlElement14.SetAttribute("select", m_nCurEquipStone.ToString());
        foreach (KeyValuePair<int, int> item9 in m_dictEquipStone)
        {
            XmlElement xmlElement15 = doc.CreateElement("node");
            xmlElement14.AppendChild(xmlElement15);
            xmlElement15.SetAttribute("id", item9.Key.ToString());
            xmlElement15.SetAttribute("level", item9.Value.ToString());
        }
        XmlElement xmlMaterials = doc.CreateElement("materials");
        xmlElement.AppendChild(xmlMaterials);

        Dictionary<int, int>.Enumerator enumerator = m_dictMaterials.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                KeyValuePair<int, int> dictMaterial = enumerator.Current;
                if (dictMaterial.Value != 0)
                {
                    XmlElement xmlNode = doc.CreateElement("node");
                    xmlMaterials.AppendChild(xmlNode);
                    xmlNode.SetAttribute("id", dictMaterial.Key.ToString());
                    xmlNode.SetAttribute("count", dictMaterial.Value.ToString());
                }
            }
        }
        finally
        {
            enumerator.Dispose();
        }
        XmlElement xmlElement18 = doc.CreateElement("unlocksign");
        xmlElement.AppendChild(xmlElement18);
        foreach (CUnlockSign item10 in m_ltUnlockSign)
        {
            XmlElement xmlElement19 = doc.CreateElement("promptsign");
            xmlElement18.AppendChild(xmlElement19);
            xmlElement19.SetAttribute("type", item10.m_nType.ToString());
            xmlElement19.SetAttribute("id", item10.m_nID.ToString());
        }
        foreach (KeyValuePair<int, int> item11 in m_dictWeaponSign)
        {
            XmlElement xmlElement20 = doc.CreateElement("weaponsign");
            xmlElement18.AppendChild(xmlElement20);
            xmlElement20.SetAttribute("id", item11.Key.ToString());
            xmlElement20.SetAttribute("sign", item11.Value.ToString());
        }
        foreach (KeyValuePair<int, int> item12 in m_dictAvatarSign)
        {
            XmlElement xmlElement21 = doc.CreateElement("avatarsign");
            xmlElement18.AppendChild(xmlElement21);
            xmlElement21.SetAttribute("id", item12.Key.ToString());
            xmlElement21.SetAttribute("sign", item12.Value.ToString());
        }
        foreach (KeyValuePair<int, int> item13 in m_dictEquipStoneSign)
        {
            XmlElement xmlElement22 = doc.CreateElement("equipstonesign");
            xmlElement18.AppendChild(xmlElement22);
            xmlElement22.SetAttribute("id", item13.Key.ToString());
            xmlElement22.SetAttribute("sign", item13.Value.ToString());
        }
        foreach (KeyValuePair<int, int> item14 in m_dictSkillSign)
        {
            XmlElement xmlElement23 = doc.CreateElement("skillsign");
            xmlElement18.AppendChild(xmlElement23);
            xmlElement23.SetAttribute("id", item14.Key.ToString());
            xmlElement23.SetAttribute("sign", item14.Value.ToString());
        }
        foreach (KeyValuePair<int, int> item15 in m_dictCharacterSign)
        {
            XmlElement xmlElement24 = doc.CreateElement("charactersign");
            xmlElement18.AppendChild(xmlElement24);
            xmlElement24.SetAttribute("id", item15.Key.ToString());
            xmlElement24.SetAttribute("sign", item15.Value.ToString());
        }
        if (m_ltIAPTransactionInfo.Count > 0)
        {
            XmlElement xmlElement25 = doc.CreateElement("transaction");
            xmlElement.AppendChild(xmlElement25);
            foreach (CIAPTransactionInfo item16 in m_ltIAPTransactionInfo)
            {
                XmlElement xmlElement26 = doc.CreateElement("node");
                xmlElement25.AppendChild(xmlElement26);
                xmlElement26.SetAttribute("key", item16.m_sIAPKey);
                xmlElement26.SetAttribute("identifier", item16.m_sIdentifier);
                xmlElement26.SetAttribute("receipt", item16.m_sReceipt);
            }
        }
        if (m_ltCrystalInBackground.Count > 0)
        {
            XmlElement xmlElement27 = doc.CreateElement("crystalinbackground");
            xmlElement.AppendChild(xmlElement27);
            foreach (CCrystalInBackground item17 in m_ltCrystalInBackground)
            {
                XmlElement xmlElement28 = doc.CreateElement("node");
                xmlElement27.AppendChild(xmlElement28);
                xmlElement28.SetAttribute("combinekey", item17.m_sCombineKey);
                xmlElement28.SetAttribute("money", item17.m_fMoney.ToString());
                xmlElement28.SetAttribute("crystal", item17.m_nCrystal.Get().ToString());
            }
        }
        if (m_dictAchievementData.Count > 0)
        {
            XmlElement xmlElement29 = doc.CreateElement("achievement");
            xmlElement.AppendChild(xmlElement29);
            foreach (CAchievementData value3 in m_dictAchievementData.Values)
            {
                XmlElement xmlElement30 = doc.CreateElement("node");
                xmlElement29.AppendChild(xmlElement30);
                xmlElement30.SetAttribute("id", value3.nID.ToString());
                xmlElement30.SetAttribute("state", value3.nState.ToString());
                xmlElement30.SetAttribute("value", value3.nCurValue.ToString());
                empty = string.Empty;
                for (int l = 0; l < 3; l++)
                {
                    empty = ((l != 0) ? (empty + "," + value3.IsGotReward(l)) : value3.IsGotReward(l).ToString());
                }
                xmlElement30.SetAttribute("isgotreward", empty);
            }
        }
        if (m_ltFreeWeapon.Count > 0)
        {
            XmlElement xmlElement31 = doc.CreateElement("freeweapon");
            xmlElement.AppendChild(xmlElement31);
            empty = string.Empty;
            for (int m = 0; m < m_ltFreeWeapon.Count; m++)
            {
                empty = ((m != 0) ? (empty + "," + m_ltFreeWeapon[m]) : m_ltFreeWeapon[m].ToString());
            }
            xmlElement31.SetAttribute("list", empty);
        }
        if (m_ltFriends.Count > 0)
        {
            XmlElement xmlElement32 = doc.CreateElement("friends");
            xmlElement.AppendChild(xmlElement32);
            foreach (string ltFriend in m_ltFriends)
            {
                XmlElement xmlElement33 = doc.CreateElement("node");
                xmlElement32.AppendChild(xmlElement33);
                xmlElement33.SetAttribute("id", ltFriend);
            }
        }
        if (m_ltTitle.Count <= 0)
        {
            return;
        }
        XmlElement xmlElement34 = doc.CreateElement("titles");
        xmlElement.AppendChild(xmlElement34);
        foreach (int item18 in m_ltTitle)
        {
            XmlElement xmlElement35 = doc.CreateElement("node");
            xmlElement34.AppendChild(xmlElement35);
            xmlElement35.SetAttribute("id", item18.ToString());
        }
    }

    public void LoadData(string content)
    {
        if (content.Length < 1)
        {
            Clear();
            m_bFirstTimePlay = true;
            m_nTutorialVillageState = -1;
            SetCharacter(1, 1, 0);
            SetWeaponLevel(1, 1);
            SetWeaponLevel(2, 1);
            Save();
            return;
        }
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(content);
        string value = string.Empty;
        string text = string.Empty;
        XmlNode documentElement = xmlDocument.DocumentElement;
        if (MyUtils.GetAttribute(documentElement, "version", ref value))
        {
            text = value;
        }
        if (text == "1.0.0")
        {
            Clear();
            Load_1_0(documentElement);
        }
        else
        {
            Clear();
            Load_1_0(documentElement);
        }
    }

    protected void Load_1_0(XmlNode root)
    {
        string value = string.Empty;
        if (MyUtils.GetAttribute(root, "gameversion", ref value))
        {
            m_sGameVersion = value;
        }
        if (MyUtils.GetAttribute(root, "gold", ref value))
        {
            m_nGold.Set(int.Parse(value));
        }
        if (MyUtils.GetAttribute(root, "crystal", ref value))
        {
            m_nCrystal.Set(int.Parse(value));
        }
        if (MyUtils.GetAttribute(root, "stashlevel", ref value))
        {
            m_nStashLevel.Set(int.Parse(value));
        }
        if (MyUtils.GetAttribute(root, "latestlevel", ref value))
        {
            m_nLatestLevel = int.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "lastlevel", ref value))
        {
            m_nLastLevel = int.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "isunlocklevel", ref value))
        {
            m_bUnLockLevel = bool.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "proccess", ref value))
        {
            m_fSceneProccess = float.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "crystaltotalgain", ref value))
        {
            m_nCrystalTotalGain = int.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "crystaltotalconsume", ref value))
        {
            m_nCrystalTotalConsume = int.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "isMusic", ref value))
        {
            m_bMusic = bool.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "isSound", ref value))
        {
            m_bSound = bool.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "isTutorial", ref value))
        {
            m_bTutorial = bool.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "tutorialVillageState", ref value))
        {
            m_nTutorialVillageState = int.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "isEvaluate", ref value))
        {
            m_bEvaluate = bool.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "enterappcount", ref value))
        {
            m_nEnterAppCount = int.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "dailyrewardcount", ref value))
        {
            m_nDailyRewardCount = int.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "dailyrewardhasgot", ref value))
        {
            m_nDailyRewardHasGot = int.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "nickname", ref value))
        {
            m_sNickName = value;
        }
        if (MyUtils.GetAttribute(root, "hunterlvl", ref value))
        {
            m_nHunterLvl.Set(int.Parse(value));
        }
        if (MyUtils.GetAttribute(root, "hunterexp", ref value))
        {
            m_nHunterExp.Set(int.Parse(value));
        }
        if (MyUtils.GetAttribute(root, "hunterexptotal", ref value))
        {
            m_nHunterExpTotal.Set(int.Parse(value));
        }
        if (MyUtils.GetAttribute(root, "combatpower", ref value))
        {
            m_nCombatPower.Set(int.Parse(value));
        }
        if (MyUtils.GetAttribute(root, "rank", ref value))
        {
            m_nRank = int.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "beadmired", ref value))
        {
            m_nBeAdmired.Set(int.Parse(value));
        }
        if (MyUtils.GetAttribute(root, "title", ref value))
        {
            int num = int.Parse(value);
            if (num <= 0)
            {
                num = 1;
            }
            m_nTitle.Set(num);
        }
        if (MyUtils.GetAttribute(root, "signature", ref value))
        {
            m_sSignature = value;
        }
        if (MyUtils.GetAttribute(root, "deadincoop", ref value))
        {
            m_DeadInCoopCount.Set(int.Parse(value));
        }
        if (MyUtils.GetAttribute(root, "reviveincoop", ref value))
        {
            m_ReviveInCoopCount.Set(int.Parse(value));
        }
        if (MyUtils.GetAttribute(root, "mvpincoop", ref value))
        {
            m_MVPCount.Set(int.Parse(value));
        }
        if (MyUtils.GetAttribute(root, "isinblackname", ref value))
        {
            m_bInBlackName = bool.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "isinwhitename", ref value))
        {
            m_bInWhiteName = bool.Parse(value);
        }
        if (MyUtils.GetAttribute(root, "photo", ref value))
        {
            m_Photo = Convert.FromBase64String(value);
        }
        if (MyUtils.GetAttribute(root, "killmonster", ref value))
        {
            m_dictKillMonster.Clear();
            string[] array = value.Split(',');
            if (array != null && array.Length > 0)
            {
                for (int i = 0; i < array.Length / 2; i++)
                {
                    SetkillMonster(int.Parse(array[i]), int.Parse(array[i + 1]));
                }
            }
        }
        if (MyUtils.GetAttribute(root, "worldmonsterkill", ref value))
        {
            m_dictWorldMonsterKill.Clear();
            string[] array = value.Split(',');
            if (array != null && array.Length > 0)
            {
                for (int j = 0; j < array.Length / 2; j++)
                {
                    AddWorldMonsterKill(int.Parse(array[j]), int.Parse(array[j + 1]));
                }
            }
        }
        if (MyUtils.GetAttribute(root, "lastlogintime", ref value))
        {
            string[] array = value.Split(',');
            if (array != null && array.Length == 6)
            {
                m_lastLoginTime = new DateTime(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]), int.Parse(array[3]), int.Parse(array[4]), int.Parse(array[5]));
            }
        }
        if (MyUtils.GetAttribute(root, "dailytask", ref value))
        {
            m_ltDailyTask.Clear();
            string[] array = value.Split(',');
            if (array != null)
            {
                for (int k = 0; k < array.Length; k++)
                {
                    m_ltDailyTask.Add(int.Parse(array[k]));
                }
            }
        }
        foreach (XmlNode item2 in root)
        {
            if (item2.Name == "passedlevel")
            {
                foreach (XmlNode item3 in item2)
                {
                    CLevelSaveInfo cLevelSaveInfo = new CLevelSaveInfo();
                    if (MyUtils.GetAttribute(item3, "id", ref value))
                    {
                        cLevelSaveInfo.nID = int.Parse(value);
                    }
                    if (MyUtils.GetAttribute(item3, "isignorecg", ref value))
                    {
                        cLevelSaveInfo.isIgnoreCG = bool.Parse(value);
                    }
                    m_ltLevelSaveInfo.Add(cLevelSaveInfo);
                }
            }
            else if (item2.Name == "character")
            {
                if (MyUtils.GetAttribute(item2, "select", ref value))
                {
                    m_nCurCharID = int.Parse(value);
                }
                foreach (XmlNode item4 in item2)
                {
                    if (!(item4.Name != "node") && MyUtils.GetAttribute(item4, "id", ref value))
                    {
                        int nCharID = int.Parse(value);
                        int nLevel = 1;
                        int nExp = 0;
                        if (MyUtils.GetAttribute(item4, "level", ref value))
                        {
                            nLevel = int.Parse(value);
                        }
                        if (MyUtils.GetAttribute(item4, "exp", ref value))
                        {
                            nExp = int.Parse(value);
                        }
                        SetCharacter(nCharID, nLevel, nExp);
                    }
                }
            }
            else if (item2.Name == "weapon")
            {
                if (MyUtils.GetAttribute(item2, "select", ref value))
                {
                    string[] array = value.Split(',');
                    for (int l = 0; l < array.Length && l < m_arrSelectWeapon.Length; l++)
                    {
                        m_arrSelectWeapon[l] = int.Parse(array[l]);
                    }
                }
                foreach (XmlNode item5 in item2)
                {
                    if (!(item5.Name != "node") && MyUtils.GetAttribute(item5, "id", ref value))
                    {
                        int nWeaponID = int.Parse(value);
                        int nWeaponLevel = 0;
                        if (MyUtils.GetAttribute(item5, "level", ref value))
                        {
                            nWeaponLevel = int.Parse(value);
                        }
                        SetWeaponLevel(nWeaponID, nWeaponLevel);
                    }
                }
            }
            else if (item2.Name == "avatar")
            {
                if (MyUtils.GetAttribute(item2, "avatarhead", ref value))
                {
                    AvatarHead = int.Parse(value);
                }
                if (MyUtils.GetAttribute(item2, "avatarupper", ref value))
                {
                    AvatarUpper = int.Parse(value);
                }
                if (MyUtils.GetAttribute(item2, "avatarlower", ref value))
                {
                    AvatarLower = int.Parse(value);
                }
                if (MyUtils.GetAttribute(item2, "avatarheadup", ref value))
                {
                    AvatarHeadup = int.Parse(value);
                }
                if (MyUtils.GetAttribute(item2, "avatarneck", ref value))
                {
                    AvatarNeck = int.Parse(value);
                }
                if (MyUtils.GetAttribute(item2, "avatarwrist", ref value))
                {
                    AvatarWrist = int.Parse(value);
                }
                if (MyUtils.GetAttribute(item2, "avatarbadge", ref value))
                {
                    AvatarBadge = int.Parse(value);
                }
                if (MyUtils.GetAttribute(item2, "avatarstone", ref value))
                {
                    AvatarStone = int.Parse(value);
                }
                foreach (XmlNode item6 in item2)
                {
                    if (!(item6.Name != "node") && MyUtils.GetAttribute(item6, "id", ref value))
                    {
                        int avatarid = int.Parse(value);
                        int avatarlevel = 0;
                        if (MyUtils.GetAttribute(item6, "level", ref value))
                        {
                            avatarlevel = int.Parse(value);
                        }
                        SetAvatar(avatarid, avatarlevel);
                    }
                }
            }
            else if (item2.Name == "skill")
            {
                foreach (XmlNode item7 in item2)
                {
                    if (item7.Name == "selectnode")
                    {
                        if (!MyUtils.GetAttribute(item7, "charid", ref value))
                        {
                            continue;
                        }
                        int nCharID2 = int.Parse(value);
                        if (MyUtils.GetAttribute(item7, "select", ref value))
                        {
                            string[] array = value.Split(',');
                            for (int m = 0; m < array.Length; m++)
                            {
                                SetSelectPassiveSkill(nCharID2, m, int.Parse(array[m]));
                            }
                        }
                    }
                    else if (item7.Name == "node")
                    {
                        if (MyUtils.GetAttribute(item7, "id", ref value))
                        {
                            int nSkillID = int.Parse(value);
                            int nLevel2 = 0;
                            if (MyUtils.GetAttribute(item7, "level", ref value))
                            {
                                nLevel2 = int.Parse(value);
                            }
                            SetPassiveSkill(nSkillID, nLevel2);
                        }
                    }
                    else if (item7.Name == "node2" && MyUtils.GetAttribute(item7, "id", ref value))
                    {
                        int nSkillID2 = int.Parse(value);
                        int nLevel3 = 0;
                        if (MyUtils.GetAttribute(item7, "level", ref value))
                        {
                            nLevel3 = int.Parse(value);
                        }
                        SetSkill(nSkillID2, nLevel3);
                    }
                }
            }
            else if (item2.Name == "equipstone")
            {
                if (MyUtils.GetAttribute(item2, "select", ref value))
                {
                    m_nCurEquipStone = int.Parse(value);
                }
                foreach (XmlNode item8 in item2)
                {
                    if (!(item8.Name != "node") && MyUtils.GetAttribute(item8, "id", ref value))
                    {
                        int nItemID = int.Parse(value);
                        int nLevel4 = 0;
                        if (MyUtils.GetAttribute(item8, "level", ref value))
                        {
                            nLevel4 = int.Parse(value);
                        }
                        SetEquipStone(nItemID, nLevel4);
                    }
                }
            }
            else if (item2.Name == "materials")
            {
                foreach (XmlNode item9 in item2)
                {
                    if (!(item9.Name != "node") && MyUtils.GetAttribute(item9, "id", ref value))
                    {
                        int nItemID2 = int.Parse(value);
                        int nCount = 0;
                        if (MyUtils.GetAttribute(item9, "count", ref value))
                        {
                            nCount = int.Parse(value);
                        }
                        SetMaterialNum(nItemID2, nCount);
                    }
                }
            }
            else if (item2.Name == "unlocksign")
            {
                foreach (XmlNode item10 in item2)
                {
                    if (item10.Name == "promptsign")
                    {
                        int num2 = -1;
                        int id = -1;
                        if (MyUtils.GetAttribute(item2, "type", ref value))
                        {
                            num2 = int.Parse(value);
                        }
                        if (MyUtils.GetAttribute(item2, "id", ref value))
                        {
                            id = int.Parse(value);
                        }
                        if (num2 != -1)
                        {
                            AddUnlockSign(num2, id);
                        }
                    }
                    else if (item10.Name == "weaponsign")
                    {
                        int num3 = 0;
                        int value2 = 0;
                        if (MyUtils.GetAttribute(item10, "id", ref value))
                        {
                            num3 = int.Parse(value);
                        }
                        if (MyUtils.GetAttribute(item10, "sign", ref value))
                        {
                            value2 = int.Parse(value);
                        }
                        if (num3 > 0 && !m_dictWeaponSign.ContainsKey(num3))
                        {
                            m_dictWeaponSign.Add(num3, value2);
                        }
                    }
                    else if (item10.Name == "avatarsign")
                    {
                        int num4 = 0;
                        int value3 = 0;
                        if (MyUtils.GetAttribute(item10, "id", ref value))
                        {
                            num4 = int.Parse(value);
                        }
                        if (MyUtils.GetAttribute(item10, "sign", ref value))
                        {
                            value3 = int.Parse(value);
                        }
                        if (num4 > 0 && !m_dictWeaponSign.ContainsKey(num4))
                        {
                            m_dictAvatarSign.Add(num4, value3);
                        }
                    }
                    else if (item10.Name == "equipstonesign")
                    {
                        int num5 = 0;
                        int value4 = 0;
                        if (MyUtils.GetAttribute(item10, "id", ref value))
                        {
                            num5 = int.Parse(value);
                        }
                        if (MyUtils.GetAttribute(item10, "sign", ref value))
                        {
                            value4 = int.Parse(value);
                        }
                        if (num5 > 0 && !m_dictEquipStoneSign.ContainsKey(num5))
                        {
                            m_dictEquipStoneSign.Add(num5, value4);
                        }
                    }
                    else if (item10.Name == "skillsign")
                    {
                        int num6 = 0;
                        int value5 = 0;
                        if (MyUtils.GetAttribute(item10, "id", ref value))
                        {
                            num6 = int.Parse(value);
                        }
                        if (MyUtils.GetAttribute(item10, "sign", ref value))
                        {
                            value5 = int.Parse(value);
                        }
                        if (num6 > 0 && !m_dictSkillSign.ContainsKey(num6))
                        {
                            m_dictSkillSign.Add(num6, value5);
                        }
                    }
                    else if (item10.Name == "charactersign")
                    {
                        int num7 = 0;
                        int value6 = 0;
                        if (MyUtils.GetAttribute(item10, "id", ref value))
                        {
                            num7 = int.Parse(value);
                        }
                        if (MyUtils.GetAttribute(item10, "sign", ref value))
                        {
                            value6 = int.Parse(value);
                        }
                        if (num7 > 0 && !m_dictCharacterSign.ContainsKey(num7))
                        {
                            m_dictCharacterSign.Add(num7, value6);
                        }
                    }
                }
            }
            else if (item2.Name == "transaction")
            {
                m_ltIAPTransactionInfo.Clear();
                foreach (XmlNode item11 in item2)
                {
                    if (item11.Name == "node")
                    {
                        if (!MyUtils.GetAttribute(item11, "key", ref value))
                        {
                            return;
                        }
                        CIAPTransactionInfo cIAPTransactionInfo = new CIAPTransactionInfo();
                        cIAPTransactionInfo.m_sIAPKey = value;
                        if (MyUtils.GetAttribute(item11, "identifier", ref value))
                        {
                            cIAPTransactionInfo.m_sIdentifier = value;
                        }
                        if (MyUtils.GetAttribute(item11, "receipt", ref value))
                        {
                            cIAPTransactionInfo.m_sReceipt = value;
                        }
                        m_ltIAPTransactionInfo.Add(cIAPTransactionInfo);
                    }
                }
            }
            else if (item2.Name == "crystalinbackground")
            {
                m_ltCrystalInBackground.Clear();
                foreach (XmlNode item12 in item2)
                {
                    if (item12.Name == "node")
                    {
                        CCrystalInBackground cCrystalInBackground = new CCrystalInBackground();
                        if (MyUtils.GetAttribute(item12, "combinekey", ref value))
                        {
                            cCrystalInBackground.m_sCombineKey = value;
                        }
                        if (MyUtils.GetAttribute(item12, "money", ref value))
                        {
                            cCrystalInBackground.m_fMoney = float.Parse(value);
                        }
                        if (MyUtils.GetAttribute(item12, "crystal", ref value))
                        {
                            cCrystalInBackground.m_nCrystal.Set(int.Parse(value));
                        }
                        m_ltCrystalInBackground.Add(cCrystalInBackground);
                    }
                }
            }
            else if (item2.Name == "achievement")
            {
                m_dictAchievementData.Clear();
                foreach (XmlNode childNode in item2.ChildNodes)
                {
                    if (!MyUtils.GetAttribute(childNode, "id", ref value))
                    {
                        continue;
                    }
                    CAchievementData cAchievementData = new CAchievementData();
                    cAchievementData.nID = int.Parse(value);
                    if (MyUtils.GetAttribute(childNode, "state", ref value))
                    {
                        cAchievementData.nState = int.Parse(value);
                    }
                    if (MyUtils.GetAttribute(childNode, "value", ref value))
                    {
                        cAchievementData.nCurValue = int.Parse(value);
                    }
                    if (MyUtils.GetAttribute(childNode, "isgotreward", ref value))
                    {
                        string[] array = value.Split(',');
                        for (int n = 0; n < array.Length && n < 3; n++)
                        {
                            cAchievementData.SetGotReward(n, bool.Parse(array[n]));
                        }
                    }
                    AddAchiData(cAchievementData.nID, cAchievementData);
                }
            }
            else if (item2.Name == "freeweapon")
            {
                if (MyUtils.GetAttribute(item2, "list", ref value))
                {
                    m_ltFreeWeapon.Clear();
                    string[] array = value.Split(',');
                    for (int num8 = 0; num8 < array.Length; num8++)
                    {
                        AddFreeWeapon(int.Parse(array[num8]));
                    }
                }
            }
            else if (item2.Name == "friends")
            {
                m_ltFriends.Clear();
                foreach (XmlNode childNode2 in item2.ChildNodes)
                {
                    if (MyUtils.GetAttribute(childNode2, "id", ref value))
                    {
                        AddFriend(value);
                    }
                }
            }
            else
            {
                if (!(item2.Name == "titles"))
                {
                    continue;
                }
                m_ltTitle.Clear();
                m_ltTitle.Add(1);
                foreach (XmlNode childNode3 in item2.ChildNodes)
                {
                    if (MyUtils.GetAttribute(childNode3, "id", ref value))
                    {
                        int item = int.Parse(value);
                        if (!m_ltTitle.Contains(item))
                        {
                            m_ltTitle.Add(item);
                        }
                    }
                }
            }
        }
    }

    public List<int> GetLevelList()
    {
        return m_ltLevelList;
    }

    public List<CLevelSaveInfo> GetLevelSaveInfoData()
    {
        return m_ltLevelSaveInfo;
    }

    public List<int> GetDailyTask()
    {
        return m_ltDailyTask;
    }

    public void AddUnlockSign(int type, int id)
    {
        CUnlockSign cUnlockSign = new CUnlockSign();
        cUnlockSign.m_nType = type;
        cUnlockSign.m_nID = id;
        m_ltUnlockSign.Add(cUnlockSign);
    }

    public List<CUnlockSign> GetUnlockSignList()
    {
        return m_ltUnlockSign;
    }

    public Dictionary<int, int> GetMaterialData()
    {
        return m_dictMaterials;
    }

    public Dictionary<int, int> GetWeaponData()
    {
        return m_dictWeapon;
    }

    public Dictionary<int, int> GetEquipStoneData()
    {
        return m_dictEquipStone;
    }

    public CCharSaveInfo GetCharacter(int nCharID)
    {
        if (!m_dictCharSaveInfo.ContainsKey(nCharID))
        {
            return null;
        }
        return m_dictCharSaveInfo[nCharID];
    }

    public Dictionary<int, int> GetPassiveSkillData()
    {
        return m_dictPassiveSkill;
    }

    public bool GetPassiveSkill(int nSkillID, ref int nSkillLevel)
    {
        if (!m_dictPassiveSkill.ContainsKey(nSkillID))
        {
            return false;
        }
        nSkillLevel = m_dictPassiveSkill[nSkillID];
        return true;
    }

    public bool GetSkill(int nSkillID, ref int nSkillLevel)
    {
        if (!m_dictSkill.ContainsKey(nSkillID))
        {
            nSkillLevel = 1;
            return true;
        }
        nSkillLevel = m_dictSkill[nSkillID];
        return true;
    }

    public int GetPassiveSkillCount()
    {
        if (m_dictPassiveSkill == null)
        {
            return 0;
        }
        int num = 0;
        foreach (int value in m_dictPassiveSkill.Values)
        {
            if (value > 0)
            {
                num++;
            }
        }
        return num;
    }

    public int GetWeaponCount()
    {
        if (m_dictWeapon == null)
        {
            return 0;
        }
        int num = 0;
        foreach (int value in m_dictWeapon.Values)
        {
            if (value > 0)
            {
                num++;
            }
        }
        return num;
    }

    public bool GetEquipStone(int nItemID, ref int nItemLevel)
    {
        if (!m_dictEquipStone.ContainsKey(nItemID))
        {
            return false;
        }
        nItemLevel = m_dictEquipStone[nItemID];
        return true;
    }

    public bool GetWeaponLevel(int nWeaponID, ref int nLevel)
    {
        if (!m_dictWeapon.ContainsKey(nWeaponID))
        {
            return false;
        }
        nLevel = m_dictWeapon[nWeaponID];
        return true;
    }

    public int GetSelectWeapon(int nIndex)
    {
        if (nIndex < 0 || nIndex >= m_arrSelectWeapon.Length)
        {
            return -1;
        }
        return m_arrSelectWeapon[nIndex];
    }

    public bool HasSelectWeapon(int nWeaponID)
    {
        for (int i = 0; i < m_arrSelectWeapon.Length; i++)
        {
            if (m_arrSelectWeapon[i] != -1 && m_arrSelectWeapon[i] == nWeaponID)
            {
                return true;
            }
        }
        return false;
    }

    public int GetSelectPassiveSkill(int nCharID, int nIndex)
    {
        if (!m_dictSelectPassiveSkill.ContainsKey(nCharID))
        {
            return -1;
        }
        int[] array = m_dictSelectPassiveSkill[nCharID];
        if (nIndex < 0 || nIndex >= array.Length)
        {
            return -1;
        }
        return array[nIndex];
    }

    public bool HasSelectPassiveSkill(int nCharID, int nSkillID)
    {
        if (!m_dictSelectPassiveSkill.ContainsKey(nCharID))
        {
            return false;
        }
        int[] array = m_dictSelectPassiveSkill[nCharID];
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != -1 && array[i] == nSkillID)
            {
                return true;
            }
        }
        return false;
    }

    public Dictionary<int, int> GetWeaponSignData()
    {
        return m_dictWeaponSign;
    }

    public Dictionary<int, int> GetAvatarSignData()
    {
        return m_dictAvatarSign;
    }

    public Dictionary<int, int> GetSkillSignData()
    {
        return m_dictSkillSign;
    }

    public Dictionary<int, int> GetEquipStoneSignData()
    {
        return m_dictEquipStoneSign;
    }

    public Dictionary<int, int> GetCharacterSignData()
    {
        return m_dictCharacterSign;
    }

    public int GetMaterialNum(int nItemID)
    {
        if (!m_dictMaterials.ContainsKey(nItemID))
        {
            return -1;
        }
        return m_dictMaterials[nItemID];
    }

    public void AddMaterialNum(int nItemID, int nCount)
    {
        if (nItemID != -1)
        {
            if (!m_dictMaterials.ContainsKey(nItemID))
            {
                m_dictMaterials.Add(nItemID, nCount);
                return;
            }
            Dictionary<int, int> dictMaterials;
            Dictionary<int, int> dictionary = (dictMaterials = m_dictMaterials);
            int key;
            int key2 = (key = nItemID);
            key = dictMaterials[key];
            dictionary[key2] = key + nCount;
        }
    }

    public void DelMaterial(int nItemID)
    {
        if (m_dictMaterials.ContainsKey(nItemID))
        {
            m_dictMaterials.Remove(nItemID);
        }
    }

    public void SetMaterialNum(int nItemID, int nCount)
    {
        if (nItemID != -1)
        {
            if (!m_dictMaterials.ContainsKey(nItemID))
            {
                m_dictMaterials.Add(nItemID, nCount);
            }
            else
            {
                m_dictMaterials[nItemID] = nCount;
            }
        }
    }

    public int CheckStashVolume(int nCount)
    {
        int stashCountMax = StashCountMax;
        int stashCount = StashCount;
        if (stashCount + nCount > stashCountMax)
        {
            return stashCountMax - stashCount;
        }
        return nCount;
    }

    public void SetWeaponLevel(int nWeaponID, int nWeaponLevel)
    {
        if (!m_dictWeapon.ContainsKey(nWeaponID))
        {
            m_dictWeapon.Add(nWeaponID, nWeaponLevel);
        }
        else
        {
            m_dictWeapon[nWeaponID] = nWeaponLevel;
        }
    }

    public void UnlockWeapon(int nWeaponID)
    {
        if (!m_dictWeapon.ContainsKey(nWeaponID))
        {
            m_dictWeapon.Add(nWeaponID, -1);
        }
    }

    public void SetCharacter(int nCharID, int nLevel, int nExp)
    {
        if (!m_dictCharSaveInfo.ContainsKey(nCharID))
        {
            m_dictCharSaveInfo.Add(nCharID, new CCharSaveInfo(nCharID));
        }
        m_dictCharSaveInfo[nCharID].nLevel = nLevel;
        m_dictCharSaveInfo[nCharID].nExp = nExp;
    }

    public void UnlockCharacter(int nCharID)
    {
        if (!m_dictCharSaveInfo.ContainsKey(nCharID))
        {
            m_dictCharSaveInfo.Add(nCharID, new CCharSaveInfo(nCharID));
            m_dictCharSaveInfo[nCharID].nLevel = -1;
            m_dictCharSaveInfo[nCharID].nExp = 0;
        }
    }

    public void SetPassiveSkill(int nSkillID, int nLevel)
    {
        if (!m_dictPassiveSkill.ContainsKey(nSkillID))
        {
            m_dictPassiveSkill.Add(nSkillID, nLevel);
        }
        m_dictPassiveSkill[nSkillID] = nLevel;
    }

    public void UnlockPassiveSkill(int nSkillID)
    {
        if (!m_dictPassiveSkill.ContainsKey(nSkillID))
        {
            m_dictPassiveSkill.Add(nSkillID, -1);
        }
    }

    public void SetSkill(int nSkillID, int nLevel)
    {
        if (!m_dictSkill.ContainsKey(nSkillID))
        {
            m_dictSkill.Add(nSkillID, nLevel);
        }
        m_dictSkill[nSkillID] = nLevel;
    }

    public void SetEquipStone(int nItemID, int nLevel)
    {
        if (!m_dictEquipStone.ContainsKey(nItemID))
        {
            m_dictEquipStone.Add(nItemID, nLevel);
        }
        else
        {
            m_dictEquipStone[nItemID] = nLevel;
        }
    }

    public void UnlockEquipStone(int nItemID)
    {
        if (!m_dictEquipStone.ContainsKey(nItemID))
        {
            m_dictEquipStone.Add(nItemID, -1);
        }
    }

    public void AddGold(int nGold)
    {
        int num = m_nGold.Get();
        num += nGold;
        if (num < 0)
        {
            num = 0;
        }
        m_nGold.Set(num);
    }

    public void AddCrystal(int nCrystal)
    {
        int num = m_nCrystal.Get();
        int num2 = num + nCrystal;
        if (num2 < 0)
        {
            num2 = 0;
        }
        m_nCrystal.Set(num2);
        if (nCrystal > 0)
        {
            m_nCrystalTotalGain += nCrystal;
        }
        if (nCrystal < 0)
        {
            if (num2 == 0)
            {
                m_nCrystalTotalConsume += num;
            }
            else
            {
                m_nCrystalTotalConsume += nCrystal;
            }
        }
    }

    public void AddCrystalInBackground(int nCrystal, float fMoney, string sCombineKey)
    {
        foreach (CCrystalInBackground item in m_ltCrystalInBackground)
        {
            if (item.m_sCombineKey == sCombineKey)
            {
                return;
            }
        }
        CCrystalInBackground cCrystalInBackground = new CCrystalInBackground();
        cCrystalInBackground.m_sCombineKey = sCombineKey;
        cCrystalInBackground.m_fMoney = fMoney;
        cCrystalInBackground.m_nCrystal.Set(nCrystal);
        m_ltCrystalInBackground.Add(cCrystalInBackground);
    }

    public void ClearCrystalInBackground()
    {
        m_ltCrystalInBackground.Clear();
    }

    public void AddHunterExp(int nHunterExp)
    {
        HunterExpTotal += nHunterExp;
        if (HunterExpTotal < 0)
        {
            HunterExpTotal = 0;
        }
    }

    public void SetSelectWeapon(int nIndex, int nWeaponID)
    {
        if (nIndex >= 0 && nIndex < m_arrSelectWeapon.Length)
        {
            m_arrSelectWeapon[nIndex] = nWeaponID;
        }
    }

    public void SetSelectPassiveSkill(int nCharID, int nIndex, int nPassiveSkillID)
    {
        if (!m_dictSelectPassiveSkill.ContainsKey(nCharID))
        {
            m_dictSelectPassiveSkill.Add(nCharID, new int[3] { -1, -1, -1 });
        }
        int[] array = m_dictSelectPassiveSkill[nCharID];
        if (nIndex >= 0 && nIndex < array.Length)
        {
            array[nIndex] = nPassiveSkillID;
        }
    }

    public void UnlockNewLevelPrepare()
    {
        m_bUnLockLevel = true;
    }

    public void UnlockNewLevelConfirm(int nNewLevel)
    {
        m_bUnLockLevel = false;
        m_nLatestLevel = nNewLevel;
    }

    public bool GetWeaponSign(int nWeaponID, ref int nSignState)
    {
        if (!m_dictWeaponSign.ContainsKey(nWeaponID))
        {
            return false;
        }
        nSignState = m_dictWeaponSign[nWeaponID];
        return true;
    }

    public bool GetAvatarSign(int nAvatarID, ref int nSignState)
    {
        if (!m_dictAvatarSign.ContainsKey(nAvatarID))
        {
            return false;
        }
        nSignState = m_dictAvatarSign[nAvatarID];
        return true;
    }

    public void SetWeaponSign(int nWeaponID, int nSignState)
    {
        if (!m_dictWeaponSign.ContainsKey(nWeaponID))
        {
            m_dictWeaponSign.Add(nWeaponID, nSignState);
        }
        else
        {
            m_dictWeaponSign[nWeaponID] = nSignState;
        }
    }

    public void SetAvatarSign(int nAvatarID, int nSignState)
    {
        if (!m_dictAvatarSign.ContainsKey(nAvatarID))
        {
            m_dictAvatarSign.Add(nAvatarID, nSignState);
        }
        else
        {
            m_dictAvatarSign[nAvatarID] = nSignState;
        }
    }

    public bool GetEquipStoneSign(int nID, ref int nSignState)
    {
        if (!m_dictEquipStoneSign.ContainsKey(nID))
        {
            return false;
        }
        nSignState = m_dictEquipStoneSign[nID];
        return true;
    }

    public void SetEquipStoneSign(int nID, int nSignState)
    {
        if (!m_dictEquipStoneSign.ContainsKey(nID))
        {
            m_dictEquipStoneSign.Add(nID, nSignState);
        }
        else
        {
            m_dictEquipStoneSign[nID] = nSignState;
        }
    }

    public bool GetSkillSign(int nID, ref int nSignState)
    {
        if (!m_dictSkillSign.ContainsKey(nID))
        {
            return false;
        }
        nSignState = m_dictSkillSign[nID];
        return true;
    }

    public void SetSkillSign(int nID, int nSignState)
    {
        if (!m_dictSkillSign.ContainsKey(nID))
        {
            m_dictSkillSign.Add(nID, nSignState);
        }
        else
        {
            m_dictSkillSign[nID] = nSignState;
        }
    }

    public bool GetCharacterSign(int nID, ref int nSignState)
    {
        if (!m_dictCharacterSign.ContainsKey(nID))
        {
            return false;
        }
        nSignState = m_dictCharacterSign[nID];
        return true;
    }

    public void SetCharacterSign(int nID, int nSignState)
    {
        if (!m_dictCharacterSign.ContainsKey(nID))
        {
            m_dictCharacterSign.Add(nID, nSignState);
        }
        else
        {
            m_dictCharacterSign[nID] = nSignState;
        }
    }

    public bool IsLevelPassed(int nLevel)
    {
        foreach (CLevelSaveInfo item in m_ltLevelSaveInfo)
        {
            if (item.nID == nLevel)
            {
                return true;
            }
        }
        return false;
    }

    public void SetPassedLevel(int nLevel)
    {
        foreach (CLevelSaveInfo item in m_ltLevelSaveInfo)
        {
            if (item.nID == nLevel)
            {
                return;
            }
        }
        CLevelSaveInfo cLevelSaveInfo = new CLevelSaveInfo();
        cLevelSaveInfo.nID = nLevel;
        cLevelSaveInfo.isIgnoreCG = true;
        m_ltLevelSaveInfo.Add(cLevelSaveInfo);
    }

    public void ClearPassedLevel()
    {
        m_ltLevelSaveInfo.Clear();
    }

    public void SetLevelIgnoreCG(int nLevel, bool bIgnore)
    {
        foreach (CLevelSaveInfo item in m_ltLevelSaveInfo)
        {
            if (item.nID != nLevel)
            {
                continue;
            }
            item.isIgnoreCG = bIgnore;
            break;
        }
    }

    public bool IsLevelIgnoreCG(int nLevel)
    {
        foreach (CLevelSaveInfo item in m_ltLevelSaveInfo)
        {
            if (item.nID == nLevel)
            {
                return item.isIgnoreCG;
            }
        }
        return false;
    }

    public string Pack()
    {
        XmlDocument xmlDocument = new XmlDocument();
        SaveData(xmlDocument);
        StringWriter stringWriter = new StringWriter();
        xmlDocument.Save(stringWriter);
        string zipedcontent = stringWriter.ToString();
        MyUtils.ZipString(zipedcontent, ref zipedcontent);
        return zipedcontent;
    }

    public bool UnPack(string sData)
    {
        Debug.Log("UnPack = " + sData);
        try
        {
            MyUtils.UnZipString(sData, ref sData);
            LoadData(sData);
            return true;
        }
        catch
        {
            Debug.Log("exception UnPack");
            return false;
        }
    }

    public void AddIAPTransactionInfo(string sIAPKey, string sIdentifier, string sReceipt, string sSignature, string sRandom, int nRat, int nRatA, int nRatB)
    {
        CIAPTransactionInfo cIAPTransactionInfo = null;
        foreach (CIAPTransactionInfo item in m_ltIAPTransactionInfo)
        {
            if (item.m_sIAPKey == sIAPKey && item.m_sIdentifier == sIdentifier && item.m_sReceipt == sReceipt)
            {
                cIAPTransactionInfo = item;
            }
        }
        if (cIAPTransactionInfo == null)
        {
            cIAPTransactionInfo = new CIAPTransactionInfo();
            m_ltIAPTransactionInfo.Add(cIAPTransactionInfo);
        }
        cIAPTransactionInfo.m_sIAPKey = sIAPKey;
        cIAPTransactionInfo.m_sIdentifier = sIdentifier;
        cIAPTransactionInfo.m_sReceipt = sReceipt;
        cIAPTransactionInfo.m_sSignature = sSignature;
        cIAPTransactionInfo.m_sRandom = sRandom;
        cIAPTransactionInfo.m_nRat = nRat;
        cIAPTransactionInfo.m_nRatA = nRatA;
        cIAPTransactionInfo.m_nRatB = nRatB;
    }

    public bool GetIAPTransactionInfo(ref string sIAPKey, ref string sIdentifier, ref string sReceipt, ref string sSignature, ref string sRandom, ref int nRat, ref int nRatA, ref int nRatB)
    {
        if (m_ltIAPTransactionInfo.Count < 1)
        {
            return false;
        }
        sIAPKey = m_ltIAPTransactionInfo[0].m_sIAPKey;
        sIdentifier = m_ltIAPTransactionInfo[0].m_sIdentifier;
        sReceipt = m_ltIAPTransactionInfo[0].m_sReceipt;
        sSignature = m_ltIAPTransactionInfo[0].m_sSignature;
        sRandom = m_ltIAPTransactionInfo[0].m_sRandom;
        nRat = m_ltIAPTransactionInfo[0].m_nRat;
        nRatA = m_ltIAPTransactionInfo[0].m_nRatA;
        nRatB = m_ltIAPTransactionInfo[0].m_nRatB;
        return true;
    }

    public void DelIAPTransactionInfo(string sIAPKey, string sIdentifier, string sReceipt)
    {
        foreach (CIAPTransactionInfo item in m_ltIAPTransactionInfo)
        {
            if (item.m_sIAPKey == sIAPKey && item.m_sIdentifier == sIdentifier && item.m_sReceipt == sReceipt)
            {
                m_ltIAPTransactionInfo.Remove(item);
                break;
            }
        }
    }

    public void AddAchiData(int nID, CAchievementData data)
    {
        if (!m_dictAchievementData.ContainsKey(nID))
        {
            m_dictAchievementData.Add(nID, data);
        }
    }

    public CAchievementData GetAchiData(int nID)
    {
        if (!m_dictAchievementData.ContainsKey(nID))
        {
            return null;
        }
        return m_dictAchievementData[nID];
    }

    public Dictionary<int, CAchievementData> GetAchiDataData()
    {
        return m_dictAchievementData;
    }

    public void AddFreeWeapon(int nWeaponID)
    {
        if (!m_ltFreeWeapon.Contains(nWeaponID))
        {
            m_ltFreeWeapon.Add(nWeaponID);
        }
    }

    public void DelFreeWeapon(int nWeaponID)
    {
        if (m_ltFreeWeapon.Contains(nWeaponID))
        {
            m_ltFreeWeapon.Remove(nWeaponID);
        }
    }

    public bool IsFreeWeaponID(int nWeaponID)
    {
        if (!m_ltFreeWeapon.Contains(nWeaponID))
        {
            return false;
        }
        return true;
    }

    public void RefreshServerDateTime(DateTime now)
    {
        iGameState gameState = iGameApp.GetInstance().m_GameState;
        if (gameState != null)
        {
            Debug.Log(string.Concat("lastday is ", m_lastLoginTime, " today is ", now));
            gameState.m_nDaysFromLastLogin = CalcPassedDays(m_lastLoginTime, now);
            gameState.m_DayOfWeek = now.DayOfWeek;
            m_lastLoginTime = now;
            if (gameState.m_nDaysFromLastLogin > 0)
            {
                RefreshDailyRewardCount(gameState.m_nDaysFromLastLogin);
                RefreshDailyTask(now.DayOfWeek);
                m_dictWorldMonsterKill.Clear();
            }
        }
    }

    public void RefreshDailyRewardCount(int nPassDays)
    {
        Debug.Log("days from lastlogin = " + nPassDays);
        if (nPassDays < 1)
        {
            return;
        }
        if (m_nDailyRewardCount == 0)
        {
            m_nDailyRewardCount = 1;
            m_nDailyRewardHasGot = 0;
        }
        else if (nPassDays == 1)
        {
            m_nDailyRewardCount++;
            if (m_nDailyRewardCount > 7)
            {
                m_nDailyRewardCount = 1;
                m_nDailyRewardHasGot = 0;
            }
        }
        else if (nPassDays > 1)
        {
            m_nDailyRewardCount = 1;
            m_nDailyRewardHasGot = 0;
        }
    }

    public void RefreshDailyTask(DayOfWeek dayofweek)
    {
        Debug.Log("today is " + dayofweek);
        List<int> list = new List<int>();
        foreach (CAchievementData value in m_dictAchievementData.Values)
        {
            bool flag = false;
            for (int i = 0; i < m_ltDailyTask.Count; i++)
            {
                if (value.nID == m_ltDailyTask[i])
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                value.Reset();
                list.Add(value.nID);
            }
        }
        m_ltDailyTask.Clear();
        foreach (int item in list)
        {
            m_dictAchievementData.Remove(item);
        }
        iGameData gameData = iGameApp.GetInstance().m_GameData;
        if (gameData == null)
        {
            Debug.LogError("gamedata is null/..");
            return;
        }
        iAchievementCenter achievementCenter = gameData.GetAchievementCenter();
        if (achievementCenter == null)
        {
            Debug.LogError("gachievement is null/..");
            return;
        }
        iDailyTaskCenter dailyTaskCenter = gameData.GetDailyTaskCenter();
        if (dailyTaskCenter == null)
        {
            Debug.LogError("dauily Task is null/..");
            return;
        }
        CDailyTaskInfo cDailyTaskInfo = dailyTaskCenter.Get((int)dayofweek);
        if (cDailyTaskInfo == null || cDailyTaskInfo.ltTask == null || cDailyTaskInfo.ltTask.Count < 1)
        {
            Debug.LogError("dailytask is null/..");
            return;
        }
        List<CAchievementInfo> dailyAchievementList = achievementCenter.GetDailyAchievementList();
        if (dailyAchievementList == null || dailyAchievementList.Count < 1)
        {
            return;
        }
        foreach (int item2 in cDailyTaskInfo.ltTask)
        {
            List<CAchievementInfo> list2 = new List<CAchievementInfo>();
            foreach (CAchievementInfo item3 in dailyAchievementList)
            {
                if (item3 != null && item2 == item3.nType)
                {
                    list2.Add(item3);
                }
            }
            if (list2.Count >= 1)
            {
                int index = UnityEngine.Random.Range(0, list2.Count);
                m_ltDailyTask.Add(list2[index].nID);
                dailyAchievementList.Remove(list2[index]);
            }
        }
        foreach (CAchievementData value2 in m_dictAchievementData.Values)
        {
            bool flag2 = false;
            for (int j = 0; j < m_ltDailyTask.Count; j++)
            {
                if (value2.nID == m_ltDailyTask[j])
                {
                    flag2 = true;
                    break;
                }
            }
            if (flag2)
            {
                value2.Reset();
            }
        }
    }

    protected int CalcPassedDays(DateTime date1, DateTime date2)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0);
        TimeSpan timeSpan = date1 - dateTime;
        return (date2 - dateTime).Days - timeSpan.Days;
    }

    public int GetWorldMonsterKill(int nMobID)
    {
        if (!m_dictWorldMonsterKill.ContainsKey(nMobID))
        {
            return 0;
        }
        return m_dictWorldMonsterKill[nMobID];
    }

    public void SetWorldMonsterKill(int nMobID, int nNum)
    {
        if (!m_dictWorldMonsterKill.ContainsKey(nMobID))
        {
            m_dictWorldMonsterKill.Add(nMobID, nNum);
        }
        else
        {
            m_dictWorldMonsterKill[nMobID] = nNum;
        }
    }

    public void AddWorldMonsterKill(int nMobID, int nNum)
    {
        SetWorldMonsterKill(nMobID, GetWorldMonsterKill(nMobID) + nNum);
    }

    public bool GenerateNameCard(ref CNameCardInfo namecardinfo)
    {
        if (namecardinfo == null)
        {
            return false;
        }
        namecardinfo.m_sID = iServerSaveData.GetInstance().CurDeviceId;
        namecardinfo.m_sGCAccount = iServerSaveData.GetInstance().CurGameCenterId;
        namecardinfo.m_sNickName = m_sNickName;
        namecardinfo.m_nTitle = m_nTitle.Get();
        namecardinfo.m_nHunterLvl = m_nHunterLvl.Get();
        namecardinfo.m_nHunterExp = m_nHunterExp.Get();
        namecardinfo.m_nCombatPower = CombatPower;
        namecardinfo.m_nRank = m_nRank;
        namecardinfo.m_nBeAdmired = m_nBeAdmired.Get();
        namecardinfo.m_nGold = m_nGold.Get();
        namecardinfo.m_nCrystal = m_nCrystal.Get();
        namecardinfo.m_fSceneProccess = m_fSceneProccess;
        namecardinfo.m_sSignature = m_sSignature;
        namecardinfo.SetPhoto(m_Photo);
        namecardinfo.m_NCPack.roleid = m_nCurCharID;
        namecardinfo.m_NCPack.head = AvatarHead;
        namecardinfo.m_NCPack.upper = AvatarUpper;
        namecardinfo.m_NCPack.lower = AvatarLower;
        namecardinfo.m_NCPack.headup = AvatarHeadup;
        namecardinfo.m_NCPack.neck = AvatarNeck;
        namecardinfo.m_NCPack.bracelet = AvatarWrist;
        namecardinfo.m_NCPack.weapon = m_arrSelectWeapon;
        return true;
    }

    public void AddFriend(string sId)
    {
        if (sId != null && sId.Length >= 1 && !m_ltFriends.Contains(sId))
        {
            m_ltFriends.Add(sId);
        }
    }

    public void DelFriend(string sId)
    {
        if (sId != null && sId.Length >= 1)
        {
            m_ltFriends.Remove(sId);
        }
    }

    public List<string> GetFriends()
    {
        m_ltFriends = m_ltFriends.Distinct().ToList();
        return m_ltFriends;
    }

    public void ClearFriends()
    {
        m_ltFriends.Clear();
    }

    public bool IsFriend(string sId)
    {
        if (sId == null || sId.Length < 1)
        {
            return false;
        }
        if (m_ltFriends.Contains(sId))
        {
            return true;
        }
        return false;
    }

    public void AddTitle(int nID)
    {
        if (!m_ltTitle.Contains(nID))
        {
            m_ltTitle.Add(nID);
        }
    }

    public void DelTitle(int nID)
    {
        if (m_ltTitle.Contains(nID))
        {
            m_ltTitle.Remove(nID);
        }
    }

    public bool GetTitle(int nID)
    {
        return m_ltTitle.Contains(nID);
    }

    public List<int> GetTitleList()
    {
        return m_ltTitle;
    }

    public void AddKillMonster(int nID, int nCount = 1)
    {
        if (!m_dictKillMonster.ContainsKey(nID))
        {
            m_dictKillMonster.Add(nID, nCount);
            return;
        }
        Dictionary<int, int> dictKillMonster;
        Dictionary<int, int> dictionary = (dictKillMonster = m_dictKillMonster);
        int key;
        int key2 = (key = nID);
        key = dictKillMonster[key];
        dictionary[key2] = key + nCount;
    }

    public void SetkillMonster(int nID, int nCount)
    {
        if (!m_dictKillMonster.ContainsKey(nID))
        {
            m_dictKillMonster.Add(nID, nCount);
        }
        else
        {
            m_dictKillMonster[nID] = nCount;
        }
    }

    public int GetKillMonster(int nID)
    {
        if (!m_dictKillMonster.ContainsKey(nID))
        {
            return 0;
        }
        return m_dictKillMonster[nID];
    }

    public Dictionary<int, int> GetAvatarData()
    {
        return m_dictAvatar;
    }

    public bool GetAvatar(int avatarid, ref int avatarlevel)
    {
        if (!m_dictAvatar.ContainsKey(avatarid))
        {
            return false;
        }
        avatarlevel = m_dictAvatar[avatarid];
        return true;
    }

    public void SetAvatar(int avatarid, int avatarlevel)
    {
        if (!m_dictAvatar.ContainsKey(avatarid))
        {
            m_dictAvatar.Add(avatarid, avatarlevel);
        }
        else
        {
            m_dictAvatar[avatarid] = avatarlevel;
        }
    }

    public void SetPhoto(Texture2D texture)
    {
        if (!(texture == null))
        {
            Texture2D texture2D = texture;
            if (texture2D.width > 40 || texture2D.height > 40)
            {
                texture2D = gyLoadImage.Resize(texture2D, 40, 40);
            }
            m_Photo = texture2D.EncodeToPNG();
        }
    }

    public void SetPhoto(byte[] photo)
    {
        if (photo == null)
        {
            return;
        }
        try
        {
            Texture2D texture2D = new Texture2D(40, 40);
            texture2D.LoadImage(photo);
            if (texture2D.width > 40 || texture2D.height > 40)
            {
                texture2D = gyLoadImage.Resize(texture2D, 40, 40);
            }
            m_Photo = texture2D.EncodeToPNG();
        }
        catch
        {
            m_Photo = null;
        }
    }

    public byte[] GetPhoto()
    {
        return m_Photo;
    }

    public List<CCrystalInBackground> GetCrystalInBackground()
    {
        return m_ltCrystalInBackground;
    }
}
