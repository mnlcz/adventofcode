(use-modules (ice-9 format)
             (ice-9 rdelim)
             (srfi srfi-1))

(define sample
  "L68\nL30\nR48\nL5\nR60\nL55\nL1\nL99\nR14\nL82")

(define input
  (call-with-input-file "./2025/inputs/01.txt"
    read-string))

(define (rotation->mov rotation)
  (let* ((ch (string->list rotation))
         (dir (car ch))
         (step (string->number (list->string (cdr ch)))))
    (if (char=? dir #\L)
        (* -1 step) step)))

(define (step-dial pos mov count-wraps?)
  (let* ((step (abs mov))
         (dir (if (< mov 0)
                  'L
                  'R))
         (new-pos (modulo (+ pos mov) 100)))
    (if count-wraps?
        (let ((crossings (if (eq? dir
                                  'R)
                             (quotient (+ pos step) 100)
                             (if (= pos 0)
                                 (quotient step 100)
                                 (quotient (+ step
                                              (- 100 pos)) 100)))))
          (cons new-pos crossings))
        (let ((landed-zero? (if (= new-pos 0) 1 0)))
          (cons new-pos landed-zero?)))))

(define* (cycle rotations count-wraps?
                #:optional (log #f))
  (let loop
    ((rest rotations)
     (dial 50)
     (counter 0))
    (if (null? rest) counter
        (let* ((r (car rest))
               (mov (rotation->mov r))
               (res (step-dial dial mov count-wraps?))
               (next-dial (car res))
               (added-count (cdr res)))
          (when log
            (format #t
                    "[LOG] dial: ~a; rotation: ~s; next: ~a; added: +~a~%"
                    dial
                    r
                    next-dial
                    added-count))
          (loop (cdr rest) next-dial
                (+ counter added-count))))))

(define (lines in)
  (remove string-null?
          (string-split in #\newline)))

(define (part1 in)
  (cycle (lines in) #f))

(define (part2 in)
  (cycle (lines in) #t))

(define (main)
  (format #t "Part 1: ~a~%Part 2: ~a~%"
          (part1 input)
          (part2 input)))

(main)
