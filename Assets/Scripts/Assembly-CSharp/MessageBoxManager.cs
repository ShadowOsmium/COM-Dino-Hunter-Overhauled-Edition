using System;
using System.Collections.Generic;
using UnityEngine;

public class MessageBoxManager : MonoBehaviour
{
	private static Dictionary<string, MessageBox> messageBoxes = new Dictionary<string, MessageBox>();

	private static readonly string linkStr = "_&_";

	public static void Show(MessageBox messageBox)
	{
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void HandleMessageBoxButtonClick(string message)
	{
		string[] array = message.Split(new string[1] { linkStr }, StringSplitOptions.RemoveEmptyEntries);
		if (array != null && array.Length >= 2 && messageBoxes.ContainsKey(array[0]))
		{
			MessageBox messageBox = messageBoxes[array[0]];
			messageBoxes.Remove(messageBox.Guid);
			if (messageBox.Callback != null)
			{
				messageBox.Callback(messageBox, Convert.ToInt32(array[1]));
			}
		}
	}
}
