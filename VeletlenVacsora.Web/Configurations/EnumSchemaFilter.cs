using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeletlenVacsora.Web.Configurations
{
	public class EnumSchemaFilter : ISchemaFilter
	{
		//From https://stackoverflow.com/questions/36452468/swagger-ui-web-api-documentation-present-enums-as-strings
		public void Apply(OpenApiSchema schema, SchemaFilterContext context)
		{
			if (context.Type.IsEnum) {
				var enumValues = schema.Enum.ToArray();
				var i = 0;
				schema.Enum.Clear();
				foreach (var n in Enum.GetNames(context.Type).ToList()) {
					schema.Enum.Add(new OpenApiString(n + $" = {((OpenApiPrimitive<int>) enumValues[i]).Value}"));
					i++;
				}
			}
		}
	}
}
