(set-state-ok t)
(include-book "io-utilities" :dir :teachpacks)
(include-book "list-utilities" :dir :teachpacks)

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
    (if (or (equal f #\space) (equal f #\newline) (equal f nil))
        (list (str->rat (chrs->str acc)) cs)
        (get-next-number (cdr cs) (append acc (list f))))))

(defun get-next-line (cs acc)
  (let* ((num-chars (get-next-number cs nil))
         (n (car num-chars))
         (new-cs (cadr num-chars))
         (f (car new-cs)))
    (if (or(equal f #\newline) (equal new-cs nil))
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
         (let* ((player-state (car (get-player-state state)))
                (prop-state (car (get-prop-state state))))
           (mv (append (list (chrs-to-list-of-lists 
                 (str->chrs input-string) nil)) (list player-state) (list prop-state)) 
             state)))))

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
      (string-append (trunk-at-dec(str->chrs(rat->str(car xs) 10)) nil) (chrs->str (list #\newline)))))

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


(defun out-help (prop_state state)
  (mv-let (error-close state)
                 (string-list->file "prop_state1.txt"
                                    (list
                                     (list-of-lists-to-string
                                       prop_state))
                                    state)
            (if error-close
                (mv error-close state)
                (mv (string-append ", output file: " "prop_state1.txt")
                    state))))

(defun write-game-state (player-state prop_state state)
  (mv-let (error-close state)
                 (string-list->file "player_state1.txt"
                                    (list
                                     (list-of-lists-to-string
                                       player-state))
                                    state)
            (if error-close
                (mv error-close state)
                (out-help prop_state state))))

;test code
(let* (
       (game (get-game-state state))
       (pl-state (get-player-state-from-game-state (car game)))
       (prop-state (get-prop-state-from-game-state (car game)))
       )
   (write-game-state (add-money-to-player 1 1000 pl-state) prop-state state)
)

