using UnityEngine;
using System.Collections.Generic;

public class DColumn
{
    private Dictionary<string, string> line_;
    private int key;
    public int Key { get { return key; } }

    public DColumn(Dictionary<string, string> line)
    {
        line_ = line;
    }

    public DColumn(int k, Dictionary<string, string> line)
    {
        line_ = line;
        key = k;
    }

    public string Cell(string fieldname)
    {
        if (line_ == null) return "";
        if (line_.ContainsKey(fieldname)) return line_[fieldname];
        return "0";
    }

    public bool IsValid
    {
        get { return line_ != null; }
    }
}

public class DatabaseHolder : ScriptableObject
{
    public string[] content;

    public Dictionary<int, Dictionary<string, string>> _database;

    public bool ContainsKey(int key)
    {
        return _database.ContainsKey(key);
    }

    public DColumn Column(int key)
    {
        if (_database.ContainsKey(key)) return new DColumn(key, _database[key]);
        Debug.LogError("Key <" + key + "> not exist");
        return new DColumn(null);
    }

    public DatabaseHolder()
    {
        content = null;
    }

    public DatabaseHolder(string[] content)
    {
        this.content = content;
    }

    public void AddTextFile(string lines)
    {
		int lineNum = 0;
        char[] separators = { '\n' };
        string[] toks = lines.Split(separators);
        List<string> linesFiltered = new List<string>();

        linesFiltered.Add(toks[lineNum]);
		lineNum += 2;
        for ( ; lineNum < toks.Length; lineNum++)
        {
            if (toks[lineNum].Length == 0) continue;
            if (toks[lineNum][0] != '1' &&
               toks[lineNum][0] != '2' &&
               toks[lineNum][0] != '3' &&
               toks[lineNum][0] != '4' &&
               toks[lineNum][0] != '5' &&
               toks[lineNum][0] != '6' &&
               toks[lineNum][0] != '7' &&
               toks[lineNum][0] != '8' &&
               toks[lineNum][0] != '9')	// key값은 숫자
			   {
					continue;
			   }
			   
            linesFiltered.Add(toks[lineNum]);
        }

        AddContent(linesFiltered.ToArray());
    }

    public void AddContent(string[] addcontent)
    {
        List<string> addList = new List<string>();
        if (content != null) addList.AddRange(content);
        addList.AddRange(addcontent);

        content = addList.ToArray();
    }

    public void MakeDatabase()
    {
		int lineNum = 0;
        if (this.content.Length == 0) return;

        _database = new Dictionary<int, Dictionary<string, string>>();

        string[] fields = GetTokens(this.content[lineNum++]);

        for ( ; lineNum < this.content.Length; lineNum++)
        {
            string line = this.content[lineNum];
            string[] toks = GetTokens(line);

            if (toks[0] == "Ref")
            {
                fields = GetTokens(line);
                continue;
            }
            if (_database.ContainsKey(int.Parse(toks[0])))
            {
                Debug.LogWarning("Duplicated key : " + toks[0]);
                continue;
            }
            _database.Add(int.Parse(toks[0]), new Dictionary<string, string>());
            _database[int.Parse(toks[0])].Add("Ref", toks[0]);

            for (int i = 1; i < toks.Length; i++)
            {
                string fieldNameTrim = fields[i];
                fieldNameTrim = fieldNameTrim.TrimEnd();
                fieldNameTrim = fieldNameTrim.TrimStart();

                if (_database[int.Parse(toks[0])].ContainsKey(fieldNameTrim))
                {
                    Debug.LogWarning("duplicated key : " + fieldNameTrim);
                }
                else
                {
                    _database[int.Parse(toks[0])].Add(fieldNameTrim, toks[i]);
                }
            }
        }
    }

    string[] GetTokens(string line)
    {
        char[] separators = { ',' };
        string[] toks = line.Split(separators);
        return toks;
    }
}
