using UnityEngine;

public class CUseSkillSpawn : CUseSkill
{
	protected bool m_bSkillEffective;

	protected float m_fTimeAnim;

	protected float m_fTimeAnimCount;

	protected float m_fTimePoint;

	protected float m_fTimePointCount;

	protected int m_nSpawnID;

	protected int m_nBonePart;

	public override kUseSkillStatus OnEnter(CCharBase charbase)
	{
		kAnimEnum type = (kAnimEnum)m_pSkillInfoLevel.nAnim;
		if (charbase.GetActionLen(type) == 0f)
		{
			type = ((!charbase.IsMob() && !charbase.IsBoss()) ? kAnimEnum.Attack : kAnimEnum.Mob_Attack);
		}
		if (charbase.IsPlayer() || charbase.IsUser())
		{
			m_fTimeAnim = charbase.CrossAnimMix(type, WrapMode.ClampForever, 0.3f, m_pSkillInfoLevel.fAnimSpeed);
		}
		else
		{
			m_fTimeAnim = charbase.CrossAnim(type, WrapMode.ClampForever, 0.3f, m_pSkillInfoLevel.fAnimSpeed, 0f);
		}
		m_fTimeAnimCount = 0f;
		float fValue = 0f;
		m_pSkillInfoLevel.GetSkillModeValue(0, ref fValue);
		m_fTimePoint = m_fTimeAnim * fValue;
		m_fTimePointCount = 0f;
		m_pSkillInfoLevel.GetSkillModeValue(1, ref m_nSpawnID);
		m_pSkillInfoLevel.GetSkillModeValue(2, ref m_nBonePart);
		m_bSkillEffective = false;
		if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
		{
			charbase.AddBuff(m_pSkillInfoLevel.nAnimBuffID, 0f);
		}
		return kUseSkillStatus.Success;
	}

	public override void OnExit(CCharBase charbase)
	{
		if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
		{
			charbase.DelBuffByID(m_pSkillInfoLevel.nAnimBuffID);
		}
	}

