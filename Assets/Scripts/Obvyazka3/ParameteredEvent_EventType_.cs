namespace Obvyazka3
{
	public class ParameteredEvent<EventType>
	{
		public string name;

		public EventType msg;

		public ParameteredEvent(string _name, EventType _msg)
		{
			name = _name;
			msg = _msg;
		}
	}
}
