#define CHECK_GENERATE_CLASS
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UnityConstantsGenerator.SourceGenerator;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.TestTools;

namespace UniEnumEditorTests
{
    public class GenerateTests
    {
        const string TestBasePath = "Assets/ConstantsGenerator.Tests/Editor";
        const string SceneDirPath = TestBasePath + "/Scenes";
        const string OutputDirPath = TestBasePath + "/TestOutput";


      
        [Test]
        public void IdentifierTest()
        {
            Assert.AreEqual("hoge", "hoge".SanitizeForIdentifier());
            Assert.AreEqual("_01234", "01234".SanitizeForIdentifier());
            Assert.AreEqual("あいうえお", "あいうえお".SanitizeForIdentifier());
            Assert.AreEqual("_123abcWXZ11", "!@#$%^&*()[]'/123 abcWXZ-11".SanitizeForIdentifier());
        }

        [Test]
        public void ModifierTest()
        {
            var sb = new StringBuilder();

            sb.Length = 0;
            sb.AppendVariableDeclareModifier(Modifier.Const);
            Assert.AreEqual("const ", sb.ToString());
            
            sb.Length = 0;
            sb.AppendVariableDeclareModifier(Modifier.Static);
            Assert.AreEqual("static ", sb.ToString());
            
            sb.Length = 0;
            sb.AppendVariableDeclareModifier(Modifier.Readonly);
            Assert.AreEqual("readonly ", sb.ToString());
            
            sb.Length = 0;
            sb.AppendVariableDeclareModifier(Modifier.Readonly | Modifier.Static);
            Assert.AreEqual("static readonly ", sb.ToString());
            
        }

        [Test]
        public void IndentTest()
        {
            var indent = new IndentState();
            Assert.AreEqual("", indent.ToString());

            indent.Indent += 1;
            Assert.AreEqual(1, indent.Indent);
            Assert.AreEqual("    ", indent.ToString());
            
            indent.Indent += 1;
            Assert.AreEqual(2, indent.Indent);
            Assert.AreEqual("        ", indent.ToString());
            
            indent.Indent -= 1;
            Assert.AreEqual(1, indent.Indent);
            Assert.AreEqual("    ", indent.ToString());
            
            indent.Indent -= 1;
            Assert.AreEqual(0, indent.Indent);
            Assert.AreEqual("", indent.ToString());
            
            indent.Indent -= 1;
            Assert.AreEqual(0, indent.Indent);
            Assert.AreEqual("", indent.ToString());
            
        }

        [UnityTest]
        public IEnumerator SceneTest()
        {
            var sceneFiles = Directory.EnumerateFiles(SceneDirPath, "*.unity", SearchOption.AllDirectories)
                .Select(v => new EditorBuildSettingsScene(v, true))
                .ToArray();

            var setting = new GenerateSetting()
            {
                @namespace = "UniEnumEditorTests",
                outputDir = OutputDirPath,
            };
            
            AssetDatabase.DeleteAsset($"{setting.outputDir}/SceneValues.Generated.cs");
            
            UnityConstantValuesGenerator.UpdateSceneValues(setting, sceneFiles);

            yield return new RecompileScripts();

#if CHECK_GENERATE_CLASS
            Assert.AreEqual("UniEnumTest_100", SceneName.UniEnumTest_100);
            Assert.AreEqual("UniEnumTest_101", SceneName.UniEnumTest_101);
            Assert.AreEqual("UniEnumTest_102", SceneName.UniEnumTest_102);

            Assert.AreEqual(0, SceneId.UniEnumTest_100);
            Assert.AreEqual(1, SceneId.UniEnumTest_101);
            Assert.AreEqual(2, SceneId.UniEnumTest_102);

            Assert.AreEqual($"{SceneDirPath}/UniEnumTest_100.unity", ScenePath.UniEnumTest_100);
            Assert.AreEqual($"{SceneDirPath}/UniEnumTest_101.unity", ScenePath.UniEnumTest_101);
            Assert.AreEqual($"{SceneDirPath}/UniEnumTest_102.unity", ScenePath.UniEnumTest_102);
#endif
        }
        
        [UnityTest]
        public IEnumerator UnityConstantsTest()
        {
            var setting = new GenerateSetting()
            {
                @namespace = "UniEnumEditorTests",
                outputDir = OutputDirPath,
            };
            
            AssetDatabase.DeleteAsset($"{setting.outputDir}/TagValues.Generated.cs");
            AssetDatabase.DeleteAsset($"{setting.outputDir}/LayerValues.Generated.cs");
            AssetDatabase.DeleteAsset($"{setting.outputDir}/SortingLayerValues.Generated.cs");
            
            UnityConstantValuesGenerator.UpdateUnityConstants(setting);
            yield return new RecompileScripts();
                        
#if CHECK_GENERATE_CLASS            
            Assert.AreEqual("12@hoge -##A", TagName._12hogeA);
            
            Assert.AreEqual(17, (int)LayerId.アイウエオ);
            Assert.AreEqual((1 << 17), (int)LayerMaskValue.アイウエオ);

            Assert.AreEqual(21, (int)LayerId._123abcWXZ11);
            Assert.AreEqual((1 << 21), (int)LayerMaskValue._123abcWXZ11);
            
            Assert.AreEqual(SortingLayer.NameToID("Default"), SortingLayerId.Default);
#endif
        }
        
    }
}
