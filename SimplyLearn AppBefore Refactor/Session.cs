namespace SimplyLearn
{
	
	public class Session
	{
		public string title { get; set; }
		public string description { get; set; }
		public bool approved { get; set; }

		public Session(string title, string description)
		{
			title = title;
			description = description;
		}
	}
}
