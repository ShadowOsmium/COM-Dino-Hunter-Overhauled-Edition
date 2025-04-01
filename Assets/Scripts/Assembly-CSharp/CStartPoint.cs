using UnityEngine;

public class CStartPoint
{
	public Vector3 v3Pos = Vector3.zero;

	public Vector3 v3Size = Vector3.one;

	public Vector3 GetRandom2D()
	{
		return v3Pos + new Vector3(Random.Range((0f - v3Size.x) / 2f, v3Size.x / 2f), 0f, Random.Range((0f - v3Size.z) / 2f, v3Size.z / 2f));
	}

	public Vector3 GetRandom()
	{
		return v3Pos + new Vector3(Random.Range((0f - v3Size.x) / 2f, v3Size.x / 2f), Random.Range((0f - v3Size.y) / 2f, v3Size.y / 2f), Random.Range((0f - v3Size.z) / 2f, v3Size.z / 2f));
	}

	public Vector3 GetCenter()
	{
		return v3Pos + v3Size / 2f;
	}

	public bool IsInside2D(Vector3 v3Pos)
	{
		if (v3Pos.x < this.v3Pos.x - v3Size.x / 2f || v3Pos.x > this.v3Pos.x + v3Size.x / 2f)
		{
			return false;
		}
		if (v3Pos.z < this.v3Pos.z - v3Size.z / 2f || v3Pos.z > this.v3Pos.z + v3Size.z / 2f)
		{
			return false;
		}
		return true;
	}

	public bool IsInside3D(Vector3 v3Pos)
	{
		if (!IsInside2D(v3Pos))
		{
			return false;
		}
		if (v3Pos.y < this.v3Pos.y - v3Size.y / 2f || v3Pos.y > this.v3Pos.y + v3Size.y / 2f)
		{
			return false;
		}
		return true;
	}
}
