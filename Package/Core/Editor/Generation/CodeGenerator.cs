using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SOA.Base;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Template
{
    private string _name;
    private string _filePath;
    private bool _isEditor = false;

    public Template()
    {
    }

    public Template(string name, string filePath, bool isEditor)
    {
        this._name = name;
        this._filePath = filePath;
        this._isEditor = isEditor;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public string FilePath
    {
        get => _filePath;
        set => _filePath = value;
    }

    public bool IsEditor
    {
        get => _isEditor;
        set => _isEditor = value;
    }
}

[Serializable]
public class GenerationRequest
{
    private Template[] _templates;
    private Type[] _types;
    private string _creationFolderPath = "Assets/SOA/Generated";
    private bool _overwrite = false;
    private bool _individualSubfolders = true;

    public Template[] Templates
    {
        get => _templates;
        set => _templates = value;
    }

    public Type[] Types
    {
        get => _types;
        set => _types = value;
    }

    public string CreationFolderPath
    {
        get => _creationFolderPath;
        set => _creationFolderPath = value;
    }

    public bool Overwrite
    {
        get => _overwrite;
        set => _overwrite = value;
    }

    public bool IndividualSubfolders
    {
        get => _individualSubfolders;
        set => _individualSubfolders = value;
    }

    public GenerationRequest(Template[] templates, Type[] types, string creationFolderPath = "Assets/SOA/Generated", bool overwrite = false,
        bool individualSubfolders = true)
    {
        this._templates = templates;
        this._types = types;
        this._creationFolderPath = creationFolderPath;
        this._overwrite = overwrite;
        this._individualSubfolders = individualSubfolders;
    }
}

public static class CodeGenerator
{
    public static void Generate(GenerationRequest request)
    {
        throw new NotImplementedException();
    }
}

//public static class CodeGenerator
//{
//    private static string _generatedDirectoryName = "Generated";
//    private static string _editorDirectoryName = "Editor";
//    private static string _templatesFolderPath = "Generation/Templates";
//    private static bool _overwriteExistingFiles = false;

//    public struct PathAndNameDataEntry
//    {
//        private static string _packageName = "com.theluxgames.soa";
//        public string TargetDirectory { get; set; }
//        public string TemplateName { get; set; }
//        public string TargetFileName { get; set; }

//        public string GetTargetFilePath(Type t)
//        {
//            return TargetDirectory + "/" + string.Format(TargetFileName,
//                       t.Name != null ? t.Name :
//                       t.FullName.Contains(".") ? t.FullName.Substring(t.FullName.LastIndexOf(".") + 1) : t.FullName);
//        }

//        public string GetTemplatePath()
//        {
//            return "C:\\Users\\Tom\\Documents\\Github\\SOA\\Package\\Core\\Editor\\Generation\\Templates" + "/" + TemplateName;
//            /*return "Packages" + "/"  + _packageName + "/" + "Package/" + "Core/Editor/" + _templatesFolderPath + "/" +
//                   TemplateName;*/
//        }

//        public string GetScriptContents(Type t)
//        {
//            var templatePath = GetTemplatePath();
//            var templateContent = File.ReadAllText(templatePath);

//            var output = templateContent;
//            var replacementStrings = ReplacementStrings(t);
//            foreach (var replacementString in replacementStrings)
//            {
//                output = output.Replace(replacementString.Key, replacementString.Value);
//            }

//            return output;
//        }

//        public static Dictionary<string, string> ReplacementStrings(Type t)
//        {
//            var replacementStrings = new Dictionary<string, string>()
//            {
//                {"$TYPE$", t.Name},
//                {"$TYPENAME$", t.Name.First().ToString().ToUpper() + t.Name.Substring(1)},
//                {"$USING$", t.Namespace}
//            };
//            return replacementStrings;
//        }
//    }

//    private static List<PathAndNameDataEntry> _pathAndNameData = new List<PathAndNameDataEntry>()
//    {
//        new PathAndNameDataEntry()
//        {
//            TargetDirectory = Application.dataPath + "/" + _generatedDirectoryName + "/" + "Game Events",
//            TemplateName = "GameEventTemplate",
//            TargetFileName = "{0}GameEvent.cs"
//        },
//        new PathAndNameDataEntry()
//        {
//            TargetDirectory = Application.dataPath + "/" + _generatedDirectoryName + "/" + "Game Events",
//            TemplateName = "GameEventListenerTemplate",
//            TargetFileName = "{0}GameEventListener.cs"
//        },
//        new PathAndNameDataEntry()
//        {
//            TargetDirectory = Application.dataPath + "/" + _generatedDirectoryName + "/" + "References",
//            TemplateName = "ReferenceTemplate",
//            TargetFileName = "{0}Reference.cs"
//        },
//        new PathAndNameDataEntry()
//        {
//            TargetDirectory = Application.dataPath + "/" + _generatedDirectoryName + "/" + "Variables",
//            TemplateName = "ReferenceListVariableTemplate",
//            TargetFileName = "{0}ReferenceListVariable.cs"
//        },
//        new PathAndNameDataEntry()
//        {
//            TargetDirectory = Application.dataPath + "/" + _generatedDirectoryName + "/" + "Variables",
//            TemplateName = "VariableTemplate",
//            TargetFileName = "{0}Variable.cs"
//        },
//        new PathAndNameDataEntry()
//        {
//            TargetDirectory = Application.dataPath + "/" + _generatedDirectoryName + "/" + _editorDirectoryName + "/" +
//                              "Game Events",
//            TemplateName = "GameEventEditorTemplate",
//            TargetFileName = "{0}GameEventEditor.cs"
//        },
//        new PathAndNameDataEntry()
//        {
//            TargetDirectory = Application.dataPath + "/" + _generatedDirectoryName + "/" + _editorDirectoryName + "/" +
//                              "References",
//            TemplateName = "ReferenceDrawerTemplate",
//            TargetFileName = "{0}ReferenceDrawer.cs"
//        },
//        new PathAndNameDataEntry()
//        {
//            TargetDirectory = Application.dataPath + "/" + _generatedDirectoryName + "/" + _editorDirectoryName + "/" +
//                              "Variables",
//            TemplateName = "VariableEditorTemplate",
//            TargetFileName = "{0}VariableEditor.cs"
//        },
//        new PathAndNameDataEntry()
//        {
//            TargetDirectory = Application.dataPath + "/" + _generatedDirectoryName + "/" + _editorDirectoryName + "/" +
//                              "Variables",
//            TemplateName = "ReferenceListVariableEditorTemplate",
//            TargetFileName = "{0}ReferenceListVariableEditor.cs"
//        },
//        new PathAndNameDataEntry()
//        {
//            TargetDirectory = Application.dataPath + "/" + _generatedDirectoryName + "/" +
//                              "Unity Events",
//            TemplateName = "EventsTemplate",
//            TargetFileName = "{0}Events.cs"
//        },
//        new PathAndNameDataEntry()
//        {
//            TargetDirectory = Application.dataPath + "/" + _generatedDirectoryName + "/" + "References",
//            TemplateName = "ReferenceListTemplate",
//            TargetFileName = "{0}ReferenceList.cs"
//        }
//    };

//    public static void Generate(Type t)
//    {
//        foreach (var entry in _pathAndNameData)
//        {
//            var targetFilePath = entry.GetTargetFilePath(t);
//            var scriptContents = entry.GetScriptContents(t);

//            if (File.Exists(targetFilePath) && !_overwriteExistingFiles)
//            {
//                Debug.LogError($"Cannot generate file at {targetFilePath}, because it overwrites an existing file.");
//                continue;
//            }
//            else
//            {
//                Debug.Log("Creating: " + targetFilePath);
//                Directory.CreateDirectory(Path.GetDirectoryName(targetFilePath));
//                File.WriteAllText(targetFilePath, scriptContents);
//            }
//        }

//        AssetDatabase.Refresh();
//    }
//}