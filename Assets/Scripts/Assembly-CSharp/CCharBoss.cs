using System.Collections.Generic;
using gyEvent;
using UnityEngine;

public class CCharBoss : CCharMob
{
    public class CBodyPart
    {
        public float m_fHardinessCur;

        public float m_fHardinessMax;

        public List<kAnimEnum> m_ltAnim;

        public float m_fDmgRate;

        public kAnimEnum GetAnimRadnom()
        {
            if (m_ltAnim == null || m_ltAnim.Count < 1)
            {
                return kAnimEnum.None;
            }
            return m_ltAnim[Random.Range(0, m_ltAnim.Count)];
        }
    }

    protected Dictionary<int, CBodyPart> m_dictBodyPart;

    protected CAIManagerInfo m_curAIManager;

    protected List<CAITriggerInfo> m_curTriggerList;

    protected List<CAITriggerInfo> m_tmpTriggerList;

    protected CAITriggerInfo m_curTrigger;

    protected float m_fLifeTime;

    protected int m_nChangeAI;

    protected List<GameObject> m_ltBodyEffect;

    protected bool m_bInBlack;

    protected float m_fCurBlackLife;

    protected float m_fMaxBlackLife;

    protected Renderer m_BlackGearRenderer;

    public int ChangeAI
    {
        get
        {
            return m_nChangeAI;
        }
        set
        {
            m_nChangeAI = value;
        }
    }

    public bool isInBlack
    {
        get
        {
            return m_bInBlack;
        }
    }

    public float CurBlackLife
    {
        get
        {
            return m_fCurBlackLife;
        }
    }

    public float MaxBlackLife
    {
        get
        {
            return m_fMaxBlackLife;
        }
    }

    public Dictionary<int, CBodyPart> GetBodyPart()
    {
        return m_dictBodyPart;
    }

    public new void Awake()
    {
        base.Awake();
        m_nType = kCharType.Boss;
        m_dictBodyPart = new Dictionary<int, CBodyPart>();
        m_curAIManager = null;
        m_curTriggerList = new List<CAITriggerInfo>();
        m_tmpTriggerList = new List<CAITriggerInfo>();
        m_curTrigger = null;
        m_fLifeTime = 0f;
        m_bShowTime = false;
        m_ltBodyEffect = new List<GameObject>();
        m_nChangeAI = -1;
    }

    public new void Start()
    {
        base.Start();
    }

    public new void Update()
    {
        base.Update();
        m_fLifeTime += Time.deltaTime;
        UpdateAITrigger(Time.deltaTime);
        if (m_nChangeAI <= 0)
        {
            return;
        }
        UnityEngine.Debug.Log("Boss进入状态 AI:" + m_nChangeAI);
        m_bShowTime = false;
        SetAI(m_nChangeAI);
        if (base.m_GameScene.m_MGManager != null)
        {
            CEventManager eventManager = base.m_GameScene.m_MGManager.GetEventManager();
            if (eventManager != null)
            {
                eventManager.Trigger(new EventCondition_MobByWave(GenerateWaveID, GenerateSequence, 2, m_nChangeAI));
                eventManager.Trigger(new EventCondition_MobByID(base.ID, 2, m_nChangeAI));
            }
        }
        m_nChangeAI = -1;
    }

    public override void InitMob(int nMobID, int nMobLevel)
    {
        base.InitMob(nMobID, nMobLevel);
        if (base.m_GameScene.CurGameLevelInfo != null && base.m_GameScene.CurGameLevelInfo.sSceneName == "SceneSnow")
        {
            iStepEffectLeft component = m_ModelEntity.GetComponent<iStepEffectLeft>();
            if (component != null)
            {
                component.nPrefabID = 1914;
            }
            iStepEffectRight component2 = m_ModelEntity.GetComponent<iStepEffectRight>();
            if (component2 != null)
            {
                component2.nPrefabID = 1914;
            }
        }
        if (m_BlackGear != null)
        {
            m_BlackGear.gameObject.active = false;
            m_BlackGearRenderer = m_BlackGear.GetComponent<Renderer>();
        }
    }

