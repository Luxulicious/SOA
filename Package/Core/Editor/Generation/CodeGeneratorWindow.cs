using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace SOA.Generation
{
    public class CodeGeneratorWindow : EditorWindow
    {
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
    }
}