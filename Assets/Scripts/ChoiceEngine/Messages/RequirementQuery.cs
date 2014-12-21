namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class RequirementQuery
    {
        public ChoiceRequirement Requirement { get; set; }

        public RequirementQuery(ChoiceRequirement requirement)
        {
            Requirement = requirement;
        }
    }
}
