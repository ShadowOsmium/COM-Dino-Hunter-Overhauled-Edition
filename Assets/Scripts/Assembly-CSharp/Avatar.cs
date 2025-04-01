using UnityEngine;

public class Avatar : MonoBehaviour
{
	public Shader m_Shader;

	public GameObject[] m_AvatarPart;

	protected GameObject[] m_AvatarEffect;

	private void Awake()
	{
		if (m_AvatarPart == null)
		{
			return;
		}
		m_AvatarEffect = new GameObject[m_AvatarPart.Length];
		for (int i = 0; i < m_AvatarPart.Length; i++)
		{
			SkinnedMeshRenderer component = m_AvatarPart[i].GetComponent<SkinnedMeshRenderer>();
			if (component != null)
			{
				if (m_Shader != null && component.material.shader.name != m_Shader.name)
				{
					component.sharedMaterial = new Material(m_Shader);
				}
				else
				{
					component.sharedMaterial = new Material(component.material);
				}
			}
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ReplaceAvatarEffect(int nIndex, GameObject effectprefab)
	{
		if (nIndex < 0 || nIndex >= m_AvatarEffect.Length)
		{
			return;
		}
		if (m_AvatarEffect[nIndex] != null)
		{
			Object.Destroy(m_AvatarEffect[nIndex]);
		}
		if (!(effectprefab == null))
		{
			GameObject gameObject = Object.Instantiate(effectprefab, m_AvatarPart[nIndex].transform.position, Quaternion.identity) as GameObject;
			if (!(gameObject == null))
			{
				gameObject.transform.parent = m_AvatarPart[nIndex].transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				m_AvatarEffect[nIndex] = gameObject;
			}
		}
	}

	public void ReplaceAvatar(int nIndex, string sPath_Prefab, string sPath_Texture)
	{
		Debug.Log(sPath_Prefab);
		Debug.Log(sPath_Texture);
		GameObject newpartprefab = Resources.Load(sPath_Prefab) as GameObject;
		Texture texture = Resources.Load(sPath_Texture) as Texture;
		ReplaceAvatar(nIndex, newpartprefab, texture);
	}

	public void ReplaceAvatar(int nIndex, string sPath_Prefab, Texture texture)
	{
		GameObject newpartprefab = Resources.Load(sPath_Prefab) as GameObject;
		ReplaceAvatar(nIndex, newpartprefab, texture);
	}

	public void ReplaceAvatar(int nIndex, GameObject newpartprefab, string sPath_Texture)
	{
		Texture texture = Resources.Load(sPath_Texture) as Texture;
		ReplaceAvatar(nIndex, newpartprefab, texture);
	}

	public void ReplaceAvatar(int nIndex, GameObject newpartprefab, Texture texture)
	{
		if (m_AvatarPart == null || newpartprefab == null || nIndex < 0 || nIndex >= m_AvatarPart.Length)
		{
			return;
		}
		GameObject gameObject = m_AvatarPart[nIndex];
		if (!(gameObject == null))
		{
			GameObject gameObject2 = Object.Instantiate(newpartprefab, Vector3.zero, Quaternion.identity) as GameObject;
			if (!(gameObject2 == null))
			{
				SkinnedMeshRenderer component = gameObject.GetComponent<SkinnedMeshRenderer>();
				SkinnedMeshRenderer componentInChildren = gameObject2.GetComponentInChildren<SkinnedMeshRenderer>();
				Replace(component, componentInChildren);
				component.sharedMaterial.mainTexture = texture;
				Object.Destroy(gameObject2);
			}
		}
	}

	protected void Replace(SkinnedMeshRenderer skinnedmeshrenderer_old, SkinnedMeshRenderer skinnedmeshrenderer_new)
	{
		if (skinnedmeshrenderer_old == null || skinnedmeshrenderer_new == null)
		{
			return;
		}
		Transform[] array = new Transform[skinnedmeshrenderer_new.bones.Length];
		for (int i = 0; i < skinnedmeshrenderer_new.bones.Length; i++)
		{
			Transform transform = null;
			Transform[] componentsInChildren = skinnedmeshrenderer_old.transform.parent.GetComponentsInChildren<Transform>();
			foreach (Transform transform2 in componentsInChildren)
			{
				if (transform2.name == skinnedmeshrenderer_new.bones[i].name)
				{
					transform = transform2;
				}
			}
			if (transform == null)
			{
				Debug.LogWarning(skinnedmeshrenderer_new.bones[i].name + " be not finded in player bones");
				break;
			}
			array[i] = transform;
		}
		skinnedmeshrenderer_old.bones = array;
		skinnedmeshrenderer_old.sharedMesh = skinnedmeshrenderer_new.sharedMesh;
	}
}
