using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using anki_gen_net.Commands;

namespace anki_gen_net
{
    internal static class Program
    {
        public static Config config;

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InitializeConfig();

            Application.Run(new Form1());
        }

        private static void InitializeConfig()
        {
            //
            // Set config.
            //

            // todo: load templates from a file.

            var dataTemplateSb = new StringBuilder();
            dataTemplateSb.Append("<div class=\"data-container\">");
            dataTemplateSb.Append(
                "<div class=\"grid-item title\">Часть речи</div>");
            dataTemplateSb.Append(
                "<div class=\"grid-item title\">Транскрипция</div>");
            dataTemplateSb.Append(
                "<div class=\"grid-item title\">Значение</div>");
            dataTemplateSb.Append(
                "<div class=\"grid-item speech-part\">{speech-part}</div>");
            dataTemplateSb.Append(
                "<div class=\"grid-item transcription\">{transcription}</div>");
            dataTemplateSb.Append(
                "<div class=\"grid-item meaning\">{meaning}</div>");

            var markersTemplateSb = new StringBuilder();
            markersTemplateSb.Append(
                "<div class=\"markers-container\">{markers}</div>");

            //
            // Markers.
            //
            var markers = new List<string>
            {
                "Formal", "Law", "Specialized", "Participle", "Informal",
                "Business"
            };

            config = new Config
            {
                EntityTag = "span",
                SentenceTemplate =
                    "<div class=\"sentence\">{sentence}{attributes}</div>",
                SentenceTypeTemplate = "<div class=\"type\"></div>",
                DataTemplate = dataTemplateSb.ToString(),
                MarkersTemplate = markersTemplateSb.ToString()
            };
        }

        public static void RunOneCommand(AbstractCommand command)
        {
            var copyToClipboardCommand = new CopyToClipboardCommand();
            var commands = new List<ICommand>
            {
                command,
                copyToClipboardCommand
            };
            var invoker = new GeneratorInvoker();
            invoker.SetOnGenerate(commands);

            // The list of results for all fields.
            var fields = new List<string>();

            invoker.BatchInvoke(ref fields);

            MessageBox.Show(@"Data copied.");
        }

        /// <summary>
        ///     Run all command to create the entire card word.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public static void GenerateDeckFile(
            ListBox.ObjectCollection savedWordsCollection,
            CheckedListBox.ObjectCollection clbMarkersItems)
        {
            try
            {
                using var writer = new StreamWriter(GetFilePath(), true);

                foreach (WordListItem savedWord in savedWordsCollection)
                {
                    var checkedItems =
                        ((IEnumerable<int>)savedWord.Data["markers"]).Select(
                            idx => clbMarkersItems[idx]).ToList();
                    var commands = GenerateCommands(savedWord, checkedItems);
                    var fields = GetFields(commands);

                    WriteFieldsIntoFile(fields, writer);
                }
            }
            catch (NoFileException)
            {
            }
        }

        private static string GetFilePath()
        {
            var sfd = new SaveFileDialog();

            sfd.Title = @"Save Deck-file";
            sfd.Filter = @"CSV files (*.csv)|*.csv";
            sfd.AddExtension = true;
            sfd.DefaultExt = "csv";
            sfd.OverwritePrompt = true;

            if (sfd.ShowDialog() == DialogResult.Cancel)
                throw new NoFileException();

            return sfd.FileName;
        }

        /// <summary>
        ///     Write word cards into the file.
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="writer"></param>
        private static void WriteFieldsIntoFile(List<string> fields,
            TextWriter writer)
        {
            var record = new StringBuilder();

            foreach (var field in fields) record.Append($"{field}\t");

            writer.WriteLine(record!.ToString());
        }

        private static List<string> GetFields(List<ICommand> commands)
        {
            var fields = new List<string>();

            // Run commands.
            var invoker = new GeneratorInvoker();
            invoker.SetOnGenerate(commands);
            invoker.BatchInvoke(ref fields);

            // Insert sound-field.
            fields.Insert(6, "[sound:]");

            return fields;
        }

        /// <summary>
        /// </summary>
        /// <param name="savedWord"></param>
        /// <param name="checkedItems"></param>
        /// <returns></returns>
        private static List<ICommand> GenerateCommands(WordListItem savedWord,
            List<object> checkedItems)
        {
            return new List<ICommand>
            {
                new GenerateFrontFieldCommand(
                    savedWord.Data["literal"].ToString().Trim(),
                    // todo: Save selected text in a sentence-type field and restore it on generate // process.
                    new List<string> { "" },
                    new List<string>
                        { savedWord.Data["entity_type"].ToString().Trim() }
                ),
                new GenerateTranslationFieldCommand(
                    savedWord.Data["translation"].ToString().Trim(),
                    // todo: Save selected text in a sentence-type field and restore it on generate // process.
                    new List<string> { "" }
                ),
                new GenerateOriginalFieldCommand(
                    savedWord.Data["original"].ToString().Trim(),
                    // todo: Save selected text in a sentence-type field and restore it on generate // process.
                    new List<string> { "" }
                ),
                new GenerateDataFieldCommand(
                    savedWord.Data["speech_part"].ToString().Trim(),
                    savedWord.Data["transcription"].ToString().Trim(),
                    savedWord.Data["meaning"].ToString().ToUpper().Trim()
                ),
                new GenerateMarkersFieldCommand(checkedItems),
                new GenerateNoTagFieldCommand(savedWord.Data["main_entity"]
                    .ToString().Trim()),
                new GenerateNoTagFieldCommand(savedWord.Data["dictionary"]
                    .ToString().Trim())
            };
        }
    }
}