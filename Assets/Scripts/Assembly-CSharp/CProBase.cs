using System.Collections.Generic;

public abstract class CProBase
{
	protected iGameData m_GameData;

	protected Dictionary<kProEnum, CProValue> m_dictPro;

	public CProBase()
	{
		m_GameData = iGameApp.GetInstance().m_GameData;
		m_dictPro = new Dictionary<kProEnum, CProValue>();
		RegisterPro(kProEnum.HPMax);
		RegisterPro(kProEnum.MoveSpeed);
		RegisterPro(kProEnum.Damage);
		RegisterPro(kProEnum.Protect);
		RegisterPro(kProEnum.Critical);
		RegisterPro(kProEnum.CriticalDmg);
		RegisterPro(kProEnum.ResistBeatBack);
		RegisterPro(kProEnum.Invincible);
	}

	public void RegisterPro(kProEnum type)
	{
		if (m_dictPro != null && !m_dictPro.ContainsKey(type))
		{
			m_dictPro.Add(type, new CProValue());
		}
	}

	public abstract void Initialize(int nID, int nLevel, bool bnetwork = false);

	public abstract void UpdateSkill(CCharBase charbase);

	public abstract void UpdateBuff(CCharBase charbase);

	public abstract void UpdateEquip(CCharBase charbase);

	public void SetValue(kProEnum type, float value)
	{
		if (m_dictPro != null && m_dictPro.ContainsKey(type))
		{
			m_dictPro[type].Value = value;
		}
	}

	public float GetValue(kProEnum type)
	{
		if (m_dictPro == null || !m_dictPro.ContainsKey(type))
		{
			return -1f;
		}
		return m_dictPro[type].Value;
	}

	public void SetValueBase(kProEnum type, float value)
	{
		if (m_dictPro != null && m_dictPro.ContainsKey(type))
		{
			m_dictPro[type].ValueBase = value;
			m_dictPro[type].UpdateValue();
		}
	}

	public float GetValueBase(kProEnum type)
	{
		if (m_dictPro == null || !m_dictPro.ContainsKey(type))
		{
			return -1f;
		}
		return m_dictPro[type].ValueBase;
	}

	public virtual CSkillPro GetSkillPro(int nSkillID)
	{
		return null;
	}

	public virtual void CaculateSkillFuncBySkillPro(CSkillInfoLevel skillinfolevel, ref List<int> ltFunc, ref List<int> ltValueX, ref List<int> ltValueY)
	{
	}

	protected bool IsProReplace(kProEnum type)
	{
		if (type == kProEnum.Skill_DeathSplitID || type == kProEnum.Skill_DeathSplitCount)
		{
			return true;
		}
		return false;
	}

	protected void ProFuncBuff(kProEnum type, int value, int operation, int valuetype)
	{
		if (m_dictPro == null || !m_dictPro.ContainsKey(type) || value == 0)
		{
			return;
		}
		CProValue cProValue = m_dictPro[type];
		if (IsProReplace(type))
		{
			cProValue.m_fValueAffectFromBuff = value;
			cProValue.UpdateValue();
			return;
		}
		switch (operation)
		{
		case 0:
			switch (valuetype)
			{
			case 0:
				cProValue.m_fValueAffectFromBuff += value;
				break;
			case 1:
				cProValue.m_fValueAffectFromBuff += MyUtils.K4S5(cProValue.m_fValueBase * (float)value / 100f);
				break;
			}
			break;
		case 1:
			switch (valuetype)
			{
			case 0:
				cProValue.m_fValueAffectFromBuff -= value;
				break;
			case 1:
				cProValue.m_fValueAffectFromBuff -= MyUtils.K4S5(cProValue.m_fValueBase * (float)value / 100f);
				break;
			}
			break;
		case 2:
			cProValue.m_fValueAffectFromBuff *= value;
			break;
		case 3:
			if (value != 0)
			{
				cProValue.m_fValueAffectFromBuff /= value;
			}
			break;
		}
		cProValue.UpdateValue();
	}

	protected void ProFuncSkill(kProEnum type, int value, int operation, int valuetype)
	{
		if (m_dictPro == null || !m_dictPro.ContainsKey(type))
		{
			return;
		}
		CProValue cProValue = m_dictPro[type];
		if (IsProReplace(type))
		{
			cProValue.m_fValueAffectFromSkill = value;
			cProValue.UpdateValue();
			return;
		}
		switch (operation)
		{
		case 0:
			switch (valuetype)
			{
			case 0:
				cProValue.m_fValueAffectFromSkill += value;
				break;
			case 1:
				cProValue.m_fValueAffectFromSkill += MyUtils.K4S5(cProValue.m_fValueBase * (float)value / 100f);
				break;
			}
			break;
		case 1:
			switch (valuetype)
			{
			case 0:
				cProValue.m_fValueAffectFromSkill -= value;
				break;
			case 1:
				cProValue.m_fValueAffectFromSkill -= MyUtils.K4S5(cProValue.m_fValueBase * (float)value / 100f);
				break;
			}
			break;
		case 2:
			cProValue.m_fValueAffectFromSkill *= value;
			break;
		case 3:
			if (value != 0)
			{
				cProValue.m_fValueAffectFromSkill /= value;
			}
			break;
		}
		cProValue.UpdateValue();
	}

	protected void ProFuncEquip(kProEnum type, int value, int operation, int valuetype)
	{
		if (m_dictPro == null || !m_dictPro.ContainsKey(type))
		{
			return;
		}
		CProValue cProValue = m_dictPro[type];
		if (IsProReplace(type))
		{
			cProValue.m_fValueAffectFromEquip = value;
			cProValue.UpdateValue();
			return;
		}
		switch (operation)
		{
		case 0:
			switch (valuetype)
			{
			case 0:
				cProValue.m_fValueAffectFromEquip += value;
				break;
			case 1:
				cProValue.m_fValueAffectFromEquip += MyUtils.K4S5(cProValue.m_fValueBase * (float)value / 100f);
				break;
			}
			break;
		case 1:
			switch (valuetype)
			{
			case 0:
				cProValue.m_fValueAffectFromEquip -= value;
				break;
			case 1:
				cProValue.m_fValueAffectFromEquip -= MyUtils.K4S5(cProValue.m_fValueBase * (float)value / 100f);
				break;
			}
			break;
		case 2:
			cProValue.m_fValueAffectFromEquip *= value;
			break;
		case 3:
			if (value != 0)
			{
				cProValue.m_fValueAffectFromEquip /= value;
			}
			break;
		}
		cProValue.UpdateValue();
	}
}
