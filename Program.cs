using FrameCalculator;

AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
{
    Console.Error.WriteLine($"Critical error: {eventArgs.ExceptionObject}");
    Environment.Exit(1);
};
    
TaskScheduler.UnobservedTaskException += (sender, eventArgs) =>
{
    Console.Error.WriteLine($"Unobserved task error: {eventArgs.Exception}");
    eventArgs.SetObserved();
    Environment.Exit(1);
};
ProgramRoot.Start();

