using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Texture")]
public class UITexture : UIWidget
{
	[SerializeField]
	[HideInInspector]
	private Rect mRect = new Rect(0f, 0f, 1f, 1f);

	[SerializeField]
	[HideInInspector]
	private Shader mShader;

	[HideInInspector]
	[SerializeField]
	private Texture mTexture;

	private Material mDynamicMat;

	private bool mCreatingMat;

	public Rect uvRect
	{
		get
		{
			return mRect;
		}
		set
		{
			if (mRect != value)
			{
				mRect = value;
				MarkAsChanged();
			}
		}
	}

	public Shader shader
	{
		get
		{
			if (mShader == null)
			{
				Material material = this.material;
				if (material != null)
				{
					mShader = material.shader;
				}
				if (mShader == null)
				{
					mShader = Shader.Find("Unlit/Texture");
				}
			}
			return mShader;
		}
		set
		{
			if (mShader != value)
			{
				mShader = value;
				Material material = this.material;
				if (material != null)
				{
					material.shader = value;
				}
			}
		}
	}

	public bool hasDynamicMaterial
	{
		get
		{
			return mDynamicMat != null;
		}
	}

	public override bool keepMaterial
	{
		get
		{
			return true;
		}
	}

	public override Material material
	{
		get
		{
			if (!mCreatingMat && base.material == null)
			{
				mCreatingMat = true;
				if (mainTexture != null)
				{
					if (mShader == null)
					{
						mShader = Shader.Find("Unlit/Texture");
					}
					mDynamicMat = new Material(mShader);
					mDynamicMat.hideFlags = HideFlags.DontSave;
					mDynamicMat.mainTexture = mainTexture;
					base.material = mDynamicMat;
				}
				mCreatingMat = false;
			}
			return base.material;
		}
		set
		{
			if (mDynamicMat != value && mDynamicMat != null)
			{
				NGUITools.Destroy(mDynamicMat);
				mDynamicMat = null;
			}
			base.material = value;
		}
	}

	public override Texture mainTexture
	{
		get
		{
			return (!(mTexture != null)) ? base.mainTexture : mTexture;
		}
		set
		{
			mTexture = value;
			if (material == null)
			{
				mDynamicMat = new Material(shader);
				mDynamicMat.hideFlags = HideFlags.DontSave;
				mDynamicMat.mainTexture = mainTexture;
				material = mDynamicMat;
			}
			base.mainTexture = value;
		}
	}

	private void OnDestroy()
	{
		NGUITools.Destroy(mDynamicMat);
	}

	public override void MakePixelPerfect()
	{
		Texture texture = mainTexture;
		if (texture != null)
		{
			Vector3 localScale = base.cachedTransform.localScale;
			localScale.x = (float)texture.width * uvRect.width;
			localScale.y = (float)texture.height * uvRect.height;
			localScale.z = 1f;
			base.cachedTransform.localScale = localScale;
		}
		base.MakePixelPerfect();
	}

	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		verts.Add(new Vector3(1f, 0f, 0f));
		verts.Add(new Vector3(1f, -1f, 0f));
		verts.Add(new Vector3(0f, -1f, 0f));
		verts.Add(new Vector3(0f, 0f, 0f));
		uvs.Add(new Vector2(mRect.xMax, mRect.yMax));
		uvs.Add(new Vector2(mRect.xMax, mRect.yMin));
		uvs.Add(new Vector2(mRect.xMin, mRect.yMin));
		uvs.Add(new Vector2(mRect.xMin, mRect.yMax));
		cols.Add(base.color);
		cols.Add(base.color);
		cols.Add(base.color);
		cols.Add(base.color);
	}
}
