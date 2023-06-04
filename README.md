# Unity Game Language Switcher

This program allows you to perform language switching in games developed with Unity. It enables you to read language data from CSV files and write it back to CSV files.

## Features

- Read language data from CSV files.
- Write language data to CSV files.

## Usage

Setting the language is simple. Just modify the `lang.language` property of the `DB_Language` class. 

For example:

```csharp
DB_Language lang;
lang.language = eLanguage.code.zhTW;
```

To switch languages, you can load the original text and perform the language switch. 

For example:

```csharp
DB_Language lang;
var result = lang.GetText("text");
```

## License

This project is licensed under the [MIT License](LICENSE).
