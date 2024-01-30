<Query Kind="Program">
  <NuGetReference>dbup-sqlserver</NuGetReference>
  <Namespace>DbUp</Namespace>
  <Namespace>DbUp.Helpers</Namespace>
</Query>

const string ConnectionString = @"Server=MOE\SQL2022; Database=PepeWorks; Trusted_connection=True; TrustServerCertificate=True";

void Main()
{
	EnsureDatabase.For.SqlDatabase(ConnectionString);
	//Naming (Scripts/RunAlways) derived from https://github.com/DbUp/DbUp/tree/master/src/Samples/SampleApplication
	ApplyScripts(@"C:\PepeWorks\src\Scripts\API\Migration");
	ApplyRunAlways(@"C:\PepeWorks\src\Scripts\API\RunAlways");	
}

public void ApplyScripts(string folderPath)
{
	Console.WriteLine("Applying Migration scripts");
	DeployChanges.To
		.SqlDatabase(ConnectionString)
		.WithScriptsFromFileSystem(folderPath)
		.LogToConsole()
		.Build()
		.PerformUpgrade();
	Console.WriteLine("Done");
}

public void ApplyRunAlways(string folderPath)
{
	if(!Directory.Exists(folderPath)){
		return;
	}
	Console.WriteLine("Applying RunAlways scripts");
	DeployChanges.To
		.SqlDatabase(ConnectionString)
		.WithScriptsFromFileSystem(folderPath)
		.JournalTo(new NullJournal())
		.LogToConsole()
		.Build()
		.PerformUpgrade();
	Console.WriteLine("Done");
}