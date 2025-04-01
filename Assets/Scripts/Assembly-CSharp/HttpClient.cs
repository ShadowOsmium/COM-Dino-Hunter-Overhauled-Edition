using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public class HttpClient
{
	public class ServerInfo
	{
		public string server;

		public string url;

		public float timeout;

		public string key;

		public ServerInfo(string server, string url, float timeout, string key)
		{
			this.server = server;
			this.url = url;
			this.timeout = timeout;
			this.key = key;
		}
	}

	private class HttpTask
	{
		private int m_taskId;

		private string m_server;

		private string m_url;

		private string m_key;

		private string m_action;

		private string m_request;

		private string m_gameObjectName;

		private string m_componentName;

		private string m_functionName;

		private string m_param;

		private float m_timeout;

		private byte[] m_requestData;

		private byte[] m_responseData;

		private bool m_complete;

		private WWW m_www;

		private int m_result;

		private string m_response;

		public HttpTask(int taskId, string server, string url, float timeout, string key, string action, string request, string gameObjectName, string componentName, string functionName, string param)
		{
			m_taskId = taskId;
			m_server = server;
			m_url = url;
			m_timeout = timeout;
			m_key = key;
			m_action = action;
			m_request = request;
			m_gameObjectName = gameObjectName;
			m_componentName = componentName;
			m_functionName = functionName;
			m_param = param;
			if (timeout < 0f)
			{
				m_timeout = -1f;
			}
			else
			{
				m_timeout = Time.realtimeSinceStartup + timeout;
			}
			UnityEngine.Debug.Log("requestdata = " + m_request);
			if (m_key != null && m_key.Length > 0)
			{
				m_requestData = XXTEAUtils.Encrypt(Encoding.UTF8.GetBytes(m_request), Encoding.UTF8.GetBytes(m_key));
			}
			else
			{
				m_requestData = Encoding.UTF8.GetBytes(m_request);
			}
			m_responseData = null;
			m_complete = false;
			m_www = null;
			m_result = -1;
			m_response = null;
		}

		public void Start()
		{
			try
			{
				m_www = new WWW(m_url + "?action=" + m_action, m_requestData);
				UnityEngine.Debug.Log(m_url + "?action=" + m_action + " ....  requestdata length = " + m_requestData.Length);
			}
			catch
			{
				Complete(-1);
			}
		}

		public void Stop()
		{
			m_www = null;
		}

		public bool Run()
		{
			if (m_timeout > 0f && m_timeout < Time.realtimeSinceStartup)
			{
				Complete(-6);
			}
			if (!m_complete && m_www.isDone)
			{
				UnityEngine.Debug.Log("--responseHeaders--");
				foreach (KeyValuePair<string, string> responseHeader in m_www.responseHeaders)
				{
					UnityEngine.Debug.Log(responseHeader.Key + " " + responseHeader.Value);
				}
				UnityEngine.Debug.Log("-----");
				m_complete = true;
				if (m_www.error != null)
				{
					m_result = -4;
					Debug.LogError(m_www.error);
					Debug.Log("error url = " + m_www.url);
				}
				else
				{
					m_result = 0;
					try
					{
						m_responseData = m_www.bytes;
						if (m_key != null && m_key.Length > 0)
						{
							byte[] array = XXTEAUtils.Decrypt(m_responseData, Encoding.UTF8.GetBytes(m_key));
							if (array != null)
							{
								m_response = Encoding.UTF8.GetString(array);
							}
							else
							{
								m_response = string.Empty;
							}
						}
						else
						{
							m_response = Encoding.UTF8.GetString(m_responseData);
						}
					}
					catch (Exception ex)
					{
						m_result = -4;
						Debug.Log("unknown error " + ex.StackTrace);
					}
				}
			}
			return m_complete;
		}

		public void FireCallback()
		{
			try
			{
				GameObject gameObject = GameObject.Find(m_gameObjectName);
				if (gameObject == null)
				{
					return;
				}
				Component component = gameObject.GetComponent(m_componentName);
				if (!(component == null))
				{
					MethodInfo method = component.GetType().GetMethod(m_functionName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (method != null)
					{
						method.Invoke(component, new object[6] { m_taskId, m_result, m_server, m_action, m_response, m_param });
					}
				}
			}
			catch
			{
			}
		}

		private void Complete(int result)
		{
			m_result = result;
			m_complete = true;
		}
	}

	private static HttpClient m_instance;

	private Dictionary<string, ServerInfo> m_serverInfoMap;

	private int m_taskId;

	private Dictionary<int, HttpTask> m_httpTaskMap;

	private HttpClient()
	{
		m_serverInfoMap = new Dictionary<string, ServerInfo>();
		m_taskId = 1;
		m_httpTaskMap = new Dictionary<int, HttpTask>();
	}

	public static HttpClient Instance()
	{
		if (m_instance == null)
		{
			m_instance = new HttpClient();
		}
		return m_instance;
	}

	public void AddServer(string server, string url, float timeout, string key)
	{
		ServerInfo serverInfo = new ServerInfo(server, url, timeout, key);
		m_serverInfoMap[serverInfo.server] = serverInfo;
	}

	public void SetServer(string server, string url, float timeout, string key)
	{
		if (!m_serverInfoMap.ContainsKey(server))
		{
			AddServer(server, url, timeout, key);
			return;
		}
		m_serverInfoMap[server].url = url;
		m_serverInfoMap[server].timeout = timeout;
		m_serverInfoMap[server].key = key;
	}

	public ServerInfo GetServer(string server)
	{
		if (!m_serverInfoMap.ContainsKey(server))
		{
			return null;
		}
		return m_serverInfoMap[server];
	}

	public int SendRequest(string server, string action, string data, string gameObjectName, string componentName, string functionName, string param)
	{
		ServerInfo serverInfo = m_serverInfoMap[server];
		int taskId = m_taskId;
		m_taskId++;
		HttpTask httpTask = new HttpTask(taskId, serverInfo.server, serverInfo.url, serverInfo.timeout, serverInfo.key, action, data, gameObjectName, componentName, functionName, param);
		httpTask.Start();
		m_httpTaskMap[taskId] = httpTask;
		return taskId;
	}

	public void HandleResponse()
	{
		if (m_httpTaskMap.Count <= 0)
		{
			return;
		}
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, HttpTask> item in m_httpTaskMap)
		{
			int key = item.Key;
			HttpTask value = item.Value;
			if (value.Run())
			{
				list.Add(key);
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			int key2 = list[i];
			HttpTask httpTask = m_httpTaskMap[key2];
			m_httpTaskMap.Remove(key2);
			httpTask.Stop();
			httpTask.FireCallback();
			httpTask = null;
		}
	}

	public void CancelTask(int taskId)
	{
		if (m_httpTaskMap.ContainsKey(taskId))
		{
			HttpTask httpTask = m_httpTaskMap[taskId];
			m_httpTaskMap.Remove(taskId);
			httpTask.Stop();
			httpTask = null;
		}
	}
}
