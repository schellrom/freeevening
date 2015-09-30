using UnityEngine;
using System.Collections.Generic;

public class DataTable : MonoBehaviour
{
	public DatabaseHolder[] holders;	
	private Dictionary<string, DatabaseHolder> sheets_ = null;
	
    // Use this for initialization
    void Awake()
    {
		Init();
    }
	
	public void Init()
	{
		if (sheets_ != null)
		{
			return;
		}
		sheets_ = new Dictionary<string, DatabaseHolder>();
		foreach (DatabaseHolder holder in holders)
		{
			holder.MakeDatabase();
			sheets_.Add(holder.name, holder);
		}
	}
	
	public DatabaseHolder GetSheet(string sheetName)
	{
		if (!sheets_.ContainsKey(sheetName))
		{
			return null;
		}
		return sheets_[sheetName];
	}
	
	public DColumn GetColumn(int key)
	{
		foreach (string k in sheets_.Keys)
		{
			Debug.LogError("key : " + k);
		}
		foreach (DatabaseHolder holder in sheets_.Values)
		{
			if (holder.ContainsKey(key))
			{
				return holder.Column(key);
			}
		}
		return new DColumn(null);
	}
}
