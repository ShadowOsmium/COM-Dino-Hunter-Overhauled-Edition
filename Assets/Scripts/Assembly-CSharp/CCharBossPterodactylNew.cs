using System.Collections.Generic;

public class CCharBossPterodactylNew : CCharBoss
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
		m_dictPartID.Add("Bip01 L Hand", 3);
		m_dictPartID.Add("Bip01 R Hand", 3);
	}

	public override void InitAnimData()
	{
	}

	public override void InitAnimData_Ground()
	{
		m_AnimData.Cleanup();
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Idle, "Ground_Idle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.MoveForward, "Ground_Walk"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.TurnLeft, "Ground_Turn_left"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.TurnRight, "Ground_Turn_right"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Rush, "Ground_Rush"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Attack, "Ground_Attack01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Dead, "Ground_Die"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadHeadShoot, "Ground_Die"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Hurt, "Ground_Body_Damage"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtFront, "Ground_Body_Damage"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtBehind, "Ground_Body_Damage"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Roar, "Ground_Roar"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_ShowTime, "Ground_Roar"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_1, "Ground_Attack01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_2, "Ground_Attack02"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_3, "Ground_Attack03"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_4, "Ground_Attack04_01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_5, "Ground_Attack04_02"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_6, "Ground_Attack04_03"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_7, "Ground_Take_off"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_11, "Sky_Attack01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_12, "Sky_Attack02"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_13, "Sky_Attack0301"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_14, "Sky_Attack0302"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_15, "Sky_Attack0303"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_16, "Sky_Attack0401"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_17, "Sky_Attack0402"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_18, "Sky_Attack0403"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_19, "Ground_Idle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_20, "Sky_Idle"));
	}

	public override void InitAnimData_Sky()
	{
		m_AnimData.Cleanup();
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Idle, "Sky_Idle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.MoveForward, "Sky_Fly"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Attack, "Sky_Attack01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Dead, "Sky_Die"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadHeadShoot, "Sky_Die"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Hurt, "Sky_Damage"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtFront, "Sky_Damage"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtBehind, "Sky_Damage"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Glide, "Sky_Fly"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Roar, "Sky_Roar"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_ShowTime, "Sky_Roar"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_1, "Ground_Attack01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_2, "Ground_Attack02"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_3, "Ground_Attack03"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_4, "Ground_Attack04_01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_5, "Ground_Attack04_02"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_6, "Ground_Attack04_03"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_7, "Ground_Take_off"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_11, "Sky_Attack01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_12, "Sky_Attack02"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_13, "Sky_Attack0301"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_14, "Sky_Attack0302"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_15, "Sky_Attack0303"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_16, "Sky_Attack0401"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_17, "Sky_Attack0402"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_18, "Sky_Attack0403"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_19, "Ground_Idle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_20, "Sky_Idle"));
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
