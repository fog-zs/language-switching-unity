using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DB_LanguageTable", menuName = "DB/DB_LanguageTable")]
public class DB_LanguageTable : ScriptableObject
{
    public List<DB_LanguageTableE> data = new List<DB_LanguageTableE>();
    public Dictionary<string, string> text = new();
    private void OnEnable()
    {
        text.Clear();
        foreach (var item in data)
        {
            text.Add(item.id, item.text);
        }
    }
}

[System.Serializable]
public class DB_LanguageTableE
{
    public string id;
    public string text;

}