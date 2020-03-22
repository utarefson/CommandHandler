using System;
using Topshelf;

namespace CommandHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x => {
                x.Service<CommandHandlerCore>(s => {
                    s.ConstructUsing(core => { return new CommandHandlerCore(); });
                    s.WhenStarted(core => { core.Start(); });
                    s.WhenStopped(core => { core.Stop(); });
                });

                x.RunAsLocalSystem();

                x.SetServiceName("CommandHandlerService");
                x.SetDisplayName("Command Handler Service");
                x.SetDescription("WindowsService who handles the commands of the 'Chiquitania S.R.L. System'.");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
