namespace NewFeature.Actions
{
    public class NewStuffAction
    {
        public NewActionOutput Index(NewActionInput input)
        {
            return new NewActionOutput()
                       {
                           Text = "standard"
                       };
        }
    }

    public class NewActionInput
    {
    }

    public class NewActionOutput
    {
        public string Text { get; set; }
    }
}