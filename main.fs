require input-util.fs

: read-parameter-position ( index -- p )
    + read intcode swap read -rot drop drop ;
    
: read-parameter-immediate ( index -- p )
    + read -rot drop drop ;

: read-parameters ( addr index opcode -- p1 p2 )
    100 / 
    3dup 3dup
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
    3dup read-parameters
    + \ result
    swap drop \ TODO: Read C
    write-result 
    drop 4 + ;

: mul ( count addr index opcode -- )
    3dup read-parameters
    * \ result
    swap drop \ TODO: Read C
    write-result
    drop 4 + ;

: non-zero ( count addr index opcode -- )
    read-parameters  
    0 <> if
        -rot drop drop
    else
       drop drop 3 +
    endif ;

: is-zero ( count addr index opcode -- )
    read-parameters
    0 = if
        -rot drop drop
    else 
       drop drop 3 +
    endif ;

: less 
    3dup read-parameters swap
    < if
        drop
        3 + read
        intcode swap
        1 swap write
    else 
        drop
        3 + read
        intcode swap
        0 swap write
    endif    
    drop 4 + ;

: equals 
    3dup read-parameters swap
    = if
        drop
        3 + read
        intcode swap
        1 swap write
    else 
        drop
        3 + read
        intcode swap
        0 swap write
    endif    
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
        5 of non-zero endof
        6 of is-zero endof
        7 of less endof
        8 of equals endof
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