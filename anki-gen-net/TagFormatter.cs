using System.Collections.Generic;
using System.Linq;

namespace anki_gen_net
{
    public class TagFormatter
    {
        private readonly Config _config;

        public TagFormatter(Config config)
        {
            _config = config;
        }

        public string TagValue(string value, string property)
        {
            // todo: if no any tags, return an unchanged string.
            return property.Replace("><", $@">{value}<");
        }

        public string TagValueByRef(string value, string property,
            string reference)
        {
            // todo: if no any refs, return an unchanged string.
            return property.Replace(reference, $@"{value}");
        }

        public string TagSentenceAttributes(List<string> attributes)
        {
            // todo: Check validity of the template.
            // There must be substrings
            // {sentence}
            // {attributes}.

            var output = "";

            if (attributes is { Count: > 0 })
                output = attributes.Aggregate(
                    output, (current, attribute) =>
                        current + TagValue(
                            attribute, _config.SentenceTypeTemplate));

            return output;
        }
    }
}