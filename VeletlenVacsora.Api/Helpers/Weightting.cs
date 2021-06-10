namespace VeletlenVacsora.Api.Helpers
{
	public class Weightting
	{
		public int Id { get; set; }
		public double Weight { get; set; }

		public Weightting(int id, int weight)
		{
			Id = id;
			Weight = weight;
		}
	}
}
