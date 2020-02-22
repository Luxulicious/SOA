using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;


namespace SOA.Generation
{
    [Serializable]
    public class TemplateEntry
    {
        public bool include = true;
        public Template template;

        public bool Include
        {
            get => include;
            set => include = value;
        }

        public Template Template
        {
            get => template;
            set => template = value;
        }

        public TemplateEntry(Template template, bool include)
        {
            this.template = template;
            this.include = include;
        }

        public TemplateEntry()
        {
            this.template = new Template();
            this.include = true;
        }
    }

    public class CodeGeneratorWindow : EditorWindow
    {
        public static int typeLimit = 10;

        public static List<TemplateEntry> templateEntries = new List<TemplateEntry>
        {
            //Variables
            new TemplateEntry(new Template("Variable",
                "SOA/Package/Core/Editor/Generation/Templates/VariableTemplate.template", false), true),
            new TemplateEntry(new Template("Variable Editor",
                "SOA/Package/Core/Editor/Generation/Templates/VariableEditorTemplate.template", true), true),
            //References
            new TemplateEntry(new Template("Reference",
                    "SOA/Package/Core/Editor/Generation/Templates/ReferenceTemplate.template", false),
                true),
            new TemplateEntry(new Template("Reference Drawer",
                    "SOA/Package/Core/Editor/Generation/Templates/ReferenceDrawerTemplate.template", true),
                true),
            //Game Events
            new TemplateEntry(new Template("Game Event",
                    "SOA/Package/Core/Editor/Generation/Templates/GameEventTemplate.template", false),
                true),
            new TemplateEntry(new Template("Game Event Listener",
                    "SOA/Package/Core/Editor/Generation/Templates/GameEventListenerTemplate.template", false),
                true),
            new TemplateEntry(new Template("Game Event Editor",
                    "SOA/Package/Core/Editor/Generation/Templates/GameEventEditorTemplate.template", true),
                true),
            //Unity Events
            new TemplateEntry(new Template(
                "Unity Events", "SOA/Package/Core/Editor/Generation/Templates/EventsTemplate.template",
                false), true),
            //Multis 
            new TemplateEntry(new Template("Reference List Variable",
                    "SOA/Package/Core/Editor/Generation/Templates/ReferenceListVariableTemplate.template", false),
                false),
            new TemplateEntry(new Template("Reference List Variable Editor",
                    "SOA/Package/Core/Editor/Generation/Templates/ReferenceListVariableEditorTemplate.template", true),
                false),
            new TemplateEntry(
                new Template("Reference List",
                    "SOA/Package/Core/Editor/Generation/Templates/ReferenceListTemplate.template", false), false)
        };

        public static string _creationFolderPath = "Assets/SOA/Generated";
        public static bool overwrite = false;
        public static bool _individualSubfolders = true;
        public static bool advancedFoldedOut = false;

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

        public static Vector2 scrollPos;

        public static string searchQuery;
        public static bool searchIsLoading = false;

        public static Dictionary<string, Type> availableTypes = new Dictionary<string, Type>();
        public static Dictionary<string, Type> filteredAvailableTypes = new Dictionary<string, Type>();
        public static Dictionary<string, Type> _selectedTypes = new Dictionary<string, Type>();

        [MenuItem("Assets/SOA/Generate Types")]
        [MenuItem("Assets/Create/SOA/Generate Types")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(CodeGeneratorWindow), false, "Generate Types");
        }

        void OnEnable()
        {
            availableTypes = GetAvailableTypes();
            Debug.Log($"Available type count: {availableTypes.Count}");

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
                EditorGUILayout.LabelField("Name");
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

        private static void DrawCreationOptions()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField("Creation Path", _creationFolderPath);
            if (GUILayout.Button("Browse..."))
                _creationFolderPath = EditorUtility.OpenFolderPanel("Select a path to save to", "", "");
            EditorGUILayout.EndHorizontal();
            overwrite = EditorGUILayout.Toggle("Overwrite", overwrite);
            _individualSubfolders = EditorGUILayout.Toggle("Individual Subfolders", _individualSubfolders);
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
            templateEntry.include = EditorGUILayout.Toggle(templateEntry.include);
            EditorGUILayout.TextField(templateEntry.Template.Name);
            EditorGUILayout.TextField(templateEntry.Template.FilePath);
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
            if (!templateEntry.include)
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

        private static GenerationRequest CreateRequest()
        {
            var templates = templateEntries.Select(templateEntry => templateEntry.template).ToArray();
            var selectedTypes = _selectedTypes.Values.ToArray();
            GenerationRequest request = new GenerationRequest(templates, selectedTypes, _creationFolderPath, _individualSubfolders, overwrite);
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