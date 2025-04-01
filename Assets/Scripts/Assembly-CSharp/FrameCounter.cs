using UnityEngine;

[RequireComponent(typeof(GUIText))]
public class FrameCounter : MonoBehaviour
{
	public float updateInterval = 0.5f;

	private float m_accum;

	private int m_frames;

	private float m_timeLeft;

	private void Awake()
	{
		m_timeLeft = updateInterval;
	}

	private void Update()
	{
		m_timeLeft -= Time.deltaTime;
		m_accum += Time.timeScale / Time.deltaTime;
		m_frames++;
		if (m_timeLeft <= 0f)
		{
			float num = m_accum / (float)m_frames;
			float num2 = 1000f / num;
			base.GetComponent<GUIText>().text = "timePerFrame: " + num2.ToString("f2") + "ms\n";
			GUIText gUIText = base.GetComponent<GUIText>();
			gUIText.text = gUIText.text + "framePerSecond: " + num.ToString("f2");
			m_timeLeft = updateInterval;
			m_accum = 0f;
			m_frames = 0;
		}
	}
}
