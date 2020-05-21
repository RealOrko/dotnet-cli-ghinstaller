using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ghinstaller.Modules.Process
{
    public class Process
    {
        private TimeSpan _waitForExit = TimeSpan.FromMinutes(5);
        private Dictionary<string, string> _environmentVariables = new Dictionary<string, string>();

        public Process()
        {
            _environmentVariables["PATH"] = Environment.GetEnvironmentVariable("PATH");
        }

        public void SetTimeout(TimeSpan waitForExit)
        {
            _waitForExit = waitForExit;
        }

        public void ClearEnvironmentVariable(string name)
        {
            _environmentVariables.Remove(name);
        }

        public void SetEnvironmentVariable(string name, string value)
        {
            _environmentVariables[name] = value;
        }

        public virtual ProcessResult Execute(string command, params string[] args)
        {
            var stderr = new StringBuilder();
            var stdout = new StringBuilder();

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = command;
            startInfo.Arguments = string.Join(" ", args);
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            foreach (var environmentVariable in _environmentVariables)
                startInfo.EnvironmentVariables[environmentVariable.Key] = environmentVariable.Value;

            int exitCode;
            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo = startInfo;

                try
                {
                    process.Start();

                    while (!process.StandardOutput.EndOfStream)
                    {
                        stdout.AppendLine(process.StandardOutput.ReadLine());
                    }
                    
                    while (!process.StandardError.EndOfStream)
                    {
                        stderr.AppendLine(process.StandardError.ReadLine());
                    }

                }
                finally
                {
                    if (!process.WaitForExit((int)_waitForExit.TotalMilliseconds))
                    {
                        process.Kill();
                    }
                    exitCode = process.ExitCode;
                }
            }

            return new ProcessResult { ErrorOutput = stderr.ToString(), ExitCode = exitCode, StandardOutput = stdout.ToString() };
        }
    }
}