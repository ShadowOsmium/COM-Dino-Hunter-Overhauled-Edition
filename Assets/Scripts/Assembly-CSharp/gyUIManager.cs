using UnityEngine;

public class gyUIManager : MonoBehaviour
{
	public Transform mParent;

	public Transform mAchorCenter;

	public gyUIPortrait mHeadPortrait;

	public Transform[] mTeamMatePortraitNode;

	public GameObject mPause;

	public GameObject mSkip;

	public iGameUIWeapon mWeapon;

	public GameObject mFastWeapon;

	public gyUISkillButton mSkill;

	public gyUIWheelButton mWheelMove;

	public gyUIWheelButton mWheelShoot;

	public gyUIScreenMask mScreenMask;

	public gyUIMovieMask mMovieMask;

	public GameObject mScreenTouch;

	public iUIAchievementTip mAchievementTip;

	public iGameTaskUIPlane mTaskPlane;

	public gyUIScreenMask mScreenBloodMask;

	public gyUIPanelMissionSuccess mPanelMissionComplete;

	public gyUIPanelMissionSuccessMutiply mPanelMissionSuccessMutiply;

	public gyUIPanelMissionFailed mPanelMissionFailed;

	public gyUIPanelMissionFailedMutiply mPanelMissionFailedMutiply;

	public gyUIPanelRevive mPanelRevive;

	public gyUIPanelReviveMutiply mPanelReviveMutiply;

	public gyUIPanelMissionSuccessLevelUp mPanelLevelUp;

	public gyUIPanelMaterial mPanelMaterial;

	public gyUIGamePauseDialog mGamePauseDialog;

	public gyUIIAPIngame mIAPDialog;

	public gyUIMessageBox mMessageBox;

	public gyUIStashFullDialog mStashFullDialog;

	public gyUITutorialsPanel mTutorialsPanel;

	public gyUIBlackWarning mBlackWarning;
}
