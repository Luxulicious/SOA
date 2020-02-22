using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;


namespace SOA.Generation
{
    [Serializable]
    public class TemplateEntry
    {
        private bool _include = true;
        private Template _template;
        private string _subfolderName;
        private string _fileNameSuffix = "";

        public bool Include
        {
            get => _include;
            set => _include = value;
        }

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

        public TemplateEntry(string fileNameSuffix, Template template, bool include = false)
        {
            this._template = template;
            this._fileNameSuffix = fileNameSuffix;
            this._include = include;
        }

        public TemplateEntry()
        {
        }
    }

    public class CodeGeneratorWindow : EditorWindow
    {
        public static int typeLimit = 10;

        public List<TemplateEntry> templateEntries;

        public string _creationFolderPath = "Assets/SOA/Generated";
        public bool overwrite = false;
        public bool _createSubfolders = true;
        public bool advancedFoldedOut = false;

        private static List<string> _namespacesToExclude = new List<string>()
        {
            "UnityScript.Lang.",
            "Boo.Lang.",
            "UnityEditor.",
            "UnityEditorInternal.",
            "CompilerGenerated.",
            "SOA."
        };

        public static Color excludedTemplateBackgroundColor = new Color(1, 0, 0, 0.2f);
        public static Color includeTemplateBackgroundColor = new Color(0, 1, 0, 0.2f);
        public static Color invalidTemplateBackgroundColor = new Color(1, 0.5f, 0, 0.2f);

        public Vector2 scrollPos;

        public string searchQuery;
        public bool searchIsLoading = false;

        public Dictionary<string, Type> availableTypes = new Dictionary<string, Type>();
        public Dictionary<string, Type> filteredAvailableTypes = new Dictionary<string, Type>();
        public Dictionary<string, Type> _selectedTypes = new Dictionary<string, Type>();

        [MenuItem("Assets/SOA/Generate Types")]
        [MenuItem("Assets/Create/SOA/Generate Types")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(CodeGeneratorWindow), false, "Generate Types");
        }

        void OnEnable()
        { 
            if (templateEntries == null)
                templateEntries = new List<TemplateEntry>();
            if (templateEntries.Count < 1)
            { 
                templateEntries.AddRange(new List<TemplateEntry>()
                {  
                    //Variables
                    new TemplateEntry(" Variable", new Template(
                        $"{Application.dataPath}/SOA/Package/Core/Editor/Generation/Templates/VariableTemplate.template",
                        false), true),
                    new TemplateEntry(" Variable Editor", new Template(
                        $"{Application.dataPath}/SOA/Package/Core/Editor/Generation/Templates/VariableEditorTemplate.template",
                        true), true),
                    //References
                    new TemplateEntry(" Reference", new Template(
                            $"{Application.dataPath}/SOA/Package/Core/Editor/Generation/Templates/ReferenceTemplate.template",
                            false),
                        true),
                    new TemplateEntry(" Reference Drawer", new Template(
                            $"{Application.dataPath}/SOA/Package/Core/Editor/Generation/Templates/ReferenceDrawerTemplate.template",
                            true),
                        true),
                    //Game Events
                    new TemplateEntry(" Game Event", new Template(
                            $"{Application.dataPath}/SOA/Package/Core/Editor/Generation/Templates/GameEventTemplate.template",
                            false),
                        true),
                    new TemplateEntry(" Game Event Listener", new Template(
                            $"{Application.dataPath}/SOA/Package/Core/Editor/Generation/Templates/GameEventListenerTemplate.template",
                            false),
                        true),
                    new TemplateEntry(" Game Event Editor", new Template(
                            $"{Application.dataPath}/SOA/Package/Core/Editor/Generation/Templates/GameEventEditorTemplate.template",
                            true),
                        true),
                    //Unity Events
                    new TemplateEntry(" Unity Events", new Template(
                        $"{Application.dataPath}/SOA/Package/Core/Editor/Generation/Templates/EventsTemplate.template",
                        false), true),
                    //Multis 
                    new TemplateEntry(" Reference List Variable", new Template(
                            $"{Application.dataPath}/SOA/Package/Core/Editor/Generation/Templates/ReferenceListVariableTemplate.template",
                            false),
                        false),
                    new TemplateEntry(" Reference List Variable Editor", new Template(
                            $"{Application.dataPath}/SOA/Package/Core/Editor/Generation/Templates/ReferenceListVariableEditorTemplate.template",
                            true),
                        false),
                    new TemplateEntry(
                        " Reference List", new Template(
                            $"{Application.dataPath}/SOA/Package/Core/Editor/Generation/Templates/ReferenceListTemplate.template",
                            false),
                        false)
                });
            }

            availableTypes = GetAvailableTypes();
            Search("", typeLimit);
        }

        void OnGUI()
        {
            var originalIndentLevel = EditorGUI.indentLevel;

            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos, false, true,
                GUILayout.ExpandHeight(true));

            EditorGUILayout.LabelField("Available Types");
            //Search available types
            DrawSearch();

