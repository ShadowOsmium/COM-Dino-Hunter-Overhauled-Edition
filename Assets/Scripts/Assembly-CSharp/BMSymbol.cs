using System;

[Serializable]
public class BMSymbol
{
	public string sequence;

	public int x;

	public int y;

	public int width;

	public int height;

	private int mLength;

	public int length
	{
		get
		{
			if (mLength == 0)
			{
				mLength = sequence.Length;
			}
			return mLength;
		}
	}
}
