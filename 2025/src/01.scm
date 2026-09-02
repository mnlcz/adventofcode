(use-modules (ice-9 format)
             (ice-9 rdelim)
             (srfi srfi-1))

(define sample
  "L68\nL30\nR48\nL5\nR60\nL55\nL1\nL99\nR14\nL82")

(define input
  (call-with-input-file "./2025/inputs/01.txt"
    read-string))

(define dial
  50)
(define range-len
  100)

(define (lines in)
  (remove string-null? (string-split in #\newline)))

(define (wrap-and-count delta)
  (cons (floor (/ delta range-len))
        (modulo delta range-len)))

(define (rotation->mov rotation)
  (let* ((ch (string->list rotation))
         (dir (car ch))
         (step (string->number (list->string (cdr ch)))))
    (if (char=? dir #\L)
        (* -1 step) step)))

(define* (cycle rotations count-wraps?
                #:optional (log #f))
  (let* ((counter 0))
    (for-each (lambda (r)
                (let* ((mov (rotation->mov r))
                       (delta (+ dial mov))
                       (wrap (wrap-and-count delta))
                       (wrap-value (cdr wrap))
                       (wrap-times (abs (car wrap))))
                  (when log
                    (format #t
                     "[DEBUG] dial: ~s; rotation: ~s; wrap-val: ~s; wrap-times: ~s~%"
                     dial
                     r
                     wrap-value
                     wrap-times))
                  (set! dial wrap-value)
                  (when (eq? wrap-value 0)
                    (set! counter
                          (+ 1 counter)))
                  #;(when count-wraps?
                    (display "Todo")))) rotations) counter))

(define (part1 use-sample?)
  (let ((rotations (if use-sample?
                       (lines sample)
                       (lines input))))
    (cycle rotations #f)))
