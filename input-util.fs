create intcode 100 cells allot \ TODO: Create array in function read-intcode

: read-intcode ( x1 x2 x3 xn n -- addr )
       dup 0 ?DO
        dup  i - 1 - rot intcode rot cells + !
     LOOP drop intcode ;

: read ( addr i -- n)
     2dup cells + @ ; 

: test 
     1 2 99 1 99 5 read-intcode ;
