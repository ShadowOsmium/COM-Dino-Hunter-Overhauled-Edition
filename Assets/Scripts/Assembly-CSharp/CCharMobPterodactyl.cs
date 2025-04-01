public class CCharMobPterodactyl : CCharMob
{
	public override void InitAnimData()
	{
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Idle, "idle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.MoveForward, "fly"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Attack, "attack01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Dead, "death01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadFly, "fly_death"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadHeadShoot, "death02"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadHitFly, "fly_death"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Hurt, "damage"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtFront, "damage"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtBehind, "damage"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Glide, "fly_circle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_1, "attack02_01"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_2, "attack02_02"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Skill_Action_3, "attack05"));
	}

	public override void InitAudioData()
	{
		m_AudioData.Add(kAudioEnum.HitBody, "Fx_Impact_body");
	}
}
