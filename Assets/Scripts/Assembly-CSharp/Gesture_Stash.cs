using UnityEngine;

public class Gesture_Stash : TUIPageGestureEx
{
	public enum Arrangement
	{
		Horizontal,
		Vertical
	}

	public float move_sensitivity;

	public Arrangement arrangement = Arrangement.Vertical;

	public Vector2 size_xy;

	private int finger_id = -1;

	private Vector3 begin_pos;

	private Vector3 current_pos;

	private bool is_gesturing;

	private float progress;

	[SerializeField]
	private float forward_progress;

	[SerializeField]
	private float backward_progress;

	public override float CurrentProgress
	{
		get
		{
			return progress;
		}
	}

	public override float ForwardProgress
	{
		get
		{
			return forward_progress;
		}
	}

	public override float BackwardProgress
	{
		get
		{
			return backward_progress;
		}
	}

	public override bool IsGesturing
	{
		get
		{
			return is_gesturing;
		}
	}

	public override void HandlePageFrameLock()
	{
		base.HandlePageFrameLock();
		Reset();
	}

	private void Reset()
	{
		is_gesturing = false;
		finger_id = -1;
		begin_pos = Vector3.zero;
		progress = 0f;
	}

	public override bool HandleInput(TUIInput input)
	{
		switch (input.inputType)
		{
		case TUIInputType.Began:
			if (PointtIn(input.position))
			{
				finger_id = input.fingerId;
				begin_pos = input.position;
				is_gesturing = true;
			}
			progress = 0f;
			break;
		case TUIInputType.Moved:
			if (finger_id == input.fingerId && is_gesturing)
			{
				if (move_sensitivity == 0f)
				{
					progress = 1f;
				}
				else
				{
					current_pos = input.position;
					Vector3 vector = current_pos - begin_pos;
					if (arrangement == Arrangement.Horizontal)
					{
						if (move_sensitivity != 0f)
						{
							progress = (0f - vector.x) / move_sensitivity;
						}
					}
					else if (arrangement == Arrangement.Vertical && move_sensitivity != 0f)
					{
						progress = vector.y / move_sensitivity;
					}
				}
			}
			return true;
		case TUIInputType.Ended:
			if (finger_id == input.fingerId && is_gesturing)
			{
				is_gesturing = false;
				finger_id = -1;
				begin_pos = Vector3.zero;
			}
			break;
		}
		return false;
	}

	protected bool PointInRect(Vector3[] rect_points, Vector2 point)
	{
		if (point.x < rect_points[0].x)
		{
			return false;
		}
		if (point.x > rect_points[1].x)
		{
			return false;
		}
		if (point.y > rect_points[1].y)
		{
			return false;
		}
		if (point.y < rect_points[3].y)
		{
			return false;
		}
		return true;
	}

	public virtual bool PointtIn(Vector2 point)
	{
		Vector3[] array = new Vector3[4];
		Rect rect = default(Rect);
		rect.xMin = (0f - size_xy.x) / 2f;
		rect.xMax = size_xy.x / 2f;
		rect.yMin = (0f - size_xy.y) / 2f;
		rect.yMax = size_xy.y / 2f;
		array[0] = base.transform.TransformPoint(rect.xMin, rect.yMax, 0f);
		array[1] = base.transform.TransformPoint(rect.xMax, rect.yMax, 0f);
		array[2] = base.transform.TransformPoint(rect.xMax, rect.yMin, 0f);
		array[3] = base.transform.TransformPoint(rect.xMin, rect.yMin, 0f);
		return PointInRect(array, point);
	}

	public void OnDrawGizmosSelected()
	{
		Vector3[] array = new Vector3[4]
		{
			base.transform.TransformPoint((0f - size_xy.x) / 2f, size_xy.y / 2f, 0f),
			base.transform.TransformPoint(size_xy.x / 2f, size_xy.y / 2f, 0f),
			base.transform.TransformPoint(size_xy.x / 2f, (0f - size_xy.y) / 2f, 0f),
			base.transform.TransformPoint((0f - size_xy.x) / 2f, (0f - size_xy.y) / 2f, 0f)
		};
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(array[0], array[1]);
		Gizmos.DrawLine(array[1], array[2]);
		Gizmos.DrawLine(array[2], array[3]);
		Gizmos.DrawLine(array[3], array[0]);
		Gizmos.DrawLine(array[0], array[2]);
	}
}
