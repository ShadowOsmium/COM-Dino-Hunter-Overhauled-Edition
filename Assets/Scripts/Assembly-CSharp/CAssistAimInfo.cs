using System.Collections.Generic;
using UnityEngine;

public class CAssistAimInfo
{
	public CCharMob m_Target;

	public List<Transform> m_ltBone;

	public CAssistAimInfo()
	{
		m_Target = null;
		m_ltBone = new List<Transform>();
	}
}
