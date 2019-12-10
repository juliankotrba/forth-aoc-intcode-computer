require input-util.fs

: main
    test 0
    
    begin
        read 99 <> while
            
            2dup read 1 = if
                ." acode is 1" 
            endif 

            2dup read 2 = if
                ." acode is 2" 
            endif 

        4 +
    repeat ;