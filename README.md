# Unity Constants Generator

[![openupm](https://img.shields.io/npm/v/com.xtaltools.unityconstantsgenerator?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.xtaltools.unityconstantsgenerator/)

Constants values utitlity

## Install

### Package Manager

```manifest.json
{
    "dependencies": {
        "com.xtaltools.unityconstantsgenerator": "https://github.com/neptaco/UnityConstantsGenerator.git?path=Assets/ConstantsGenerator"
    }
}
```

## Generate Unity constant values

Open `ProjectSettings -> Constants Generator` and set the generation target.

|Target|Generated classes|
|----|-------|
|Scene|SceneId<br/>SceneName<br/>ScenePath|
|Layer|LayerId<br/>LayerMaskValue|
|SortingLayer|SortingLyaerId<br/>SortingLayerName|
|Tag|TagName|

## Samples

```csharp:SceneValues.cs
    public class SceneId
    {
        /// <summary>
        /// <para>0: ProfileScene</para>
        /// Assets/UniEnumDev/Scenes/ProfileScene.unity
        /// </summary>
        public const int ProfileScene = 0;

    }

    public class SceneName
    {
        /// <summary>
        /// <para>0: ProfileScene</para>
        /// Assets/UniEnumDev/Scenes/ProfileScene.unity
        /// </summary>
        public const string ProfileScene = "ProfileScene";

    }

    public class ScenePath
    {
        /// <summary>
        /// <para>0: ProfileScene</para>
        /// Assets/UniEnumDev/Scenes/ProfileScene.unity
        /// </summary>
        public const string ProfileScene = "Assets/UniEnumDev/Scenes/ProfileScene.unity";

    }
```


```csharp:LayerValues.cs
    public enum LayerId
    {
        /// <summary>
        /// Default
        /// </summary>
        Default = 0,

        /// <summary>
        /// TransparentFX
        /// </summary>
        TransparentFX = 1,

        /// <summary>
        /// Ignore Raycast
        /// </summary>
        IgnoreRaycast = 2,

        /// <summary>
        /// Water
        /// </summary>
        Water = 4,

        /// <summary>
        /// UI
        /// </summary>
        UI = 5,

        /// <summary>
        /// アイウエオ
        /// </summary>
        アイウエオ = 17,

        /// <summary>
        /// !@#$%^&*()[]'/123 abcWXZ-11
        /// </summary>
        _123abcWXZ11 = 21,

    }

    [System.Flags]
    public enum LayerMaskValue
    {
        /// <summary>
        /// Default
        /// </summary>
        Default = 1 << 0,

        /// <summary>
        /// TransparentFX
        /// </summary>
        TransparentFX = 1 << 1,

        /// <summary>
        /// Ignore Raycast
        /// </summary>
        IgnoreRaycast = 1 << 2,

        /// <summary>
        /// Water
        /// </summary>
        Water = 1 << 4,

        /// <summary>
        /// UI
        /// </summary>
        UI = 1 << 5,

        /// <summary>
        /// アイウエオ
        /// </summary>
        アイウエオ = 1 << 17,

        /// <summary>
        /// !@#$%^&*()[]'/123 abcWXZ-11
        /// </summary>
        _123abcWXZ11 = 1 << 21,

    }
```

```csharp:SortingLayerValues.cs
    public class SortingLayerId
    {
        public static readonly int Default = SortingLayer.NameToID("Default");

    }

    public class SortingLayerName
    {
        public const string Default = "Default";

    }
```

```csharp:TagValues.cs
    public class TagName
    {
        /// <summary>
        /// Untagged
        /// </summary>
        public static readonly string Untagged = "Untagged";

        /// <summary>
        /// Respawn
        /// </summary>
        public static readonly string Respawn = "Respawn";

        /// <summary>
        /// Finish
        /// </summary>
        public static readonly string Finish = "Finish";

        /// <summary>
        /// EditorOnly
        /// </summary>
        public static readonly string EditorOnly = "EditorOnly";

        /// <summary>
        /// MainCamera
        /// </summary>
        public static readonly string MainCamera = "MainCamera";

        /// <summary>
        /// Player
        /// </summary>
        public static readonly string Player = "Player";

        /// <summary>
        /// GameController
        /// </summary>
        public static readonly string GameController = "GameController";

        /// <summary>
        /// 12@hoge -##A
        /// </summary>
        public static readonly string _12hogeA = "12@hoge -##A";

    }
```


### Generation timing

|Target|Timing|
|----|-------|
|Scene|When the scene list is changed in the Build Settings window|
|Layer<br />SortingLayer<br />Tag|When the Postprocessor detects a change in TagManager.asset|


### How to manually update scene variables on batch build

Call the following method.

```
UnityConstantValuesGenerator.UpdateSceneValues(EditorBuildSettingScenes[] scenes)
```

### License

This library is under the MIT License.