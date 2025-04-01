public class CBuffInfo
{
	public int nID;

	public int nType;

	public int nSlot;

	public int nPriority;

	public int[] arrEffHold;

	public int[] arrEffAdd;

	public int[] arrEffDel;

	public string sAudioEffHold = string.Empty;

	public string sAudioEffAdd = string.Empty;

	public string sAudioEffDel = string.Empty;

	public float fEffectTime;

	public int[] arrFunc;

	public int[] arrValueX;

	public int[] arrValueY;

	public CBuffInfo()
	{
		arrEffHold = new int[2];
		arrEffAdd = new int[2];
		arrEffDel = new int[2];
		arrFunc = new int[3];
		arrValueX = new int[3];
		arrValueY = new int[3];
	}
}
