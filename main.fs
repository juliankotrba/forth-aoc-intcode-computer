require input-util.fs

: read-parameters
    2dup 2dup 
    1 + read intcode swap read -rot     \ 1. parameter
    2 + read  intcode swap read ;       \ 2. parameter

: write-result
    -rot 
    3 + read intcode -rot write ;

: add ( count addr index -- )
    read-parameters
    + \ result
    write-result ;

: mul ( count addr index -- )
    read-parameters
    * \ result
    write-result ;

: run ( count addr -- count addr index )
    s\" \n" type
    0
    begin
        print s\" \n" type
        2dup read 99 <> while
            
            2dup 2dup read 1 = if  
               add
            else 
                mul
            endif 

        4 +
    repeat ;
