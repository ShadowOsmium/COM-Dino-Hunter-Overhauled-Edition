public class CBattleRegion
{
	public int nGroupID;

	public float fMin;

	public float fMax;

	public CBattleRegion(int nGroupID, float fMin, float fMax)
	{
		this.nGroupID = nGroupID;
		this.fMin = fMin;
		this.fMax = fMax;
	}

	public bool IsMatch(float fValue)
	{
		return fValue >= fMin && fValue <= fMax;
	}
}
