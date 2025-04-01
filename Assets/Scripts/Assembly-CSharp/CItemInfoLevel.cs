using System.Collections.Generic;

public class CItemInfoLevel
{
	public int nID;

	public int nLevel;

	public int nType;

	public int nRare;

	public int nModel;

	public string sIcon;

	public string sName;

	public string sDesc;

	public int[] arrFunc;

	public int[] arrValueX;

	public int[] arrValueY;

	public int nTakenBuff;

	public bool isCrystalPurchase;

	public int nPurchasePrice;

	public bool isCrystalSell;

	public int nSellPrice;

	public List<int> ltMaterials;

	public List<int> ltMaterialsCount;

	public string sLevelUpDesc = string.Empty;

	public CItemInfoLevel()
	{
		arrFunc = new int[3];
		arrValueX = new int[3];
		arrValueY = new int[3];
		ltMaterials = new List<int>();
		ltMaterialsCount = new List<int>();
	}
}
