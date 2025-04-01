using System;
using UnityEngine;

[Serializable]
public class TUIBMFont
{
	public static class Reader
	{
		public static void Load(TUIBMFont font, string name, byte[] bytes)
		{
			font.Clear();
			if (bytes == null)
			{
				return;
			}
			TUIByteReader tUIByteReader = new TUIByteReader(bytes);
			char[] separator = new char[1] { ' ' };
			while (tUIByteReader.canRead)
			{
				string text = tUIByteReader.ReadLine();
				if (string.IsNullOrEmpty(text))
				{
					break;
				}
				string[] array = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				if (array[0] == "char")
				{
					if (array.Length <= 8)
					{
						Debug.LogError("Unexpected number of entries for the 'char' field (" + name + ", " + array.Length + "):\n" + text);
						break;
					}
					int @int = GetInt(array[1]);
					TUIBMGlyph glyph = font.GetGlyph(@int, true);
					if (glyph != null)
					{
						glyph.x = GetInt(array[2]);
						glyph.y = GetInt(array[3]);
						glyph.width = GetInt(array[4]);
						glyph.height = GetInt(array[5]);
						glyph.offsetX = GetInt(array[6]);
						glyph.offsetY = GetInt(array[7]);
						glyph.advance = GetInt(array[8]);
					}
				}
				else if (array[0] == "kerning")
				{
					if (array.Length <= 3)
					{
						Debug.LogError("Unexpected number of entries for the 'kerning' field (" + name + ", " + array.Length + "):\n" + text);
						break;
					}
					int int2 = GetInt(array[1]);
					int int3 = GetInt(array[2]);
					int int4 = GetInt(array[3]);
					TUIBMGlyph glyph2 = font.GetGlyph(int3, true);
					if (glyph2 != null)
					{
						glyph2.SetKerning(int2, int4);
					}
				}
				else if (array[0] == "common")
				{
					if (array.Length <= 5)
					{
						Debug.LogError("Unexpected number of entries for the 'common' field (" + name + ", " + array.Length + "):\n" + text);
						break;
					}
					font.charSize = GetInt(array[1]);
					font.baseOffset = GetInt(array[2]);
					font.texWidth = GetInt(array[3]);
					font.texHeight = GetInt(array[4]);
					int int5 = GetInt(array[5]);
					if (int5 != 1)
					{
						break;
					}
				}
				else if (array[0] == "page" && array.Length > 2)
				{
					font.TextureName = GetString(array[2]).Replace("\"", string.Empty);
					font.TextureName = font.TextureName.Replace(".png", string.Empty);
					font.TextureName = font.TextureName.Replace(".tga", string.Empty);
				}
			}
		}

		private static string GetString(string s)
		{
			int num = s.IndexOf('=');
			return (num != -1) ? s.Substring(num + 1) : string.Empty;
		}

		private static int GetInt(string s)
		{
			int result = 0;
			string @string = GetString(s);
			int.TryParse(@string, out result);
			return result;
		}
	}

	[SerializeField]
	private TUIBMGlyph[] mGlyphs;

	[SerializeField]
	private int mSize;

	[SerializeField]
	private int mBase;

	[SerializeField]
	private int mWidth;

	[SerializeField]
	private int mHeight;

	[SerializeField]
	private string mTextureName;

	public string TextureName
	{
		get
		{
			return mTextureName;
		}
		set
		{
			mTextureName = value;
		}
	}

	public bool isValid
	{
		get
		{
			return mGlyphs != null && mGlyphs.Length > 0;
		}
	}

	public int charSize
	{
		get
		{
			return mSize;
		}
		set
		{
			mSize = value;
		}
	}

	public int baseOffset
	{
		get
		{
			return mBase;
		}
		set
		{
			mBase = value;
		}
	}

	public int texWidth
	{
		get
		{
			return mWidth;
		}
		set
		{
			mWidth = value;
		}
	}

	public int texHeight
	{
		get
		{
			return mHeight;
		}
		set
		{
			mHeight = value;
		}
	}

	public int glyphCount
	{
		get
		{
			if (mGlyphs != null)
			{
				return mGlyphs.Length;
			}
			return 0;
		}
	}

	private int GetArraySize(int index)
	{
		if (index < 256)
		{
			return 256;
		}
		if (index < 65536)
		{
			return 65536;
		}
		if (index < 262144)
		{
			return 262144;
		}
		return 0;
	}

	public TUIBMGlyph GetGlyph(int index, bool createIfMissing)
	{
		if (mGlyphs == null)
		{
			if (!createIfMissing)
			{
				return null;
			}
			int arraySize = GetArraySize(index);
			if (arraySize == 0)
			{
				return null;
			}
			mGlyphs = new TUIBMGlyph[arraySize];
		}
		if (index >= mGlyphs.Length)
		{
			if (!createIfMissing)
			{
				return null;
			}
			int arraySize2 = GetArraySize(index);
			if (arraySize2 == 0)
			{
				return null;
			}
			TUIBMGlyph[] array = new TUIBMGlyph[arraySize2];
			for (int i = 0; i < mGlyphs.Length; i++)
			{
				array[i] = mGlyphs[i];
			}
			mGlyphs = array;
		}
		TUIBMGlyph tUIBMGlyph = mGlyphs[index];
		if (tUIBMGlyph == null && createIfMissing)
		{
			tUIBMGlyph = new TUIBMGlyph();
			mGlyphs[index] = tUIBMGlyph;
		}
		return tUIBMGlyph;
	}

	public TUIBMGlyph GetGlyph(int index)
	{
		return GetGlyph(index, false);
	}

	public void Clear()
	{
		mGlyphs = null;
	}
}
