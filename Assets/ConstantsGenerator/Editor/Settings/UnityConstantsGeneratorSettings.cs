using System.IO;
using UnityConstantsGenerator.SourceGenerator;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityConstantsGenerator
{
    internal class UnityConstantsGeneratorSettings : ScriptableObject
    {
        public const string SavePath = "ProjectSettings/UnityConstantsGeneratorSettings.asset";

        public GenerateSetting generateSetting = new GenerateSetting();

        private static UnityConstantsGeneratorSettings _instance;

        public static UnityConstantsGeneratorSettings Instance => _instance ? _instance : GetOrCreate();

        UnityConstantsGeneratorSettings()
        {
            _instance = this;
        }

        private static UnityConstantsGeneratorSettings GetOrCreate()
        {
            InternalEditorUtility.LoadSerializedFileAndForget(SavePath);
            
            if (_instance == null)
            {
                var instance = CreateInstance<UnityConstantsGeneratorSettings>();
                instance.hideFlags = HideFlags.HideAndDontSave;
            }
            
            Debug.Assert(_instance != null);
            
            return _instance;
        }

        public static void Save()
        {
            if (_instance == null)
            {
                Debug.Log("Cannot save " + nameof(UnityConstantsGeneratorSettings));
                return;
            }

            string folderPath = Path.GetDirectoryName(SavePath);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            
            InternalEditorUtility.SaveToSerializedFileAndForget(new Object[] {_instance}, SavePath, true);

        }
        

        internal static SerializedObject GetSerializedObject()
        {
            var serializedObject = new SerializedObject(Instance);
            serializedObject.UpdateIfRequiredOrScript();
            return serializedObject;
        } 
    }
}
