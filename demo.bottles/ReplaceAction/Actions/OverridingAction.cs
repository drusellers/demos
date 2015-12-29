using NewFeature.Actions;

namespace ReplaceAction.Actions
{
    public class OverridingAction
    {
        public NewActionOutput Index(NewActionInput input)
        {
            return new NewActionOutput()
                       {
                           Text = "custom"
                       };
        }
    }
}