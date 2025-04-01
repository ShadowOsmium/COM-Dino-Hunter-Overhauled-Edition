using UnityEngine;

public class iSpawnBullet_Venom : iSpawnBullet_Parabola
{
	public int nGroundVenom;

	protected override void OnHitGround(CCharBase actor, Vector3 v3HitPos)
	{
		base.OnHitGround(actor, v3HitPos);
		GameObject gameObject = m_GameScene.AddSceneGameObject(nGroundVenom, v3HitPos + new Vector3(0f, 0.01f, 0f), Vector3.forward, -1f);
		if (!(gameObject == null))
		{
			iSceneDamage component = gameObject.GetComponent<iSceneDamage>();
			if (component != null)
			{
				component.SetActive(true);
			}
		}
	}
}
