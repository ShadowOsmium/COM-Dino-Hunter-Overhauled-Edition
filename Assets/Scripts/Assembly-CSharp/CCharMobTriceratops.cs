public class CCharMobTriceratops : CCharMob
{
	public override void InitAnimData()
	{
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Idle, "Idle01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.MoveForward, "Run01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Attack, "Attack02"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Dead, "Death01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadHitFly, "Death02"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadHeadShoot, "Death01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Hurt, "Damage_body01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtFront, "Damage_body01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtBehind, "Damage_body01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Roar, "Roar01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_1, "Attack01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_2, "Attack00_1"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_3, "Attack00_2"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_4, "Attack00_3"));
	}

	public override void InitAudioData()
	{
		m_AudioData.Add(kAudioEnum.HitBody, "Fx_Impact_body");
	}
}
