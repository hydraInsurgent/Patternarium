# /start-quest - Start a Quest Session

Initialize a new quest batch session. Use this when starting a series of practice-rep problems from a quest track (e.g., LeetCode Quest "Arrays 2", "Strings 1"). Sets up `active-problem.md` with the Quest Batch header and signals the AI to use **lighter ceremony**: single approach per problem, no forced pattern extraction, optional reflection.

## When to Trigger

Invoke this skill (via the Skill tool) when the user says any of:
- "I'm starting a quest" / "starting a quest"
- "doing the next LeetCode quest"
- "starting a quest batch"
- "starting [TrackName]" where TrackName matches a quest track (e.g., "Arrays 2", "Strings 1", "Hashmaps 1")
- "I'm doing the next track"
- Explicit `/start-quest` invocation

**Do NOT trigger for:**
- "I'm doing leetcode" or "starting Blind 75" - that's regular Mode 1 (`/save-problem` flow)
- "Reviewing a problem" - that's `/review`
- A single problem from any list - quests are batches

If the signal is ambiguous, ask: "Is this a quest batch (lighter flow, /save-quest at the end) or a single problem (full ceremony, /save-problem at the end)?"

## Behavior

### Step 1 - Check for interrupted session

If `active-problem.md` or `active-solution.cs` is non-empty, ask:

"There is an unfinished session. (a) Save it with `/save-problem` or `/save-quest` depending on format, (b) Pause it with `/pause-problem`, (c) Discard and start fresh, (d) Resume where you left off"

Wait for user choice before proceeding. Do not overwrite active files without confirmation.

### Step 2 - Ask for track and list

Ask the user two questions:

1. "Which quest track? (e.g., 'Arrays 2', 'Strings 1')"
2. "Which list does this belong to? (default: `leetcode-quests`)"

Use `leetcode-quests` as the list slug if user does not specify. Use the track name as-is for the parenthetical (e.g., `Arrays 2`).

### Step 3 - Get current time

Run `date +"%Y-%m-%d %H:%M"` to get the start timestamp. Never approximate.

### Step 4 - Initialize active-problem.md

Write the Quest Batch header. Leave it open-ended below the separator - question blocks get appended as the user pastes them:

```markdown
## Quest Batch: <Track Name>
**Lists:** <list-slug> (<Track Name>)
**Time Started:** YYYY-MM-DD HH:MM
**Flow:** quest

---

```

### Step 5 - Initialize active-solution.cs

Clear `active-solution.cs` to empty. Question solution blocks get appended as the user pastes their code.

### Step 6 - Check workbench list

Read `workbench/lists/<list-slug>.md`. If the file does not exist or does not have a `## <Track Name>` section yet, ask:

"The workbench list does not have a `<Track Name>` section yet. Add a stub section now? (You can paste the problem list later, or it auto-fills as you paste each Q.)"

If user agrees:
- If file does not exist, create from the leetcode-quests template (see `workbench/lists/leetcode-quests.md` as reference)
- Append a new `## <Track Name>` section with progress header `**Progress: 0 / ? solved**` and the empty rows table

### Step 7 - Activate lighter flow and prompt user

Tell the user:

"Quest session active for **<Track Name>**. Lighter ceremony enabled:
- Paste each quest problem with your solution (or all at once). I will capture statement, approach, complexity, and surface techniques.
- I will not push for hints, alternative approaches, pattern extraction, or reflection unless you ask.
- When the batch is done, run `/save-quest`."

## During the Quest Session (Behavior Contract)

The AI behavior changes once a quest session is active. This section is the contract.

### Per-question flow (when user pastes a Q)

For each question the user pastes (with or without solution):

1. Identify the LC number if not given (verify with WebSearch if uncertain)
2. Append a `## Q<N>:` block to active-problem.md:

```markdown
## Q<N>: <Title>
**Number:** <LC#>
**Difficulty:** Easy | Medium | Hard
**Status:** solved | in-progress

### Statement
[as pasted]

### Approach: <Name>
**Time:** O(?)
**Space:** O(?)
**Key Idea:** [one sentence from user, or AI inference if user already solved it]

#### Constructs (optional)
- <slug> - one-line role

#### Concepts (optional)
- <slug>

#### Mistakes (optional - rare)
- [bug description if any]
```

3. If user pasted code, append a matching `// ==== Q<N>:` block to active-solution.cs:

```csharp
// ==== Q<N>: <Title> (LC <num>) ====
// Approach: <Name>
// Time:  O(?)
// Space: O(?)
// Key Idea: <one sentence>

[user's code]
```

4. Update the workbench list row for this LC number (status: solved if confirmed)

### What NOT to do during a quest session

- **Do not enter Modes 1-9 ceremony**. The lighter flow replaces them.
- **Do not push for thinking, hints, or alternative approaches** unless the user explicitly asks.
- **Do not run pattern extraction (Mode 8) by default**. If a pattern genuinely jumps out, mention it briefly. Otherwise skip it.
- **Do not run reflection (Mode 9) by default**.
- **Do not insist on multi-approach** - quests are reps, not deliberate study.

### When to break the lighter flow

If the user explicitly says "I am stuck" / "give me a hint" / "let's do an alternative approach" / "extract the pattern" / "reflect on this" - switch to the relevant Mode for that question only, then return to the lighter flow for the next Q.

### End-of-batch surfacing

When the user signals the batch is done (e.g., "that's all for today", "ready to save"), surface techniques in a `## Techniques Surfaced` block at the bottom of active-problem.md:

```markdown
## Techniques Surfaced
- **`<technique-slug>`** (Q<N>, Q<N+1>) - one-line description
- **`<other-slug>`** (Q<N+2>) - description
```

Confirm the technique names with the user before they run `/save-quest`. The save command relies on this block to know what to create/update.

## Rules

- A quest session is identified by the `## Quest Batch:` header in active-problem.md. The presence of this header is the signal that the AI should use lighter flow.
- Multiple tracks within the same quest series (Arrays 1, Arrays 2) each become their own quest batch session. Do not pile them into one active-problem.md.
- A single quest batch can hold any number of questions. Common is 3, but tracks may grow.
- Use `/save-quest` to persist a quest batch, never `/save-problem`. The active-problem.md format is incompatible with the latter.
- If the user mid-session decides this is actually deliberate practice (wants full Mode 1-9 ceremony), they can convert: ask the user to confirm, then restructure active-problem.md to single-problem format and treat as a normal session.