            //Available types
            DrawAvailableTypes();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Selected Types");
            //Selected types
            DrawSelectedTypes();

            EditorGUILayout.Space();
            //Creation options
            DrawCreationOptions();


            EditorGUILayout.Space();
            //Creation buttons
            DrawCreationButtons();

            EditorGUILayout.Space();
            //Advanced
            DrawAdvanced();

            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            EditorGUI.indentLevel = originalIndentLevel;
        }

        private void DrawAdvanced()
        {
            advancedFoldedOut = EditorGUILayout.Foldout(advancedFoldedOut, "Advanced");
            if (advancedFoldedOut)
            {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Include");
                EditorGUILayout.LabelField("Is Editor");
                EditorGUILayout.LabelField("File Name Suffix");
                if (_createSubfolders)
                    EditorGUILayout.LabelField("Subfolder Name");
                EditorGUILayout.LabelField("Path");
                EditorGUILayout.EndHorizontal();

                var templatesToRemove = new List<TemplateEntry>();
                for (int i = 0; i < templateEntries.Count; i++)
                {
                    var drawnTemplate = DrawTemplate(templateEntries[i]);
                    if (drawnTemplate != null)
                        templateEntries[i] = drawnTemplate;
                    else
                        templatesToRemove.Add(templateEntries[i]);
                }

                foreach (var templateToRemove in templatesToRemove)
                {
                    templateEntries.Remove(templateToRemove);
                }

                if (GUILayout.Button("+"))
                {
                    templateEntries.Add(new TemplateEntry());
                }

                EditorGUILayout.EndVertical();
            }
        }

        private void DrawCreationButtons()
        {
            if (GUILayout.Button("Create"))
                Create();
            /*
            if (GUILayout.Button("Regenerate (WIP)"))
                Regenerate();
            if (GUILayout.Button("Regenerate All (WIP)"))
                RegenerateAll();
            */
        }

        private void DrawCreationOptions()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField("Creation Path", _creationFolderPath);
            if (GUILayout.Button("Browse..."))
                _creationFolderPath = EditorUtility.OpenFolderPanel("Select a path to save to", "", "");
            EditorGUILayout.EndHorizontal();
            overwrite = EditorGUILayout.Toggle("Overwrite", overwrite);
            _createSubfolders = EditorGUILayout.Toggle("Create Subfolders", _createSubfolders);
        }

        private void DrawSelectedTypes()
        {
            foreach (var selectedTypeName in _selectedTypes.Keys)
            {
                if (_selectedTypes.Count >= typeLimit) break;
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("-", GUILayout.MaxWidth(25)))
                {
                    Remove(selectedTypeName);
                    Search(searchQuery, typeLimit);
                    break;
                }

                EditorGUILayout.LabelField(selectedTypeName);
                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawSearch()
        {
            EditorGUI.BeginChangeCheck();
            searchQuery = EditorGUILayout.TextField("Search", searchQuery);
            if (EditorGUI.EndChangeCheck())
                Search(searchQuery, typeLimit);
        }

        private void DrawAvailableTypes()
        {
            if (!searchIsLoading)
            {
                foreach (var availableTypeName
                    in filteredAvailableTypes.Keys)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("+", GUILayout.MaxWidth(25)))
                    {
                        Add(availableTypeName);
                        Search(searchQuery, typeLimit);
                        break;
                    }

                    EditorGUILayout.LabelField(availableTypeName);
                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                EditorGUILayout.LabelField($"Loading");
            }
        }

        void OnInspectorUpdate()
        {
            // This will only get called 10 times per second.
            try
            {
                if (Event.current.type != EventType.Layout) return;
            }
            catch (Exception)
            {
            }

            Repaint();
        }

        private void Add(string typeName)
        {
            _selectedTypes.Add(typeName, filteredAvailableTypes[typeName]);
            filteredAvailableTypes.Remove(typeName);
        }

        private async void Search(string query, int limit)
        {
            filteredAvailableTypes = new Dictionary<string, Type>();
            searchIsLoading = true;
            await Task.Run(() =>
            {
                foreach (var availableType in availableTypes)
                {
                    if (filteredAvailableTypes.Count >= limit)
                        break;
                    if (!string.IsNullOrEmpty(query))
                    {
                        if (!availableType.Key.StartsWith(query, true, CultureInfo.InvariantCulture) &&
                            !availableType.Value.Name.StartsWith(query, true, CultureInfo.InvariantCulture))
                        {
                            continue;
                        }
                    }

                    var alreadySelectedType = false;
                    foreach (var selectedType in _selectedTypes)
                    {
                        if (selectedType.Value == availableType.Value)
                        {
                            alreadySelectedType = true;
                            break;
                        }
                    }

                    if (alreadySelectedType)
                        continue;

                    filteredAvailableTypes.Add(availableType.Key, availableType.Value);
                }

                searchIsLoading = false;
            });
        }

        private void Remove(string typeName)
        {
            _selectedTypes.Remove(typeName);
        }

        private TemplateEntry DrawTemplate(TemplateEntry templateEntry)
        {
            GUIStyle style = null;
            style = GetTemplateStyle(templateEntry);

            if (style != null)
                EditorGUILayout.BeginHorizontal(style);
            else
                EditorGUILayout.BeginHorizontal();
            templateEntry.Include = EditorGUILayout.Toggle(templateEntry.Include);
            templateEntry.Template.IsEditor = EditorGUILayout.Toggle(templateEntry.Template.IsEditor);
            templateEntry.FileNameSuffix = EditorGUILayout.TextField(templateEntry.FileNameSuffix);
            if (_createSubfolders)
                templateEntry.SubfolderName = EditorGUILayout.TextField(templateEntry.SubfolderName);
            templateEntry.Template.FilePath = EditorGUILayout.TextField(templateEntry.Template.FilePath);
            if (GUILayout.Button("Browse..."))
            {
                var pathSelected = EditorUtility.OpenFilePanel("Select a template to use", "", "template");
                templateEntry.Template.FilePath = !string.IsNullOrEmpty(pathSelected)
                    ? pathSelected
                    : templateEntry.Template.FilePath;
            }

            if (GUILayout.Button("-", GUILayout.MaxWidth(25)))
            {
                return null;
            }

            EditorGUILayout.EndHorizontal();
            return templateEntry;
        }

        private static GUIStyle GetTemplateStyle(TemplateEntry templateEntry)
        {
            GUIStyle style = null;
            if (!templateEntry.Include)
            {
                style = new GUIStyle();
                var background = new Texture2D(1, 1);
                background.SetPixel(1, 1, excludedTemplateBackgroundColor);
                background.Apply();
                style.normal.background = background;
                style.normal.textColor = Color.white;
            }
            else
            {
                var isValid = IsValidTemplate(templateEntry);
                if (isValid)
                {
                    style = new GUIStyle();
                    var background = new Texture2D(1, 1);
                    background.SetPixel(1, 1, includeTemplateBackgroundColor);
                    background.Apply();
                    style.normal.background = background;
                    style.normal.textColor = Color.white;
                }
                else
                {
                    style = new GUIStyle();
                    var background = new Texture2D(1, 1);
                    background.SetPixel(1, 1, invalidTemplateBackgroundColor);
                    background.Apply();
                    style.normal.background = background;
                    style.normal.textColor = Color.white;
                }
            }

            return style;
        }

        //TODO Move this method to template entry itself
        private static bool IsValidTemplate(TemplateEntry templateEntry)
        {
            //TODO Replace this statement with an actual check
            var validPath = true;
            var endsWithDotTemplate = templateEntry.Template.FilePath.EndsWith("." + "template");
            var isValid = endsWithDotTemplate && validPath;
            return isValid;
        }

        private void Select(string type)
        {
            throw new NotImplementedException();
        }

        private void RegenerateAll()
        {
            throw new NotImplementedException();
        }

        private void Regenerate()
        {
            throw new NotImplementedException();
        }

        private void Create()
        {
            var request = CreateRequest();
            CodeGenerator.Generate(request);
        }

        private GenerationRequest CreateRequest()
        {
            //Get included templates from template entries
            var templates = templateEntries.Where(templateEntry => templateEntry.Include)
                .Select(templateEntry => templateEntry.Template).ToArray();
            var selectedTypes = _selectedTypes.Values.ToArray();
            var templateRequests = new GenerationRequest.TemplateRequest[templates.Length];
            for (int i = 0; i < templates.Length; i++)
            {
                templateRequests[i] =
                    new GenerationRequest.TemplateRequest(templates[i], templateEntries[i].FileNameSuffix, templateEntries[i].SubfolderName);
            }

            GenerationRequest request = new GenerationRequest(templateRequests, selectedTypes, _creationFolderPath,
                _createSubfolders, overwrite);
            return request;
        }

        private Dictionary<string, Type> GetAvailableTypes()
        {
            var currentDomainsAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var currentDomainsTypes = new Dictionary<string, Type>();
            foreach (var assembly in currentDomainsAssemblies)
            {
                foreach (var t in assembly.GetExportedTypes())
                {
                    var invalidNamespace = false;
                    foreach (var namespaceToExclude in _namespacesToExclude)
                    {
                        if (t.Namespace != null)
                            if (t.Namespace.StartsWith(namespaceToExclude))
                            {
                                invalidNamespace = true;
                                break;
                            }

                        if (t.FullName != null)
                            if (t.FullName.StartsWith(namespaceToExclude))
                            {
                                invalidNamespace = true;
                                break;
                            }
                    }

                    if (invalidNamespace)
                        continue;

                    var key = t.FullName ?? t.Name;
                    var value = t;
                    try
                    {
                        currentDomainsTypes.Add(key, value);
                    }
                    catch (ArgumentException)
                    {
                        continue;
                    }
                }
            }

            return currentDomainsTypes;
        }
    }
}