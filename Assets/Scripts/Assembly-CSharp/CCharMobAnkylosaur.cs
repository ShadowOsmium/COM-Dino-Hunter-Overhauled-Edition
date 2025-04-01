public class CCharMobAnkylosaur : CCharMob
{
	public override void InitAnimData()
	{
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Idle, "Idle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.MoveForward, "Run"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Moribund, "Changeball"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Moribunding, "Changeball_Idle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.MoribundBack, "Changeball_back"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.MoribundDeath, "Changeball_death"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Attack, "Attack01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Dead, "Death01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadHeadShoot, "Death01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadHitFly, "Death02"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Hurt, "Damage_body"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtFront, "Damage_body"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtBehind, "Damage_body"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Roar, "Roar"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Rush, "Run"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_1, "Attack01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_2, "Attack02"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_3, "Attack03"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_4, "Under"));
	}

	public override void InitAudioData()
	{
		m_AudioData.Add(kAudioEnum.HitBody, "Fx_Impact_body");
	}
}
