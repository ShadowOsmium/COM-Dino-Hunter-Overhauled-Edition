public class CCharMobDilophosaurus : CCharMob
{
	public override void InitAnimData()
	{
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Idle, "Idle01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.MoveForward, "Run01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Attack, "Attack01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Dead, "Death01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadHitFly, "Death02"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadHeadShoot, "Death01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Hurt, "Dagame_body01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtFront, "Dagame_body01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtBehind, "Dagame_body01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Roar, "Roar01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_1, "Attack00"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_2, "Attack00_1"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_3, "Attack01_left"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_4, "Attack01_right"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_5, "Attack01"));
	}

	public override void InitAudioData()
	{
		m_AudioData.Add(kAudioEnum.HitBody, "Fx_Impact_body");
	}
}
