using System;
using System.Collections.Generic;
using System.Text;
using VeletlenVacsora.Data;

namespace Veletlenvacsora.Data {
    public class RecepieIngredient {
        

        public int RecepieID { get; set; }
        public Recepie Recepie { get; set; }
        public int IngredientID { get; set; }
        public Ingredient Ingredient { get; set; }

        public RecepieIngredient() {

        }

        public RecepieIngredient(Recepie rec, Ingredient ing) {
            Recepie = rec;
            Ingredient = ing;
        }

    }
}
