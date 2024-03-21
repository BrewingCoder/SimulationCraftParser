using Antlr4.Runtime.Misc;
using SimCParser.antlr;
using SimCParser.structure;

namespace SimCParser;

public class BasicSimcVisitor : SimcParserBaseVisitor<object>
{

    public override object VisitProfile([NotNull] SimcParser.ProfileContext ctx)
    {
        var actions = new List<IAction>();

        for (var i=0; i<ctx.ChildCount; i++)
        {
            var child = ctx.GetChild(i);
            
            if (child is SimcParser.SimpleActionContext actionContext)
            {
                if (VisitSimpleAction(actionContext) is SimpleAction simpleAction)
                {
                    simpleAction.Sequence = actions.Count + 1;
                    actions.Add(simpleAction);
                }
            }


            if (child is SimcParser.ConditionalActionContext conditionalActionContext)
            {
                if (VisitConditionalAction(conditionalActionContext) is ConditionalAction conditionalAction)
                {
                    conditionalAction.Sequence = actions.Count + 1;
                    actions.Add(conditionalAction);
                }
            }
        }
        return actions;
    }

    public override object VisitSimpleAction([NotNull] SimcParser.SimpleActionContext ctx)
    {
        var simpleAction = new SimpleAction
        {
            ActionType = ACTIONTYPE.SimpleAction,
            ActionClause = ctx.actionpart().GetText(),
            ActionName = ctx.actionName.Text,
            SubName = ctx.actionpart().subName?.Text
        };
        return simpleAction;
    }

    public override object VisitConditionalAction([NotNull] SimcParser.ConditionalActionContext ctx)
    {
        var conditionalAction = new ConditionalAction
        {
            ActionType = ACTIONTYPE.ConditionalAction,
            ActionClause = ctx.actionpart().GetText(),
            ActionName = VisitDotted_name(ctx.actionName) as string,
            SubName = ctx.actionpart().subName?.Text
        };
        return conditionalAction;
    }

    public override object VisitDotted_name(SimcParser.Dotted_nameContext ctx)
    {
        return ctx.GetText();
    }
}