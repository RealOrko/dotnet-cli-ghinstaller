using System;

namespace ghinstaller.Modules.Build.Golang
{
    public class Build
    {
        public static void SourceDirectory(string sourceDirectory)
        {
            var process = new Process.Process()
                .SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH"))
                .SetWorkingDirectory(sourceDirectory)
                .SetTimeout(TimeSpan.FromHours(1));

            var result = process.Execute("go", "build");

            if (result.ExitCode != 0)
            {
                Console.WriteLine(result.ErrorOutput);
            }
        }
    }
}