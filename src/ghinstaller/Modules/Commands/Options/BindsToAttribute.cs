﻿using System;

 namespace ghinstaller.Modules.Commands.Options
{
    public class BindsAttribute : Attribute
    {
        public BindsAttribute(Type arguments)
        {
            Arguments = arguments;
        }

        public Type Arguments { get; set; }
    }
}