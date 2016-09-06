using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DSHL
{
    public class Ini
    {
        internal class Section
        {
            public Dictionary<string, string> entries;
            public string name;

            public Section(string name)
            {
                this.name = name;
                this.entries = new Dictionary<string, string>();
            }
        }

        private List<Section> sections;
        private List<string> sectionNames;

        public Ini()
        {
            sections = new List<Section>();
            sectionNames = new List<string>();
        }

        public Ini(string file)
        {
            sections = new List<Section>();
            sectionNames = new List<string>();
            Load(file);
        }

        public void Load(string file)
        {
            using (StreamReader sr = File.OpenText(file))
            {
                sections.Clear();
                int lineNo = 0;
                Section section = null;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine().Trim();
                    lineNo += 1;

                    // Remove comments
                    if (line.Contains("#"))
                    {
                        int index = line.IndexOf("#");
                        line = line.Remove(index).TrimEnd();
                    }

                    // Check if line is empty
                    if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line)) continue;

                    // Parse line
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        // Add current section to collection so we can start anew
                        if (section != null)
                        {
                            sections.Add(section);
                            sectionNames.Add(section.name);
                        }

                        // Get section name
                        section = new Section(line.Substring(1, line.Length - 2));
                    }
                    else if (line.Contains("="))
                    {
                        // Get the index of the first '=' and then split the stuff
                        int index1 = line.IndexOf('=');
                        string key = line.Substring(0, index1).TrimEnd();
                        string value = line.Substring(index1 + 1).TrimStart();

                        if (section.entries.ContainsKey(key)) section.entries[key] = value;
                        else section.entries.Add(key, value);
                    }
                    else
                    {
                        throw new Exception("Unsure how to parse line " + lineNo + "!");
                    }
                }

                // Add final section (if there are any)
                if (section != null)
                {
                    sections.Add(section);
                    sectionNames.Add(section.name);
                }
            }
        }

        public void Save(string file)
        {
            using (StreamWriter sw = File.CreateText(file))
            {
                sw.Flush();

                // If there's nothing, it will be a blank file...
                if (sections.Count > 0)
                {
                    // Write each section
                    foreach (Section section in sections)
                    {
                        // Write section name
                        sw.WriteLine("[" + section.name + "]");

                        // Write keys
                        if (section.entries.Count > 0)
                        {
                            foreach (string key in section.entries.Keys)
                            {
                                sw.WriteLine(key + "=" + section.entries[key]);
                            }
                        }

                        // Add some blank space between each section
                        sw.WriteLine();
                    }
                }
            }
        }

        public string this[string section, string key]
        {
            get
            {
                if (sectionNames.Contains(section))
                {
                    int index = sectionNames.IndexOf(section);
                    if (sections[index].entries.ContainsKey(key)) return sections[index].entries[key];
                    else return string.Empty;
                }
                else return string.Empty;
            }
            set
            {
                //bool sectionExists = false;
                if (sectionNames.Contains(section))
                {
                    int index = sectionNames.IndexOf(section);

                    if (sections[index].entries.ContainsKey(key))
                        sections[index].entries[key] = value;
                    else
                        sections[index].entries.Add(key, value);
                }
                else
                {
                    Section s = new Section(section);
                    s.entries.Add(key, value);

                    sections.Add(s);
                    sectionNames.Add(s.name);
                }
            }
        }

        #region Get

        public string GetString(string section, string key)
        {
            return this[section, key];
        }

        public int GetInt32(string section, string key, int fromBase = 10)
        {
            return Convert.ToInt32(this[section, key], fromBase);
        }

        public uint GetUInt32(string section, string key, int fromBase = 10)
        {
            return Convert.ToUInt32(this[section, key], fromBase);
        }

        #endregion

        #region Set

        public void SetString(string section, string key, string value)
        {
            this[section, key] = value;
        }

        public void SetInt32(string section, string key, int value, string intFormat = "")
        {
            this[section, key] = value.ToString(intFormat);
        }

        public void SetUInt32(string section, string key, uint value, string intFormat = "")
        {
            this[section, key] = value.ToString(intFormat);
        }

        #endregion

        public TreeNode ToTreeNodes(string parent)
        {
            TreeNode root = new TreeNode(parent);
            for (int i = 0; i < sections.Count; i++)
            {
                TreeNode section = new TreeNode(sections[i].name);
                foreach (string key in sections[i].entries.Keys)
                {
                    TreeNode kEy = new TreeNode(key);
                    kEy.Nodes.Add(new TreeNode(sections[i].entries[key]));
                    section.Nodes.Add(kEy);
                }
                root.Nodes.Add(section);
            }
            return root;
        }

        public string[] GetSectionNames()
        {
            return sectionNames.ToArray();
        }

        public bool CopySectionTo(string section, Ini destination)
        {
            if (sectionNames.Contains(section)) // There
            {
                int index = sectionNames.IndexOf(section);
                destination.sections.Add(sections[index]);
                destination.sectionNames.Add(sections[index].name);
                return true;
            }
            else return false;
        }

        public bool ContainsSection(string section)
        {
            foreach (Section s in sections)
            {
                if (s.name == section) return true;
            }
            return false;
        }
    }
}
