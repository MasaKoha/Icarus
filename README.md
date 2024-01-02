# Icarus
Text Management & Localize System in Unity (MIT Lisence)
テキスト管理 & ローカライズシステム

## How To Use
1. Open Icarus.unitypackage
2. Click [Tools] -> [Icarus] -> [LocalizationEnumGenerate]
3. Create Source Code & Enum Code (Icarus/Generated/*.cs)

### Covert csv To enum
- Convert Assets/Icarus/Resources/IcarusLocalizedText.csv to Assets/Irarus/Generated/LocalizationEnum.cs
- In csv File Support "##" & "//"
    - "## abc" add #region abc and #endregion in exported Enum.cs.
    - "// abc" don't Export Enum.cs.
#### Example Generated Enum
- IcarusLocalizedText.csv
```csv
key,ja,en
// ....
Key1Test,テスト,Test
## Category1
KeyTest,テスト,Test
## Category2
KeyTest2,テスト2,Test2
```

convert csv -> Enum.cs

- LocalizationEnum.cs
```cs
public enum LocalizationEnum
{
    // ja : テスト
    // en : Test
    Key1Test,

    #region  Category1
    // ja : テスト
    // en : Test    
    KeyTest,

    #endregion

    #region  Category2
    // ja : テスト2
    // en : Test2
    KeyTest2,

    #endregion
}
```

### LocalizedFileText

``` CS
// UnityEngine.Start
private void Start()
{
    // Must Initialize
    // arg1 "ja" Default Language
    // arg2 "en" Target Language
    TextLocalizer.Initialize("ja", "en");
    // if "en" column is string.Empty, Get "ja" text;
}
```

or

``` CS
private void Start()
{
    // Enable Another Text inject;
    // example
    var csvText = Load(***);
    TextLocalizer.Initialize("ja", "en", csvText);
}
```

### Usage
- Code Usage
``` CS
private void GetTextExample()
{
    // if get text, exist 2 patterns.
    // usage enum (recommend)
    var text1 = TextLocalizer.GetText(LocalizationEnum.KeyTest);
    // usage enumNameText (unrecommend) because typo possible fail.
    var text2 = TextLocalizer.GetText("KeyTest");
}
```

- Attach Script(IcarusTextMeshProUGUITextLocalize.cs or IcarusUGUITextLocalize.cs) to Text or TextMeshProUGUI Attached GameObject.

- if Attach IcarusTextMeshProUGUITextLocalize.cs or IcarusUGUITextLocalize.cs, support input string only.

![AttachComponent](https://github.com/MasaKoha/Icarus/assets/5647635/853b012e-1e04-4f13-aced-7e41c5057a90)

