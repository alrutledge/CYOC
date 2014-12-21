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
        INVENTORY
    }

    public class ChoiceRequirement
    {
        public ChoiceRequirementType Type { get; set; }
        public int Requirement { get; set; }
    }
}
