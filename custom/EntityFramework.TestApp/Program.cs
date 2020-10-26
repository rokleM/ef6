namespace EntityFramework.TestApp
{
	using EntityFramework.External;

	class Program
	{
		static void
		Main()
		{
			using(var model = new Model()) {
				// model.MetadataWorkspace.LoadFromAssembly(typeof(TestEnum).Assembly);
				model.TestTables.AddObject(new TestTable {
					Enum1 = TestEnum.Second,
					Enum2 = NativeEnum.End,
				});
				model.SaveChanges();
			}
		}
	}
}
