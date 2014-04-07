(set-state-ok t)
(include-book "MainMonopoly")



(defun help(pl-num property)
  (let* (
         (prop-num (car property))
         (pl-num (cadr property))
         (up (caddr property))
         (rest-p (cdddr property))
         )
    (cons prop-num
          (cons pl-num
                (cons (+ up 1)
                      rest-p)))))

(defun up-prop (prop-num prop-state)
                     (if (equal prop-num (car(car prop-state)))
                         (cons (help prop-num (car prop-state)) (cdr prop-state))
                         (cons (car prop-state) 
                               (up-prop prop-num 
                                                    (cdr prop-state)))))

(defun main(state)
  (let* (
       (game (get-game-state state))
       (game-state (car game))
       (st (cadr game))
       (next-move (car (get-next-move-from-game-state game-state)))
       (player-state (get-player-state-from-game-state game-state))
       (prop-state (get-prop-state-from-game-state game-state))
       (prop-num (car next-move))
       )
   (write-game-state player-state (up-prop prop-num prop-state) st)))
