using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace DSHL.Formats.Pokémon.Scripting
{

    public class CommandDatabase
    {
        private Dictionary<ushort, Command> commandsByValue;
        private Dictionary<string, Command> commandsByName;

        public CommandDatabase()
        {
            commandsByValue = new Dictionary<ushort, Command>();
            commandsByName = new Dictionary<string, Command>();
        }

        ~CommandDatabase()
        {
            //commandsByName.Clear();
            //commandsByValue.Clear();
        }

        public void Load(string file)
        {
            StreamReader sr = File.OpenText(file);
            commandsByValue.Clear();
            commandsByName.Clear();

            while (!sr.EndOfStream)
            {
                // Get next line
                string line = sr.ReadLine().Trim();

                // Skip blank lines
                if (string.IsNullOrEmpty(line)) continue;
                else if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2) continue; // For now, just skip it

                // Try get value
                ushort value = 0;
                if (!ushort.TryParse(parts[0], out value)) continue; // All errors are skipped

                // Parse
                Command cmd = new Command();
                cmd.Value = value;
                cmd.Name = parts[1].ToLower(); // My scripting is case-insensitive
                cmd.Arguments = null;

                if (parts.Length > 2) // Get args, if any
                {
                    cmd.Arguments = new string[parts.Length - 2];
                    for (int i = 0; i < cmd.Arguments.Length; i++)
                    {
                        cmd.Arguments[i] = parts[i + 2].ToLower();
                    }
                }

                if (commandsByName.ContainsKey(cmd.Name)) throw new Exception("Command '" + cmd.Name + "' was defined twice!");
                if (commandsByValue.ContainsKey(cmd.Value)) throw new Exception("Command value " + cmd.Value + " was defined twice!");
                commandsByName.Add(cmd.Name, cmd);
                commandsByValue.Add(cmd.Value, cmd);
            }
        }

        // Gets
        #region Gets

        public string GetCommandName(ushort cmd)
        {
            if (commandsByValue.ContainsKey(cmd)) return commandsByValue[cmd].Name;
            else return cmd.ToString("X4");
        }

        public string GetAllCommandNames()
        {
            string result = "";
            foreach (string name in commandsByName.Keys)
            {
                result += name + " ";
            }
            return result.Remove(result.Length - 1);
        }

        public ushort GetCommandValue(string name)
        {
            if (commandsByName.ContainsKey(name)) return commandsByName[name].Value;
            else return ushort.MaxValue; // ERROR, essentially
        }

        public string[] GetCommandArguments(ushort cmd)
        {
            if (commandsByValue.ContainsKey(cmd)) return commandsByValue[cmd].Arguments;
            else return null;
        }

        public string[] GetCommandArguments(string name)
        {
            if (commandsByName.ContainsKey(name)) return commandsByName[name].Arguments;
            else return null;
        }
        #endregion

        private class Command
        {
            public Command()
            { }

            public ushort Value;
            public string Name;
            public string[] Arguments;
        }
    }
}
