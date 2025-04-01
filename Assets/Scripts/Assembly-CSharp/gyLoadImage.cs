using UnityEngine;

public class gyLoadImage
{
	public Texture m_Texture { get; set; }

	public gyLoadImage()
	{
		m_Texture = null;
	}

	public void Load(string sPath)
	{
		sPath = sPath.Replace("/", "//");
		sPath = "file://" + sPath;
		Debug.Log(sPath);
		WWW wWW = new WWW(sPath);
		while (!wWW.isDone)
		{
		}
		if (wWW.error == null)
		{
			m_Texture = wWW.texture;
		}
	}

	public static Texture2D Resize(Texture2D texture, int width, int height)
	{
		if (texture == null)
		{
			return null;
		}
		Texture2D texture2D = new Texture2D(width, height);
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				Color pixelBilinear = texture.GetPixelBilinear((float)i / (float)width, (float)j / (float)height);
				texture2D.SetPixel(i, j, pixelBilinear);
			}
		}
		texture2D.Apply();
		return texture2D;
	}
}
