namespace anki_gen_net
{
    public class Config
    {
        private string _entityTag;

        public string EntityTag
        {
            get => $@"<{_entityTag}></{_entityTag}>";
            set => _entityTag = value;
        }

        public string SentenceTypeTemplate { get; set; }

        // The template which the Front Field will be formatted with.
        public string SentenceTemplate { get; set; }
        public string DataTemplate { get; set; }
        public string MarkersTemplate { get; set; }
    }
}