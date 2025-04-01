using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Label")]
public class UILabel : UIWidget
{
	public enum Effect
	{
		None,
		Shadow,
		Outline
	}

	[HideInInspector]
	[SerializeField]
	private UIFont mFont;

	[HideInInspector]
	[SerializeField]
	private string mText = string.Empty;

	[SerializeField]
	[HideInInspector]
	private int mMaxLineWidth;

	[HideInInspector]
	[SerializeField]
	private bool mEncoding = true;

	[SerializeField]
	[HideInInspector]
	private int mMaxLineCount;

	[HideInInspector]
	[SerializeField]
	private bool mPassword;

	[SerializeField]
	[HideInInspector]
	private bool mShowLastChar;

	[HideInInspector]
	[SerializeField]
	private Effect mEffectStyle;

	[SerializeField]
	[HideInInspector]
	private Color mEffectColor = Color.black;

	[HideInInspector]
	[SerializeField]
	private UIFont.SymbolStyle mSymbols = UIFont.SymbolStyle.Uncolored;

	[SerializeField]
	[HideInInspector]
	private Vector2 mEffectDistance = Vector2.one;

	[SerializeField]
	[HideInInspector]
	private float mLineWidth;

	[HideInInspector]
	[SerializeField]
	private bool mMultiline = true;

	private bool mShouldBeProcessed = true;

	private string mProcessedText;

	private Vector3 mLastScale = Vector3.one;

	private string mLastText = string.Empty;

	private int mLastWidth;

	private bool mLastEncoding = true;

	private int mLastCount;

	private bool mLastPass;

	private bool mLastShow;

	private Effect mLastEffect;

	private Vector3 mSize = Vector3.zero;

	private bool hasChanged
	{
		get
		{
			return mShouldBeProcessed || mLastText != text || mLastWidth != mMaxLineWidth || mLastEncoding != mEncoding || mLastCount != mMaxLineCount || mLastPass != mPassword || mLastShow != mShowLastChar || mLastEffect != mEffectStyle;
		}
		set
		{
			if (value)
			{
				mChanged = true;
				mShouldBeProcessed = true;
				return;
			}
			mShouldBeProcessed = false;
			mLastText = text;
			mLastWidth = mMaxLineWidth;
			mLastEncoding = mEncoding;
			mLastCount = mMaxLineCount;
			mLastPass = mPassword;
			mLastShow = mShowLastChar;
			mLastEffect = mEffectStyle;
		}
	}

	public UIFont font
	{
		get
		{
			return mFont;
		}
		set
		{
			if (mFont != value)
			{
				mFont = value;
				material = ((!(mFont != null)) ? null : mFont.material);
				mChanged = true;
				hasChanged = true;
				MarkAsChanged();
			}
		}
	}

	public string text
	{
		get
		{
			return mText;
		}
		set
		{
			if (value != null && mText != value)
			{
				mText = value;
				hasChanged = true;
			}
		}
	}

	public bool supportEncoding
	{
		get
		{
			return mEncoding;
		}
		set
		{
			if (mEncoding != value)
			{
				mEncoding = value;
				hasChanged = true;
				if (value)
				{
					mPassword = false;
				}
			}
		}
	}

	public UIFont.SymbolStyle symbolStyle
	{
		get
		{
			return mSymbols;
		}
		set
		{
			if (mSymbols != value)
			{
				mSymbols = value;
				hasChanged = true;
			}
		}
	}

	public int lineWidth
	{
		get
		{
			return mMaxLineWidth;
		}
		set
		{
			if (mMaxLineWidth != value)
			{
				mMaxLineWidth = value;
				hasChanged = true;
			}
		}
	}

	public bool multiLine
	{
		get
		{
			return mMaxLineCount != 1;
		}
		set
		{
			if (mMaxLineCount != 1 != value)
			{
				mMaxLineCount = ((!value) ? 1 : 0);
				hasChanged = true;
				if (value)
				{
					mPassword = false;
				}
			}
		}
	}

	public int maxLineCount
	{
		get
		{
			return mMaxLineCount;
		}
		set
		{
			if (mMaxLineCount != value)
			{
				mMaxLineCount = Mathf.Max(value, 0);
				hasChanged = true;
				if (value == 1)
				{
					mPassword = false;
				}
			}
		}
	}

	public bool password
	{
		get
		{
			return mPassword;
		}
		set
		{
			if (mPassword != value)
			{
				if (value)
				{
					mMaxLineCount = 1;
					mEncoding = false;
				}
				mPassword = value;
				hasChanged = true;
			}
		}
	}

