using AutoMapper;
using VeletlenVacsora.Api.ViewModels;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Api.MappingProfiles
{
	public class RecepiesMapping:Profile
	{
		public RecepiesMapping()
		{
			CreateMap<Recepie, RecepieResponse>();
			CreateMap<RecepieRequest, Recepie>();
		}
	}
}
