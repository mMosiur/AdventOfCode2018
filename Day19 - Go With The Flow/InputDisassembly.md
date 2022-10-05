# Input disassembly

## Declarations

`#ip 3` - Bind the instruction pointer to `r[3]`.

Later on `r[3]` will be annotated with `r<3>` instead to signalize that it is bound to the instruction pointer.

## Instructions

We can divide the instructions into 4 blocks:

### Jump to setup block

|  # |  Instruction  |  Pseudocode                   |
|---:|:--------------|:------------------------------|
|  0 | `addi 3 16 3` | `r<3> += 16`                  |

Translation of the block above:

- ( go to #17 ), so directly start a [setup block](#base-setup-block)

### Main loop block

|  # |  Instruction  |  Pseudocode                   |
|---:|:--------------|:------------------------------|
|  1 | `seti 1  0 4` | `r[4] = 1`                    |
|  2 | `seti 1  7 2` | `r[2] = 1`                    |
|  3 | `mulr 4  2 1` | `r[1] = r[4] * r[2]`          |
|  4 | `eqrr 1  5 1` | `r[1] = r[1] == r[5] ? 1 : 0` |
|  5 | `addr 1  3 3` | `r<3> += r[1]`                |
|  6 | `addi 3  1 3` | `r<3> += 1`                   |
|  7 | `addr 4  0 0` | `r[0] += r[4]`                |
|  8 | `addi 2  1 2` | `r[2] += 1`                   |
|  9 | `gtrr 2  5 1` | `r[1] = r[2] > r[5] ? 1 : 0`  |
| 10 | `addr 3  1 3` | `r<3> += r[1]`                |
| 11 | `seti 2  6 3` | `r<3> = 2`                    |
| 12 | `addi 4  1 4` | `r[4] += 1`                   |
| 13 | `gtrr 4  5 1` | `r[1] = r[4] > r[5] ? 1 : 0`  |
| 14 | `addr 1  3 3` | `r<3> += r[1]`                |
| 15 | `seti 1  3 3` | `r<3> = 1`                    |
| 16 | `mulr 3  3 3` | `r<3> *= r<3>`                |

Translation of the block above:

```
for r4 = 1..r5
    for r2 = 1..r5
      if r4 * r2 == r5:
        r0 += r4
```

At the end of the loop:

- `r[0]` = sum of all divisors of `r[5]`
- `r<3>` = 256, so the program halts

### Base setup block

|  # |  Instruction  |  Pseudocode                   |
|---:|:--------------|:------------------------------|
| 17 | `addi 5  2 5` | `r[5] += 2`                   |
| 18 | `mulr 5  5 5` | `r[5] *= r[5]`                |
| 19 | `mulr 3  5 5` | `r[5] *= r<3>`                |
| 20 | `muli 5 11 5` | `r[5] *= 11`                  |
| 21 | `addi 1  6 1` | `r[1] += 6`                   |
| 22 | `mulr 1  3 1` | `r[1] *= r<3>`                |
| 23 | `addi 1 13 1` | `r[1] += 13`                  |
| 24 | `addr 5  1 5` | `r[5] += r[1]`                |
| 25 | `addr 3  0 3` | `r<3> += r[0]`                |
| 26 | `seti 0  6 3` | `r<3> = 0`                    |

Translation of the block above:

- `r[1]` = 145 ( based on assumption that `r[1]` starts with 0 )
- `r[5]` = 981 ( based on assumption that `r[5]` and `r[1]` start with 0 )
- ( go to `r[0]`+1 ), effectively:
  - if `r[0]` == 0: ( go to #1 ) by stepping into line 26 and jump to [main loop block](#main-loop-block).
  - else if `r[0]` == 1: ( go to #27 ) by stepping over line 26 and jump to [extended setup block](#extended-setup-block)
  - else: undefined behavior and not supported.

Effectively: if we start with `r[0]` = 0 we jump into [the main loop](#main-loop-block) with `r[5]` = 981. If we start with `r[0]` = 1 we continue into the [extended setup](#extended-setup-block).

### Extended setup block

|  # |  Instruction  |  Pseudocode                   |
|---:|:--------------|:------------------------------|
| 27 | `setr 3  1 1` | `r[1] = r<3>`                 |
| 28 | `mulr 1  3 1` | `r[1] *= r<3>`                |
| 29 | `addr 3  1 1` | `r[1] += r<3>`                |
| 30 | `mulr 3  1 1` | `r[1] *= r<3>`                |
| 31 | `muli 1 14 1` | `r[1] *= 14`                  |
| 32 | `mulr 1  3 1` | `r[1] *= r<3>`                |
| 33 | `addr 5  1 5` | `r[5] += r[1]`                |
| 34 | `seti 0  0 0` | `r[0] = 0`                    |
| 35 | `seti 0  3 3` | `r<3> = 0`                    |

Translation of the block above:

- `r[1]` = 10550400
- `r[5]` += `r[1]`
- `r[0]` = 0
- `r<3>` = 0 ( go to #1 ), jump into [main loop block](#main-loop-block)

Effectively: We add `10550400` into `r[5]` and jump to [main loop block](main-loop-block).

## Conclusion

Considering all blocks and flow between them there are two supported flows that correspond with our part one and part two data:

If we start with `r[0]` = 0:

- We start with a jump into [base setup block](#base-setup-block) that leaves us with `r[5]` = 981.
- We jump straight into [main loop block](#main-loop-block) and calculate the sum of the divisors of `r[5]` = 981.
- When we exit the `r[0]` contains that sum the divisors of 981.

If we start with `r[1]` = 0:

- We start with a jump into [base setup block](#base-setup-block) that leaves us with `r[5]` = 981.
- We step into [extended setup block](#extended-setup-block) that leaves us with `r[5]` = 981 + 10550400 = 10551381.
- We jump into [main loop block](#main-loop-block) and calculate the sum of the divisors of `r[5]` = 10551381.
- When we exit the `r[0]` contains that sum the divisors of 10551381.
