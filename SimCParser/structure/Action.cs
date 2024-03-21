namespace SimCParser.structure
{
    public abstract class Action : IAction {
        public ACTIONTYPE ActionType {get; set;}
        public string? ActionClause {get; set;}
        public string? ActionName {get; set;}
        public string? SubName {get; set;}
        public int Sequence { get; set; }
    }

    
}