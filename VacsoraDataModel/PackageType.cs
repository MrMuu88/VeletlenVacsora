namespace VeletlenVacsora.Data {
	public class PackageType {
		public virtual int ID {get;set;}
		public virtual string TypeName {get;set;}
		
		public PackageType() {}

		public PackageType(string name) {
			TypeName = name;
		}
	}
}
