﻿using System;

namespace NSISInfoWriter.OutputGenerators
{
    public class NsisScriptGenerator : ScriptGenerator
    {
        public NsisScriptGenerator(ScriptGeneratorOptions options) : base(options) { }

        protected override string ProcessItem(string key, string value) {
            value = value.Replace(@"""", @"$\""");
            return String.Format("!define {0} \"{1}\"", this.GetKeyName(key), value);
        }
    }
}
