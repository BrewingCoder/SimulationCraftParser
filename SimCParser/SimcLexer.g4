lexer grammar SimcLexer;

ACTIONS  : 'actions' ;
ASSIGN  : '+=/';
VARIABLE  : 'variable,';
VARIABLEASSIGN : 'name=';
VARDEFAULT : 'default=';
VARVALUE  : 'value=';
VARVALELSE: 'value_else=';
VARCONDITION: 'condition=';
VAROP : 'op=';
ACTIF : 'if=';
ACTCALL : ('call_action_list' | 'run_action_list');
TARGETIF : 'target_if=';
MIN : 'min:';
MAX : 'max:';
EXTERN : 'invoke_external_buff';
TARGETCYCLE : 'cycle_targets=';
ACTINTERRUPT : 'interrupt=';
INTERRUPTIF : 'interrupt_if=';
USEITEM : 'use_item';
PRECOMBATTIME : 'precombat_time=';
USEOFFGCD : 'use_off_gcd=';
SLOT : 'slot=';

COMMENT : '#' .+? ('\n'|EOF) -> skip ;
NEWLINE                  : ('\r'? '\n' | '\r')+;

BITWISE_OR               : '|';
OPEN_PARENS              : '(';
CLOSE_PARENS             : ')';
COMMA                    : ',';
PLUS                     : '+';
MINUS                    : '-';
STAR                     : '*';
DIV                      : '/';
PERCENT                  : '%';
AMP                      : '&';
LT                       : '<';
GT                       : '>';
DOT                      : '.';
OP_EQ                    : '=';
OP_NOT                   : '!';
OP_LE                    : '<=';
OP_GE                    : '>=';
HASH                     : '#';

IDENTIFIER       : LETTER+ [A-Za-z_0-9]*;
DECIMAL_NUMERAL  : NUMERAL ('.' NUMERAL)?;
NUMERAL          : DIGIT+;

fragment LETTER : [A-Z_a-z];
fragment DIGIT  : [0-9];




