namespace Pagita
{
	public class EventArgs<T>
	{
		public object Item { get; set;}

		public EventArgs(object item)
		{
			Item = item;
		}
	}
}