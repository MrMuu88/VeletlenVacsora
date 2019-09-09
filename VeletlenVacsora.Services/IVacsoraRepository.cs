﻿using System.Collections.Generic;
using VeletlenVacsora.Data;

namespace VeletlenVacsora.Services {
	public interface IVacsoraRepository {

		//TODO Modify Repository Interface Sygnatures to be async tasks

		ICollection<Recepie> GetAllRecepies();
		ICollection<Recepie> GetRecepiesByType(string type);
		Recepie GetRecepieByID(int id);


		ICollection<Category> GetAllCategories();
		ICollection<Category> GetCategoryByType(string type);
		Category GetCategoryByID(int ID);


		ICollection<Ingredient> GetAllIngredients();
		ICollection<Ingredient> GetIngredientsByType(string type, string package);
		Ingredient GetIngredientByID(int ID);


		void Add<T>(T obj) where T : class;
		void Delete<T>(T obj) where T : class;
		bool SaveChanges();
	}
}
