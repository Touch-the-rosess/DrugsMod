using System.Collections.Generic;

namespace Obvyazka3
{
	public class EventCallbackSet<EventType>
	{
		public Queue<ParameteredEvent<EventType>> queue = new Queue<ParameteredEvent<EventType>>();

		public List<EventPair<EventType>> cbs = new List<EventPair<EventType>>();

		public void addEventCallback(string type, TypedCallback<EventType> cb, bool once = false)
		{
			EventPair<EventType> eventPair = new EventPair<EventType>();
			eventPair.name = type;
			eventPair.cb = cb;
			eventPair.once = once;
			cbs.Add(eventPair);
		}

		public void delayedEmitEvent(string type, ref EventType msg)
		{
			queue.Enqueue(new ParameteredEvent<EventType>(type, msg));
		}

		public void emitFromQueue()
		{
			while (queue.Count > 0)
			{
				ParameteredEvent<EventType> parameteredEvent = queue.Dequeue();
				emitEvent(parameteredEvent.name, ref parameteredEvent.msg);
			}
		}

		public void emitEvent(string type, ref EventType msg)
		{
			for (int i = 0; i < cbs.Count; i++)
			{
				EventPair<EventType> eventPair = cbs[i];
				if (eventPair.name == type)
				{
					eventPair.cb(ref msg);
					if (eventPair.once)
					{
						cbs.Remove(eventPair);
						i--;
					}
				}
			}
		}

		public void removeEventCallback(string type, TypedCallback<EventType> cb)
		{
			for (int i = 0; i < cbs.Count; i++)
			{
				EventPair<EventType> eventPair = cbs[i];
				if (eventPair.name == type && cb == eventPair.cb)
				{
					cbs.Remove(eventPair);
					i--;
				}
			}
		}

		public void removeAllCallbacks(string type)
		{
			for (int i = 0; i < cbs.Count; i++)
			{
				EventPair<EventType> eventPair = cbs[i];
				if (eventPair.name == type)
				{
					cbs.Remove(eventPair);
					i--;
				}
			}
		}
	}
}
