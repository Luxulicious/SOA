using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Template
{
    private string _filePath = "";
    private bool _isEditor = false;

    public Template()
    {
    }

    public Template(string filePath, bool isEditor)
    {
        this._filePath = filePath;
        this._isEditor = isEditor;
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

public static class TemplateExtensions
{
    public static bool FilePathExists(this Template template)
    {
        return File.Exists(template.FilePath);
    }
}


public class NonExistingTemplateFilePathException : Exception
{
    protected Template _template;

    public NonExistingTemplateFilePathException(string message, Template template) : base(message)
    {
        this._template = template;
    }
}

[Serializable]
public class GenerationRequest
{
    public class TemplateRequest
    {
        private Template _template;
        private string _subfolderName;
        private string _fileNameSuffix = "";

        public Template Template
        {
            get => _template;
            set => _template = value;
        }

        public string SubfolderName
        {
            get => _subfolderName;
            set => _subfolderName = value;
        }

        public string FileNameSuffix
        {
            get => _fileNameSuffix;
            set => _fileNameSuffix = value;
        }

        public TemplateRequest(Template template, string fileNameSuffix, string subfolderName = "")
        {
            _template = template;
            _fileNameSuffix = fileNameSuffix;
            _subfolderName = subfolderName;
        }
    }

    private TemplateRequest[] _templateRequests;
    private Type[] _types;
    private string _creationFolderPath = "Assets/SOA/Generated";
    private bool _overwrite = false;
    private bool _individualSubfolders = true;

    public TemplateRequest[] TemplateRequests
    {
        get => _templateRequests;
        set
        {
            Debug.Log("Checking if template file paths exist...");
            foreach (var templateRequest in value)
            {
                if (!templateRequest.Template.FilePathExists())
                {
                    throw new NonExistingTemplateFilePathException(
                        $"File \"{templateRequest.Template.FilePath}\" does not exist for template named \"{templateRequest.FileNameSuffix}\"",
                        templateRequest.Template);
                }
            }

            Debug.Log("All template file paths are exist!");
            _templateRequests = value;
        }
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

    public GenerationRequest(TemplateRequest[] templateRequests, Type[] types, string creationFolderPath = "Assets/SOA/Generated",
        bool overwrite = false,
        bool individualSubfolders = true)
    {
        this.TemplateRequests = templateRequests;
        this.Types = types;
        this.CreationFolderPath = creationFolderPath;
        this.Overwrite = overwrite;
        this.IndividualSubfolders = individualSubfolders;
    }
}

public static class CodeGenerator
{
    public static void Generate(GenerationRequest request)
    {
        foreach (var type in request.Types)
        {
            Debug.Log($"Creating files for type of {type.Name}");
            foreach (var templateRequest in request.TemplateRequests)
            {
                Debug.Log($"Creating {type.Name + templateRequest.FileNameSuffix}");

                Debug.Log($"Finished creating {type.Name + templateRequest.FileNameSuffix}");
            }

            Debug.Log($"Finished creating files for type of {type.Name}");
        }
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