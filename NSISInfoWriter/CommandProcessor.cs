﻿using System;
using System.ComponentModel;
using System.Diagnostics;

namespace NSISInfoWriter
{
    public class CommandProcessor
    {
        private string Command { get; set; }

        private string WorkingDirectory { get; set; }

        public CommandProcessor(string cmd, string workingDirectory) {
            this.Command = cmd;
            this.WorkingDirectory = workingDirectory;
        }

        public CommandProcessor(string cmd) : this(cmd, Environment.CurrentDirectory) { }

        private ProcessStartInfo GetPSI(string args) {
            return new ProcessStartInfo {
                FileName = this.Command,
                Arguments = args,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                WorkingDirectory = this.WorkingDirectory
            };
        }

        public int GetExitCode(string args) {
            try {
                var p = Process.Start(this.GetPSI(args));
                p.WaitForExit();
                return p.ExitCode;
            } catch (Win32Exception) {
                return -1; // error code
            }
        }

        public bool IsZeroExitCode(string args) {
            return this.GetExitCode(args) == 0;
        }

        public string GetOut(string args) {
            var p = Process.Start(this.GetPSI(args));
            var output = p.StandardOutput.ReadToEnd();
            return output.Trim();
        }
    }
}
