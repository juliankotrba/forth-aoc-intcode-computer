require input-util.fs

: read-parameter-position ( index -- p )
    + read intcode swap read -rot drop drop ;
    
: read-parameter-immediate ( index -- p )
    + read -rot drop drop ;

: read-parameters ( addr index opcode -- addr index opcode p1 p2 )
    100 / 
    3dup 3dup 3dup
    10 mod
    case
        0 of 2dup 1 read-parameter-position endof
        1 of 2dup 1 read-parameter-immediate endof
    endcase
    >r
    10 / 10 mod
    case
        0 of 2dup 2 read-parameter-position endof
        1 of 2dup 2 read-parameter-immediate endof
    endcase
    
    r> ;

: write-result ( addr index value -- )
    -rot
    3 + read intcode
    -rot write ;

: add ( count addr index opcode -- )
    read-parameters
    + \ result
    swap drop \ TODO: Read C
    write-result 
    drop 4 + ;

: mul ( count addr index opcode -- )
    read-parameters
    * \ result
    swap drop \ TODO: Read C
    write-result
    drop 4 + ;

: input ( count addr index opcode -- )
    drop 
    2dup
    1 + read intcode swap
    s\" Input: " type
    std-input
    s\" \n" type
    swap write
    2 + ;

: output ( count addr index opcode -- )
    100 / 
    3dup
    10 mod
    case
        0 of 2dup 1 read-parameter-position . endof
        1 of 2dup 1 read-parameter-immediate . endof
    endcase
    s\" \n" type
    drop 2 + ;

: read-opcode
    2dup read dup 100 mod
    case
        1 of add endof
        2 of mul endof
        3 of input endof
        4 of output endof
    endcase ;

: run ( count addr -- )
    s\" \n\n" type
    0
    begin
        \ print
        2dup read 99 <> while
            read-opcode

    repeat drop drop drop ;

: run-print ( count addr -- )
    s\" \n\n" type
    0
    begin
        print
        2dup read 99 <> while
            read-opcode

    repeat drop drop drop ;