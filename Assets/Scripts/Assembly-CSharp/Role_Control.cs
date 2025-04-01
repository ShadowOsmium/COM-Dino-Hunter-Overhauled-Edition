using System.Collections.Generic;
using UnityEngine;

public class Role_Control : MonoBehaviour
{
	private enum WeaponType
	{
		None,
		CloseWeapons,
		Crossbow,
		LiquidFireGun,
		MachineGun,
		RPG,
		ViolenceGun
	}

	public enum MoveType
	{
		None,
		Enter,
		Exit
	}

	public Transform prefab_role_101;

	public Transform prefab_role_102;

	public Transform prefab_role_103;

	public Transform prefab_role_104;

	public Transform prefab_role_105;

	public Transform prefab_role_106;

	public Transform prefab_role_107;

	public Transform prefab_weapon_001;

	public Transform prefab_weapon_002;

	public Transform prefab_weapon_003;

	public Transform prefab_weapon_004;

	public Transform prefab_weapon_005;

	public Transform prefab_weapon_006;

	public Transform prefab_weapon_007;

	public Transform prefab_weapon_008;

	public Transform prefab_weapon_009;

	public Transform prefab_weapon_010;

	public Transform prefab_weapon_011;

	public Transform prefab_weapon_012;

	public Transform prefab_weapon_013;

	public Transform prefab_weapon_014;

	public Transform prefab_weapon_015;

	public Transform prefab_weapon_016;

	public Transform prefab_weapon_017;

	public Transform prefab_weapon_018;

	public Transform prefab_weapon_019;

	public Transform prefab_weapon_021;

	public Transform prefab_weapon_023;

	public Transform prefab_weapon_024;

	public Transform prefab_weapon_025;

	public Transform prefab_weapon_026;

	public Transform prefab_weapon_027;

	public Transform prefab_weapon_028;

	public Transform prefab_weapon_029;

	public Transform prefab_weapon_030;

	public Transform prefab_weapon_031;

	public Transform prefab_weapon_032;

	public Transform prefab_weapon_033;

	public Transform prefab_weapon_034;

	public Transform plane_bottom;

	private Transform role_101;

	private Transform role_102;

	private Transform role_103;

	private Transform role_104;

	private Transform role_105;

	private Transform role_106;

	private Transform role_107;

	private Transform weapon_001;

	private Transform weapon_002;

	private Transform weapon_003;

	private Transform weapon_004;

	private Transform weapon_005;

	private Transform weapon_006;

	private Transform weapon_007;

	private Transform weapon_008;

	private Transform weapon_009;

	private Transform weapon_010;

	private Transform weapon_011;

	private Transform weapon_012;

	private Transform weapon_013;

	private Transform weapon_014;

	private Transform weapon_015;

	private Transform weapon_016;

	private Transform weapon_017;

	private Transform weapon_018;

	private Transform weapon_019;

	private Transform weapon_021;

	private Transform weapon_023;

	private Transform weapon_024;

	private Transform weapon_025;

	private Transform weapon_026;

	private Transform weapon_027;

	private Transform weapon_028;

	private Transform weapon_029;

	private Transform weapon_030;

	private Transform weapon_031;

	private Transform weapon_032;

	private Transform weapon_033;

	private Transform weapon_034;

	private Transform weapon_now;

	private Transform role_now;

	private Transform go_role_hand;

	private WeaponType weapon_type;

	private bool play_wepaon_animation;

	private float ani_time_gap;

	private float ani_time;

	private int weapon_id;

	protected float m_fShootRate = 0.5f;

	protected float m_fShootRateCount;

	private bool open_random_weapon;

	private List<int> random_weapon_list;

	private Vector3 move_old_pos = Vector3.zero;

	private Vector3 move_target_pos = Vector3.zero;

	private MoveType open_move;

	private float move_speed = 5f;

	private bool stop_event;

	private bool exit_event;

	protected bool m_bActive = true;

	private void Awake()
	{
		if (role_now != null)
		{
			go_role_hand = role_now.GetComponent<Role_BeControl>().GetHand();
			ChangeWeapon(1);
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_bActive)
		{
			UpdateAnimation();
			UpdatePosMove();
		}
	}

	public void SetRotation(float wparam, float lparam)
	{
		if (role_now != null)
		{
			role_now.Rotate(new Vector3(0f, 0f - wparam, 0f));
		}
	}

