parser grammar SimcParser;

options {
    tokenVocab = SimcLexer;
}



profile : 
    (
        (commentLine | useItemAction | externAction | callAction | variableAction | simpleAction| conditionalAction )
        NEWLINE 
        )*
    ;
    
commentLine:
    COMMENT;
    
callAction:
    actionpart
    (OP_EQ | ASSIGN)
    ACTCALL
    COMMA
    VARIABLEASSIGN
    variableName=dotted_name
   (COMMA ACTIF exp_if=exp)?
    ;
 
externAction:
    actionpart
    (OP_EQ | ASSIGN)
    EXTERN
    COMMA
    VARIABLEASSIGN
    variableName=dotted_name
    (COMMA ACTIF exp_if=exp)?
    ;
    
useItemAction:
    actionpart
    (OP_EQ | ASSIGN)
    USEITEM
    (COMMA use_off_gcd_condition)?
    (COMMA SLOT IDENTIFIER)?
    (COMMA VARIABLEASSIGN variableName=dotted_name)?
    (COMMA ACTIF exp_if=exp)?
    ;
    
    
variableAction :
    actionpart
    (OP_EQ | ASSIGN)
    VARIABLE 
    VARIABLEASSIGN 
    variableName=dotted_name
    (COMMA VARDEFAULT qualifier)?
    (COMMA VAROP variableOperator=IDENTIFIER)?
    (COMMA VARVALUE variableValue=exp)?
    (COMMA VARVALELSE variableElse=exp)?
    (COMMA VARCONDITION exp_condition=exp)?
    (COMMA ACTIF exp_if=exp)?
    ;
    
conditionalAction :
    actionpart
    (OP_EQ | ASSIGN)
    actionName=dotted_name
    (COMMA TARGETCYCLE exp)?
    (COMMA precombat_time)?
    (COMMA target_if)?
    (COMMA use_off_gcd_condition)?
    (COMMA if_condition)?
    (COMMA ACTINTERRUPT exp)?
    (COMMA INTERRUPTIF exp)?
    
;

simpleAction :
    actionpart
    (OP_EQ | ASSIGN)
    actionName=IDENTIFIER
;


actionpart : 
    ACTIONS (DOT subName=IDENTIFIER)?;

target_cycle
    : COMMA TARGETCYCLE OP_EQ DECIMAL_NUMERAL
    ;

target_if
    : (TARGETIF (MIN | MAX) exp) | (TARGETIF exp)
    ;
dotted_name 
    : IDENTIFIER (DOT IDENTIFIER)? 
    | dotted_name DOT IDENTIFIER
    | dotted_name DOT DECIMAL_NUMERAL
    ; 

eval 
    : BITWISE_OR 
    | LT 
    | GT 
    | OP_EQ 
    | OP_NOT 
    | OP_LE 
    | OP_GE 
    | PERCENT PERCENT
    | PERCENT
    ;

exp 
  : OPEN_PARENS exp CLOSE_PARENS
  | OP_NOT exp
  | exp BITWISE_OR exp
  | exp AMP exp
  | exp eval exp
  | exp (PLUS | MINUS | STAR | DIV) exp
  | exp OP_EQ exp
  | dotted_name
  | DECIMAL_NUMERAL
;

qualifier :
    DECIMAL_NUMERAL | dotted_name
;

precombat_time 
    : PRECOMBATTIME
    DECIMAL_NUMERAL
    ;
    
if_condition : ACTIF exp;
use_off_gcd_condition : USEOFFGCD exp;
  



