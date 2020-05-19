# Divinity 2 Definitive Edition - Portuguese / Português

This project translates almost the entire game to portuguese keeping untouched the game tags. It makes use of the translations by the community [Traduz O Jogo](https://www.facebook.com/traduzojogo/) and Google Translator. 

This translation is compatible with the game version: **v.3.6.44.4046**. Be advised that further versions compability is not guaranteed. 

The community translation version from [Traduz O Jogo](https://www.facebook.com/traduzojogo/) used by this project is '[02-05-2020-BR_EN](https://drive.google.com/drive/u/0/folders/0B3R5i4ne8pTrZGNGSkFEWlhDYmc)'.

## Installation
1) Extract the localization files from *'<divinity path>/Data/Localization/english.pak'* with the [Export Tool](https://drive.google.com/open?id=0B3R5i4ne8pTreUVTQ1VCZHhGNnc).
- extract the ExportTool-v1.8.4 to a folder
- open ConverterApp.exe
- click on the top dropdown 'game' and select 'Divinity: Original sin 2' 
- select the tab 'PAK/LSV Tools'
- in 'Package Path', select the file *'<divinity path>/Data/Localization/english.pak'*
- in 'Destination Path', select the folder: *'<divinity path>/Data/'*
- Click on the 'Extract Package' button (make sure not to click on the wrong one)
- Close the ConverterApp.exe
- open the folder *'<divinity path>/Data/Localization/English'*

2) Replace the just extracted *'english.xml'* with the translated one ([download here](https://github.com/miguelcjalmeida/Divinity2DETranslator/blob/master/Divinity2DETranslator/Assets/Translated/english.zip?raw=true)).

3) After all, make sure you have following game folder structure: 
```
<divinity path>/Data/Localization
  |_English
    |_english.xml (replaced one)
    |_language.lsx
    |_...
```

4) Try opening the game. If needed, delete the file *'<divinity path>/Data/Localization/English.pak'*

## Download
[Translation v1.0.1](https://github.com/miguelcjalmeida/Divinity2DETranslator/blob/master/Divinity2DETranslator/Assets/Translated/english.zip?raw=true) - Current version

## Credits
Thanks [Traduz O Jogo](https://www.facebook.com/traduzojogo/) for the great community effort into providing portuguese translations for all players that need it.

## License
[MIT](https://choosealicense.com/licenses/mit/)