using System.Collections.Generic;

namespace anki_gen_net.Commands
{
    public class GenerateDataFieldCommand : AbstractCommand
    {
        private readonly string _meaning;
        private readonly string _speechPart;
        private readonly string _transcription;
        private string _template;

        public GenerateDataFieldCommand(
            string speechPart,
            string transcription,
            string meaning) : base(Program.config)
        {
            _speechPart = speechPart;
            _transcription = transcription;
            _meaning = meaning;
        }

        public override void Execute(ref List<string> fields)
        {
            // todo: refactoring: move to the 'AbstractCommand' class.
            var tagFormatter = new TagFormatter(BaseConfig);

            _template = BaseConfig.DataTemplate;

            _template = tagFormatter.TagValueByRef(
                _speechPart,
                _template,
                "{speech-part}");
            _template = tagFormatter.TagValueByRef(
                _transcription,
                _template,
                "{transcription}");
            _template = tagFormatter.TagValueByRef(
                _meaning,
                _template,
                "{meaning}");

            fields.Add(_template);
        }
    }
}