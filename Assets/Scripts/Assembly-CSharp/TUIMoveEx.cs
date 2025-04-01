using UnityEngine;

[AddComponentMenu("TUI/Control/MoveEx")]
public class TUIMoveEx : TUIControlImpl
{
	public const int CommandBegin = 1;

	public const int CommandMoveBegin = 2;

	public const int CommandMove = 3;

	public const int CommandMoveEnd = 4;

	public const int CommandClick = 5;

	public float minX;

	public float minY;

	protected int fingerId = -1;

	protected Vector2 fingerPosition = Vector2.zero;

	protected Vector2 clickPosition = Vector2.zero;

	protected bool move;

	public override bool HandleInput(TUIInput input)
	{
		if (input.inputType == TUIInputType.Began)
		{
			if (PtInControl(input.position))
			{
				if (move)
				{
					fingerId = -1;
					fingerPosition = Vector2.zero;
					move = false;
					PostEvent(this, 4, 0f, 0f, null);
				}
				fingerId = input.fingerId;
				fingerPosition = input.position;
				move = false;
				clickPosition = Input.mousePosition;
				PostEvent(this, 1, 0f, 0f, null);
			}
			return false;
		}
		if (input.fingerId != fingerId)
		{
			return false;
		}
		if (input.inputType == TUIInputType.Moved)
		{
			if (!PtInControl(input.position))
			{
				return false;
			}
			float num = input.position.x - fingerPosition.x;
			float num2 = input.position.y - fingerPosition.y;
			float num3 = ((!(num >= 0f)) ? (0f - num) : num);
			float num4 = ((!(num2 >= 0f)) ? (0f - num2) : num2);
			if (!move && (num3 > minX || num4 > minY))
			{
				move = true;
				PostEvent(this, 2, 0f, 0f, null);
			}
			if (move)
			{
				fingerPosition = input.position;
				PostEvent(this, 3, num, num2, null);
			}
			return true;
		}
		if (input.inputType == TUIInputType.Ended)
		{
			bool flag = move;
			fingerId = -1;
			fingerPosition = Vector2.zero;
			move = false;
			if (flag)
			{
				PostEvent(this, 4, 0f, 0f, null);
				return true;
			}
			clickPosition = Input.mousePosition;
			PostEvent(this, 5, 0f, 0f, null);
			clickPosition = Vector2.zero;
			return false;
		}
		return false;
	}

	public Vector2 GetClickPosition()
	{
		return clickPosition;
	}
}
