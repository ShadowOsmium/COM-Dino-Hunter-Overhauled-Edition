using System.Collections.Generic;

public class CRecipeInfo
{
	public int nWeaponID;

	public int nWeaponIDNext;

	public int nNeedGold;

	public List<CMaterialInfo> ltMaterial;

	public CRecipeInfo()
	{
		ltMaterial = new List<CMaterialInfo>();
	}
}