    public override void Destroy()
    {
        ClearBodyEffect();
        base.Destroy();
    }

    public override void InitHardiness(int nMobID, int nMobLevel)
    {
        CMobInfoLevel mobInfo = base.m_GameData.GetMobInfo(nMobID, nMobLevel);
        if (mobInfo == null || mobInfo.ltHardinessInfo == null)
        {
            return;
        }
        foreach (CHardinessInfo item in mobInfo.ltHardinessInfo)
        {
            CBodyPart cBodyPart = new CBodyPart();
            cBodyPart.m_fHardinessMax = item.fHardiness;
            cBodyPart.m_fHardinessCur = cBodyPart.m_fHardinessMax;
            cBodyPart.m_ltAnim = new List<kAnimEnum>();
            cBodyPart.m_ltAnim.Add((kAnimEnum)item.nAnimEnum);
            cBodyPart.m_fDmgRate = item.fDmgRate;
            m_dictBodyPart.Add(item.nPartID, cBodyPart);
        }
    }

    public override void InitAnimData()
    {
        base.InitAnimData();
    }

    public override bool IsMobCanThink()
    {
        return base.IsMobCanThink();
    }

    public override void AddHP(float fHP)
    {
        base.AddHP(fHP);
    }

    public override void OnDead(kDeadMode nDeathMode)
    {
        base.OnDead(nDeathMode);
        if (base.m_GameData.m_DataCenter != null)
        {
            base.m_GameData.m_DataCenter.AddKillMonster(base.ID);
        }
    }

    protected bool AddHardinessValue(CBodyPart info, float fValue)
    {
        if (info == null)
        {
            return false;
        }
        info.m_fHardinessCur += fValue * (info.m_fDmgRate / 100f);
        if (info.m_fHardinessCur <= 0f)
        {
            info.m_fHardinessCur = info.m_fHardinessMax;
            m_HurtAnim = info.GetAnimRadnom();
            return true;
        }
        if (info.m_fHardinessCur > info.m_fHardinessMax)
        {
            info.m_fHardinessCur = info.m_fHardinessMax;
        }
        return false;
    }

    protected void UpdateAITrigger(float deltaTime)
    {
        if (m_nChangeAI != -1 || m_curTriggerList == null)
        {
            return;
        }
        m_tmpTriggerList.Clear();
        foreach (CAITriggerInfo curTrigger in m_curTriggerList)
        {
            if (curTrigger.nAI == m_nCurAIID)
            {
                continue;
            }
            switch (curTrigger.nType)
            {
                case 1:
                    if (MyUtils.Compare(curTrigger.nValue, curTrigger.nOprate, m_fLifeTime, 0f) && (m_curTrigger == null || m_curTrigger.nPriority < curTrigger.nPriority))
                    {
                        m_tmpTriggerList.Add(curTrigger);
                    }
                    break;
                case 2:
                    if (MyUtils.Compare(curTrigger.nValue, curTrigger.nOprate, m_fHP, m_fHPMax) && (m_curTrigger == null || m_curTrigger.nPriority < curTrigger.nPriority))
                    {
                        m_tmpTriggerList.Add(curTrigger);
                    }
                    break;
            }
        }
        if (m_tmpTriggerList.Count == 0)
        {
            return;
        }
        if (m_tmpTriggerList.Count == 1)
        {
            m_curTrigger = m_tmpTriggerList[0];
        }
        else
        {
            for (int i = 0; i < m_tmpTriggerList.Count; i++)
            {
                if (i == 0)
                {
                    m_curTrigger = m_tmpTriggerList[i];
                }
                else if (m_curTrigger.nPriority < m_tmpTriggerList[i].nPriority)
                {
                    m_curTrigger = m_tmpTriggerList[i];
                }
            }
        }
        if (m_curTrigger != null)
        {
            m_nChangeAI = m_curTrigger.nAI;
        }
    }

