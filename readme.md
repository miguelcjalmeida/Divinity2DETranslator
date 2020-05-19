# Divinity 2 Definitive Edition - Portuguese / Português

This project translates almost the entire game to portuguese keeping untouched the game tags. It makes use of the translations by the community ['Traduz O Jogo'](https://www.facebook.com/traduzojogo/) and Google Translator. 

This translation is compatible with the game version: **v.3.6.44.4046**. Be advised that further versions compability is not guaranteed. 

## Installation
- Extract the localization file 'english.xml' from divinity2/english.pak with the [Export Tool](https://drive.google.com/open?id=0B3R5i4ne8pTreUVTQ1VCZHhGNnc).
1) extract export tool files
2) open ConverterApp.exe
3) click in the top 'game' dropdown and select 'Divinity: Original sin 2' 
4) select tab 'PAK/LSV Tools'
5) in 'Package Path' select the file 'english.pak' from divinity2/Data/Localization
6) in 'Destination Path' select the following folder: divinity2/Data/Localization
7) Click on the 'Extract Package' button (make sure not to click on the wrong one)
8) Close the ConverterApp
9) open the folder divinity2/Data/Localization/English

- Replace the just extracted english.xml with the translated one (download here).

- After all, make sure you have following game folder structure: 
```
divinity2/Data/Localization
  |_English
    |_english.xml (replaced one)
    |_language.lsx
    |_...
```


Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)


## Credits
- ['Traduz O Jogo'](https://www.facebook.com/traduzojogo/) for providing translations
- Mikael for testing