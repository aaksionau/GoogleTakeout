using GoogleTakeout.Commands;


var app = ConsoleApp.Create(args);
app.AddCommands<ImportMediaCommand>();
app.Run();
