namespace EntityFramework.External
{
	using System.Data.Entity.Core.Objects.DataClasses;

	[EdmEnumType(NamespaceName="EntityFramework.External", Name="TestEnum")]
	public enum TestEnum
	{
		First  = 1,
		Second = 2,
	}
}
