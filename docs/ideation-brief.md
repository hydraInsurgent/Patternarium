# Ideation Brief

## The Idea
A personal quiz engine built into DSA Buddy that generates grounded, multiple-choice questions from your actual learning journey. Questions are pulled from your pattern library, your logged mistakes, and common failure modes around correct solutions. The question bank builds itself as you solve problems - zero manual card creation.

## The Problem
After solving a problem, pattern knowledge fades without reinforcement. Existing tools either require manual flashcard creation (Anki), teach generic patterns (Educative, AlgoMonster), or generate questions without understanding algorithm concepts (Quizgecko). Nothing converts your own mistake log into targeted quiz questions, and nothing integrates quiz reinforcement into a guided learning workflow.

## What Already Exists
- **Anki / grind** - spaced repetition for coding, but fully manual. No generation, no DSA awareness.
- **AlgoMaster / AlgoMonster / Educative** - teach patterns well but no quiz reinforcement layer. Generic curriculum, not personalized.
- **General quiz generators (Quizgecko, NoteGPT)** - turn notes into questions but do not understand algorithm concepts well enough to generate conceptually correct questions.

**Gap:** No tool converts your personal mistake log + pattern library into a growing, grounded question bank with zero manual effort.

## Our Angle
The question bank is yours. Built from your actual solving history - not a generic curriculum. Questions cover:
- Pattern identification from scenarios
- Trade-off reasoning between approaches
- Your logged mistakes (check before or after storing, etc.)
- Common failure modes around correct solutions - ways the right answer could go wrong

Understanding how a correct solution fails deepens understanding of why it works. That is the unique teaching mechanic.

## Question Types

| Type | Source |
|------|--------|
| Pattern identification | `patterns/*.md` - when to use |
| Trade-off reasoning | pattern tradeoffs + complexity |
| Mistake recognition | `problems/*/notes.md`, `LESSONS.md` |
| Order of operations | pattern pseudocode templates |
| Precondition check | pattern requirements |
| Complexity recall | complexity tables in notes |
| Failure mode | correct solution + common wrong paths |
| External MCQ (user-imported) | user pastes manually, stored in bank |

## Data Sources (Anti-Hallucination Rule)
All generated questions must be grounded in repo data only:
- `patterns/*.md`
- `problems/*/notes.md`
- `LESSONS.md`
- `pattern-index.json`

External MCQs are user-imported manually - their choice, their library. The system stores them in the same format but does not generate them.

## Feasibility
- **Complexity:** Low-Medium
- **Effort to v1:** Small
- **Key risks:**
  - Question quality degrades if prompt is not strictly grounded - mitigated by command discipline
  - Anki integration depends on user having AnkiConnect installed - optional stretch goal
  - Question bank grows stale if not updated as patterns evolve - acceptable
- **Stack:** JSON question bank (`quiz-bank.json`), `/quiz` command, Anki export as stretch goal. No UI for now - VS Code webview flagged as far future.

## v1 Must-Have
Generate grounded, personalized quiz questions from the user's own pattern library and mistake logs with minimal effort - no manual card creation.

## Future Considerations
- Anki integration via AnkiConnect API for spaced repetition
- VS Code webview / card UI (far future)
- Multi-user shared question bank (far far future - not in scope)

## Decision
Build. Proceed to `/initiate-project` when ready.
