using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
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
        private bool _overwrite;
        private Template _template;
        private string _creationFolderPath = "";
        private string _subfolderPath = "";
        private string _fileNameSuffix = "";

        public Template Template
        {
            get => _template;
            set => _template = value;
        }

        public bool Overwrite
        {
            get => _overwrite;
            set => _overwrite = value;
        }


        public string CreationFolderPath
        {
            get => _creationFolderPath;
            set
            {
                _creationFolderPath = RemoveEndingSlashAndSpaces(value);
                _creationFolderPath = RemoveStartingSlash(value);
            }
        }


        public string SubfolderPath
        {
            get => _subfolderPath;
            set => _subfolderPath = RemoveEndingSlashAndSpaces(value);
        }

        private string RemoveStartingSlash(string value)
        {
            var subfolderPath = value;
            while (SubfolderPath.StartsWith("/") || SubfolderPath.StartsWith("\\") || SubfolderPath.EndsWith(" "))
            {
                subfolderPath = subfolderPath.Substring(1);
            }

            return subfolderPath;
        }

        private string RemoveEndingSlashAndSpaces(string value)
        {
            var subfolderPath = value;
            while (SubfolderPath.EndsWith("/") || SubfolderPath.EndsWith("\\") || SubfolderPath.EndsWith(" "))
            {
                subfolderPath = subfolderPath.Substring(0, subfolderPath.Length - 1);
            }

            return subfolderPath;
        }

        public string FileNameSuffix
        {
            get => _fileNameSuffix;
            set => _fileNameSuffix = value;
        }

        public TemplateRequest(Template template, string fileNameSuffix, string creationFolderPath,
            string subfolderPath = "", bool overwrite = false)
        {
            Template = template;
            FileNameSuffix = fileNameSuffix;
            SubfolderPath = subfolderPath;
            CreationFolderPath = creationFolderPath;
            Overwrite = overwrite;
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
            foreach (var templateRequest in value)
            {
                if (!templateRequest.Template.FilePathExists())
                {
                    throw new NonExistingTemplateFilePathException(
                        $"File \"{templateRequest.Template.FilePath}\" does not exist for template named \"{templateRequest.FileNameSuffix}\"",
                        templateRequest.Template);
                }
            }

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

    public GenerationRequest(TemplateRequest[] templateRequests, Type[] types,
        string creationFolderPath = "Assets/SOA/Generated",
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
            //Debug.Log($"Creating files for type of {type.Name}");
            foreach (var templateRequest in request.TemplateRequests)
            {
                Debug.Log($"Creating {type.Name + templateRequest.FileNameSuffix}");
                var scriptContent =
                    GetScriptContent(type, templateRequest.Template.FilePath, GetReplacementStrings(type));
                var creationFilePath = GetCreationFilePath(templateRequest, type);
                FileInfo file = new FileInfo(creationFilePath);
                file.Directory.Create(); // If the directory already exists, this method does nothing.
                File.WriteAllText(file.FullName, scriptContent);
                //Debug.Log($"\n{scriptContent}");
                //Debug.Log($"Finished creating {type.Name + templateRequest.FileNameSuffix}");
            }

            //Debug.Log($"Finished creating files for type of {type.Name}");
        }
        AssetDatabase.Refresh();
    }

    private static string GetCreationFilePath(GenerationRequest.TemplateRequest templateRequest, Type type)
    {
        var creationFilePath = "";
        creationFilePath += $"{templateRequest.CreationFolderPath}/";
        var isEditor = templateRequest.Template.IsEditor;
        creationFilePath += isEditor ? "Editor/" : "";
        var subfoldPathExist = !string.IsNullOrEmpty(templateRequest.SubfolderPath);
        creationFilePath += subfoldPathExist ? $"{templateRequest.SubfolderPath}/" : "";
        creationFilePath += $"{type.Name}{templateRequest.FileNameSuffix}";
        var fileExtension = "cs";
        creationFilePath += $".{fileExtension}";
        return creationFilePath;
    }

    public static string GetScriptContent(Type t, string templateFilePath, Dictionary<string, string> stringsToReplace)
    {
        var templateContent = File.ReadAllText(templateFilePath);
        string output = templateContent;
        foreach (var replacementString in stringsToReplace)
        {
            output = output.Replace(replacementString.Key, replacementString.Value);
        }

        return output;
    }

    //TODO Should be added into the request eventually
    public static Dictionary<string, string> GetReplacementStrings(Type t)
    {
        var replacementStrings = new Dictionary<string, string>()
        {
            {"$TYPE$", t.Name},
            {"$TYPENAME$", t.Name.First().ToString().ToUpper() + t.Name.Substring(1)},
            {"$USING$", t.Namespace}
        };
        return replacementStrings;
    }
}