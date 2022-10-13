# Input disassembly

## Declarations

`#ip 5` - Bind the instruction pointer to `r[5]`.

Later on `r[5]` will be annotated with `r<5>` instead to signalize that it is bound to the instruction pointer.

## Instructions

|  # |  Instruction        |  Pseudocode                   | Notes                                                                    |
|---:|:--------------------|:------------------------------|:-------------------------------------------------------------------------|
|  0 | `seti 123 0 3`      | `r[3] = 123`                  |                                                                          |
|  1 | `bani 3 456 3`      | `r[3] &= 456`                 |                                                                          |
|  2 | `eqri 3 72 3`       | `r[3] = r[3] == 72 ? 1 : 0`   |                                                                          |
|  3 | `addr 3 5 5`        | `r<5> += r[3]`                | Jump to 5 if `123 & 456 == 72`                                           |
|  4 | `seti 0 0 5`        | `r<5> = 0`                    | Jump to 1, infinite loop if bitwise AND operation does not work properly |
|  5 | `seti 0 5 3`        | `r[3] = 0`                    |                                                                          |
|  6 | `bori 3 65536 2`    | `r[2] = r[3] | 65536`         |                                                                          |
|  7 | `seti 832312 1 3`   | `r[3] = 832312`               |                                                                          |
|  8 | `bani 2 255 1`      | `r[1] = r[2] & 255`           |                                                                          |
|  9 | `addr 3 1 3`        | `r[3] += r[1]`                |                                                                          |
| 10 | `bani 3 16777215 3` | `r[3] &= 16777215`            |                                                                          |
| 11 | `muli 3 65899 3`    | `r[3] *= 65899`               |                                                                          |
| 12 | `bani 3 16777215 3` | `r[3] &= 16777215`            |                                                                          |
| 13 | `gtir 256 2 1`      | `r[1] = 256 > r[2] ? 1 : 0`   |                                                                          |
| 14 | `addr 1 5 5`        | `r<5> += r[1]`                | Jump to 16 if `256 > r[2]`                                               |
| 15 | `addi 5 1 5`        | `r<5> += 1`                   | Jump to 17                                                               |
| 16 | `seti 27 7 5`       | `r<5> = 27`                   | Jump to 28                                                               |
| 17 | `seti 0 2 1`        | `r[1] = 0`                    |                                                                          |
| 18 | `addi 1 1 4`        | `r[4] = r[1] + 1`             |                                                                          |
| 19 | `muli 4 256 4`      | `r[4] *= 256`                 |                                                                          |
| 20 | `gtrr 4 2 4`        | `r[4] = r[4] > r[2] ? 1 : 0`  |                                                                          |
| 21 | `addr 4 5 5`        | `r<5> += r[4]`                | Jump to 23 if `r[4] > r[2]`                                              |
| 22 | `addi 5 1 5`        | `r<5> += 1`                   | Jump to 24                                                               |
| 23 | `seti 25 1 5`       | `r<5> = 25`                   | Jump to 26                                                               |
| 24 | `addi 1 1 1`        | `r[1] += 1`                   |                                                                          |
| 25 | `seti 17 0 5`       | `r<5> = 17`                   | Jump to 18                                                               |
| 26 | `setr 1 7 2`        | `r[2] = r[1]`                 |                                                                          |
| 27 | `seti 7 2 5`        | `r<5> = 7`                    | Jump to 8                                                                |
| 28 | `eqrr 3 0 1`        | `r[1] = r[3] == r[0] ? 1 : 0` |                                                                          |
| 29 | `addr 1 5 5`        | `r<5> += r[1]`                | **FINISH** if `r[3] == r[0]`                                             |
| 30 | `seti 5 5 5`        | `r<5> = 5`                    | Jump to 6                                                                |

## Conclusion

The only place where controllable register `0` appears is in instruction 28.
It is being checked for equality with register `3`.

### Part one

We need to find the value of register `0` hat causes the program to halt after executing the fewest instructions.
that is, the one that will halt after stumbling upon said instruction 28 and equaling register `3` right away.

So the in the disassembly we look for the index of said target instruction (28),
the number of the register it is being compared to (in my case `3`).
Later on we execute the program until instruction pointer reaches found value (28),
so the next instruction is going to be the check.
At that point the value of targeted register (my `3`) is the answer to the first part.
