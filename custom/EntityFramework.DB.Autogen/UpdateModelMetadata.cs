namespace EntityFramework.DB.Autogen
{
	using EntityFramework.External;
	using OL.Core;
	using OL.Core.Linq;
	using OL.EFToolkit;
	using OL.EFToolkit.Metadata.Autogen;
	using System;
	using System.Linq;
	using System.Xml.Linq;

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

		void
		UpdateEnumDeclarations(XDocument document, Type[] enumTypes, Type[] externalEnumTypes = null)
		{
			externalEnumTypes        ??= Array.Empty<Type>();
			var externalEnumTypesSet   = externalEnumTypes.ToHashSet();
			var existingEnumTypes      = enumTypes.Concat(externalEnumTypes).Join(document.EnumTypes(), et => et.Name, et => et.Name(), (et, _) => et);
			var missingEnumTypes       = enumTypes.Concat(externalEnumTypes).Except(existingEnumTypes);

			if(missingEnumTypes.Any()) {
				var schema = document.Schema();
				var ns     = schema.Name.Namespace;
				foreach(var enumType in missingEnumTypes) {
					var type = enumType;
					schema.Add(
						new XElement(
							ns + "EnumType",
							new object[] {
								new XAttribute("Name",           type.Name),
								new XAttribute("IsFlags",        type.GetCustomAttributes(typeof(FlagsAttribute), true).Length == 1),
								new XAttribute("UnderlyingType", type.GetEnumUnderlyingType().Name),
								externalEnumTypesSet.Contains(enumType)
								?	new XAttribute(Edm.CodeGeneration + "ExternalTypeName", type.AssemblyQualifiedName)
								:	default
								,
								Enum
									.GetValues(type)
									.OfType<object>()
									.Select(v => new { Name = Enum.GetName(type, v), Value = Convert.ToInt64(v) })
									.Select(a => new XElement(ns + "Member",
										new XAttribute("Name", a.Name),
										new XAttribute("Value", a.Value)
									))
							}
							.ExceptNull()
						)
					);
				}
			}
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
