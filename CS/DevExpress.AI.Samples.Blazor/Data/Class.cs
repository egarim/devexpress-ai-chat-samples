namespace DevExpress.AI.Samples.Blazor.Data
{
    public class MessageData
    {
        public string Message { get; set; }
        public List<Option> Options { get; set; }
        public string MessageTemplateName { get; set; }
        public List<Option> SelectedOptions { get; set; }

    }

    public class OptionSet
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Option> Options { get; set; }
    }
    public class Option
    {
        public string Image { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }

}
