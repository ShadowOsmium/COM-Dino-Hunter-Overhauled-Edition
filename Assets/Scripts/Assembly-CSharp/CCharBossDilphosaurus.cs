using System.Collections.Generic;

public class CCharBossDilphosaurus : CCharBoss
{
	public class kPart
	{
		public const int Head = 1;

		public const int Body = 2;

		public const int Leg = 3;
	}

	protected Dictionary<string, int> m_dictPartID;

	public new void Awake()
	{
		base.Awake();
		m_dictPartID = new Dictionary<string, int>();
		m_dictPartID.Add("Bip01 Head", 1);
		m_dictPartID.Add("Bip01 Spine", 2);
		m_dictPartID.Add("Bip01 L Foot", 3);
		m_dictPartID.Add("Bip01 R Foot", 3);
	}

	public override void InitAnimData()
	{
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Idle, "Idle01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.MoveForward, "Run01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.TurnLeft, "Left_Rotation01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.TurnRight, "Right_Rotation01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Attack, "Attack01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Dead, "Death01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadHeadShoot, "Death01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Hurt, "Dagame_body01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtFront, "Dagame_body01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtBehind, "Dagame_body01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Roar, "Roar01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_ShowTime, "Roar01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_1, "Attack00"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_2, "Attack00_1"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_3, "Attack01_left"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_4, "Attack01_right"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_5, "Attack01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_6, "Attack_Pee"));
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
