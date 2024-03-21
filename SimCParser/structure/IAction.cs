namespace SimCParser.structure
{
    public interface IAction {
        int Sequence {get; set;}
        ACTIONTYPE ActionType {get; set;}
        string? ActionClause {get; set;}
        string? ActionName {get; set;}
        string? SubName {get; set;}

    }
}