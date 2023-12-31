# Icarus
LocalizeSystem in Unity (MIT Lisence)

## How To Use
1. Open Icarus.unitypackage
2. Click [Tools] -> [Icarus] -> [LocalizationEnumGenerate]
3. Create Source Code & Enum Code (Icarus/Generated/*.cs)

### Covert csv To enum
- Convert Assets/Icarus/Resources/IcarusLocalizedText.csv to Assets/Irarus/Generated/LocalizationEnum.cs

#### Example Generated Enum
- IcarusLocalizedText.csv
```csv
key,ja,en
Key1Test,テスト,Test
// ----- Comment "//"
KeyTest,テスト,Test
// ----- Comment2 ---------
KeyTest2,テスト2,Test2
```

convert csv -> Enum.cs

- LocalizationEnum.cs
```cs
public enum LocalizationEnum
{
    /// <summary>
    /// ja : テスト
    /// en : Test
    /// </summary>
    Key1Test,
    #region  ----- Comment "//"
    /// <summary>
    /// ja : テスト
    /// en : Test
    /// </summary>
    KeyTest,
    #endregion

    #region  ----- Comment2 ---------
    /// <summary>
    /// ja : テスト2
    /// en : Test2
    /// </summary>
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

