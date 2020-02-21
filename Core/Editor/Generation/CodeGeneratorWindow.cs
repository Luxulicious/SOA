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
    public class Template
    {
        public bool include = true;
        public string name = "";
        public string filePath = "SOA/Package/Core/Editor/Generation/Templates/...";
    }

    public class CodeGeneratorWindow : EditorWindow
    {
        public static int typeLimit = 10;

        public static List<Template> templates = new List<Template>
        {
            //Variables
            new Template()
            {
                filePath = "SOA/Package/Core/Editor/Generation/Templates/VariableTemplate.template", name = "Variable",
                include = true
            },
            new Template()
            {
                filePath = "SOA/Package/Core/Editor/Generation/Templates/VariableEditorTemplate.template",
                name = "Variable Editor", include = true
            },
            //References
            new Template()
            {
                filePath = "SOA/Package/Core/Editor/Generation/Templates/ReferenceTemplate.template",
                name = "Reference", include = true
            },
            new Template()
            {
                filePath = "SOA/Package/Core/Editor/Generation/Templates/ReferenceDrawerTemplate.template",
                name = "Reference Drawer", include = true
            },
            //Game Events
            new Template()
            {
                filePath = "SOA/Package/Core/Editor/Generation/Templates/GameEventTemplate.template",
                name = "Game Event"
            },
            new Template()
            {
                filePath = "SOA/Package/Core/Editor/Generation/Templates/GameEventListenerTemplate.template",
                name = "Game Event Listener", include = true
            },
            new Template()
            {
                filePath = "SOA/Package/Core/Editor/Generation/Templates/GameEventEditorTemplate.template",
                name = "Game Event Editor", include = true
            },
            //Unity Events
            new Template()
            {
                filePath = "SOA/Package/Core/Editor/Generation/Templates/EventsTemplate.template",
                name = "Unity Events", include = true
            },
            //Multis 
            new Template()
            {
                filePath = "SOA/Package/Core/Editor/Generation/Templates/ReferenceListVariableTemplate.template",
                name = "Reference List Variable", include = false
            },
            new Template()
            {
                filePath = "SOA/Package/Core/Editor/Generation/Templates/ReferenceListVariableEditorTemplate.template",
                name = "Reference List Variable Editor", include = false
            },
            new Template()
            {
                filePath = "SOA/Package/Core/Editor/Generation/Templates/ReferenceListTemplate.template",
                name = "Reference List", include = false
            }
        };

        public static string path = "Assets/SOA/Generated";
        public static bool overwrite = false;
        public static bool individualSubfolders = true;
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
        public static Dictionary<string, Type> selectedTypes = new Dictionary<string, Type>();

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
            EditorGUI.BeginChangeCheck();
            searchQuery = EditorGUILayout.TextField("Search", searchQuery);
            if (EditorGUI.EndChangeCheck())
            {
                Search(searchQuery, typeLimit);
            }

            //Available types
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

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Selected Types");
            //Selected types
            foreach (var selectedTypeName in selectedTypes.Keys)
            {
                if (selectedTypes.Count >= typeLimit) break;
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

            EditorGUILayout.Space();
            //Creation options
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField("Creation Path", path);
            if (GUILayout.Button("Browse..."))
                path = EditorUtility.OpenFolderPanel("Select a path to save to", "", "");
            EditorGUILayout.EndHorizontal();
            overwrite = EditorGUILayout.Toggle("Overwrite", overwrite);
            individualSubfolders = EditorGUILayout.Toggle("Individual Subfolders", individualSubfolders);


            EditorGUILayout.Space();
            //Creation buttons
            if (GUILayout.Button("Create"))
                Create();
            /*
            if (GUILayout.Button("Regenerate (WIP)"))
                Regenerate();
            if (GUILayout.Button("Regenerate All (WIP)"))
                RegenerateAll();
            */
            EditorGUILayout.Space();
            advancedFoldedOut = EditorGUILayout.Foldout(advancedFoldedOut, "Advanced");
            if (advancedFoldedOut)
            {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Include");
                EditorGUILayout.LabelField("Name");
                EditorGUILayout.LabelField("Path");
                EditorGUILayout.EndHorizontal();

                var templatesToRemove = new List<Template>();
                for (int i = 0; i < templates.Count; i++)
                {
                    var drawnTemplate = DrawTemplate(templates[i]);
                    if (drawnTemplate != null)
                        templates[i] = drawnTemplate;
                    else
                        templatesToRemove.Add(templates[i]);
                }

                foreach (var templateToRemove in templatesToRemove)
                {
                    templates.Remove(templateToRemove);
                }

                if (GUILayout.Button("+"))
                {
                    templates.Add(new Template());
                }

                EditorGUILayout.EndVertical();
            }

            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            EditorGUI.indentLevel = originalIndentLevel;
        }

        void OnInspectorUpdate()
        {
            // This will only get called 10 times per second.
            try
            {
                if (Event.current.type != EventType.Layout) return;
            }
            catch (Exception e)
            {
            }

            Repaint();
        }

        private void Add(string typeName)
        {
            selectedTypes.Add(typeName, filteredAvailableTypes[typeName]);
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
                    foreach (var selectedType in selectedTypes)
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
            selectedTypes.Remove(typeName);
        }

        private Template DrawTemplate(Template template)
        {
            GUIStyle style = null;
            style = GetTemplateStyle(template);

            if (style != null)
                EditorGUILayout.BeginHorizontal(style);
            else
                EditorGUILayout.BeginHorizontal();
            template.include = EditorGUILayout.Toggle(template.include);
            EditorGUILayout.TextField(template.name);
            EditorGUILayout.TextField(template.filePath);
            if (GUILayout.Button("Browse..."))
            {
                var pathSelected = EditorUtility.OpenFilePanel("Select a template to use", "", "template");
                template.filePath = !string.IsNullOrEmpty(pathSelected) ? pathSelected : template.filePath;
            }

            if (GUILayout.Button("-", GUILayout.MaxWidth(25)))
            {
                return null;
            }

            EditorGUILayout.EndHorizontal();
            return template;
        }

        private static GUIStyle GetTemplateStyle(Template template)
        {
            GUIStyle style = null;
            if (!template.include)
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
                var isValid = IsValidTemplate(template);
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

        private static bool IsValidTemplate(Template template)
        {
            //TODO Replace this statement with an actual check
            var validPath = true;
            var endsWithDotTemplate = template.filePath.EndsWith("." + "template");
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
            Debug.Log("Creating...");
            foreach (var selectedType in selectedTypes)
            {
                CodeGenerator.Generate(selectedType.Value);
                Debug.Log($"Created: {selectedType.Value?.Name}");
            }
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

            /*
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var allTypeNames = new Dictionary<string, Type>();
            Func<Type, bool> typePredicate = x =>
                !x.IsAbstract &&
                !x.ContainsGenericParameters;
            foreach (var a in allAssemblies)
            {
                foreach (var t in a.GetExportedTypes().Where(typePredicate))
                {
                    var skip = false;
                    foreach (var n in _namespacesToExclude)
                    {
                        if (t.Namespace?.StartsWith(n) ?? (t.FullName?.StartsWith(n) ?? false))
                        {
                            skip = true;
                            break;
                        }
                    }

                    if (skip)
                        continue;
                    var key = t.Name != null ? t.Name :
                        t.FullName.Contains(".") ? t.FullName.Substring(t.FullName.LastIndexOf(".")) : t.FullName;
                    var value = t;
                    allTypeNames.Add(key, value);
                }
            }
            return allTypeNames;
            */
        }

        /*
        private int _choiceIndex = 0;
        private string _query = "";
        [Range(0, 999)] private int _choiceLimit = 10;
        private string[] _choices;
        private Dictionary<string, string> _allowedTypes;
        private Vector2 _scrollPosition;
        private static int _maxChoiceLimit = 100;

        private static List<string> _namespacesToExclude = new List<string>()
        {
            "UnityScript.Lang.",
            "Boo.Lang.",
            "UnityEditor.",
            "UnityEditorInternal.",
            "CompilerGenerated.",
            "SOA."
        };

        [MenuItem("SOA/Create Types")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(CodeGeneratorWindow), false, "Generate Types");
        }

        void OnGUI()
        {
            if (_allowedTypes == null || _allowedTypes.Count <= 0)
            {
                _allowedTypes = GetAllowedTypeNames();
            }

            _query = EditorGUILayout.TextField("Search", _query);
            var filledInChoiceLimit = EditorGUILayout.IntField("Result Limit", this._choiceLimit);
            _choiceLimit = filledInChoiceLimit > 0
                ? filledInChoiceLimit > _maxChoiceLimit ? _maxChoiceLimit : filledInChoiceLimit
                : 1;

            List<string> displayedOptions = new List<string>();
            //Primitives and UnityEngine types get special treatment
            if (_query == "bool")
                displayedOptions.Add(typeof(bool).FullName);
            else if (_query.Contains("byte"))
                displayedOptions.Add(typeof(byte).FullName);
            else if (_query.Contains("sbyte,"))
                displayedOptions.Add(typeof(sbyte).FullName);
            else if (_query.Contains("char"))
                displayedOptions.Add(typeof(char).FullName);
            else if (_query.Contains("decimal"))
                displayedOptions.Add(typeof(decimal).FullName);
            else if (_query.Contains("float"))
                displayedOptions.Add(typeof(float).FullName);
            else if (_query.Contains("int"))
                displayedOptions.Add(typeof(int).FullName);
            else if (_query.Contains("uint"))
                displayedOptions.Add(typeof(uint).FullName);
            else if (_query.Contains("long"))
                displayedOptions.Add(typeof(long).FullName);
            else if (_query.Contains("ulong"))
                displayedOptions.Add(typeof(ulong).FullName);
            else if (_query.Contains("object"))
                displayedOptions.Add(typeof(object).FullName);
            else if (_query.Contains("short"))
                displayedOptions.Add(typeof(short).FullName);
            else if (_query.Contains("ushort"))
                displayedOptions.Add(typeof(ushort).FullName);
            else if (_query.Contains("string"))
                displayedOptions.Add(typeof(string).FullName);

            //TODO Refactor these foreaches into a delegate
            foreach (var t in _allowedTypes)
            {
                if (displayedOptions.Contains(t.Value)) continue;
                if (displayedOptions.Count >= _choiceLimit)
                    break;
                if (t.Key.Equals(_query, StringComparison.CurrentCultureIgnoreCase))
                    displayedOptions.Add(t.Value);
            }

            foreach (var t in _allowedTypes)
            {
                if (displayedOptions.Contains(t.Value)) continue;
                if (displayedOptions.Count >= _choiceLimit)
                    break;
                if (t.Key.StartsWith(_query, StringComparison.CurrentCultureIgnoreCase))
                    displayedOptions.Add(t.Value);
            }

            foreach (var t in _allowedTypes)
            {
                if (displayedOptions.Contains(t.Value)) continue;
                if (displayedOptions.Count >= _choiceLimit)
                    break;
                if (t.Key.Contains(_query))
                    displayedOptions.Add(t.Value);
            }

            foreach (var t in _allowedTypes)
            {
                if (displayedOptions.Contains(t.Value)) continue;
                if (displayedOptions.Count >= _choiceLimit)
                    break;
                if (t.Value.Contains(_query))
                    displayedOptions.Add(t.Value);
            }

            if (_choiceLimit > 0)
                displayedOptions = displayedOptions.Take(_choiceLimit).ToList();
            else
                _choiceLimit = 10;

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, false, true);
            _choiceIndex = GUILayout.SelectionGrid
            (
                _choiceIndex,
                displayedOptions.ToArray(),
                1
            );
            GUILayout.EndScrollView();
            if (GUILayout.Button("Create"))
            {
                Debug.Log("Creating...");
                var typeName = displayedOptions[_choiceIndex];
                var type = GetTypeInCurrentDomain(typeName);

                CodeGenerator.Generate(type);
                Debug.Log($"Created: {type?.Name}");
            }
        }

        //TODO Move to type extensions class
        private static Type GetTypeInCurrentDomain(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }

            return null;
        }

        private static Dictionary<string, string> GetAllowedTypeNames()
        {
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var allTypeNames = new Dictionary<string, string>();
            Func<Type, bool> typePredicate = x =>
                !x.IsAbstract &&
                !x.ContainsGenericParameters;
            foreach (var a in allAssemblies)
            {
                foreach (var t in a.GetExportedTypes().Where(typePredicate))
                {
                    var skip = false;
                    foreach (var n in _namespacesToExclude)
                    {
                        if (t.Namespace?.StartsWith(n) ?? (t.FullName?.StartsWith(n) ?? false))
                        {
                            skip = true;
                            break;
                        }
                    }

                    if (skip)
                        continue;
                    var key = t.Name != null ? t.Name :
                        t.FullName.Contains(".") ? t.FullName.Substring(t.FullName.LastIndexOf(".")) : t.FullName;
                    var value = t.FullName;
                    allTypeNames[key] = value;
                }
            }

            return allTypeNames;
        }
        */
    }
}