using System;
using System.Collections;
using UnityEngine;

public class iClearMemory : MonoBehaviour
{
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ClearMemory()
	{
		ClearImmidately();
	}

	protected IEnumerator Clear()
	{
		Debug.Log("Clear");
		GC.Collect();
		yield return Resources.UnloadUnusedAssets();
	}

	protected void ClearImmidately()
	{
		Debug.Log("Clear");
		GC.Collect();
		Resources.UnloadUnusedAssets();
	}
}
