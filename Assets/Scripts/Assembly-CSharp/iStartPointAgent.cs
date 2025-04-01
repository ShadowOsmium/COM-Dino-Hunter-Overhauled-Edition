using UnityEngine;

[ExecuteInEditMode]
public class iStartPointAgent : MonoBehaviour
{
	public int nID;

	protected iStartPointEditor m_StartPointManager;

	private void Start()
	{
	}

	private void Update()
	{
		if (!(m_StartPointManager == null))
		{
			CStartPoint cStartPoint = new CStartPoint();
			cStartPoint.v3Pos = base.transform.position;
			cStartPoint.v3Size = base.transform.localScale;
			m_StartPointManager.SetPoint(nID, cStartPoint);
		}
	}

	private void OnDestroy()
	{
		m_StartPointManager.DelPoint(nID);
	}

	public void Initialize(iStartPointEditor manager, int nID, Vector3 v3Pos, Vector3 v3Size)
	{
		m_StartPointManager = manager;
		this.nID = nID;
		base.transform.position = v3Pos;
		base.transform.localScale = v3Size;
	}
}
