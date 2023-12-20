using System;
using System.Collections.Generic;

namespace anki_gen_net.Commands
{
    public class GenerateFrontFieldCommand : SentenceFieldCommand
    {
        public GenerateFrontFieldCommand(string sentence,
            List<string> selectedText, List<string> entityAttributes) :
            base(sentence, selectedText, entityAttributes, Program.config)
        {
        }
    }

    public class GenerateTranslationFieldCommand : SentenceFieldCommand
    {
        public GenerateTranslationFieldCommand(string sentence,
            List<string> selectedText) :
            base(sentence, selectedText, Program.config)
        {
        }
    }

    public class GenerateOriginalFieldCommand : SentenceFieldCommand
    {
        public GenerateOriginalFieldCommand(string sentence,
            List<string> selectedText) :
            base(sentence, selectedText, Program.config)
        {
        }
    }

    // Common class for fields: Front, Translation, Original.
    public abstract class SentenceFieldCommand : AbstractCommand
    {
        private readonly List<string> _entityAttributes;
        private readonly List<string> _selectedText;
        private string _sentence;
        private string _template;

        protected SentenceFieldCommand(
            string sentence,
            List<string> selectedText,
            List<string> entityAttributes,
            Config config) : base(config)
        {
            _selectedText = selectedText;
            _entityAttributes = entityAttributes;
            _sentence = sentence;
        }

        protected SentenceFieldCommand(
            string sentence,
            List<string> selectedText,
            Config config) : base(config)
        {
            _selectedText = selectedText;
            _sentence = sentence;
        }


        public override void Execute(ref List<string> fields)
        {
            // todo: refactoring: move to the 'AbstractCommand' class.
            var tagFormatter = new TagFormatter(BaseConfig);

            // Mark the main entity with tags in the sentence.
            foreach (var text in _selectedText)
            {
                var startIndex = _sentence.IndexOf(
                    text,
                    StringComparison.Ordinal);

                _sentence = _sentence.Remove(startIndex, text.Length);

                var taggedEntity = tagFormatter.TagValue(
                    text,
                    BaseConfig.EntityTag);

                _sentence = _sentence.Insert(startIndex, taggedEntity);
            }

            // Insert the sentence into the template.
            _template = tagFormatter.TagValueByRef(
                _sentence,
                BaseConfig.SentenceTemplate,
                "{sentence}");

            // Process the sentence attribute list.
            var taggedAttributes =
                tagFormatter.TagSentenceAttributes(_entityAttributes);
            _template = _template.Replace("{attributes}", taggedAttributes);

            fields.Add(_template);
        }
    }
}