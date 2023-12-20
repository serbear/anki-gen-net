using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using anki_gen_net.Commands;

namespace anki_gen_net
{
    public partial class Form1 : Form
    {
        private const string ListTabText = "List";
        private readonly Dictionary<object, TextBox> _noTagFields = new();
        private readonly Dictionary<string, object> _taggedControls = new();

        private readonly Dictionary<string, object> _wordCardValues = new();

        public Form1()
        {
            InitializeComponent();
            _noTagFields.Add(btnCopyDictionaryDescription,
                txtDictionaryDescription);
            _noTagFields.Add(btnMainEntryCopy, txtMainEntity);
            RecursivelyDoOnObjects(FillTaggedControls, this);
        }

        private void tsbLanguageMenu_Click(object sender, EventArgs e)
        {
            tsMainTools.Visible = false;
            tlpLanguagesList.Visible = true;
            lblComingSoon.Visible = false;
            tlpFieldsWordsScreen.Visible = false;
            UpdateSelectedLanguageTitle("");
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            tlpLanguagesList.Visible = false;
            tlpFieldsWordsScreen.Visible = true;
            UpdateSelectedLanguageTitle("English");
            tsMainTools.Visible = true;
            
            btnAddNewCard.Enabled = true;
            btnClearForm.Enabled = true;
            txtLiteral.Focus();
        }

        private void btnEstonian_Click(object sender, EventArgs e)
        {
            tlpLanguagesList.Visible = false;
            lblComingSoon.Visible = true;
            UpdateSelectedLanguageTitle("Eesti");
            tsMainTools.Visible = true;

            btnAddNewCard.Enabled = false;
            btnClearForm.Enabled = false;
        }

        private void UpdateSelectedLanguageTitle(string languageName)
        {
            tslSelectedLanguage.Text = languageName;
        }

        private void btnCopyFront_Click(object sender, EventArgs e)
        {
            var command = new GenerateFrontFieldCommand(
                txtLiteral.Text.Trim(),
                new List<string> { txtLiteral.SelectedText.Trim() },
                new List<string> { cmbType.Text }
            );
            Program.RunOneCommand(command);
        }

        private void btnCopyTranslation_Click(object sender, EventArgs e)
        {
            var command = new GenerateTranslationFieldCommand(
                txtTranslation.Text.Trim(),
                new List<string> { txtTranslation.SelectedText.Trim() }
            );
            Program.RunOneCommand(command);
        }

        private void btnCopyData_Click(object sender, EventArgs e)
        {
            var command = new GenerateDataFieldCommand(
                cbSpeechPart.Text.Trim(),
                txtTrascription.Text.Trim(),
                txtMeaning.Text.Trim().ToUpper()
            );
            Program.RunOneCommand(command);
        }

        private void btnCopyMarkers_Click(object sender, EventArgs e)
        {
            var command =
                new GenerateMarkersFieldCommand(clbMarkers.CheckedItems);
            Program.RunOneCommand(command);
        }


        private void tsbAddNewWordCard_Click(object sender, EventArgs e)
        {
            if (!CheckMandatoryFields()) return;

            _wordCardValues.Clear();
            RecursivelyDoOnObjects(GetFieldValues, this);
            lbSavedWords.Items.Add(new WordListItem(_wordCardValues));
            RecursivelyDoOnObjects(ClearAllFormFields, this);

            btnGenerateFile.Enabled = true;
            btnRemoveWord.Enabled = true;
            btnClearList.Enabled = true;

            UpdateTabTitle();
        }

