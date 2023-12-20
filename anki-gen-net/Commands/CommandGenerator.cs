using System.Collections.Generic;
using System.Windows.Forms;

namespace anki_gen_net.Commands
{
    public class CommandGenerator
    {
        public GenerateFrontFieldCommand GetFrontFieldCommand(
            string literalText,
            string literalSelectedText, string entityType)
        {
            return new GenerateFrontFieldCommand(
                literalText.Trim(),
                new List<string> { literalSelectedText.Trim() },
                new List<string> { entityType.Trim() });
        }

        public GenerateTranslationFieldCommand GetTranslationFieldCommand(
            string translationText, string translationSelectedText
        )
        {
            return new GenerateTranslationFieldCommand(
                translationText.Trim(),
                new List<string> { translationSelectedText.Trim() });
        }

        public GenerateOriginalFieldCommand GetOriginalFieldCommand(
            string originalText, string originalSelectedText)
        {
            return new GenerateOriginalFieldCommand(
                originalText.Trim(),
                new List<string> { originalSelectedText.Trim() }
            );
        }

        public GenerateDataFieldCommand GetDataFieldCommand(
            string speechPartText, string transcriptionText, string meaningText)
        {
            return new GenerateDataFieldCommand(
                speechPartText.Trim(),
                transcriptionText.Trim(),
                meaningText.Trim().ToUpper()
            );
        }

        public GenerateMarkersFieldCommand GetMarkersFieldCommand(
            CheckedListBox.CheckedItemCollection checkedItems)
        {
            return new GenerateMarkersFieldCommand(checkedItems);
        }
    }
}