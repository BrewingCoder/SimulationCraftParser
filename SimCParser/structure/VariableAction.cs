namespace SimCParser.structure
{
    public class VariableAction : Action
    {
        public string? VariableName { get; set; }
        public string? Qualifier { get; set; }
        public string? VariableOperator { get; set; }
        public string? VariableValue { get; set; }
        public string? VariableCondition { get; set; }
        public string? ActIf { get; set; }
        
        public VariableAction()
        {
            ActionType = ACTIONTYPE.VariableAction;
        }

    }
}
