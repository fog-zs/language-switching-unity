using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DB_Language))]
public class E_Language : Editor
{
    private DB_Language db;

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Import CSV"))
        {
            db = target as DB_Language;
            Import();
        }
        if (GUILayout.Button("Export CSV"))
        {
            db = target as DB_Language;
            Export();
        }
        base.OnInspectorGUI();
    }

    void Import()
    {
        var path = db.path;
        if (string.IsNullOrEmpty(path))
        {
            path = EditorUtility.OpenFilePanel("Open csv", "", "csv");
        }
        if (string.IsNullOrEmpty(path)) return;

        db.path = path;
        var txt = File.ReadAllText(path, System.Text.Encoding.UTF8);

        ReadData(txt);
    }

    void Export()
    {
        var path = EditorUtility.SaveFilePanel("", "", "DB_Language", "csv");

        if (string.IsNullOrEmpty(path)) return;

        var txt = WriteData();

        File.WriteAllText(path, txt, System.Text.Encoding.UTF8);
    }

    private void ReadData(string txt)
    {
        List<string[]> data = new();

        // ReadData
        foreach (var line in txt.Split('\n'))
        {
            var splitLine = line.Split(',');
            if (splitLine.Length == 1) continue;

            data.Add(splitLine);
        }

        // SetData
        for (var j = 1; j < data[0].Length; j++)
        {
            var languageTable = db.data[j-1].table.data;
            languageTable.Clear();

            for (int i = 1; i < data.Count; i++)
            {
                if (data[i].Length < j) continue;

                var element = new DB_LanguageTableE
                {
                    id = data[i][0],    //‘æ1—ñ
                    text = data[i][j],  //‘æn—ñ
                };

                languageTable.Add(element);
            }
            EditorUtility.SetDirty(db.data[j - 1].table);
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Import successful");
    }

    private string WriteData()
    {
        var txt = "id,";

        // Header
        foreach (var code in Enum.GetNames(typeof(eLanguage.code)))
        {
            txt += $"{code},";
        }
        txt = txt.Remove(txt.Length - 1);
        txt += "\n";

        // Content
        for (var i = 0; i < db.data[0].table.data.Count; i++)
        {
            txt += db.data[0].table.data[i].id;
            for (var j = 0; j < db.data.Count; j++)
            {
                txt += $",{db.data[j].table.data[i].text}";
            }
            txt += "\n";
        }
        txt = txt.Remove(txt.Length - 1);
        txt = txt.Replace("\r", "");

        return txt;
    }
}
