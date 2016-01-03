﻿using System.Collections.Generic;

namespace NSISInfoWriter.VCSInformationParser
{
    public abstract class VCSInfoParser
    {
        protected string workingDirectory;
        protected string timeFormat;
        private string vcsExec;
        protected CommandProcessor cmdProcessor;

        protected Dictionary<string, string> generated = new Dictionary<string, string>();

        public VCSInfoParser(string vcsExec, string wd, string timeFormat) {
            this.vcsExec = vcsExec;
            this.workingDirectory = wd;
            this.timeFormat = timeFormat;
            this.cmdProcessor = new CommandProcessor(vcsExec, wd);
        }
    
        abstract public string Prefix {get; }

        virtual public bool IsAvailableVCSExecutable()
        {
            int code = this.cmdProcessor.GetExitCode("--version");
            return code == 0;
        }

        /// <summary>
        /// Indicate working directory under VCS control or not
        /// </summary>
        /// <returns></returns>
        abstract public bool IsUnderControl();

        abstract protected void ParseInformation();

        // TODO: replace with proper implementation
        public Dictionary<string, string> GetInformation() {
            this.generated.Clear();
            this.ParseInformation();
            return this.generated;
        }

        protected void AddInformation(string key, string value) {
            key = string.Format("{0}_{1}", this.Prefix, key);
            this.generated.Add(key, value);
        }
    }
}
