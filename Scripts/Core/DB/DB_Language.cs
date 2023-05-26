using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DB_Language", menuName = "DB/DB_Language")]
public class DB_Language : ScriptableObject
{
    public eLanguage.code language;
    public string path;
    public List<DB_LanguageE> data = new();
    public Dictionary<eLanguage.code, DB_LanguageTable> lang = new();
    bool doneInit = false;

    public void Init()
    {
        lang.Clear();
        foreach (var item in data)
        {
            lang.Add(item.code, item.table);
        }
    }

    public string GetText(string text)
    {
        if (!doneInit) Init();

        var code = language;
        
        if(!lang.ContainsKey(code)) { Debug.LogWarning($"{text} language data not found"); return text; }
        var table = lang[code];

        if (!table.text.ContainsKey(text)) { Debug.LogWarning($"{text} language data not found"); return text; }
        return table.text[text];
    }
}

[System.Serializable]
public class DB_LanguageE
{
    public eLanguage.code code;
    public DB_LanguageTable table;
}