	public bool showLastPasswordChar
	{
		get
		{
			return mShowLastChar;
		}
		set
		{
			if (mShowLastChar != value)
			{
				mShowLastChar = value;
				hasChanged = true;
			}
		}
	}

	public Effect effectStyle
	{
		get
		{
			return mEffectStyle;
		}
		set
		{
			if (mEffectStyle != value)
			{
				mEffectStyle = value;
				hasChanged = true;
			}
		}
	}

	public Color effectColor
	{
		get
		{
			return mEffectColor;
		}
		set
		{
			if (!mEffectColor.Equals(value))
			{
				mEffectColor = value;
				if (mEffectStyle != 0)
				{
					hasChanged = true;
				}
			}
		}
	}

	public Vector2 effectDistance
	{
		get
		{
			return mEffectDistance;
		}
		set
		{
			if (mEffectDistance != value)
			{
				mEffectDistance = value;
				hasChanged = true;
			}
		}
	}

	public string processedText
	{
		get
		{
			if (mLastScale != base.cachedTransform.localScale)
			{
				mLastScale = base.cachedTransform.localScale;
				mShouldBeProcessed = true;
			}
			if (hasChanged)
			{
				ProcessText();
			}
			return mProcessedText;
		}
	}

	public override Material material
	{
		get
		{
			Material material = base.material;
			if (material == null)
			{
				material = (this.material = ((!(mFont != null)) ? null : mFont.material));
			}
			return material;
		}
	}

	public override Vector2 relativeSize
	{
		get
		{
			if (mFont == null)
			{
				return Vector3.one;
			}
			if (hasChanged)
			{
				ProcessText();
			}
			return mSize;
		}
	}

	protected override void OnStart()
	{
		if (mLineWidth > 0f)
		{
			mMaxLineWidth = Mathf.RoundToInt(mLineWidth);
			mLineWidth = 0f;
		}
		if (!mMultiline)
		{
			mMaxLineCount = 1;
			mMultiline = true;
		}
	}

	public override void MarkAsChanged()
	{
		hasChanged = true;
		base.MarkAsChanged();
	}

	private void ProcessText()
	{
		mChanged = true;
		hasChanged = false;
		mLastText = mText;
		mProcessedText = mText.Replace("\\n", "\n");
		if (mPassword)
		{
			string text = string.Empty;
			if (mShowLastChar)
			{
				int i = 1;
				for (int length = mProcessedText.Length; i < length; i++)
				{
					text += "*";
				}
				if (mProcessedText.Length > 0)
				{
					text += mProcessedText[mProcessedText.Length - 1];
				}
			}
			else
			{
				int j = 0;
				for (int length2 = mProcessedText.Length; j < length2; j++)
				{
					text += "*";
				}
			}
			mProcessedText = mFont.WrapText(text, (float)mMaxLineWidth / base.cachedTransform.localScale.x, mMaxLineCount, false, UIFont.SymbolStyle.None);
		}
		else if (mMaxLineWidth > 0)
		{
			mProcessedText = mFont.WrapText(mProcessedText, (float)mMaxLineWidth / base.cachedTransform.localScale.x, mMaxLineCount, mEncoding, mSymbols);
		}
		else if (mMaxLineCount > 0)
		{
			mProcessedText = mFont.WrapText(mProcessedText, 100000f, mMaxLineCount, mEncoding, mSymbols);
		}
		mSize = (string.IsNullOrEmpty(mProcessedText) ? Vector2.one : mFont.CalculatePrintedSize(mProcessedText, mEncoding, mSymbols));
		float x = base.cachedTransform.localScale.x;
		mSize.x = Mathf.Max(mSize.x, (!(mFont != null) || !(x > 1f)) ? 1f : ((float)lineWidth / x));
		mSize.y = Mathf.Max(mSize.y, 1f);
	}

	public void MakePositionPerfect()
	{
		float num = ((!(font.atlas != null)) ? 1f : font.atlas.pixelSize);
		Vector3 localScale = base.cachedTransform.localScale;
		if (mFont.size == Mathf.RoundToInt(localScale.x / num) && mFont.size == Mathf.RoundToInt(localScale.y / num) && base.cachedTransform.localRotation == Quaternion.identity)
		{
			Vector2 vector = relativeSize * localScale.x;
			int num2 = Mathf.RoundToInt(vector.x / num);
			int num3 = Mathf.RoundToInt(vector.y / num);
			Vector3 localPosition = base.cachedTransform.localPosition;
			localPosition.x = Mathf.FloorToInt(localPosition.x / num);
			localPosition.y = Mathf.CeilToInt(localPosition.y / num);
			localPosition.z = Mathf.RoundToInt(localPosition.z);
			if (num2 % 2 == 1 && (base.pivot == Pivot.Top || base.pivot == Pivot.Center || base.pivot == Pivot.Bottom))
			{
				localPosition.x += 0.5f;
			}
			if (num3 % 2 == 1 && (base.pivot == Pivot.Left || base.pivot == Pivot.Center || base.pivot == Pivot.Right))
			{
				localPosition.y -= 0.5f;
			}
			localPosition.x *= num;
			localPosition.y *= num;
			if (base.cachedTransform.localPosition != localPosition)
			{
				base.cachedTransform.localPosition = localPosition;
			}
		}
	}

