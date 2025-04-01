using UnityEngine;

public class iGameUIWeapon : MonoBehaviour
{
	public UISprite mWeaponIcon;

	public UISprite mBulletIcon;

	public UILabel mBulletLabel;

	public GameObject mPurchase;

	protected int m_nCurBullet;

	protected int m_nMaxBullet;

	protected Vector3 m_v3BulletLabelScale;

	private void Awake()
	{
		ShowPurchase(false);
		m_nCurBullet = 0;
		m_nMaxBullet = 0;
		m_v3BulletLabelScale = mBulletLabel.transform.localScale;
	}

	private void Update()
	{
	}

	public void Show(bool bShow)
	{
		base.gameObject.SetActiveRecursively(bShow);
		if (bShow)
		{
			SetBullet(m_nCurBullet, m_nMaxBullet);
		}
		else
		{
			ShowPurchase(false);
		}
	}

	public void SetIcon(string str)
	{
		if (!(mWeaponIcon == null))
		{
			GameObject gameObject = PrefabManager.Get("Artist/Atlas/Weapon/" + str);
			if (gameObject != null)
			{
				mWeaponIcon.atlas = gameObject.GetComponent<UIAtlas>();
			}
		}
	}

	public void SetBullet(int nCur, int nMax)
	{
		m_nCurBullet = nCur;
		m_nMaxBullet = nMax;
		if (nMax == 0)
		{
			mBulletLabel.gameObject.active = false;
			mBulletIcon.gameObject.active = false;
			ShowPurchase(false);
		}
		else if (!(mBulletLabel == null))
		{
			mBulletLabel.gameObject.active = true;
			mBulletIcon.gameObject.active = true;
			float num = (float)nCur / (float)nMax;
			if (num <= 0.2f)
			{
				ShowPurchase(true);
				mBulletLabel.text = "[FF0000]" + nCur + "[-]/" + nMax;
			}
			else
			{
				ShowPurchase(false);
				mBulletLabel.text = nCur + "/" + nMax;
			}
		}
	}

	public void ShowPurchase(bool bShow)
	{
		if (mPurchase == null)
		{
			return;
		}
		iGameSceneBase gameScene = iGameApp.GetInstance().m_GameScene;
		if (gameScene != null && gameScene.isTutorialStage)
		{
			return;
		}
		gyUIAnimationHop component = mPurchase.GetComponent<gyUIAnimationHop>();
		if (component != null)
		{
			if (bShow)
			{
				if (!component.isActive)
				{
					component.Go(new Vector3(0f, 1f, 0f));
				}
			}
			else
			{
				component.Stop();
			}
		}
		mPurchase.SetActiveRecursively(bShow);
	}

	public void PlayFullBulletAnim()
	{
		if (!(mBulletLabel == null))
		{
			TweenScale tweenScale = TweenScale.Begin(mBulletLabel.gameObject, 0.2f, Vector3.zero);
			if (tweenScale != null)
			{
				tweenScale.from = m_v3BulletLabelScale * 1.5f;
				tweenScale.to = m_v3BulletLabelScale;
				tweenScale.method = UITweener.Method.BounceIn;
			}
		}
	}
}
