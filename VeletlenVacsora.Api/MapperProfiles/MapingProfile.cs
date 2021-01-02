using AutoMapper;
using VeletlenVacsora.Api.ViewModels;
using VeletlenVacsora.Data.Models;
using VeletlenVacsora.Data.Repositories;

namespace VeletlenVacsora.Api.MapperProfiles
{
	public class MapingProfile: Profile
	{
		public MapingProfile()
		{
			CreateMap<CategoryModel, Category>().ReverseMap();
			
			CreateMap<RecepieModel, Recepie>()
				.ForMember(r => r.Category, rm => rm.MapFrom(rm => rm.Category.Name));
			CreateMap<Recepie, RecepieModel>()
				.ForMember(r => r.Category, rm => rm.Ignore());

			CreateMap<IngredientModel, Ingredient>()
				.ForMember(i => i.IngredientType, im => im.MapFrom(im => im.IngredientType.Name))
				.ForMember(i => i.PackageType, im => im.MapFrom(im => im.PackageType.Name));
			CreateMap<Ingredient, IngredientModel>()
				.ForMember(i => i.IngredientType, im => im.Ignore())
				.ForMember(i => i.PackageType, im => im.Ignore());

			CreateMap<RecepieIngredientModel,RecepieIngredient>()
				.ForMember(ri=>ri.Ingredient,rim=>rim.MapFrom(rim=> rim.Ingredient.Name))
				.ForMember(ri=>ri.PackageType,rim=>rim.MapFrom(rim=> rim.Ingredient.PackageType.Name))
				.ForMember(ri=>ri.IngredientType,rim=>rim.MapFrom(rim=> rim.Ingredient.IngredientType.Name))
				.ReverseMap();
		}

	}
}
