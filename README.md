# Authors

S. Markov
Y. Krasnobokyi

# Purpose

The application is designed to generate a file containing a set of vocabulary cards for the Anki application.
This file can then be imported into the Anki application.

Anki vocabulary card contains a set of user-defined fields.
Each field can be formatted with html tags.

Thus, the Application automates the process of formatting fields according to a pre-created template.

# User interface

## Screens

The Application has two main screens:

- Language deck selection screen.

  On this screen, the user selects which language to edit the deck for.
  This version, at the moment, only allows you to edit the deck for English.

  ![Language selection screen](/img/language_screen.png "Language selection screen")

- The deck editing screen.

  On this screen, the user directly edits the vocabulary card fields and builds the deck.

  ![Word card field editing screen](/img/fields_screen.png "Word card field editing screen")

  The screen contains two tabs:

  - **Fields**: form for editing the fields of the word card.
  - **List**: a list of vocabulary cards in a deck.
 
    ![Word list tab](/img/word_list.png "Word list tab")

## Toolbar

![Main toolbar](/img/toolbar.png "Main toolbar")

The toolbar has the following set of buttons:

- **Languages**: Displays the deck language selection screen.
- **Make File**: generates a deck file.
- **Add Card**: adds a new vocabulary card to the deck.
- **Clear Form**: clears the word card editing form.
- **Remove Card**: removes a word card from the deck.
- **Clear List**: removes all word cards from the deck.
- **Exit**: exit the Application.

# Creating a word card

After selecting a deck language, the Application displays a screen for editing word card fields.

The list of fields corresponds to the pre-created structure of the word card and contains such fields:

- **Front**: (required) A literal translation of the original English sentence.
- **Literal**: (required) A literary translation of the original English sentence.
- **Original**: (required) The original English sentence where the entity sought is used.
- **Type**: Type of entity. Can be empty or have the values: Idion, Phrasal verb.
- **Data**: Group containing fields:
    - **Speech part**: The part of speech to which the entity being sought belongs. Has the meanings: Noun, Pronoun, Adjective, Verb, Adverd, Preposition.
    - **Meaning**: the meaning of the entity.
    - **Transcription**: transcription into Russian.
- **Markers**: area of common use of the entity. Has the meanings: Business, Formal, Informal, Law, Participle, Specialized.
- **Dictionary description**: a description of the entity from the source.
- **Main entity**: (required) title of the dictionary entry.

Next to each field is the "Copy" button, pressing which copies the data of the corresponding field formatted in a specific way for that field. The copied data can be used to paste it into the Anki application directly.
For example, code of the Original field value:
```html
<div class="sentence">The ball <span>bounced</span> off the goalpost and into the net.</div>
```

# Deck file

The deck file is a text file in csv format, with fields separated by a tab character.

For instance, deck file with three word cards:
```csv
<div class="sentence"><span></span>Мяч отскочил от стойки ворот и в сеть.<div class="type"></div></div>	<div class="sentence"><span></span>Мяч отскочил от стойки ворот и попал в сетку.</div>	<div class="sentence"><span></span>The ball bounced off the goalpost and into the net.</div>	<div class="data-container"><div class="grid-item title">Часть речи</div><div class="grid-item title">Транскрипция</div><div class="grid-item title">Значение</div><div class="grid-item speech-part">Verb</div><div class="grid-item transcription">/baʊns/</div><div class="grid-item meaning">JUMP</div>	<div class="markers-container"></div>	bounce	[sound:]	to (cause to) move up or away after hitting a surface	
<div class="sentence"><span></span>Они пошли на путешествие охотиться для сокровиша в западные холмы.<div class="type"></div></div>	<div class="sentence"><span></span>Они отправились на поиски сокровищ в западные холмы.</div>	<div class="sentence"><span></span>They went on an adventure hunting for treasure in the western hills.</div>	<div class="data-container"><div class="grid-item title">Часть речи</div><div class="grid-item title">Транскрипция</div><div class="grid-item title">Значение</div><div class="grid-item speech-part">Noun</div><div class="grid-item transcription">/ədˈven.tʃɚ/</div><div class="grid-item meaning"></div>	<div class="markers-container"></div>	adventure	[sound:]	an unusual, exciting, and possibly dangerous activity, such as a trip or experience, or the excitement produced by such an activity	
<div class="sentence"><span></span>Лиз и Фил имеют дочь и три сына.<div class="type"></div></div>	<div class="sentence"><span></span>У Лиз и Фила есть дочь и трое сыновей.</div>	<div class="sentence"><span></span>Liz and Phil have a daughter and three sons.</div>	<div class="data-container"><div class="grid-item title">Часть речи</div><div class="grid-item title">Транскрипция</div><div class="grid-item title">Значение</div><div class="grid-item speech-part">Noun</div><div class="grid-item transcription">/ˈdɑː.t̬ɚ/</div><div class="grid-item meaning"></div>	<div class="markers-container"></div>	daughter	[sound:]	your female child
```

# Future release features

- Editing a word card already added to the deck.
- Support of cards for Estonian language.
- Loading marker list from a separate file.
- Loading formatting templates from a separate file.
- Loading entity type list from a separate file.
