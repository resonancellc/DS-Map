using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DSHL.Formats.Pokémon.Scripting.Ast
{
    public class SCommand
    {
        public ushort Value;
        public List<SArgument> Arguments;

        public SCommand(ushort value)
        {
            Value = value;
            Arguments = new List<SArgument>();
        }

        public int GetSize()
        {
            int i = 2;
            for (int x = 0; x < Arguments.Count; x++)
            {
                if (Arguments[x] is SPointer) i += 4;
                else if (Arguments[x] is SNumber<byte>) i += 1;
                else if (Arguments[x] is SNumber<ushort>) i += 2;
                else if (Arguments[x] is SNumber<uint>) i += 4;
            }
            return i;
        }

        public override string ToString()
        {
            string s = Value.ToString("X4");
            foreach (var arg in Arguments)
            {
                s += " " + arg.ToString();
            }
            return s;
        }
    }

    public abstract class SArgument { }

    public class SNumber<T> : SArgument
    {
        public T Value;

        public SNumber(T value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class SPointer : SArgument
    {
        public string Destination;

        public SPointer(string dest)
        {
            Destination = dest;
        }

        public override string ToString()
        {
            return "~" + Destination;
        }
    }

    public class MCommand
    {
        public ushort Type, Steps;

        public MCommand(ushort type, ushort steps)
        {
            Type = type;
            Steps = steps;
        }

        public int GetSize()
        {
            return 4;
        }

        public override string ToString()
        {
            return Type + " for " + Steps + " steps";
        }
    }

    public abstract class Block
    {
        public virtual int GetSize()
        {
            return 0;
        }
    }

    public class SBlock : Block
    {
        public List<SCommand> Commands;
        public string Name;

        public SBlock(string name)
        {
            Name = name;
            Commands = new List<SCommand>();
        }

        public int GetSize()
        {
            int i = 0;
            foreach (var cmd in Commands)
            {
                i += cmd.GetSize();
            }
            return i;
        }

        public override string ToString()
        {
            string s = Name + " = \n";
            foreach (var cmd in Commands)
            {
                s += cmd.ToString() + "\n";
            }
            return s;
        }
    }

    public class MBlock : Block
    {
        public string Name;
        public List<MCommand> Commands;

        public MBlock(string name)
        {
            Name = name;
            Commands = new List<MCommand>();
        }

        public int GetSize()
        {
            int s = 4; // for an extra end command
            foreach (var cmd in Commands)
            {
                s += cmd.GetSize();
            }
            return s;
        }

        public override string ToString()
        {
            string s = Name + " =\n";
            foreach (var cmd in Commands)
            {
                s += cmd.ToString() + "\n";
            }
            return s;
        }
    }
}
