namespace VeletlenVacsora.Data.Models
{
	public abstract class BaseModel
	{
		public int Id { get; internal set; }

		public string Name { get; set; }

		public BaseModel(string name) {
			Name = name;
		}
	}
}
