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

- The deck editing screen.

  On this screen, the user directly edits the vocabulary card fields and builds the deck.

  The screen contains two tabs:

  - **Fields**: form for editing the fields of the word card.
  - **List**: a list of vocabulary cards in a deck.

## Toolbar

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

# Deck file

The deck file is a text file in csv format, with fields separated by a tab character.

# Future release features

- Editing a word card already added to the deck.
- Support of cards for Estonian language.
- Loading marker list from a separate file.
- Loading formatting templates from a separate file.
- Loading entity type list from a separate file.
