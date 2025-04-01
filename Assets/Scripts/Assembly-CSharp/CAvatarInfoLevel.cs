using System.Collections.Generic;

public class CAvatarInfoLevel
{
	public int m_nLevel;

	public int[] arrFunc;

	public int[] arrValueX;

	public int[] arrValueY;

	public List<int> ltMaterials;

	public List<int> ltMaterialsCount;

	public bool isCrystalPurchase;

	public int nPurchasePrice;

	public string sDesc = string.Empty;

	public string sLevelUpDesc = string.Empty;

	public CAvatarInfoLevel()
	{
		arrFunc = new int[3];
		arrValueX = new int[3];
		arrValueY = new int[3];
		ltMaterials = new List<int>();
		ltMaterialsCount = new List<int>();
	}
}
