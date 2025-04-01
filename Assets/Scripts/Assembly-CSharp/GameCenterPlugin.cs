public class GameCenterPlugin
{
	public enum LOGIN_STATUS
	{
		LOGIN_STATUS_IDLE,
		LOGIN_STATUS_WAIT,
		LOGIN_STATUS_SUCCESS,
		LOGIN_STATUS_ERROR
	}

	public enum SUBMIT_STATUS
	{
		SUBMIT_STATUS_IDLE,
		SUBMIT_STATUS_WAIT,
		SUBMIT_STATUS_SUCCESS,
		SUBMIT_STATUS_ERROR
	}

	public enum FRIEND_STATUS
	{
		FRIEND_LOAD,
		FRIEND_LOADDATA,
		FRIEND_ERROR,
		FRIEND_SUCCESS
	}

	public enum PHOTO_STATUS
	{
		NONE,
		LOADING,
		SUCCESS,
		FAILED
	}

	public static void Initialize()
	{
	}

	public static void Uninitialize()
	{
	}

	public static bool IsSupported()
	{
		return true;
	}

	public static bool IsLogin()
	{
		return false;
	}

	public static bool Login()
	{
		return false;
	}

	public static LOGIN_STATUS LoginStatus()
	{
		return LOGIN_STATUS.LOGIN_STATUS_IDLE;
	}

	public static string GetAccount()
	{
		return string.Empty;
	}

	public static string GetName()
	{
		return string.Empty;
	}

	public static bool SubmitScore(string category, int score)
	{
		return false;
	}

	public static SUBMIT_STATUS SubmitScoreStatus(string category, int score)
	{
		return SUBMIT_STATUS.SUBMIT_STATUS_IDLE;
	}

	public static bool SubmitAchievement(string category, int percent)
	{
		return false;
	}

	public static SUBMIT_STATUS SubmitAchievementStatus(string category, int percent)
	{
		return SUBMIT_STATUS.SUBMIT_STATUS_IDLE;
	}

	public static bool OpenLeaderboard()
	{
		return false;
	}

	public static bool OpenLeaderboard(string category)
	{
		return false;
	}

	public static bool OpenAchievement()
	{
		return false;
	}

	public static void LoadFriends()
	{
	}

	public static FRIEND_STATUS GetLoadFriendStatus()
	{
		return FRIEND_STATUS.FRIEND_SUCCESS;
	}

	public static int GetFriendCount()
	{
		return -1;
	}

	public static string GetFriendAccount(int index)
	{
		return string.Empty;
	}

	public static bool GetPhoto(string account, byte[] imgdata)
	{
		return false;
	}

	public static void LoadPhoto(string account)
	{
	}

	public static void ReleasePhoto(string account)
	{
	}

	public static int GetPhotoState(string account)
	{
		return -1;
	}

	public static int GetPhotoSize(string account)
	{
		return 0;
	}
}
