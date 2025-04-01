using System;
using System.Collections.Generic;
using System.Reflection;

namespace EventCenter
{
	public class EventCenter
	{
		public delegate void EventHandler<T>(object sender, T evt);

		private static EventCenter instance;

		private Dictionary<Type, List<object>> handlers = new Dictionary<Type, List<object>>();

		public static EventCenter Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new EventCenter();
				}
				return instance;
			}
		}

		public void Register<T>(EventHandler<T> handler)
		{
			Register(typeof(T), handler);
		}

		private void Register<T>(Type eventType, EventHandler<T> handler)
		{
			if (!handlers.ContainsKey(eventType))
			{
				handlers.Add(eventType, new List<object>());
			}
			if (!handlers[eventType].Contains(handler))
			{
				handlers[eventType].Add(handler);
			}
		}

		public void Unregister<T>(EventHandler<T> handler)
		{
			Unregister(typeof(T), handler);
		}

		private void Unregister<T>(Type eventType, EventHandler<T> handler)
		{
			if (handlers.ContainsKey(eventType))
			{
				handlers[eventType].Remove(handler);
				if (handlers[eventType].Count == 0)
				{
					handlers.Remove(eventType);
				}
			}
		}

		public void Publish<T>(object sender, T evt)
		{
			Publish(sender, typeof(T), evt);
		}

		public void Publish<T>(object sender, Type eventType, T evt)
		{
			if (!handlers.ContainsKey(eventType))
			{
				return;
			}
			handlers[eventType].RemoveAll((object handler) => handler == null);
			foreach (object item in handlers[eventType])
			{
				MethodInfo method = item.GetType().GetMethod("Invoke");
				method.Invoke(item, new object[2] { sender, evt });
			}
		}

		public void Clear()
		{
			handlers.Clear();
		}
	}
}