	public void ChangeWeapon(int m_id)
	{
		Transform transform = null;
		bool flag = true;
		switch (m_id)
		{
		case 1:
			if (weapon_001 == null)
			{
				weapon_001 = (Transform)Object.Instantiate(prefab_weapon_001);
				weapon_001.parent = base.transform;
				weapon_001.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_001;
			weapon_type = WeaponType.Crossbow;
			break;
		case 2:
			if (weapon_002 == null)
			{
				weapon_002 = (Transform)Object.Instantiate(prefab_weapon_002);
				weapon_002.parent = base.transform;
				weapon_002.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_002;
			weapon_type = WeaponType.CloseWeapons;
			break;
		case 3:
			if (weapon_003 == null)
			{
				weapon_003 = (Transform)Object.Instantiate(prefab_weapon_003);
				weapon_003.parent = base.transform;
				weapon_003.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_003;
			weapon_type = WeaponType.ViolenceGun;
			break;
		case 4:
			if (weapon_004 == null)
			{
				weapon_004 = (Transform)Object.Instantiate(prefab_weapon_004);
				weapon_004.parent = base.transform;
				weapon_004.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_004;
			weapon_type = WeaponType.MachineGun;
			break;
		case 5:
			if (weapon_005 == null)
			{
				weapon_005 = (Transform)Object.Instantiate(prefab_weapon_005);
				weapon_005.parent = base.transform;
				weapon_005.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_005;
			weapon_type = WeaponType.RPG;
			break;
		case 6:
			if (weapon_006 == null)
			{
				weapon_006 = (Transform)Object.Instantiate(prefab_weapon_006);
				weapon_006.parent = base.transform;
				weapon_006.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_006;
			weapon_type = WeaponType.CloseWeapons;
			break;
		case 7:
			if (weapon_007 == null)
			{
				weapon_007 = (Transform)Object.Instantiate(prefab_weapon_007);
				weapon_007.parent = base.transform;
				weapon_007.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_007;
			weapon_type = WeaponType.CloseWeapons;
			break;
		case 8:
			if (weapon_008 == null)
			{
				weapon_008 = (Transform)Object.Instantiate(prefab_weapon_008);
				weapon_008.parent = base.transform;
				weapon_008.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_008;
			weapon_type = WeaponType.CloseWeapons;
			break;
		case 9:
			if (weapon_009 == null)
			{
				weapon_009 = (Transform)Object.Instantiate(prefab_weapon_009);
				weapon_009.parent = base.transform;
				weapon_009.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_009;
			weapon_type = WeaponType.Crossbow;
			break;
		case 10:
			if (weapon_010 == null)
			{
				weapon_010 = (Transform)Object.Instantiate(prefab_weapon_010);
				weapon_010.parent = base.transform;
				weapon_010.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_010;
			weapon_type = WeaponType.Crossbow;
			break;
		case 11:
			if (weapon_011 == null)
			{
				weapon_011 = (Transform)Object.Instantiate(prefab_weapon_011);
				weapon_011.parent = base.transform;
				weapon_011.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_011;
			weapon_type = WeaponType.ViolenceGun;
			break;
		case 12:
			if (weapon_012 == null)
			{
				weapon_012 = (Transform)Object.Instantiate(prefab_weapon_012);
				weapon_012.parent = base.transform;
				weapon_012.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_012;
			weapon_type = WeaponType.ViolenceGun;
			break;
		case 13:
			if (weapon_013 == null)
			{
				weapon_013 = (Transform)Object.Instantiate(prefab_weapon_013);
				weapon_013.parent = base.transform;
				weapon_013.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_013;
			weapon_type = WeaponType.RPG;
			break;
		case 14:
			if (weapon_014 == null)
			{
				weapon_014 = (Transform)Object.Instantiate(prefab_weapon_014);
				weapon_014.parent = base.transform;
				weapon_014.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_014;
			weapon_type = WeaponType.LiquidFireGun;
			break;
		case 15:
			if (weapon_015 == null)
			{
				weapon_015 = (Transform)Object.Instantiate(prefab_weapon_015);
				weapon_015.parent = base.transform;
				weapon_015.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_015;
			weapon_type = WeaponType.MachineGun;
			break;
		case 16:
			if (weapon_016 == null)
			{
				weapon_016 = (Transform)Object.Instantiate(prefab_weapon_016);
				weapon_016.parent = base.transform;
				weapon_016.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_016;
			weapon_type = WeaponType.MachineGun;
			break;
		case 17:
			if (weapon_017 == null)
			{
				weapon_017 = (Transform)Object.Instantiate(prefab_weapon_017);
				weapon_017.parent = base.transform;
				weapon_017.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_017;
			weapon_type = WeaponType.ViolenceGun;
			break;
		case 18:
			if (weapon_018 == null)
			{
				weapon_018 = (Transform)Object.Instantiate(prefab_weapon_018);
				weapon_018.parent = base.transform;
				weapon_018.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_018;
			weapon_type = WeaponType.MachineGun;
			break;
		case 19:
			if (weapon_019 == null)
			{
				weapon_019 = (Transform)Object.Instantiate(prefab_weapon_019);
				weapon_019.parent = base.transform;
				weapon_019.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_019;
			weapon_type = WeaponType.RPG;
			break;
		case 21:
			if (weapon_021 == null)
			{
				weapon_021 = (Transform)Object.Instantiate(prefab_weapon_021);
				weapon_021.parent = base.transform;
				weapon_021.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_021;
			weapon_type = WeaponType.Crossbow;
			break;
		case 23:
			if (weapon_023 == null)
			{
				weapon_023 = (Transform)Object.Instantiate(prefab_weapon_023);
				weapon_023.parent = base.transform;
				weapon_023.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_023;
			weapon_type = WeaponType.LiquidFireGun;
			break;
		case 24:
			if (weapon_024 == null)
			{
				weapon_024 = (Transform)Object.Instantiate(prefab_weapon_024);
				weapon_024.parent = base.transform;
				weapon_024.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_024;
			weapon_type = WeaponType.CloseWeapons;
			break;
		case 25:
			if (weapon_025 == null)
			{
				weapon_025 = (Transform)Object.Instantiate(prefab_weapon_025);
				weapon_025.parent = base.transform;
				weapon_025.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_025;
			weapon_type = WeaponType.LiquidFireGun;
			break;
		case 26:
			if (weapon_026 == null)
			{
				weapon_026 = (Transform)Object.Instantiate(prefab_weapon_026);
				weapon_026.parent = base.transform;
				weapon_026.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_026;
			weapon_type = WeaponType.CloseWeapons;
			break;
		case 27:
			if (weapon_027 == null)
			{
				weapon_027 = (Transform)Object.Instantiate(prefab_weapon_027);
				weapon_027.parent = base.transform;
				weapon_027.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_027;
			weapon_type = WeaponType.MachineGun;
			break;
		case 28:
			if (weapon_028 == null)
			{
				weapon_028 = (Transform)Object.Instantiate(prefab_weapon_028);
				weapon_028.parent = base.transform;
				weapon_028.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_028;
			weapon_type = WeaponType.MachineGun;
			break;
		case 29:
			if (weapon_029 == null)
			{
				weapon_029 = (Transform)Object.Instantiate(prefab_weapon_029);
				weapon_029.parent = base.transform;
				weapon_029.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_029;
			weapon_type = WeaponType.RPG;
			break;
		case 30:
			if (weapon_030 == null)
			{
				weapon_030 = (Transform)Object.Instantiate(prefab_weapon_030);
				weapon_030.parent = base.transform;
				weapon_030.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_030;
			weapon_type = WeaponType.RPG;
			break;
		case 31:
			if (weapon_031 == null)
			{
				weapon_031 = (Transform)Object.Instantiate(prefab_weapon_031);
				weapon_031.parent = base.transform;
				weapon_031.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_031;
			weapon_type = WeaponType.Crossbow;
			break;
		case 32:
			if (weapon_032 == null)
			{
				weapon_032 = (Transform)Object.Instantiate(prefab_weapon_032);
				weapon_032.parent = base.transform;
				weapon_032.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_032;
			weapon_type = WeaponType.Crossbow;
			break;
		case 33:
			if (weapon_033 == null)
			{
				weapon_033 = (Transform)Object.Instantiate(prefab_weapon_033);
				weapon_033.parent = base.transform;
				weapon_033.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_033;
			weapon_type = WeaponType.ViolenceGun;
			break;
		case 34:
			if (weapon_034 == null)
			{
				weapon_034 = (Transform)Object.Instantiate(prefab_weapon_034);
				weapon_034.parent = base.transform;
				weapon_034.localPosition = new Vector3(0f, 0f, 0f);
			}
			transform = weapon_034;
			weapon_type = WeaponType.ViolenceGun;
			break;
		default:
			flag = false;
			break;
		}
		if (!flag)
		{
			return;
		}
		if (weapon_now != null)
		{
			weapon_now.parent = base.gameObject.transform;
			weapon_now.gameObject.SetActiveRecursively(false);
		}
		weapon_now = transform;
		if (weapon_now != null)
		{
			weapon_id = m_id;
			weapon_now.gameObject.SetActiveRecursively(true);
			weapon_now.parent = go_role_hand;
			weapon_now.localPosition = new Vector3(0f, 0f, 0f);
			weapon_now.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
			string animName = GetAnimName(weapon_type);
			if (animName.Length > 0 && role_now.GetComponent<Animation>()[animName] != null)
			{
				role_now.GetComponent<Animation>()[animName].wrapMode = WrapMode.Loop;
				role_now.GetComponent<Animation>()[animName].layer = -1;
				role_now.GetComponent<Animation>().CrossFade(animName);
			}
			if (weapon_type == WeaponType.MachineGun)
			{
				m_fShootRate = 0.5f;
			}
			else
			{
				m_fShootRate = 1f;
			}
		}
	}

	public void Show(bool bShow)
	{
		if (!(role_now == null))
		{
			role_now.gameObject.SetActiveRecursively(bShow);
			m_bActive = bShow;
		}
	}

	public void ChangeRole(int id)
	{
		if (role_now != null)
		{
			role_now.gameObject.SetActiveRecursively(false);
		}
		switch (id)
		{
		case 1:
			if (role_101 == null)
			{
				role_101 = (Transform)Object.Instantiate(prefab_role_101);
				role_101.parent = base.transform;
				role_101.localPosition = new Vector3(0f, 0f, 0f);
				role_101.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			}
			role_now = role_101;
			break;
		case 5:
			if (role_105 == null)
			{
				role_105 = (Transform)Object.Instantiate(prefab_role_105);
				role_105.parent = base.transform;
				role_105.localPosition = new Vector3(0f, 0f, 0f);
				role_105.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			}
			role_now = role_105;
			break;
		case 4:
			if (role_104 == null)
			{
				role_104 = (Transform)Object.Instantiate(prefab_role_104);
				role_104.parent = base.transform;
				role_104.localPosition = new Vector3(0f, 0f, 0f);
				role_104.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			}
			role_now = role_104;
			break;
		case 3:
			if (role_103 == null)
			{
				role_103 = (Transform)Object.Instantiate(prefab_role_103);
				role_103.parent = base.transform;
				role_103.localPosition = new Vector3(0f, 0f, 0f);
				role_103.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			}
			role_now = role_103;
			break;
		case 2:
			if (role_102 == null)
			{
				role_102 = (Transform)Object.Instantiate(prefab_role_102);
				role_102.parent = base.transform;
				role_102.localPosition = new Vector3(0f, 0f, 0f);
				role_102.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			}
			role_now = role_102;
			break;
		case 6:
			if (role_106 == null && prefab_role_106 != null)
			{
				role_106 = (Transform)Object.Instantiate(prefab_role_106);
				role_106.parent = base.transform;
				role_106.localPosition = new Vector3(0f, 0f, 0f);
				role_106.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			}
			role_now = role_106;
			break;
		case 7:
			if (role_107 == null && prefab_role_107 != null)
			{
				role_107 = (Transform)Object.Instantiate(prefab_role_107);
				role_107.parent = base.transform;
				role_107.localPosition = new Vector3(0f, 0f, 0f);
				role_107.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			}
			role_now = role_107;
			break;
		default:
			Debug.Log("role id error! id=" + id);
			return;
		}
		role_now.gameObject.SetActiveRecursively(true);
		string animName = GetAnimName(weapon_type);
		if (animName.Length > 0 && role_now.GetComponent<Animation>()[animName] != null)
		{
			role_now.GetComponent<Animation>()[animName].wrapMode = WrapMode.Loop;
			role_now.GetComponent<Animation>()[animName].layer = -1;
			role_now.GetComponent<Animation>().CrossFade(animName);
		}
		go_role_hand = role_now.GetComponent<Role_BeControl>().GetHand();
		if (weapon_now != null)
		{
			weapon_now.parent = go_role_hand;
			weapon_now.gameObject.SetActiveRecursively(true);
			weapon_now.localPosition = new Vector3(0f, 0f, 0f);
			weapon_now.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
		}
	}

	public void ChangeAvatar(int index, GameObject model, Texture tex)
	{
		if (!(role_now == null))
		{
			Avatar component = role_now.GetComponent<Avatar>();
			if (!(component == null) && component.m_AvatarPart != null && index >= 0 && index < component.m_AvatarPart.Length)
			{
				component.ReplaceAvatar(index, model, tex);
			}
		}
	}

	public void ChangeAvatarEffect(int index, GameObject effectprefab)
	{
		if (!(role_now == null))
		{
			Avatar component = role_now.GetComponent<Avatar>();
			if (!(component == null) && component.m_AvatarPart != null && index >= 0 && index < component.m_AvatarPart.Length)
			{
				component.ReplaceAvatarEffect(index, effectprefab);
			}
		}
	}

	public void ChangeRoleNull()
	{
		if (role_now != null)
		{
			Object.Destroy(role_now.gameObject);
		}
	}

	public void UpdateAnimation()
	{
		if (open_move != 0 || role_now == null)
		{
			return;
		}
		ani_time += Time.deltaTime;
		if (play_wepaon_animation)
		{
			m_fShootRateCount += Time.deltaTime;
			if (m_fShootRateCount >= m_fShootRate)
			{
				m_fShootRateCount = 0f;
				string animName = GetAnimName(weapon_type, true);
				if (animName.Length > 0 && role_now.GetComponent<Animation>()[animName] != null)
				{
					role_now.GetComponent<Animation>()[animName].speed = 1f / m_fShootRate;
					role_now.GetComponent<Animation>()[animName].time = 0f;
					role_now.GetComponent<Animation>().Sample();
					PlayFireEffect();
					role_now.GetComponent<Animation>().Play(animName);
				}
			}
		}
		if (!(ani_time >= ani_time_gap))
		{
			return;
		}
		ani_time = 0f;
		play_wepaon_animation = !play_wepaon_animation;
		if (play_wepaon_animation)
		{
			ani_time_gap = 3f;
			if (open_random_weapon && random_weapon_list != null)
			{
				int count = random_weapon_list.Count;
				if (count > 0)
				{
					int id = random_weapon_list[Random.Range(0, 1000) % count];
					ChangeWeapon(id);
				}
			}
		}
		else
		{
			ani_time_gap = 3f;
		}
	}

	public void PlayFireEffect()
	{
		TUIMappingInfo.FireEffect fireEffect = TUIMappingInfo.Instance().GetFireEffect();
		if (fireEffect == null || weapon_id == 0)
		{
			return;
		}
		Transform transform = fireEffect(weapon_id);
		if (!(transform != null))
		{
			return;
		}
		Transform transform2 = weapon_now.Find("Dummy01/texiao");
		if (transform2 != null)
		{
			transform.parent = transform2;
			transform.localPosition = new Vector3(0f, 0f, 0f);
			transform.forward = role_now.forward;
			Object.Destroy(transform.gameObject, 1f);
			return;
		}
		transform2 = weapon_now.Find("Dummy01/texiao01");
		if (transform2 != null)
		{
			transform.parent = transform2;
			transform.localPosition = new Vector3(0f, 0f, 0f);
			transform.forward = role_now.forward;
			Object.Destroy(transform.gameObject, 1f);
			return;
		}
		transform2 = weapon_now.Find("Dummy01/danyao");
		if (transform2 != null)
		{
			transform.parent = transform2;
			transform.localPosition = new Vector3(0f, 0f, 0f);
			transform.forward = role_now.forward;
			Object.Destroy(transform.gameObject, 1f);
		}
		else
		{
			Object.Destroy(transform.gameObject);
		}
	}

	public void SetRoleFixedRotation(Vector3 m_rotation)
	{
		if (role_now != null)
		{
			role_now.localRotation = Quaternion.Euler(m_rotation);
		}
	}

	public void SetRandomWeapon(bool m_open, List<int> m_weapon_list)
	{
		open_random_weapon = m_open;
		random_weapon_list = m_weapon_list;
		if (m_weapon_list == null)
		{
			return;
		}
		foreach (int item in m_weapon_list)
		{
			Debug.Log(item);
		}
	}

	public bool GetRoleNull()
	{
		if (role_now != null)
		{
			return false;
		}
		return true;
	}

	private string GetAnimName(WeaponType weapontype, bool isattack = false)
	{
		string result = string.Empty;
		switch (weapon_type)
		{
		case WeaponType.CloseWeapons:
			result = ((!isattack) ? "melee_ground_idle" : "melee_ground_attack02");
			break;
		case WeaponType.Crossbow:
			result = ((!isattack) ? "crossbow_ground_idle" : "crossbow_ground_attack01");
			break;
		case WeaponType.LiquidFireGun:
			result = ((!isattack) ? "holdgun_ground_idle" : "holdgun_ground_attack01");
			break;
		case WeaponType.MachineGun:
			result = ((!isattack) ? "autorifle_ground_idle" : "autorifle_ground_attack01");
			break;
		case WeaponType.RPG:
			result = ((!isattack) ? "rocket_ground_idle" : "rocket_ground_attack01");
			break;
		case WeaponType.ViolenceGun:
			result = ((!isattack) ? "shootgun_ground_idle" : "shootgun_ground_attack01");
			break;
		}
		return result;
	}

	private string GetRunAnimName(WeaponType weapontype)
	{
		string result = string.Empty;
		switch (weapon_type)
		{
		case WeaponType.CloseWeapons:
			result = "melee_ground_move_forward";
			break;
		case WeaponType.Crossbow:
			result = "crossbow_ground_move_forward";
			break;
		case WeaponType.LiquidFireGun:
			result = "holdgun_ground_move_forward";
			break;
		case WeaponType.MachineGun:
			result = "autorifle_ground_move_forward";
			break;
		case WeaponType.RPG:
			result = "rocket_ground_move_forward";
			break;
		case WeaponType.ViolenceGun:
			result = "shootgun_ground_move_forward";
			break;
		}
		return result;
	}

	public void SetBottomShow(bool m_show, int m_layer = 0)
	{
		if (plane_bottom != null)
		{
			plane_bottom.gameObject.SetActiveRecursively(m_show);
			plane_bottom.gameObject.layer = m_layer;
		}
	}

	public void SetPosMove(Vector3 m_old_pos, Vector3 m_target_pos, MoveType m_type)
	{
		move_old_pos = m_old_pos;
		move_target_pos = m_target_pos;
		open_move = m_type;
		base.transform.localPosition = m_old_pos;
		string runAnimName = GetRunAnimName(weapon_type);
		if (runAnimName.Length > 0 && role_now.GetComponent<Animation>()[runAnimName] != null)
		{
			role_now.GetComponent<Animation>()[runAnimName].wrapMode = WrapMode.Loop;
			role_now.GetComponent<Animation>().Play(runAnimName);
		}
		if (m_type == MoveType.Exit)
		{
			exit_event = true;
		}
	}

	private void UpdatePosMove()
	{
		if (open_move == MoveType.None)
		{
			return;
		}
		float num = 0f;
		float num2 = Vector3.Distance(move_old_pos, move_target_pos);
		Vector3 vector = move_target_pos - move_old_pos;
		vector.Normalize();
		base.transform.localPosition = base.transform.localPosition + vector * move_speed * Time.deltaTime;
		float num3 = Vector3.Distance(base.transform.localPosition, move_old_pos);
		if (!(num3 > num2))
		{
			return;
		}
		base.transform.localPosition = move_target_pos;
		if (open_move == MoveType.Exit)
		{
			return;
		}
		open_move = MoveType.None;
		if (role_now != null)
		{
			stop_event = true;
			role_now.GetComponent<Animation>().Stop();
			string animName = GetAnimName(weapon_type);
			if (animName.Length > 0 && role_now.GetComponent<Animation>()[animName] != null)
			{
				role_now.GetComponent<Animation>()[animName].time = 0f;
				role_now.GetComponent<Animation>().CrossFade(animName);
			}
		}
	}

	public bool PosMoveStopEvent()
	{
		if (stop_event)
		{
			stop_event = false;
			return true;
		}
		return false;
	}

	public bool PosMoveExitEvent()
	{
		if (exit_event)
		{
			exit_event = false;
			return true;
		}
		return false;
	}

	public bool IsEmptyWeapon()
	{
		if (weapon_now == null)
		{
			return true;
		}
		return false;
	}
}
