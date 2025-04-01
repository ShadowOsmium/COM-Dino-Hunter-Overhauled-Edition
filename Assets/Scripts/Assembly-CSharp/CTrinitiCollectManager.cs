using System.Collections;

public class CTrinitiCollectManager
{
	public class kEnumCollect
	{
		public const int register = 1;

		public const int login = 2;

		public const int pay = 3;

		public const int online = 4;

		public const int crystalconsume = 5;

		public const int goldconsume = 6;

		public const int passlevel = 7;

		public const int revive = 8;

		public const int leavegame = 9;

		public const int entercoop = 10;

		public const int quitcoop = 11;

		public const int challeageboss = 12;

		public const int hunterlevel = 13;

		public const int charlevel = 14;
	}

	public class kEnumConsume
	{
		public const string buyweapon = "buyweapon";

		public const string upgradeweapon = "upweapon";

		public const string buyavatar = "buyavatar";

		public const string upgradeavatar = "upavatar";

		public const string buyequip = "buyequip";

		public const string upgradeequip = "upequip";

		public const string unlockskill = "unlockskill";

		public const string buyskill = "buyskill";

		public const string upgradeskill = "upskill";

		public const string unlockcharacter = "unlockcharacter";

		public const string buycharacter = "buycharacter";

		public const string buymaterials = "exchange";

		public const string buybullet = "buybullet";

		public const string revive = "rel";

		public const string buygold = "exgold";

		public const string buystash = "buystash";
	}

	protected static CTrinitiCollectManager m_Instance;

	protected float m_fCoopTime;

	protected float m_fHeartBeatTime = 120f;

	protected float m_fHeartBeatTimeCount;

	public static CTrinitiCollectManager GetInstance()
	{
		if (m_Instance == null)
		{
			m_Instance = new CTrinitiCollectManager();
		}
		return m_Instance;
	}

	public void Update(float deltaTime)
	{
		if (m_fCoopTime >= 0f)
		{
			m_fCoopTime += deltaTime;
			m_fHeartBeatTimeCount += deltaTime;
			if (m_fHeartBeatTimeCount >= m_fHeartBeatTime)
			{
				m_fHeartBeatTimeCount = 0f;
				SendOnline();
			}
		}
		else
		{
			m_fHeartBeatTimeCount = 0f;
		}
	}

	public void SendRegister()
	{
		iTrinitiDataCollect.GetInstance().logEvent(1);
	}

	public void SendLogin()
	{
		iTrinitiDataCollect.GetInstance().logEvent(2);
	}

	public void SendPay(int crystalbeforepay, int count, float cost)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("bcrys", crystalbeforepay.ToString());
		hashtable.Add("gcrys", count.ToString());
		hashtable.Add("num", "1");
		hashtable.Add("iap", cost.ToString());
		iTrinitiDataCollect.GetInstance().logEvent(3, hashtable);
	}

	public void SendOnline()
	{
		iTrinitiDataCollect.GetInstance().logEvent(4);
	}

	public void SendConsumeCrystal(int crystal, string type, int id, int count)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("cnum", crystal.ToString());
		hashtable.Add("typ", type);
		hashtable.Add("gid", id.ToString());
		hashtable.Add("num", count.ToString());
		iTrinitiDataCollect.GetInstance().logEvent(5, hashtable);
	}

	public void SendConsumeGold(int gold, string type, int id, int count)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("gnum", gold.ToString());
		hashtable.Add("typ", type);
		hashtable.Add("gid", id.ToString());
		hashtable.Add("num", count.ToString());
		iTrinitiDataCollect.GetInstance().logEvent(5, hashtable);
	}

	public void SendPassLevel(int levelid)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("mid", levelid.ToString());
		iTrinitiDataCollect.GetInstance().logEvent(7, hashtable);
	}

	public void SendRevive()
	{
		iTrinitiDataCollect.GetInstance().logEvent(8);
	}

	public void SendLeaveGame()
	{
		iTrinitiDataCollect.GetInstance().logEvent(9);
	}

	public void SendEnterCoop()
	{
		iTrinitiDataCollect.GetInstance().logEvent(10);
		m_fCoopTime = 0f;
	}

	public void SendQuitCoop()
	{
		if (m_fCoopTime != -1f)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("utime", m_fCoopTime.ToString());
			iTrinitiDataCollect.GetInstance().logEvent(11, hashtable);
			m_fCoopTime = -1f;
		}
	}

	public void SendChalleageBoss()
	{
		iTrinitiDataCollect.GetInstance().logEvent(12);
	}

	public void SendHunterLevel(int hunterlevel)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("level", hunterlevel.ToString());
		iTrinitiDataCollect.GetInstance().logEvent(13, hashtable);
	}

	public void SendCharLevel(int charid, int charlevel)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("jid", charid.ToString());
		hashtable.Add("jlevel", charlevel.ToString());
		iTrinitiDataCollect.GetInstance().logEvent(14, hashtable);
	}
}
