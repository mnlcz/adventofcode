(use-modules (ice-9 format)
             (ice-9 rdelim)
             (srfi srfi-1))

(define sample
  "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124")

(define input
  (string-trim-right (call-with-input-file "./2025/inputs/02.txt"
                       read-string)))

(define (ranges in)
  (string-split in #\,))

(define* (is-invalid? n
                      #:optional (log #f))
  (if (or (< n 10)
          (odd? (string-length (number->string n)))) #f

      (let* ((num-lst (string->list (number->string n)))
             (chunk-size (quotient (length num-lst) 2)))
        (when log
          (format #t "[LOG] n: ~s; num-lst: ~s; chunk-size: ~s~%" n num-lst
                  chunk-size))
        (if (= 1 chunk-size)
            (char=? (car num-lst)
                    (cadr num-lst))
            (list= char=?
                   (list-head num-lst chunk-size)
                   (list-tail num-lst chunk-size))))))

(define (invalid-ids start finish)
  (let loop
    ((ns start)
     (invalids '()))
    (if (> ns finish) invalids
        (if (is-invalid? ns)
            (loop (+ 1 ns)
                  (cons ns invalids))
            (loop (+ 1 ns) invalids)))))

(define (part1 in)
  (let loop
    ((rs (ranges in))
     (invalids '()))
    (if (null? rs)
        (apply +
               (concatenate invalids))
        (let* ((curr (string-split (car rs) #\-))
               (start (string->number (car curr)))
               (finish (string->number (cadr curr))))
          (loop (cdr rs)
                (cons (invalid-ids start finish) invalids))))))
