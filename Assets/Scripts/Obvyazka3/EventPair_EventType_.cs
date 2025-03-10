namespace Obvyazka3
{
	public class EventPair<EventType>
	{
		public TypedCallback<EventType> cb;

		public string name;

		public bool once;
	}
}
