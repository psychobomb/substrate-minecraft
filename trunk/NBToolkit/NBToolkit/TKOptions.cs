﻿using System;
using System.Collections.Generic;
using System.Text;
using NDesk.Options;
using System.IO;

namespace NBToolkit
{
    public interface IOptions
    {
        void Parse (string[] args);
        void PrintUsage ();
    }

    public class TKOptions : IOptions
    {
        private OptionSet commonOpt = null;

        public string OPT_WORLD = "";

        // Verbosity
        public bool OPT_V = false;
        public bool OPT_VV = false;
        public bool OPT_HELP = false;

        public TKOptions ()
        {
            commonOpt = new OptionSet()
            {
                { "w|world=", "World directory",
                    v => OPT_WORLD = v },
                { "h|help", "Print this help message",
                    v => OPT_HELP = true },
                { "v", "Verbose output",
                    v => OPT_V = true },
                { "vv", "Very verbose output",
                    v => { OPT_V = true; OPT_VV = true; } },
            };
        }

        public TKOptions (string[] args)
            : this()
        {
            Parse(args);
        }

        public virtual void Parse (string[] args)
        {
            commonOpt.Parse(args);
        }

        public virtual void PrintUsage ()
        {
            Console.WriteLine("Common Options:");
            commonOpt.WriteOptionDescriptions(Console.Out);
        }

        public virtual void SetDefaults ()
        {
            if (OPT_HELP) {
                this.PrintUsage();
                throw new TKOptionException();
            }

            if (OPT_WORLD.Length == 0) {
                Console.WriteLine("Error: You must specify a World path");
                Console.WriteLine();
                this.PrintUsage();

                throw new TKOptionException();
            }

            if (!File.Exists(Path.Combine(OPT_WORLD, "level.dat"))) {
                Console.WriteLine("Error: The supplied world path is invalid");
                Console.WriteLine();
                this.PrintUsage();

                throw new TKOptionException();
            }
        }
    }

    class TKOptionException : Exception
    {
        public TKOptionException () { }

        public TKOptionException (String msg) : base(msg) { }

        public TKOptionException (String msg, Exception innerException) : base(msg, innerException) { }
    }
}