        private bool CheckMandatoryFields()
        {
            var mandatoryFields = new List<TextBox>
            {
                txtLiteral,
                txtTranslation,
                txtOriginal,
                txtMainEntity
            };
            var messages = new Dictionary<string, string>
            {
                {
                    txtLiteral.Name,
                    "The field for word-for-word translation must be filled in."
                },
                {
                    txtTranslation.Name,
                    "The field for literary translation must be filled in."
                },
                {
                    txtOriginal.Name,
                    "The field for the original text must be filled in."
                },
                {
                    txtMainEntity.Name,
                    "The field describing the main entity must be filled in."
                }
            };

            var result = mandatoryFields
                .Where(t => string.IsNullOrEmpty(t.Text)).Select(t => t)
                .FirstOrDefault();

            if (result is null) return true;

            MessageBox.Show(messages[result.Name], @"Empty Field Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            result.Focus();

            return false;
        }

        private void UpdateTabTitle()
        {
            var listCount = lbSavedWords.Items.Count;
            tabLanguage.TabPages[1].Text = listCount == 0
                ? ListTabText
                : $@"{ListTabText} ({listCount})";
        }

        private void tsbClearForm_Click(object sender, EventArgs e)
        {
            RecursivelyDoOnObjects(ClearAllFormFields, this);
        }

        private void tsbRemoveWordCard_Click(object sender, EventArgs e)
        {
            lbSavedWords.Items.RemoveAt(lbSavedWords.SelectedIndex);
            btnGenerateFile.Enabled = lbSavedWords.Items.Count > 0;
            btnRemoveWord.Enabled = lbSavedWords.Items.Count > 0;
            UpdateTabTitle();
        }

        private void tsbClearList_Click(object sender, EventArgs e)
        {
            lbSavedWords.Items.Clear();
            btnGenerateFile.Enabled = false;
            btnRemoveWord.Enabled = false;
            btnClearList.Enabled = false;
            UpdateTabTitle();
        }

        private void tsbGenerateFile_Click(object sender, EventArgs e)
        {
            Program.GenerateDeckFile(lbSavedWords.Items, clbMarkers.Items);
        }

        private void GetFieldValues(object c)
        {
            if (((Control)c).Tag == null) return;

            object value = c switch
            {
                TextBox textBox => textBox.Text.Trim(),
                ComboBox box => box.Text,
                CheckedListBox => GetMarkersSelectedIndices(),
                _ => null
            };

            _wordCardValues.Add(((Control)c).Tag.ToString(), value);
        }

        private int[] GetMarkersSelectedIndices()
        {
            var x = new int[clbMarkers.CheckedIndices.Count];
            clbMarkers.CheckedIndices.CopyTo(x, 0);
            return x;
        }

        private void ClearAllFormFields(object c)
        {
            if (((Control)c).Tag == null) return;

            switch (c)
            {
                case TextBox textBox:
                    textBox.Text = "";
                    break;
                case ComboBox box:
                    box.SelectedItem = null;
                    break;
                case CheckedListBox listBox:
                    for (var i = 0; i < listBox.Items.Count; i++)
                        listBox.SetItemChecked(i, false);

                    break;
            }
        }


        private void FillTaggedControls(object o)
        {
            if (((Control)o).Tag == null) return;
            _taggedControls.Add(((Control)o).Tag.ToString(), o);
        }


        private void RecursivelyDoOnObjects(DoSomethingWithObject aDel,
            Control aCtrl)
        {
            aDel(aCtrl);
            foreach (Control c in aCtrl.Controls)
                RecursivelyDoOnObjects(aDel, c);
        }

        private void lbSavedWords_DoubleClick(object sender, EventArgs e)
        {
            // read data from saved word

            var itemData = ((WordListItem)lbSavedWords.SelectedItem).Data;

            foreach (var tagAndValue in itemData)
                switch (_taggedControls[tagAndValue.Key])
                {
                    case TextBox textBox:
                        textBox.Text = tagAndValue.Value.ToString();
                        break;
                    case ComboBox box:
                        box.SelectedItem = tagAndValue.Value.ToString();
                        break;
                    case CheckedListBox listBox:
                        // clear list
                        for (var i = 0; i < clbMarkers.Items.Count; i++)
                            listBox.SetItemChecked(i, false);

                        // set checks within list
                        foreach (var idx in (IEnumerable<int>)tagAndValue.Value)
                            listBox.SetItemChecked(idx, true);

                        break;
                }
            // Switch to the word card tab.
            tabLanguage.SelectedIndex = 0;
        }


        private void btnCopyOriginal_Click(object sender, EventArgs e)
        {
            var command = new GenerateOriginalFieldCommand(
                txtOriginal.Text.Trim(),
                new List<string> { txtOriginal.SelectedText.Trim() }
            );
            Program.RunOneCommand(command);
        }


        private void CopyNoTagField(object sender, EventArgs eventArgs)
        {
            var command =
                new GenerateNoTagFieldCommand(
                    _noTagFields[sender].Text.Trim());
            Program.RunOneCommand(command);
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private delegate void DoSomethingWithObject(object c);
    }
}