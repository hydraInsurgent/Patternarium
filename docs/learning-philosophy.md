# Learning Philosophy

## The Core Belief

Struggle is not a sign of failure. It is the mechanism of learning. When a learner feels stuck, that is the moment right before a pattern clicks. The system must protect that moment, not shortcut past it.

The Patternarium is built on the belief that understanding compounds. Every problem you solve does not just add a solution to a folder - it feeds the ecosystem. Patterns gain new examples, mistake logs grow sharper, and connections between problems reveal themselves. The system gets more valuable the more you use it because *you* get more capable the more you use it.

## Patterns Over Solutions

A solution answers one problem. A pattern answers a family of problems.

The goal of every session is not to produce working code. It is to surface the underlying pattern that made the solution possible. Code is evidence of understanding. It is not understanding itself.

## Multi-Approach Learning

Every problem should be solved in at least two ways when possible. Each approach must teach a different idea, not just a faster version of the same idea.

**Two Sum example:**
- Brute force - teaches: exhaustive search, O(n squared) thinking
- HashMap - teaches: trade space for time, O(1) lookup, complement transformation
- Sorting + Two Pointers - teaches: use structure (ordering) instead of memory, O(1) space

Each approach builds a new mental model. The learner is not just solving faster. They are learning to think differently about the same problem.

## Guided Struggle

The AI never gives the solution first. It asks for the user's approach, validates what is correct, and identifies gaps through questions, not corrections. Hints escalate progressively - see `toolkit.md` for the specific levels.

The user should feel like they discovered the solution, not like it was handed to them.

## The Reflection Loop

Reflection converts short-term problem memory into long-term pattern memory. After every problem, the AI asks reflection questions - see `toolkit.md` for the specific questions.

## Earning the Optimization

Alternative approaches are never suggested upfront. They are introduced only when:
1. The user completes one solution - "Nice. There is another way if the array is sorted..."
2. The user hits a limitation - "Can we avoid the inner loop?"
3. The user's own approach opens a door - "Since you sorted it, can we use two pointers?"

This timing matters. The user needs to feel the limitation before they can appreciate the solution.

## The Mental Model Priority

> Thinking > Coding
> Patterns > Syntax
> Understanding > Speed

A user who understands why HashMap works will remember it in six months. A user who memorized the HashMap solution will not.

## The Living System

The Patternarium is not a notebook that collects dust. It is designed to stay alive:

- **Patterns grow** - every solved problem adds examples, sharpens triggers, and documents new traps
- **Mistakes graduate** - tracked lessons move from active to graduated when you prove mastery
- **Review tests recall** - the system quizzes you on patterns from memory, not recognition
- **Connections compound** - the more problems you solve, the more cross-problem links emerge

The repo itself is the artifact. Over time, it becomes a personal DSA knowledge base that reflects how you think - not how someone else explained it to you.

