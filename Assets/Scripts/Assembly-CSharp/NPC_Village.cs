using UnityEngine;

public class NPC_Village : MonoBehaviour
{
	public enum NPCType
	{
		NPC_01,
		NPC_02
	}

	public NPCType npc_type;

	public GameObject spark_prefab;

	private bool is_playing;

	private float ani_time;

	private float ani_total_time;

	private void Start()
	{
		if (base.GetComponent<Animation>() != null)
		{
			base.GetComponent<Animation>().wrapMode = WrapMode.Loop;
		}
	}

	private void Update()
	{
		UpdateAnimation(Time.deltaTime);
	}

	protected void UpdateAnimation(float delta_time)
	{
		if (base.GetComponent<Animation>() == null)
		{
			return;
		}
		ani_total_time += delta_time;
		if (!(ani_total_time >= ani_time))
		{
			return;
		}
		ani_total_time = 0f;
		switch (Random.Range(1, 1000) % 2)
		{
		case 0:
			ani_time = Random.Range(0.5f, 1.5f);
			if (npc_type == NPCType.NPC_01)
			{
				base.GetComponent<Animation>().CrossFade("Standby02");
			}
			else if (npc_type == NPCType.NPC_02)
			{
				base.GetComponent<Animation>().CrossFade("Standby04");
			}
			break;
		case 1:
			ani_time = Random.Range(1f, 2f);
			if (npc_type == NPCType.NPC_01)
			{
				base.GetComponent<Animation>().CrossFade("Standby01");
			}
			else if (npc_type == NPCType.NPC_02)
			{
				base.GetComponent<Animation>().CrossFade("Standby03");
			}
			break;
		}
	}

	public void PlaySpark()
	{
		if (spark_prefab != null)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(spark_prefab);
			gameObject.transform.parent = base.transform;
			gameObject.transform.localPosition = new Vector3(0.5506728f, 0.5539323f, 0.6555531f);
			gameObject.transform.eulerAngles = new Vector3(0f, 170.03f, 0f);
			Object.Destroy(gameObject, 1f);
			CUISound.GetInstance().Play("Mat_Blacksmith");
		}
	}
}
