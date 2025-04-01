using UnityEngine;

public class LevelStars : MonoBehaviour
{
	public TUIMeshSprite star01;

	public TUIMeshSprite star02;

	public TUIMeshSprite star03;

	public TUIMeshSprite star04;

	public TUIMeshSprite star05;

	private string texture_empty = "shenjixingxing2";

	private string texture_full = "shenjixingxing1";

	public GameObject prefab_star_blink;

	private void Start()
	{
		if (texture_empty == string.Empty || texture_full == string.Empty)
		{
			Debug.Log("no texture!");
			return;
		}
		star01.texture = texture_empty;
		star02.texture = texture_empty;
		star03.texture = texture_empty;
		star04.texture = texture_empty;
		star05.texture = texture_empty;
	}

	private void Update()
	{
	}

	public void SetStars(int count, Vector3 m_position, int blink_index = 0)
	{
		if (texture_empty == string.Empty || texture_full == string.Empty)
		{
			Debug.Log("no texture!");
			return;
		}
		base.gameObject.SetActiveRecursively(true);
		base.gameObject.transform.localPosition = m_position;
		switch (count)
		{
		case 0:
			star01.texture = texture_empty;
			star02.texture = texture_empty;
			star03.texture = texture_empty;
			star04.texture = texture_empty;
			star05.texture = texture_empty;
			break;
		case 1:
			star01.texture = texture_full;
			star02.texture = texture_empty;
			star03.texture = texture_empty;
			star04.texture = texture_empty;
			star05.texture = texture_empty;
			break;
		case 2:
			star01.texture = texture_full;
			star02.texture = texture_full;
			star03.texture = texture_empty;
			star04.texture = texture_empty;
			star05.texture = texture_empty;
			break;
		case 3:
			star01.texture = texture_full;
			star02.texture = texture_full;
			star03.texture = texture_full;
			star04.texture = texture_empty;
			star05.texture = texture_empty;
			break;
		case 4:
			star01.texture = texture_full;
			star02.texture = texture_full;
			star03.texture = texture_full;
			star04.texture = texture_full;
			star05.texture = texture_empty;
			break;
		case 5:
			star01.texture = texture_full;
			star02.texture = texture_full;
			star03.texture = texture_full;
			star04.texture = texture_full;
			star05.texture = texture_full;
			break;
		}
		if (prefab_star_blink != null)
		{
			switch (blink_index)
			{
			case 1:
			{
				GameObject gameObject5 = (GameObject)Object.Instantiate(prefab_star_blink);
				gameObject5.transform.parent = star01.transform;
				gameObject5.transform.localPosition = new Vector3(0f, 0f, -1f);
				gameObject5.GetComponent<StarBlink>().ShowBlink();
				break;
			}
			case 2:
			{
				GameObject gameObject4 = (GameObject)Object.Instantiate(prefab_star_blink);
				gameObject4.transform.parent = star02.transform;
				gameObject4.transform.localPosition = new Vector3(0f, 0f, -1f);
				gameObject4.GetComponent<StarBlink>().ShowBlink();
				break;
			}
			case 3:
			{
				GameObject gameObject3 = (GameObject)Object.Instantiate(prefab_star_blink);
				gameObject3.transform.parent = star03.transform;
				gameObject3.transform.localPosition = new Vector3(0f, 0f, -1f);
				gameObject3.GetComponent<StarBlink>().ShowBlink();
				break;
			}
			case 4:
			{
				GameObject gameObject2 = (GameObject)Object.Instantiate(prefab_star_blink);
				gameObject2.transform.parent = star04.transform;
				gameObject2.transform.localPosition = new Vector3(0f, 0f, -1f);
				gameObject2.GetComponent<StarBlink>().ShowBlink();
				break;
			}
			case 5:
			{
				GameObject gameObject = (GameObject)Object.Instantiate(prefab_star_blink);
				gameObject.transform.parent = star05.transform;
				gameObject.transform.localPosition = new Vector3(0f, 0f, -1f);
				gameObject.GetComponent<StarBlink>().ShowBlink();
				break;
			}
			}
		}
	}

	public void SetStars(int count)
	{
		if (texture_empty == string.Empty || texture_full == string.Empty)
		{
			Debug.Log("no texture!");
			return;
		}
		base.gameObject.SetActiveRecursively(true);
		switch (count)
		{
		case 0:
			star01.texture = texture_empty;
			star02.texture = texture_empty;
			star03.texture = texture_empty;
			star04.texture = texture_empty;
			star05.texture = texture_empty;
			break;
		case 1:
			star01.texture = texture_full;
			star02.texture = texture_empty;
			star03.texture = texture_empty;
			star04.texture = texture_empty;
			star05.texture = texture_empty;
			break;
		case 2:
			star01.texture = texture_full;
			star02.texture = texture_full;
			star03.texture = texture_empty;
			star04.texture = texture_empty;
			star05.texture = texture_empty;
			break;
		case 3:
			star01.texture = texture_full;
			star02.texture = texture_full;
			star03.texture = texture_full;
			star04.texture = texture_empty;
			star05.texture = texture_empty;
			break;
		case 4:
			star01.texture = texture_full;
			star02.texture = texture_full;
			star03.texture = texture_full;
			star04.texture = texture_full;
			star05.texture = texture_empty;
			break;
		case 5:
			star01.texture = texture_full;
			star02.texture = texture_full;
			star03.texture = texture_full;
			star04.texture = texture_full;
			star05.texture = texture_full;
			break;
		}
	}

	public void SetStarsDisable()
	{
		base.gameObject.SetActiveRecursively(false);
	}
}
