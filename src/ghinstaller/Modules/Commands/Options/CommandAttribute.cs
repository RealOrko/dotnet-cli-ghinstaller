﻿﻿using System;

 namespace ghinstaller.Modules.Commands.Options
{
    public class CommandAttribute : Attribute
    {
        public CommandAttribute(string command)
        {
            Command = command;
        }

        public string Command { get; set; } = string.Empty;
    }
}