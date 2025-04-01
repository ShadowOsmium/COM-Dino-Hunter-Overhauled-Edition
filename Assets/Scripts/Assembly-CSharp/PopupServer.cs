using UnityEngine;

public class PopupServer : MonoBehaviour
{
	public GameObject go_popup;

	public TUILabel label_text;

	public TUIButtonClick btn_cancle;

	public TUIButtonClick btn_ok;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void DoCreate(string m_text, GameObject m_invoke_obj, string m_component_name, string m_invoke_ok_func, string m_invoke_cancel_func)
	{
		if (go_popup != null && go_popup.GetComponent<Animation>() != null)
		{
			go_popup.GetComponent<Animation>().Play();
		}
		if (label_text != null && m_text != string.Empty)
		{
			label_text.Text = m_text;
		}
		if (btn_cancle != null)
		{
			btn_cancle.invokeOnEvent = true;
			btn_cancle.invokeObject = m_invoke_obj;
			btn_cancle.componentName = m_component_name;
			btn_cancle.invokeFunctionName = m_invoke_cancel_func;
			btn_cancle.Reset();
		}
		if (btn_ok != null)
		{
			btn_ok.invokeOnEvent = true;
			btn_ok.invokeObject = m_invoke_obj;
			btn_ok.componentName = m_component_name;
			btn_ok.invokeFunctionName = m_invoke_ok_func;
			btn_ok.Reset();
		}
	}

	public void DoDestroy()
	{
		Object.Destroy(base.gameObject);
	}
}
