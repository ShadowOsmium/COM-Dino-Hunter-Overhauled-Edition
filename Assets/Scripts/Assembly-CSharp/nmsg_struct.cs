using TNetSdk;

public class nmsg_struct
{
	public kGameNetEnum msghead;

	public virtual SFSObject Pack()
	{
		return new SFSObject();
	}

	public virtual void UnPack(SFSObject data)
	{
	}
}