    public override void InitAI(int nAIManager)
    {
        base.InitAI(nAIManager);
        CAIManagerInfo aIManagerInfo = base.m_GameData.GetAIManagerInfo(nAIManager);
        if (aIManagerInfo == null)
        {
            return;
        }
        m_curTriggerList.Clear();
        foreach (CAITriggerInfo item in aIManagerInfo.ltAITrigger)
        {
            m_curTriggerList.Add(item);
        }
    }

    protected override void OnEnterAI(int nLastAI, int nAI)
    {
        CAIInfo aIInfo = base.m_GameData.GetAIInfo(nAI);
        if (aIInfo == null)
        {
            return;
        }
        ClearBodyEffect();
        for (int i = 0; i < aIInfo.ltEffect.Count; i++)
        {
            switch (aIInfo.ltEffect[i])
            {
                case 1500:
                    AddBodyEffect(1500, GetBone(8));
                    AddBodyEffect(1500, GetBone(9));
                    break;
                case 1501:
                    AddBodyEffect(1501, GetBone(2));
                    break;
            }
        }
    }

    protected void ClearBodyEffect()
    {
        foreach (GameObject item in m_ltBodyEffect)
        {
            Object.Destroy(item);
        }
        m_ltBodyEffect.Clear();
    }

    protected void AddBodyEffect(int nPrefabID, Transform node)
    {
        if (node == null)
        {
            return;
        }
        GameObject gameObject = PrefabManager.Get(nPrefabID);
        if (!(gameObject == null))
        {
            GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject);
            if (!(gameObject2 == null))
            {
                gameObject2.transform.parent = node;
                gameObject2.transform.localPosition = Vector3.zero;
                gameObject2.transform.localRotation = Quaternion.identity;
                m_ltBodyEffect.Add(gameObject2);
            }
        }
    }

    public void SetReadyToBlack(bool bReadyToBlack, float fBlackLife = 0f)
    {
        m_bReadyToBlack = bReadyToBlack;
        m_fReadyToBlackLife = fBlackLife;
        if (bReadyToBlack)
        {
            ResetAI();
        }
    }

    public void SetBlack(bool bBlack, float fBlackLife = 0f)
    {
        if (m_bInBlack == bBlack)
        {
            return;
        }
        m_bInBlack = bBlack;
        m_fCurBlackLife = fBlackLife;
        m_fMaxBlackLife = fBlackLife;
        if (bBlack)
        {
            SetLifeBarStyle(1, 1f);
            if (m_BlackGear != null)
            {
                m_BlackGear.gameObject.active = true;
            }
            if (base.m_GameScene.m_nBlackMonsterCount == 0)
            {
                iGameUIBase gameUI = base.m_GameScene.GetGameUI();
                if (gameUI != null)
                {
                    gameUI.ShowBlackWarning(true);
                }
            }
            base.m_GameScene.m_nBlackMonsterCount++;
            return;
        }
        SetLifeBarStyle(0, base.CurHP / base.MaxHP);
        if (m_BlackGear != null)
        {
            m_BlackGear.gameObject.active = false;
        }
        Transform bone = GetBone(2);
        if (bone != null)
        {
            base.m_GameScene.AddEffect(bone.position, base.Dir2D, 2f, 1952);
        }
        base.m_GameScene.m_nBlackMonsterCount--;
        if (base.m_GameScene.m_nBlackMonsterCount < 0)
        {
            base.m_GameScene.m_nBlackMonsterCount = 0;
        }
        if (base.m_GameScene.m_nBlackMonsterCount == 0)
        {
            iGameUIBase gameUI2 = base.m_GameScene.GetGameUI();
            if (gameUI2 != null)
            {
                gameUI2.ShowBlackWarning(false);
            }
        }
    }

    public void AddBlackDmg(float fDmg)
    {
        if (m_bInBlack)
        {
            m_fCurBlackLife += fDmg;
            if (m_LifeBar != null)
            {
                m_LifeBar.SetLife(m_fCurBlackLife / m_fMaxBlackLife);
            }
            if (m_fCurBlackLife <= 0f)
            {
                SetBlack(false);
            }
            else if (m_fCurBlackLife > m_fMaxBlackLife)
            {
                m_fCurBlackLife = m_fMaxBlackLife;
            }
        }
    }
}