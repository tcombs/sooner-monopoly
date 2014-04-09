(set-state-ok t)
(include-book "MainMonopoly")

;Now these functions only add and remove from the end of lists
;    (list name 0 1500 0 0)

(defun remove-player (pstate)
  (if (null (cadr pstate))
    nil
    (cons (car pstate) (remove-player (cdr pstate)))))

(defun add-player (pstate pnum)
  (if (null pstate)
    (list pnum 0 1500 0 0)
    (cons (car pstate) (add-player (cdr pstate) (+ pnum 1))))) ;This should work.

    ;Is there automatically one player? or am I adding from 0?

(defun main(state)
  (let* (
        (game (get-game-state state))
        (game-state (car game))
        (player-state (get-player-state-from-game-state))
        (next-move (car (get-next-move-from-game-state game-state)))
        (prop-state (get-prop-state-from-game-state game-state))
        (pflag (car next-move))
        )
    (if (= pflag -1)
      (write-game-state (remove-player pstate) prop-state state)
      (write-game-state (add-player pstate 1) prop-state state))))  ;The pflag won't ever be anything except -1 or 1, so I only need to check one of them
