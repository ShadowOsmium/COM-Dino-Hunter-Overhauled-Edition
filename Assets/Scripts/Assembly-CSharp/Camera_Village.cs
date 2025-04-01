using System;
using UnityEngine;

public class Camera_Village : MonoBehaviour
{
	public float rotation_damping = 0.5f;

	public float angle_min = 125f;

	public float angle_max = 195f;

	public float border_min = 120f;

	public float border_max = 200f;

	private float target_angle;

	private bool open_closer;

	private Vector3 target_position;

	private float closer_angle;

	private bool open_scroll = true;

	private bool open_scroll_back;

	private float angle_speed;

	private float closer_move_speed = 0.8f;

	private float closer_angle_speed = 2f;

	private void Start()
	{
		target_angle = base.transform.rotation.eulerAngles.y;
		closer_angle = base.transform.rotation.eulerAngles.y;
	}

	private void LateUpdate()
	{
		UpdateMove();
		UpdateCloser();
	}

	public void DoBegin()
	{
		target_angle = base.transform.rotation.eulerAngles.y;
		open_scroll = false;
	}

	public void DoMoveBegin()
	{
		open_scroll = false;
	}

	public void DoMove(float wapram)
	{
		float num = wapram / 400f;
		if (num > 1f)
		{
			num = 1f;
		}
		if (num < -1f)
		{
			num = -1f;
		}
		float num2 = Mathf.Asin(num) * 2f * (180f / (float)Math.PI);
		if (base.transform.eulerAngles.y > angle_max || base.transform.eulerAngles.y < angle_min)
		{
			num2 *= 0.25f;
		}
		target_angle -= num2;
		if (target_angle < border_min)
		{
			target_angle = border_min;
		}
		else if (target_angle > border_max)
		{
			target_angle = border_max;
		}
	}

	public void DoMoveEnd()
	{
		open_scroll = true;
	}

	private void UpdateMove()
	{
		if (!open_scroll)
		{
			float y = base.transform.eulerAngles.y;
			y = Mathf.LerpAngle(y, target_angle, rotation_damping * 8f * Time.deltaTime);
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, y, base.transform.eulerAngles.z);
			return;
		}
		if (base.transform.eulerAngles.y < angle_min)
		{
			target_angle = angle_min;
		}
		else if (base.transform.eulerAngles.y > angle_max)
		{
			target_angle = angle_max;
		}
		float y2 = base.transform.eulerAngles.y;
		y2 = Mathf.LerpAngle(y2, target_angle, rotation_damping * 8f * Time.deltaTime);
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, y2, base.transform.eulerAngles.z);
	}

	public void SetCloser(Transform go_target)
	{
		if (!open_closer)
		{
			open_closer = true;
			target_position = go_target.position;
			closer_angle = Quaternion.FromToRotation(base.transform.forward, target_position - base.transform.position).eulerAngles.y;
			if (closer_angle > 180f)
			{
				closer_angle -= 360f;
			}
			closer_angle += base.transform.eulerAngles.y;
		}
	}

	private void UpdateCloser()
	{
		if (open_closer)
		{
			Vector3 position = base.transform.transform.position;
			position = Vector3.Lerp(position, target_position, closer_move_speed * Time.deltaTime);
			base.transform.position = position;
			float y = base.transform.eulerAngles.y;
			y = Mathf.Lerp(y, closer_angle, closer_angle_speed * Time.deltaTime);
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, y, base.transform.eulerAngles.z);
		}
	}

	public void SetCurrentAngle(Vector3 m_angle)
	{
		base.transform.eulerAngles = m_angle;
		if (base.transform.eulerAngles.y < angle_min)
		{
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, angle_min, base.transform.eulerAngles.z);
		}
		if (base.transform.eulerAngles.y > angle_max)
		{
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, angle_max, base.transform.eulerAngles.z);
		}
		target_angle = base.transform.eulerAngles.y;
	}

	public Vector3 GetCurrentAngle()
	{
		return base.transform.eulerAngles;
	}

	public float GetPersentAngle()
	{
		float result = 0f;
		if (angle_max == angle_min)
		{
			Debug.Log("error!");
			return result;
		}
		return (base.transform.eulerAngles.y - angle_min) / (angle_max - angle_min);
	}
}
