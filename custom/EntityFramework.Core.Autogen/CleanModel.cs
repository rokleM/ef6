namespace EntityFramework.Core.Autogen
{
	public class CleanModel : OL.EFToolkit.Autogen.CleanModel
	{
		protected override string
		Clean(string code)
		{
			return base.Clean(code).Replace("enum TestEnum ", "enum TestEnum_ ");
		}
	}
}
