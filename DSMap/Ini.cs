using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DSMap
{
    public class Ini
    {
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

                    // comment
                    if (line.Contains("#"))
                    {
                        int index = line.IndexOf("#");
                        line = line.Remove(index).TrimEnd();
                    }

                    // check if it's empty
                    if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line)) continue;

                    // parse
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        // add current section to collection so we can start anew
                        if (section != null)
                        {
                            sections.Add(section);
                            sectionNames.Add(section.name);
                        }

                        // get section name
                        section = new Section(line.Substring(1, line.Length - 2));
                    }
                    else if (line.Contains("="))
                    {
                        /*string[] parts = line.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length != 2) throw new Exception("Line " + lineNo + ": Bad section entry format!");

                        if (section == null) throw new Exception("Line " + lineNo + ": Cannot declare an entry without being in a section!");

                        // add or replace this entry
                        if (section.entries.ContainsKey(parts[0])) section.entries[parts[0]] = parts[1];
                        else section.entries.Add(parts[0], parts[1]);*/

                        // new method:
                        int index1 = line.IndexOf('=');
                        string key = line.Substring(0, index1);
                        string value = line.Substring(index1 + 1);

                        if (section.entries.ContainsKey(key)) section.entries[key] = value;
                        else section.entries.Add(key, value);
                    }
                    else
                    {
                        throw new Exception("Unsure how to parse line " + lineNo + "!");
                    }
                }

                // add final section
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

                // if there's nothing, it will be a blank file...
                if (sections.Count > 0)
                {
                    // write each section
                    foreach (Section section in sections)
                    {
                        // write section name
                        sw.WriteLine("[" + section.name + "]");

                        // write keys
                        if (section.entries.Count > 0)
                        {
                            foreach (string key in section.entries.Keys)
                            {
                                sw.WriteLine(key + "=" + section.entries[key]);
                            }
                        }

                        // add some buffer space
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

                /*for (int i = 0; i < sections.Count; i++)
                {
                    if (sections[i].name == section)
                    {
                        return sections[i].entries[key];
                    }
                }*/

                //return string.Empty;
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

                /*for (int i = 0; i < sections.Count; i++)
                {
                    if (sections[i].name == section)
                    {
                        sectionExists = true;
                        if (sections[i].entries.ContainsKey(key))
                            sections[i].entries[key] = value;
                        else
                            sections[i].entries.Add(key, value);
                    }
                }

                if (!sectionExists)
                {
                    
                }*/
            }
        }

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
            /*string[] sectionNames = new string[sections.Count];
            for (int i = 0; i < sectionNames.Length; i++)
            {
                sectionNames[i] = sections[i].name;
            }
            return sectionNames;*/
            return sectionNames.ToArray();
        }

        /*public string[] GetAllKeys(string section)
        {
            // This is terrible code, and needs to be redone.
            if (ContainsSection(section))
            {
                foreach (Section s in sections)
                {
                    if (s.name == section)
                    {
                        List<string> ss = new List<string>();
                        foreach (var key in s.entries.Keys)
                        {
                            ss.Add(key);
                        }
                        return ss.ToArray();
                    }
                }
                return null;
            }
            else return null;
        }*/

        public bool CopySectionTo(string section, Ini destination)
        {
            /*for (int i = 0; i < sections.Count; i++)
            {
                if (sections[i].name == section)
                {
                    /*foreach (var x in sections[i].entries)
                    {
                        destination[section, x.Key] = x.Value;
                    }*
                    destination.sections.Add(sections[i]);
                    
                    return true;
                }
            }*/
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
    }
}
