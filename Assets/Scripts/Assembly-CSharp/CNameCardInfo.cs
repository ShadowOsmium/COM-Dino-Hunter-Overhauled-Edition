using System.Collections.Generic;
using UnityEngine;

public class CNameCardInfo
{
	public string m_sID = string.Empty;

	public string m_sGCAccount = string.Empty;

	public string m_sNickName = string.Empty;

	public int m_nTitle;

	public Texture2D m_Photo;

	public CNCPack m_NCPack;

	public int m_nPhotoState;

	protected float m_fPhoto_RefreshTime;

	protected float m_fPhoto_RefreshTimeCount;

	protected float m_fNameCard_RefreshTime;

	protected float m_fNameCard_RefreshTimeCount;

	public int m_nRoleID
	{
		get
		{
			return m_NCPack.roleid;
		}
		set
		{
			m_NCPack.roleid = value;
		}
	}

	public int m_nHunterLvl
	{
		get
		{
			return m_NCPack.lvl;
		}
		set
		{
			m_NCPack.lvl = value;
		}
	}

	public int m_nHunterExp
	{
		get
		{
			return m_NCPack.exp;
		}
		set
		{
			m_NCPack.exp = value;
		}
	}

	public int m_nCombatPower
	{
		get
		{
			return m_NCPack.power;
		}
		set
		{
			m_NCPack.power = value;
		}
	}

	public int m_nRank
	{
		get
		{
			return m_NCPack.rank;
		}
		set
		{
			m_NCPack.rank = value;
		}
	}

	public int m_nBeAdmired
	{
		get
		{
			return m_NCPack.admire;
		}
		set
		{
			m_NCPack.admire = value;
		}
	}

	public int m_nGold
	{
		get
		{
			return m_NCPack.gold;
		}
		set
		{
			m_NCPack.gold = value;
		}
	}

	public int m_nCrystal
	{
		get
		{
			return m_NCPack.crystal;
		}
		set
		{
			m_NCPack.crystal = value;
		}
	}

	public float m_fSceneProccess
	{
		get
		{
			return m_NCPack.proc;
		}
		set
		{
			m_NCPack.proc = value;
		}
	}

	public string m_sSignature
	{
		get
		{
			return m_NCPack.sign;
		}
		set
		{
			m_NCPack.sign = value;
		}
	}

	public List<int> m_ltWeapon
	{
		get
		{
			if (m_NCPack.weapon == null)
			{
				return null;
			}
			List<int> list = new List<int>();
			for (int i = 0; i < m_NCPack.weapon.Length; i++)
			{
				if (m_NCPack.weapon[i] > 0)
				{
					list.Add(m_NCPack.weapon[i]);
				}
			}
			return list;
		}
	}

	public CNameCardInfo()
	{
		m_NCPack = new CNCPack();
		m_fPhoto_RefreshTime = iMacroDefine.Photo_RefreshTime;
		m_fPhoto_RefreshTimeCount = m_fPhoto_RefreshTime;
		m_fNameCard_RefreshTime = iMacroDefine.NameCard_RefreshTime;
		m_fNameCard_RefreshTimeCount = m_fNameCard_RefreshTime;
	}

	public void Update(float deltaTime)
	{
		if (m_fPhoto_RefreshTimeCount < m_fPhoto_RefreshTime)
		{
			m_fPhoto_RefreshTimeCount += deltaTime;
		}
		if (m_fNameCard_RefreshTimeCount < m_fNameCard_RefreshTime)
		{
			m_fNameCard_RefreshTimeCount += deltaTime;
		}
	}

	public bool IsPhotoExpired()
	{
		return m_fPhoto_RefreshTimeCount >= m_fPhoto_RefreshTime;
	}

	public bool IsNameCardExpired()
	{
		return m_fNameCard_RefreshTimeCount >= m_fNameCard_RefreshTime;
	}

	public void ResetPhotoTime()
	{
		m_fPhoto_RefreshTimeCount = 0f;
	}

	public void ResetNameCardTime()
	{
		m_fNameCard_RefreshTimeCount = 0f;
	}

	public void SetPhoto(byte[] photo)
	{
		if (photo == null)
		{
			return;
		}
		try
		{
			m_Photo = new Texture2D(40, 40);
			m_Photo.LoadImage(photo);
			if (m_Photo.width > 40 || m_Photo.height > 40)
			{
				m_Photo = gyLoadImage.Resize(m_Photo, 40, 40);
			}
			m_NCPack.photo = m_Photo.EncodeToPNG();
			Debug.Log("from byte[] photo size = " + m_NCPack.photo.Length);
		}
		catch
		{
			m_Photo = null;
			m_NCPack.photo = null;
		}
	}

	public void SetPhoto(Texture2D texture)
	{
		if (!(texture == null))
		{
			m_Photo = texture;
			if (m_Photo.width > 40 || m_Photo.height > 40)
			{
				m_Photo = gyLoadImage.Resize(m_Photo, 40, 40);
			}
			m_NCPack.photo = m_Photo.EncodeToPNG();
			Debug.Log("from texture2d photo size = " + m_NCPack.photo.Length);
		}
	}

	public Texture2D GetPhoto()
	{
		if (m_Photo == null)
		{
			SetPhoto(m_NCPack.photo);
		}
		return m_Photo;
	}
}
