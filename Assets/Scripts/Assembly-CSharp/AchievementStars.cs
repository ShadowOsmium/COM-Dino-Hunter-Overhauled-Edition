using UnityEngine;

public class AchievementStars : MonoBehaviour
{
	public TUIMeshSprite img_star01;

	public TUIMeshSprite img_star02;

	public TUIMeshSprite img_star03;

	private void Start()
	{
		if (img_star01 == null || img_star02 == null || img_star03 == null)
		{
			Debug.Log("error!");
		}
	}

	private void Update()
	{
	}

	public void SetInfo(AchievementLevelType m_level)
	{
		switch (m_level)
		{
		case AchievementLevelType.Level0:
			img_star01.gameObject.SetActiveRecursively(false);
			img_star02.gameObject.SetActiveRecursively(false);
			img_star03.gameObject.SetActiveRecursively(false);
			break;
		case AchievementLevelType.Level1:
			img_star01.gameObject.SetActiveRecursively(true);
			img_star02.gameObject.SetActiveRecursively(false);
			img_star03.gameObject.SetActiveRecursively(false);
			break;
		case AchievementLevelType.Level2:
			img_star01.gameObject.SetActiveRecursively(true);
			img_star02.gameObject.SetActiveRecursively(true);
			img_star03.gameObject.SetActiveRecursively(false);
			break;
		case AchievementLevelType.Level3:
			img_star01.gameObject.SetActiveRecursively(true);
			img_star02.gameObject.SetActiveRecursively(true);
			img_star03.gameObject.SetActiveRecursively(true);
			break;
		}
	}

	public Vector3 GetStarPos(AchievementLevelType m_level)
	{
		switch (m_level)
		{
		case AchievementLevelType.Level1:
			return img_star01.transform.localPosition;
		case AchievementLevelType.Level2:
			return img_star02.transform.localPosition;
		case AchievementLevelType.Level3:
			return img_star03.transform.localPosition;
		default:
			return Vector3.zero;
		}
	}
}
