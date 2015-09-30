using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class CreateDatabaseHolder
{
    [MenuItem("DatabaseHolder/CreateDatabaseHolder")]
    static void Execute()
	{
		Object[] objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
		foreach (Object o in objs)
		{
			string path_ = AssetDatabase.GetAssetPath(o);
			
			if(!path_.Contains("/CSVFiles/")) continue;
			if(!path_.Contains(".csv")) continue;
			
			string rootPath_ = objRoot(o);			
			//  string filename = path_.Replace(rootPath_, "");
			string dirPath = rootPath_ + "_AssetTables/";
			
			path_ = path_.Replace("Assets/", "");
			string[] contents = CreateHolderFile(path_);
			DatabaseHolder holder =(DatabaseHolder)ScriptableObject.CreateInstance(typeof(DatabaseHolder));
			holder.content = contents;
			
			// save file
			if (!Directory.Exists(dirPath))
			{
				Directory.CreateDirectory(dirPath);
			}
			
			string filePath = dirPath + o.name + ".asset";
			
		    DatabaseHolder asset = (DatabaseHolder)AssetDatabase.LoadAssetAtPath(filePath, typeof(DatabaseHolder));
			if (asset == null)
			{
				AssetDatabase.CreateAsset(holder, filePath);
			}
			else
			{
				asset.content = contents;
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
			Debug.Log(dirPath + o.name + ".asset");
		}
	}
	
	static string objRoot(Object o)
	{
		string root = AssetDatabase.GetAssetPath(o);
		return root.Substring(0, root.LastIndexOf('/') + 1);
	}
	
	static string[] CreateHolderFile(string fileName)
	{
		string line;
		List<string> contents = new List<string>();
		StreamReader reader = new StreamReader(Application.dataPath + "/" + fileName);
		
		// field
		line = reader.ReadLine();
		contents.Add(line);
		// type -> not used
		line = reader.ReadLine();
		// add
		line = reader.ReadLine();
		while (line != null)
		{
			contents.Add(line);
			line = reader.ReadLine();
		}
		
		return contents.ToArray();
	}
}
