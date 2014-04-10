(include-book "io-utilities" :dir :teachpacks)
(include-book "list-utilities" :dir :teachpacks)
(set-state-ok t)

(defun main (f-in f-out state)
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
