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
(defun main(state)
   (mv-let (game-state state)
           (get-game-state state)
  (let* (
       (next-move (car (get-next-move-from-game-state game-state)))
       (player-state (get-player-state-from-game-state game-state))
       (prop-state (get-prop-state-from-game-state game-state))
       (pnum (car next-move))
       (money (cadr next-move))
       )
   (write-game-state (add-money-to-player pnum money player-state) prop-state state))))