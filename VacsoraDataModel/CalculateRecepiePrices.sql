/****** Script for SelectTopNRows command from SSMS  ******/
SELECT Sum(ri.Amount*i.Price) as price 
FROM [Recepies] As r 
	inner join
	 [RecepieIngredient] as ri on r.RecepieID = ri.RecepieID  
	inner join
	 [Ingredients] as i on ri.IngredientID = i.IngredientID
  group by r.name
  