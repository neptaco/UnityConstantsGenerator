using UnityEngine.Serialization;

namespace UnityConstantsGenerator.SourceGenerator
{
    [System.Serializable]
    public class GenerateSetting
    {
        public string @namespace = "UnityConstantsGenerator";
        public string outputDir = "Assets/Plugins/UnityConstantsGenerator/Generated";
        
        public bool generateSceneValues;
        public bool generateSortingLayerValues;
        public bool generateLayerValues;
        public bool generateTagValues;
       
    }
}
