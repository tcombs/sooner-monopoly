(set-state-ok t)
(include-book "MainMonopoly")



(defun help(pl-num property)
  (let* (
         (prop-num (car property))
         (rest-p (cddr property))
         )
    (cons prop-num
          (cons pl-num
                rest-p))))

(defun buy-prop (pl-num prop-num prop-state)
                     (if (equal prop-num (car(car prop-state)))
                         (cons (help pl-num (car prop-state)) (cdr prop-state))
                         (cons (car prop-state) 
                               (buy-prop pl-num prop-num 
                                                    (cdr prop-state)))))

(defun main(state)
  (let* (
       (game (get-game-state state))
       (game-state (car game))
       (st (cadr game))
       (next-move (car (get-next-move-from-game-state game-state)))
       (player-state (get-player-state-from-game-state game-state))
       (prop-state (get-prop-state-from-game-state game-state))
       (pl-num (car next-move))
       (prop-num (cadr next-move))
       )
   (write-game-state player-state (buy-prop pl-num prop-num prop-state) st)))
