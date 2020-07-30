using System;
using UnityConstantsGenerator.SourceGenerator;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace UnityConstantsGenerator
{
    public class UnityConstantsGeneratorSettingsProvider : SettingsProvider
    {
        private static class Styles
        {
            public static readonly GUIContent SourceGenerationLabel = new GUIContent("Source Generation", "Source code generation settings");
            public static readonly GUIContent GenerateTargetsLabel = new GUIContent("Targets");
        }

        private GenerateSetting _generateSetting;
        

        [SettingsProvider]
        public static SettingsProvider Create()
        {
            var path = "Project/Constants Generator";
            var provider = new UnityConstantsGeneratorSettingsProvider(path, SettingsScope.Project)
            {
                keywords = GetSearchKeywordsFromPath(UnityConstantsGeneratorSettings.SavePath)
            };

            return provider;
        }

        private UnityConstantsGeneratorSettingsProvider(string path, SettingsScope scopes) : base(path, scopes)
        { }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            base.OnActivate(searchContext, rootElement);
            
            var settings = UnityConstantsGeneratorSettings.Instance;
            _generateSetting = settings.generateSetting;
        }

        private struct LabeledScope : IDisposable
        {
            public LabeledScope(GUIContent labelContent) 
                : this(labelContent, GUIStyle.none)
            {
                
            }

            public LabeledScope(GUIContent labelContent, GUIStyle boxStyle)
            {
                if (boxStyle == GUIStyle.none)
                    EditorGUILayout.BeginVertical();
                else 
                    EditorGUILayout.BeginVertical(boxStyle);
                
                EditorGUILayout.LabelField(labelContent, EditorStyles.boldLabel);
                
                EditorGUI.indentLevel += 1;
            }

            public void Dispose()
            {
                EditorGUI.indentLevel -= 1;
                EditorGUILayout.EndVertical();
            }
        }

        public override void OnGUI(string searchContext)
        {
            using (new LabeledScope(Styles.SourceGenerationLabel))
            {
                EditorGUI.BeginChangeCheck();
                _generateSetting.@namespace =
                    EditorGUILayout.TextField(new GUIContent("Namespace"), _generateSetting.@namespace);
                _generateSetting.outputDir =
                    EditorGUILayout.TextField(new GUIContent("Output Directory"), _generateSetting.outputDir);

                using (new LabeledScope(Styles.GenerateTargetsLabel))
                {
                    _generateSetting.generateSceneValues = EditorGUILayout.Toggle(new GUIContent("Scene"),
                        _generateSetting.generateSceneValues);
                    _generateSetting.generateSortingLayerValues = EditorGUILayout.Toggle(new GUIContent("SortingLayer"),
                        _generateSetting.generateSortingLayerValues);
                    _generateSetting.generateLayerValues = EditorGUILayout.Toggle(new GUIContent("Layer"),
                        _generateSetting.generateLayerValues);
                    _generateSetting.generateTagValues =
                        EditorGUILayout.Toggle(new GUIContent("Tag"), _generateSetting.generateTagValues);
                }

                EditorGUILayout.Space();

                if (EditorGUI.EndChangeCheck())
                {
                    UnityConstantsGeneratorSettings.Save();
                }

                if (GUILayout.Button("Generate", EditorStyles.miniButtonRight))
                {
                    using (AssetEditing.Scope())
                    {
                        UnityConstantValuesGenerator.UpdateUnityConstants();
                        UnityConstantValuesGenerator.UpdateSceneValues();
                    }

                    AssetDatabase.Refresh();
                }
            }

        }

    }
}
