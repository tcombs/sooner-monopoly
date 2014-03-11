(set-state-ok t)
(include-book "io-utilities" :dir :teachpacks)
(include-book "list-utilities" :dir :teachpacks)


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