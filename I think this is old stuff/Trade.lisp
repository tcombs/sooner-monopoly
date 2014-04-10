(set-state-ok t)
(include-book "MainMonopoly")


(defun add-money (player money)
  (let* (
         (pnum (car player))
         (space (cadr player))
         (oldmoney (caddr player))
         (els (cdddr player))
         )
    (cons pnum (cons space (cons (+ oldmoney money) els)))
    )
  )
      ;This function's behavior for positive amounts of money
      ;is to add it to the current amount. To subtract money,
      ;use negative values of money

(defun trade-money (sender receiver money pstate)
  (if (null (car pstate))
    nil
    (if (equal sender (caar pstate))
      (cons (add-money (car pstate) (- 0 money)) (trade-money sender receiver money (cdr pstate)))
      (if (equal receiver (caar pstate))
        (cons (add-money (car pstate) money) (trade-money sender receiver money (cdr pstate)))
        (trade-money sender receiver money (cdr pstate))
        )
      )
    )
  )
;"sender" gives "receiver" "money" amount of dollars

(defun trade-props (player1 props1 player2 props2 prop-state)
  (if (null (car props1))
    (if (null (car props2))
      prop-state
      (trade-props player1 props1 player2 (cdr props2) (find-give-prop player1 (car props2) prop-state))
      )
    (trade-props player1 (cdr props1) player2 props2 (find-give-prop player2 (car props1) prop-state))
    )
  )
;"player1" receives "props2"
;"player2" receives "props1"

(defun find-give-prop (player pnum prop-state)
  (if (equal pnum (caar prop-state))
    (cons (give-property player (car prop-state)) (cdr prop-state))
    (find-give-prop (player pnum (cdr prop-state)))
    )
  )

(defun give-property (player property)
  (let* (
         (prop-num (car property))
         (els (cddr property))
         )
    (cons prop-num (cons player els))
    )
  )

;outputs a modified property state entry
;Essentially, this takes a property from one player and gives it to another player.
;

;1st function: Trade money
;2nd function: trade properties

;check if property acquisition changes property state (owning all three properties of a color)

(defun main(state)
  (let* (
       (game (get-game-state state))
       (game-state (car game))
       (next-move (car (get-next-move-from-game-state game-state)))
       (player-state (get-player-state-from-game-state game-state))
       (prop-state (get-prop-state-from-game-state game-state))
       (pnum1 (nth 0 next-move)) ;Player 1
       (props1 (nth 1 next-move)) ;The properties p1 is offering
       (money1 (nth 2 next-move)) ;The money p1 is offering
       (pnum2 (nth 3 next-move)) ;Player2
       (props2 (nth 4 next-move)) ;The properties p2 is offering
       (money2 (nth 5 next-move)) ;The money p2 is offering
       )
    (if (equal money1 money2)
      (write-game-state player-state (trade-props pnum1 props1 pnum2 props2 prop-state) state)
      (if (< money1 money2) 
        (write-game-state (trade-money pnum2 pnum1 (- money2 money1) player-state) (trade-props pnum1 props1 pnum2 props2 prop-state) state)
        (write-game state (trade-money pnum1 pnum2 (- money1 money2) player-state) (trade-props pnum1 props1 pnum2 props2 prop-state) state))
      )
    )
  )
;Find the difference between the money owed: 
;player1 offers money1 to player2
;player2 offers money2 to player1
;find abs(money1-money2), if money1>money2, give it to player2 while subtracting from player1
;if money1 < money2, give to player1, while subtracting from player2
;if money1=money2, don't even call the trade-money function
