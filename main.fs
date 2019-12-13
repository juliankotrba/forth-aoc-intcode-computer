require input-util.fs

: read-parameters ( addr index -- addr index p1 p2 )
    2dup 2dup 2dup
    1 + read intcode swap read -rot     \ 1. parameter
    2 + read intcode swap read ;        \ 2. parameter

: write-result ( addr index value -- )
    -rot 
    3 + read intcode -rot write ;

: add ( count addr index -- )
    read-parameters
    + \ result
    write-result 4 + ;

: mul ( count addr index -- )
    read-parameters
    * \ result
    write-result 4 + ;

: run ( count addr -- )
    s\" \n\n" type
    0
    begin
        print
        2dup read 99 <> while
            2dup read 1 = if  
               add
            else
                2dup read 2 = if  
                    mul
                else
                    1 + \ invalid case, just read next value
                endif
            endif 
    repeat drop drop drop ;
