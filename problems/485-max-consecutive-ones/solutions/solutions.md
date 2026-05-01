---
problem: 485
problem-title: Max Consecutive Ones
difficulty: Easy
category: solutions
flow: quest
patterns: []
constructs: [math-functions]
ds-used: [array]
algorithms: []
techniques: [running-max-with-reset]
concepts: []
approaches:
  - name: Running Count with Reset on Break
    file: running-count-with-reset-on-break.cs
    patterns: []
    constructs: [math-functions]
    ds-used: [array]
    techniques: [running-max-with-reset]
    time: "O(n)"
    space: "O(1)"
---

# Max Consecutive Ones - Solutions

## Approaches

### Approach 1: Running Count with Reset on Break
**Code:** [running-count-with-reset-on-break.cs](running-count-with-reset-on-break.cs)
**Time:** O(n) | **Space:** O(1)

**Key Idea:** Walk the array tracking a current streak count. On 0, settle the running max and reset the count. After the loop, settle once more to capture a streak that runs to the end.

**Thinking:** A consecutive-1s problem is a streak problem. The current count is only meaningful within an unbroken run of 1s. When 0 is hit, the streak ends - that's when its value should compete for the maximum, and the counter resets. The post-loop `Math.Max` is the catch for arrays that end mid-streak (e.g., `[1,1,1]` never hits a 0).

---

## Techniques

- Running Max with Reset (Approach 1) - settle on break, reset counter, settle once more after the loop