	public override void MakePixelPerfect()
	{
		if (mFont != null)
		{
			float num = ((!(font.atlas != null)) ? 1f : font.atlas.pixelSize);
			Vector3 localScale = base.cachedTransform.localScale;
			localScale.x = (float)mFont.size * num;
			localScale.y = localScale.x;
			localScale.z = 1f;
			Vector2 vector = relativeSize * localScale.x;
			int num2 = Mathf.RoundToInt(vector.x / num);
			int num3 = Mathf.RoundToInt(vector.y / num);
			Vector3 localPosition = base.cachedTransform.localPosition;
			localPosition.x = Mathf.FloorToInt(localPosition.x / num);
			localPosition.y = Mathf.CeilToInt(localPosition.y / num);
			localPosition.z = Mathf.RoundToInt(localPosition.z);
			if (base.cachedTransform.localRotation == Quaternion.identity)
			{
				if (num2 % 2 == 1 && (base.pivot == Pivot.Top || base.pivot == Pivot.Center || base.pivot == Pivot.Bottom))
				{
					localPosition.x += 0.5f;
				}
				if (num3 % 2 == 1 && (base.pivot == Pivot.Left || base.pivot == Pivot.Center || base.pivot == Pivot.Right))
				{
					localPosition.y -= 0.5f;
				}
			}
			localPosition.x *= num;
			localPosition.y *= num;
			base.cachedTransform.localPosition = localPosition;
			base.cachedTransform.localScale = localScale;
		}
		else
		{
			base.MakePixelPerfect();
		}
	}

	private void ApplyShadow(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, int start, int end, float x, float y)
	{
		Color color = mEffectColor;
		color.a *= base.color.a;
		Color32 color2 = color;
		for (int i = start; i < end; i++)
		{
			verts.Add(verts.buffer[i]);
			uvs.Add(uvs.buffer[i]);
			cols.Add(cols.buffer[i]);
			Vector3 vector = verts.buffer[i];
			vector.x += x;
			vector.y += y;
			verts.buffer[i] = vector;
			cols.buffer[i] = color2;
		}
	}

	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		if (mFont == null)
		{
			return;
		}
		MakePositionPerfect();
		Pivot pivot = base.pivot;
		int size = verts.size;
		switch (pivot)
		{
		case Pivot.TopLeft:
		case Pivot.Left:
		case Pivot.BottomLeft:
			mFont.Print(processedText, base.color, verts, uvs, cols, mEncoding, mSymbols, UIFont.Alignment.Left, 0);
			break;
		case Pivot.TopRight:
		case Pivot.Right:
		case Pivot.BottomRight:
			mFont.Print(processedText, base.color, verts, uvs, cols, mEncoding, mSymbols, UIFont.Alignment.Right, Mathf.RoundToInt(relativeSize.x * (float)mFont.size));
			break;
		default:
			mFont.Print(processedText, base.color, verts, uvs, cols, mEncoding, mSymbols, UIFont.Alignment.Center, Mathf.RoundToInt(relativeSize.x * (float)mFont.size));
			break;
		}
		if (effectStyle == Effect.None)
		{
			return;
		}
		Vector3 localScale = base.cachedTransform.localScale;
		if (localScale.x != 0f && localScale.y != 0f)
		{
			int size2 = verts.size;
			float num = 1f / (float)mFont.size;
			float num2 = num * mEffectDistance.x;
			float num3 = num * mEffectDistance.y;
			ApplyShadow(verts, uvs, cols, size, size2, num2, 0f - num3);
			if (effectStyle == Effect.Outline)
			{
				size = size2;
				size2 = verts.size;
				ApplyShadow(verts, uvs, cols, size, size2, 0f - num2, num3);
				size = size2;
				size2 = verts.size;
				ApplyShadow(verts, uvs, cols, size, size2, num2, num3);
				size = size2;
				size2 = verts.size;
				ApplyShadow(verts, uvs, cols, size, size2, 0f - num2, 0f - num3);
			}
		}
	}
}