	public override kUseSkillStatus OnUpdate(CCharBase charbase, float deltaTime)
	{
		if (!IsSkillValid())
		{
			if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
			{
				charbase.DelBuffByID(m_pSkillInfoLevel.nAnimBuffID);
			}
			return kUseSkillStatus.Failure;
		}
		if (!m_bSkillEffective)
		{
			m_fTimePointCount += deltaTime;
			if (m_fTimePointCount >= m_fTimePoint)
			{
				m_bSkillEffective = true;
				float fValue = 0f;
				float fValue2 = 0f;
				float fValue3 = 0f;
				int nValue = 0;
				m_pSkillInfoLevel.GetSkillModeValue(3, ref fValue);
				m_pSkillInfoLevel.GetSkillModeValue(4, ref fValue2);
				m_pSkillInfoLevel.GetSkillModeValue(5, ref fValue3);
				m_pSkillInfoLevel.GetSkillModeValue(6, ref nValue);
				switch (m_pSkillInfoLevel.nRangeType)
				{
				case 0:
				{
					float fValue6 = 0f;
					float fValue7 = 0f;
					m_pSkillInfoLevel.GetSkillRangeValue(0, ref fValue6);
					m_pSkillInfoLevel.GetSkillRangeValue(1, ref fValue7);
					float num4 = Vector3.Distance(charbase.Pos, m_Target.GetBone(1).position);
					if (num4 < fValue6)
					{
						num4 = fValue6;
					}
					else if (num4 > fValue7)
					{
						num4 = fValue7;
					}
					if (nValue > 1)
					{
						for (int j = 0; j < nValue; j++)
						{
							Vector3 position3 = charbase.GetBone(m_nBonePart).position;
							Vector3 position4 = m_Target.GetBone(1).position;
							Vector3 normalized2 = (position4 - position3).normalized;
							normalized2.y = 0f;
							Vector3 dir2D2 = charbase.Dir2D;
							charbase.Dir3D = normalized2;
							charbase.Transform.RotateAroundLocal(charbase.transform.right, 0f - fValue3);
							charbase.Transform.RotateAround(charbase.Pos, Vector3.up, fValue2 + (float)Random.Range(-45, 45));
							normalized2 = charbase.Dir3D;
							charbase.Dir2D = dir2D2;
							normalized2 *= fValue;
							iSpawnBullet iSpawnBullet3 = m_GameScene.AddSpawn(charbase.UID, m_nSpawnID, m_pSkillInfoLevel, position3, normalized2);
							if (iSpawnBullet3 != null)
							{
								iSpawnBullet_Parabola iSpawnBullet_Parabola3 = iSpawnBullet3 as iSpawnBullet_Parabola;
								if (iSpawnBullet_Parabola3 != null)
								{
									normalized2.y = 0f;
									float num5 = num4 / normalized2.magnitude;
									float num6 = iSpawnBullet_Parabola3.fGravity * num5 * 0.5f;
									iSpawnBullet_Parabola3.SetForce(normalized2 + Vector3.up * num6);
								}
								else
								{
									iSpawnBullet3.SetForce((position4 - position3).normalized * fValue);
								}
							}
						}
						break;
					}
					Vector3 position5 = charbase.GetBone(m_nBonePart).position;
					Vector3 position6 = m_Target.GetBone(1).position;
					Vector3 normalized3 = (position6 - position5).normalized;
					normalized3.y = 0f;
					Vector3 dir2D3 = charbase.Dir2D;
					charbase.Dir3D = normalized3;
					charbase.Transform.RotateAroundLocal(charbase.transform.right, 0f - fValue3);
					charbase.Transform.RotateAround(charbase.Pos, Vector3.up, fValue2);
					normalized3 = charbase.Dir3D;
					charbase.Dir2D = dir2D3;
					normalized3 *= fValue;
					iSpawnBullet iSpawnBullet4 = m_GameScene.AddSpawn(charbase.UID, m_nSpawnID, m_pSkillInfoLevel, position5, normalized3);
					if (iSpawnBullet4 != null)
					{
						iSpawnBullet_Parabola iSpawnBullet_Parabola4 = iSpawnBullet4 as iSpawnBullet_Parabola;
						if (iSpawnBullet_Parabola4 != null)
						{
							normalized3.y = 0f;
							float num7 = num4 / normalized3.magnitude;
							float num8 = position6.y - position5.y;
							float num9 = num8 / num7 - 0.5f * (0f - iSpawnBullet_Parabola4.fGravity) * num7;
							iSpawnBullet_Parabola4.SetForce(normalized3 + Vector3.up * num9);
						}
						else
						{
							iSpawnBullet4.SetForce((position6 - position5).normalized * fValue);
						}
					}
					break;
				}
				case 1:
				{
					float fValue4 = 0f;
					float fValue5 = 0f;
					m_pSkillInfoLevel.GetSkillRangeValue(0, ref fValue4);
					m_pSkillInfoLevel.GetSkillRangeValue(1, ref fValue5);
					for (int i = 0; i < nValue; i++)
					{
						float num = Random.Range(fValue4, fValue5);
						Vector3 position = charbase.GetBone(m_nBonePart).position;
						Vector3 position2 = m_Target.GetBone(1).position;
						Vector3 normalized = (position2 - charbase.Pos).normalized;
						normalized.y = 0f;
						Vector3 dir2D = charbase.Dir2D;
						charbase.Dir3D = normalized;
						charbase.Transform.RotateAroundLocal(charbase.transform.right, -70f);
						charbase.Transform.RotateAround(charbase.Pos, Vector3.up, fValue2 + (float)Random.Range(-180, 180));
						normalized = charbase.Dir3D;
						charbase.Dir2D = dir2D;
						normalized *= fValue;
						iSpawnBullet iSpawnBullet2 = m_GameScene.AddSpawn(charbase.UID, m_nSpawnID, m_pSkillInfoLevel, position, normalized);
						if (iSpawnBullet2 != null)
						{
							iSpawnBullet_Parabola iSpawnBullet_Parabola2 = iSpawnBullet2 as iSpawnBullet_Parabola;
							if (iSpawnBullet_Parabola2 != null)
							{
								normalized.y = 0f;
								float num2 = num / normalized.magnitude;
								float num3 = iSpawnBullet_Parabola2.fGravity * num2 * 0.5f;
								iSpawnBullet_Parabola2.SetForce(normalized + Vector3.up * num3);
							}
							else
							{
								iSpawnBullet2.SetForce((position2 - position).normalized * fValue);
							}
						}
					}
					break;
				}
				}
			}
		}
		m_fTimeAnimCount += deltaTime;
		if (m_fTimeAnimCount >= m_fTimeAnim)
		{
			if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
			{
				charbase.DelBuffByID(m_pSkillInfoLevel.nAnimBuffID);
			}
			m_pSkillInfoLevel = null;
			return kUseSkillStatus.Success;
		}
		return kUseSkillStatus.Executing;
	}
}
