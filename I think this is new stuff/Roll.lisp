(include-book "io-utilities" :dir :teachpacks)
(include-book "list-utilities" :dir :teachpacks)
(set-state-ok t)

(defun mainx (f-in f-out state)
 (mv-let (input-string err-open state)
 (file->string f-in state)
 (if err-open
 (mv err-open state)
 (mv-let (err-close state)
 (string-list->file f-out
 (list input-string)
 state)
 (if err-close
 (mv err-close state)
 (mv (string-append "input file: "
 (string-append f-in
 (string-append ", output file: " f-out)))
 state))))))

(defun rac (xs)
  (if (consp (cdr xs)) ; more than one elem?
      (rac (cdr xs))   ; yes, more than one
      (car xs)))       ; no, one or fewer

(defun rdc (xs)
  (if (consp (cdr xs))               ; more than one elem?
      (cons (car xs) (rdc (cdr xs))) ; yes, more than one
      (cdr xs)))                     ; no, one or fewer

(defun get-next-number (cs  acc)
  (let* ((f (car cs))) ; the first character
    (if (or (equal f #\space) (equal f #\Newline) (equal f nil))
        (list (str->rat (chrs->str acc)) cs)
        (get-next-number (cdr cs) (append acc (list f))))))

(defun get-next-line (cs acc)
  (let* ((num-chars (get-next-number cs nil))
         (n (car num-chars))
         (new-cs (cadr num-chars))
         (f (car new-cs)))
    (if (or (equal f #\Newline) (equal new-cs nil))
        (list (append acc (list n)) (cdr new-cs))
        (get-next-line (cdr new-cs) (append acc (list n))))))

(defun chrs-to-list-of-lists (cs acc) 
  (let* ((ln-cs (get-next-line cs nil))
         (ln (car ln-cs)) ;the first line in list form
         (rest-cs (cadr ln-cs)) ;the rest of the chars to be scanned
         )
    (if (null rest-cs) ;no more chars
        (append acc (list ln))
        (chrs-to-list-of-lists rest-cs (append acc (list ln)))
        )))

(defun get-player-state (state)
  (mv-let (input-string error-open state)
          (file->string "player_state.txt" state)
     (if error-open
         (mv error-open state)
         (mv (chrs-to-list-of-lists
                 (str->chrs input-string) nil) 
             state))))

(defun get-prop-state (state)
  (mv-let (input-string error-open state)
          (file->string "prop_state.txt" state)
     (if error-open
         (mv error-open state)
         (mv (chrs-to-list-of-lists
                 (str->chrs input-string) nil) 
             state))))

(defun get-game-state (state)
  (mv-let (input-string error-open state)
          (file->string "next_move.txt" state)
     (if error-open
         (mv error-open state)
         (mv-let (player-state state) 
                 (get-player-state state)
            (mv-let (prop-state state)
                    (get-prop-state state)
           (mv (append (list (chrs-to-list-of-lists 
                 (str->chrs input-string) nil)) (list player-state) (list prop-state)) 
             state))))))

;(defun get-game-state (state)
;  (mv-let (input-string error-open state)
;          (file->string "next_move.txt" state)
;     (if error-open
;         (mv error-open state)
;         (let* ((player-state (car (get-player-state state)))
;                (prop-state (car (get-prop-state state))))
;           (mv (append (list (chrs-to-list-of-lists 
;                 (str->chrs input-string) nil)) (list player-state) (list prop-state)) 
;             state)))))

(defun get-next-move-from-game-state(game-state)
  (car game-state))
(defun get-player-state-from-game-state(game-state)
  (cadr game-state))
(defun get-prop-state-from-game-state(game-state)
  (caddr game-state))

(defun contains (cs c)
  (if (consp cs)
      (if (equal (car cs) c)
          t
          (contains (cdr cs) c))
      nil))
;defun remove dec from chars (returns a string)
(defun trunk-at-dec (cs acc)
  (if (not (contains cs #\.))
      (chrs->str cs)
              (if (equal (car cs) #\.)
                  (chrs->str acc)
                  (trunk-at-dec (cdr cs)(append acc (list (car cs)))))))

;takes a list of rational numbers and convert to a string
(defun list-of-rat-to-string (xs)
  (if (consp (cdr xs))
      (string-append (string-append (trunk-at-dec(str->chrs(rat->str(car xs) 10)) nil) " " )
              (list-of-rat-to-string (cdr xs)))
      (string-append (trunk-at-dec(str->chrs(rat->str(car xs) 10)) nil) (chrs->str (list #\Newline)))))

(defun list-of-lists-to-string (xss)
                         (if (consp (cdr xss))
                             (string-append (list-of-rat-to-string (car xss)) 
                                     (list-of-lists-to-string (cdr xss)))
                             (chrs->str(rdc(str->chrs(list-of-rat-to-string (car xss)))))
                             ))
(defun add-money (n player)
           (let* ((pnum (car player))
                  (space (car (cdr player)))
                  (money (car (cdr(cdr player))))
                  (els (cdr(cdr(cdr player))))
                  )
             (cons pnum (cons space (cons (+ money n) els)))))
(defun add-money-to-player (pnum n player-state)
                     (if (equal pnum 1)
                         (cons (add-money n (car player-state)) (cdr player-state))
                         (cons (car player-state) 
                               (add-money-to-player (- pnum 1) n 
                                                    (cdr player-state)))))


(defun out-help (prop-state state)
  (mv-let (error-close state)
                 (string-list->file "prop_state.txt"
                                    (list
                                     (list-of-lists-to-string
                                       prop-state))
                                    state)
            (if error-close
                (mv error-close state)
                (mv "complete" state))))

(defun write-game-state (player-state prop_state state)
  (mv-let (error-close state)
                 (string-list->file "player_state.txt"
                                    (list
                                     (list-of-lists-to-string
                                       player-state))
                                    state)
            (if error-close
                (mv error-close state)
                (out-help prop_state state))))

(defun read-property-info(state)
  (mv-let (input-string error-open state)
          (file->string "PROPERTIES.txt" state)
     (if error-open
         (mv error-open state)
         (mv (chrs-to-list-of-lists
                 (str->chrs input-string) nil) 
             state))))


(defun RollHelp (id space passgo player)
  (if (equal space 30)
      (cond
        ((equal id 1) 
         (list (cons id (cons 40 (nthcdr 2 (car player)))) 
               (cadr player) (caddr player) (cadddr player)))
        ((equal id 2) 
         (list (car player)
               (cons id (cons 40 (nthcdr 2 (cadr player))))
               (caddr player)(cadddr player)))
        ((equal id 3) (list (car player)(cadr player)
                            (cons id (cons 40 (nthcdr 2 (caddr player))))
                            (cadddr player)))
        ((equal id 4) (list (car player)(cadr player)(caddr player)
                            (cons id (cons 40 (nthcdr 2 (cadddr player))))))
        
        )
      (if (equal passgo T)
      (cond
        ((equal id 1) 
         (list (cons id (cons space (cons (+ 200 (caddr (car player)))(nthcdr 3 (car player)))))
               (cadr player) (caddr player) (cadddr player)))
        ((equal id 2) 
         (list (car player)
               (cons id (cons space (cons (+ 200 (caddr (cadr player)))(nthcdr 3 (cadr player)))))
               (caddr player)(cadddr player)))
        ((equal id 3) (list (car player)(cadr player)
                            (cons id (cons space (cons (+ 200 (caddr (caddr player)))(nthcdr 3 (caddr player)))))
                            (cadddr player)))
        ((equal id 4) (list (car player)(cadr player)(caddr player)
                            (cons id (cons space (cons (+ 200 (caddr (cadddr player)))(nthcdr 3 (cadddr player)))))))
        
        )
      (cond
        ((equal id 1) 
         (list (cons id (cons space (nthcdr 2 (car player)))) 
               (cadr player) (caddr player) (cadddr player)))
        ((equal id 2) 
         (list (car player)
               (cons id (cons space (nthcdr 2 (cadr player))))
               (caddr player)(cadddr player)))
        ((equal id 3) (list (car player)(cadr player)
                            (cons id (cons space (nthcdr 2 (caddr player))))
                            (cadddr player)))
        ((equal id 4) (list (car player)(cadr player)(caddr player)
                            (cons id (cons space (nthcdr 2 (cadddr player))))))
        )
      )
 )
)

(defun JailTurn(id D1 D2 player)

  (if (equal D1 D2)
       (cond
         ((equal id 1)
         (list (cons id (cons (+ 10(+ D1 D2))( cons(caddr (car player)) '(0 0))))
               (cadr player)(caddr player)(cadddr player)
          )          
          )
         ((equal id 2)
         (list (car player)
               (cons id (cons (+ 10(+ D1 D2))( cons (caddr (cadr player)) '(0 0))))
               (caddr player)(cadddr player)
          )          
          )
         ((equal id 3)
         (list (car player)(cadr player)
               (cons id (cons (+ 10(+ D1 D2))( cons (caddr (caddr player)) '(0 0))))
               (cadddr player)
          )          
          )
         ((equal id 4)
          (list (car player)(cadr player)(caddr player)
          (cons id (cons (+ 10(+ D1 D2))( cons (caddr (cadddr player)) '(0 0))))
          )          
          )
        )
        (cond
        ((equal id 1) 
         (if (equal 2 (cadddr (cdar player))) 
         (list (cons id (cons 10( cons (- (caddr (car player)) 50) '(0 0))))
               (cadr player)(caddr player)(cadddr player)
          )
             (list (append(take 4 (car player)) (cons (+ 1 (cadddr (cdar player))) nil)) 
               (cadr player) (caddr player) (cadddr player))))
        ((equal id 2) 
         (if (equal 2 (cadddr (cdadr player))) 
         (list (car player)
               (cons id (cons 10( cons (- (caddr (cadr player)) 50) '(0 0))))
               (caddr player)(cadddr player)
          )      
         (list (car player)
               (append(take 4 (cadr player)) (cons (+ 1 (cadddr (cdadr player))) nil))
               (caddr player)(cadddr player))))
        ((equal id 3)
         (if (equal 2 (cadddr (cdaddr player))) 
         (list (car player)(cadr player)
               (cons id (cons 10( cons (- (caddr (caddr player)) 50) '(0 0))))
               (cadddr player)
          )
         (list (car player)(cadr player)
                            (append(take 4 (caddr player)) (cons (+ 1 (cadddr (cdaddr player))) nil))
                            (cadddr player))))
        ((equal id 4) 
         (if (equal 2 (cadddr (cdr(cadddr player)))) 
          (list (car player)(cadr player)(caddr player)
          (cons id (cons 10( cons (- (caddr (cadddr player)) 50) '(0 0))))
          )
         (list (car player)(cadr player)(caddr player)
                            (append(take 4 (cadddr player)) (cons (+ 1 (cadddr (cdr(cadddr player)))) nil)))))
        )
 )
)

; output -> player state ((1 ...)  (2 ...) (3 ...) (4 ...))
(defun Roll (player-number die1 die2 player-state)
    (cond
    ((equal player-number 1)
     (if (equal( cadr(car player-state)) 40)
         (JailTurn 1 die1 die2 player-state)
          (if (<= (+ (+ die1 die2)( cadr(car player-state))) 39)
              (RollHelp 1 (+ 
                               (+ die1 die2)( cadr(car player-state))) nil player-state)
              (RollHelp 1 (-
                               (+ 
                                (+ die1 die2)( cadr(car player-state))) 40) T player-state)
              )))
    
    ((equal player-number 2)
     (if (equal( cadr(cadr player-state)) 40)
         (JailTurn 2 die1 die2 player-state)
          (if (<= (+ (+ die1 die2)( cadr(cadr player-state))) 39)
              (RollHelp 2 (+
                               (+ die1 die2)( cadr(cadr player-state))) nil player-state)
              (RollHelp 2 (-
                               (+
                                (+ die1 die2)( cadr(cadr player-state))) 40) T player-state)
              )))
    
    ((equal player-number 3)
     (if (equal( cadr(caddr player-state)) 40)
         (JailTurn 3 die1 die2 player-state)
          (if (<= (+ (+ die1 die2)( cadr(caddr player-state))) 39)
              (RollHelp 3 (+ 
                               (+ die1 die2)( cadr(caddr player-state))) nil player-state)
              (RollHelp 3 (-
                               (+ 
                                (+ die1 die2)( cadr(caddr player-state))) 40) T player-state)
              )))
    ((equal player-number 4)
     (if (equal( cadr(cadddr player-state)) 40)
         (JailTurn 4 die1 die2 player-state)
          (if (<= (+ (+ die1 die2)( cadr(cadddr player-state))) 39)
              (RollHelp 4 (+ 
                               (+ die1 die2)( cadr(cadddr player-state))) nil player-state)
              (RollHelp 4 (-
                               (+ 
                                (+ die1 die2)( cadr(cadddr player-state))) 40) T player-state)
              )))
    
    ( T NIL)))

(defun main(state)
     (mv-let (game-state state)
          (get-game-state state)
   
  (let* (
       (next-move (car (get-next-move-from-game-state game-state)))
       (player-state (get-player-state-from-game-state game-state))
       (prop-state (get-prop-state-from-game-state game-state))
       (pnum (car next-move))
       (roll1 (cadr next-move))
       (roll2 (caddr next-move))
       (new-player-state (Roll pnum roll1 roll2 player-state))
       )
   (write-game-state new-player-state prop-state state))))