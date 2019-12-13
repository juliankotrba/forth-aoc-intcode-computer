create intcode 160 cells allot \ TODO: Create array in function read-intcode

: read-intcode ( x1 x2 x3 xn n -- addr )
     dup 0 ?DO
        dup i - 1 - rot intcode rot cells + !
     LOOP intcode ;

: read ( addr i -- n)
     cells + @ ; 

: write ( addr n1 n2 -- )
     { a v index }
     v a index CELLS + ! ;

 
: print ( count addr i -- )
     swap rot dup 2swap rot 0 ?DO
          i 2dup read . drop
     LOOP swap s\" \n\n" type ;

: print-first ( count addr i -- )
     intcode 0 read . ;