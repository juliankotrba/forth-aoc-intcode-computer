require input-util.fs

: run ( count addr -- todo )
    0
    begin
        2dup read 99 <> while
            
            2dup 2dup read 1 = if  
               2dup 2dup 
               
                1 + read intcode swap read -rot  \ 1. parameter
                2 + read  intcode swap read    \ 2. parameter
                + \ result
                -rot 
                3 + read intcode -rot write 
            else 
                2dup 2dup 
               
                1 + read intcode swap read -rot  \ 1. parameter
                2 + read  intcode swap read    \ 2. parameter
                * \ result
                -rot 
                3 + read intcode -rot write  
            endif 

        4 +
    repeat ;


test-input-1 run print
\ test-input-2 run print
\ test-input-3 run print
\ test-input-4 run print
\ test-input-long run print-first
