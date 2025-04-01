public class CCharMobBombworm : CCharMob
{
	public override void InitAnimData()
	{
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Idle, "Idle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.MoveForward, "Fly"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Attack, "Attack"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Dead, "Death"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadFly, "Death"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadHeadShoot, "Death"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_DeadHitFly, "Death"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Hurt, "Idle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtFront, "Idle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.BigHurtBehind, "Idle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Roar, "Idle"));
		m_AnimData.Add(new CAnimInfo(kAnimEnum.Mob_Rush, "Fly"));
	}

	public override void InitAudioData()
	{
		m_AudioData.Add(kAudioEnum.HitBody, "Fx_Impact_body");
	}
}
