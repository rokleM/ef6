namespace EntityFramework.DB.Autogen
{
	using OL.Core;
	using OL.EFToolkit.Metadata.Autogen;
	using System.Xml.Linq;
	using EntityFramework.External;

	public class UpdateModelMetadata : OL.EFToolkit.Metadata.Autogen.UpdateModelMetadata
	{
		public override void
		UpdateStorage(XDocument document)
		{
			RemoveDefiningQueries(document);
			RemoveUnneededEntities(document);
			UpdateTimestampProperties(document);
			UpdateStorageComputedProperties(document);
		}

		static void
		UpdateStorageComputedProperties(XDocument document)
		{
		}

		static void
		UpdateConceptualComputedProperties(XDocument document)
		{
		}

		void
		RemoveDefiningQueries(XDocument document)
		{
		}

		void
		RemoveUnneededEntities(XDocument document)
		{
		}

		enum NativeEnum
		{
			Start = 2,
			End,
		}

		public override void
		UpdateConceptual(XDocument document)
		{
			RemoveUnneededEntities(document);
			// UpdateFunctionImports(document);
			UpdateTimestampProperties(document);

			RemoveEnumTypes(document);
			var enumTypes         = new []{ typeof(NativeEnum) };
			var externalEnumTypes = new []{ typeof(TestEnum) };
			UpdateEnumDeclarations(document, enumTypes, externalEnumTypes);
			RenameEnumTypes(document);

			RenameDeclarations(document);
			UpdateConceptualComputedProperties(document);
		}

		static void
		RenameEnumTypes(XDocument document)
		{
			var entities = new[] {
				new {
					EntityName = "TestTable",
					Properties = new[] {
						new { PropertyName = "Enum1", PropertyType = "EntityFramework.TestApp.TestEnum" },
						new { PropertyName = "Enum2", PropertyType = "EntityFramework.TestApp.NativeEnum" },
					}
				},
			};
			foreach(var entity in entities) {
				var entityType = document.EntityType(entity.EntityName);
				foreach(var property in entity.Properties) {
					entityType
						.Property(property.PropertyName)
						.Maybe(p => p.Type($"{property.PropertyType}"))
					;
				}
			}
		}

		static void
		RemoveEnumTypes(XDocument document)
		{
			document.EnumTypes().Remove();
		}

		static void
		RenameDeclarations(XDocument document)
		{
		}

		public override void
		UpdateMapping(XDocument document)
		{
			RemoveUnneededEntities(document);
			RenameMappingProperties(document);
			// RenameFunctionImportTypeMappings(document);
			UpdateMappingComputedProperties(document);
		}

		void
		UpdateMappingComputedProperties(XDocument document)
		{
		}

		static void
		RenameMappingProperties(XDocument document)
		{
		}
	}
}
