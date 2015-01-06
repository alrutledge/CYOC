namespace Assets.Scripts.ChoiceEngine
{
    public enum ChoiceRequirementType
    {
        ATTRIBUTE_MAX_MENTAL,
        ATTRIBUTE_MAX_PHYSICAL,
        ATTRIBUTE_MAX_SOCIAL,
        ATTRIBUTE_CURRENT_MENTAL,
        ATTRIBUTE_CURRENT_PHYSICAL,
        ATTRIBUTE_CURRENT_SOCIAL,
        ATTRIBUTE_MYTHOS_KNOWLEDGE,
        HAVE_ITEM,
        NOT_HAVE_ITEM,
        HAVE_FLAG,
        NOT_HAVE_FLAG,
        FLAGS
    }

    public class ChoiceRequirement
    {
        public ChoiceRequirementType Type { get; set; }
        public string Requirement { get; set; }

        public ChoiceRequirement(){}

        public ChoiceRequirement(ChoiceRequirementType type, string value)
        {
            Type = type;
            Requirement = value;
        }
    }
}
