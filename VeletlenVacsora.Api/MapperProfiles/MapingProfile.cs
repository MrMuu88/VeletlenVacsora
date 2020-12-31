using AutoMapper;
using VeletlenVacsora.Api.ViewModels;
using VeletlenVacsora.Data.Models;

namespace VeletlenVacsora.Api.MapperProfiles
{
	public class MapingProfile: Profile
	{
		public MapingProfile()
		{
			CreateMap<CategoryModel, Category>().ReverseMap();
			CreateMap<RecepieModel, Recepie>()
				.ForMember(r=>r.Category,rm=>rm.MapFrom(rm=>rm.Category.Name))
				.ReverseMap();

			CreateMap<IngredientModel,Ingredient>()
				.ForMember(i => i.IngredientType,im=> im.MapFrom(im=> im.IngredientType.Name))
				.ForMember(i => i.PackageType,im=> im.MapFrom(im=> im.PackageType.Name))
				.ReverseMap();

			CreateMap<RecepieIngredientModel,RecepieIngredient>()
				.ForMember(ri=>ri.Ingredient,rim=>rim.MapFrom(rim=> rim.Ingredient.Name))
				.ReverseMap();
		}
	}
}
