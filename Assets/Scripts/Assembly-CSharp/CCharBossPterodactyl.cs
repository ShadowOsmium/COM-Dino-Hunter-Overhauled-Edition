using System.Collections.Generic;

public class CCharBossPterodactyl : CCharBoss
{
	public class kPart
	{
		public const int Head = 1;

		public const int Body = 2;
	}

	protected Dictionary<string, int> m_dictPartID;

	public new void Awake()
	{
		base.Awake();
		m_dictPartID = new Dictionary<string, int>();
		m_dictPartID.Add("Bip01 Head", 1);
		m_dictPartID.Add("Bip01 Spine", 2);
	}

	public override void InitAnimData()
	{
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Idle, "idle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.MoveForward, "fly"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Attack, "attack01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Dead, "death01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadFly, "fly_death"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadHeadShoot, "death02"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Hurt, "damage"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtFront, "damage"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtBehind, "damage"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Glide, "fly_circle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_ShowTime, "exit"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_1, "attack02_01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_2, "attack02_02"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_3, "attack05"));
	}

	public override void InitAudioData()
	{
		m_AudioData.Add(kAudioEnum.HitBody, "Fx_Impact_body");
	}

	public override bool AddHardiness(float fDamage, string sBoneName = "")
	{
		bool result = false;
		if (sBoneName != string.Empty)
		{
			if (!m_dictPartID.ContainsKey(sBoneName))
			{
				return false;
			}
			int key = m_dictPartID[sBoneName];
			if (!m_dictBodyPart.ContainsKey(key))
			{
				return false;
			}
			result = AddHardinessValue(m_dictBodyPart[key], fDamage);
		}
		else
		{
			int count = m_dictBodyPart.Count;
			fDamage /= (float)count;
			foreach (CBodyPart value in m_dictBodyPart.Values)
			{
				if (AddHardinessValue(value, fDamage))
				{
					result = true;
				}
			}
		}
		return result;
	}
}
