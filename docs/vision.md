# Patternarium - Architecture Vision

## What It Is Today

Patternarium is a prompt-driven prototype. Claude Code plays the role of the backend, markdown files are the database, JSON files are the indexes, and VS Code is the frontend. The system works - problems get solved, patterns get catalogued, knowledge compounds - but the architecture is tightly coupled to this specific stack.

Today's stack:

| Role | Current implementation |
|------|----------------------|
| Orchestration (business logic) | Claude Code + toolkit.md rules |
| Data store | Markdown files with YAML frontmatter + JSON indexes |
| Session state | active-problem.md + active-solution.cs |
| Derived views | AI-maintained sections (Seen In, Coverage, list tables) |
| Rendering | VS Code + Dataview Preview extension |
| User interface | IDE + terminal + structured prompts |

This works at small scale (12 problems, 11 patterns). It will not work at 100 problems, 30 patterns, or multiple users.

## Where It Is Going

The long-term vision: Patternarium's core is a **data architecture and workflow engine**, not an AI chat session. The current implementation is the prototype. If you replaced Claude Code with API endpoints, markdown with a database, and VS Code with a web frontend, you would have a product.

That statement is the design test for every decision we make: **could this layer be swapped without rewriting the others?**

### The architectural mapping

| Current (Prototype) | Future (Product) | What stays the same |
|---------------------|-----------------|-------------------|
| Claude Code + toolkit.md rules | API server with business logic | The workflow: Mode 1-9, hint escalation, reflection |
| master-index.json (promoted from pattern-index.json) | Database tables | The schema: problem -> patterns -> data structures |
| YAML frontmatter on .md files | Row data in database tables | The fields: difficulty, lists, status, ds-used, tags |
| /save-problem skill | POST /sessions/{id}/save endpoint | The transaction: validate, write sources of truth, respond |
| Dataview queries | Frontend components / dashboards | The query: "show all problems using this pattern" |
| active-problem.md session state | Stateful session API | The lifecycle: in-progress -> solved -> saved |
| Mode 1-9 workflow | State machine driving the UI | The pedagogy: guided struggle, hint escalation, reflection |
| LESSONS.md | User-specific learning log | The feedback loop: mistakes -> lessons -> graduation |

The learning philosophy, the pattern system, and the workflow design are the product. Everything else is infrastructure.

### AI is not being replaced - it is being structured

A critical distinction: the future product does not remove AI. The API server calls AI internally for generation - hints, pattern extraction, solution prettification, reflection prompts. To the user, it is an API call. To the backend, it is an AI call whose output gets validated, structured, and stored.

What changes is not whether AI is involved, but how its output flows:

| Today | Future |
|-------|--------|
| AI generates text and writes it directly to files | AI generates text, API validates and stores it in the database |
| AI maintains derived views (Seen In, Coverage) | Derived views are queries - AI never touches them |
| AI reads 15 files to understand context | API serves structured context from indexed data |
| Failure mid-save leaves partial state | Transaction wraps the whole save - rollback on failure |

The AI is still the engine. The architecture is the transmission that turns its output into reliable, structured, queryable data.

## Design Principles

These principles govern every architectural decision, now and in the future.

### 1. Single Source of Truth

Every fact lives in exactly one place. No derived copies maintained by the system.

- "Problem 121 uses the Linear Scan pattern" lives in `master-index.json`. Period.
- Pattern files, data structure files, list files, and goals do not store this fact. They query it.
- Intentional denormalization (caching) is allowed for AI performance - e.g., `title` and `difficulty` in master-index.json are copies from problem.md frontmatter. The rule: the source of truth always wins if they drift.
- If you need to know something, you read it from the source. If the source changes, every view updates automatically.

**Current violation:** Seen In sections, Coverage tables, list status rows, and goals percentages all duplicate facts from the indexes. The Dataview refactor eliminates this.

### 2. Schema Contracts

YAML frontmatter fields are not optional metadata. They are the schema. If a field is missing, the system is broken.

- Every file type has a defined set of required fields
- Required fields are validated, not hoped for
- The schema is documented and versioned
- Adding a new field means backfilling all existing files

In a product, these would be database column constraints. In the prototype, they are YAML field conventions enforced by the AI and eventually by validation scripts.

### 3. Atomicity

A write operation either succeeds completely or fails cleanly. No partial states.

- /save-problem is a transaction. If step 5 of 10 fails, the user should know what completed and what did not.
- Today this is aspirational (toolkit.md has a "Note - Atomicity" acknowledging the gap). The refactor reduces the write surface, making atomicity more achievable.
- In a product, this is a database transaction with rollback.

### 4. Separation of Concerns

Three layers, each independently replaceable:

**Data layer** - where facts are stored
- Indexes (master-index.json - promoted from pattern-index.json, absorbs construct cross-references)
- YAML frontmatter on content files
- These are the only things /save-problem writes to (besides the content files themselves)

**Content layer** - human-authored narrative, written once
- Problem statements, solution code, notes, pattern explanations
- Written at save time, never updated by later saves
- These are the "documents" in a document database

**View layer** - derived, read-only, rendered on demand
- Seen In sections, pattern Solved Problems, goals coverage %
- Never written by AI. Always computed from the data layer.
- In the prototype: Dataview queries. In a product: frontend components.

**Note:** Coverage tables and list rosters are Layer 2 (curated content), not Layer 3. They contain editorial data (planned techniques, unsolved problem rosters) that cannot be derived from queries. AI updates specific columns during /save-problem but the table structure is hand-maintained.

### 5. Cost Efficiency

The system should do the minimum work necessary to maintain consistency.

- AI writes only source-of-truth data. Derived views are free (computed at render time).
- AI reads frontmatter first to decide relevance, loads full files only when needed.
- Session context should not grow unboundedly. Compress, summarize, scope.
- Every file operation has a cost. Fewer writes = lower token usage = lower dollar cost.

## How the Dataview Refactor Fits

The Dataview refactor (see [docs/dataview/refactor-plan.md](dataview/refactor-plan.md)) is the first major step toward this vision. It does three things:

1. **Enforces single source of truth** - removes all derived copies, replaces with queries
2. **Establishes schema contracts** - defines and backfills YAML frontmatter across every file type
3. **Separates data from views** - AI writes data, Dataview renders views

After the refactor, the data layer is clean enough that a future migration to a real database would be a straightforward mapping exercise, not a redesign.

## What This Does NOT Cover

This vision is about architecture, not features. These are out of scope:

- **Multi-user support** - the current system is single-user. Multi-user would need auth, user-scoped data, and shared vs personal pattern libraries. Interesting but premature.
- **Web frontend** - the current UI is VS Code. A web frontend is possible but not planned.
- **Mobile app** - same as web. Architecture would support it; not building it now.
- **AI model portability** - the system currently depends on Claude. The workflow design (modes, hints, reflection) is model-agnostic, but the prompt engineering is Claude-specific. Abstracting this is future work.

## The Test

Before making an architectural decision, ask:

1. **Does this fact live in exactly one place?** If not, you are creating a maintenance burden.
2. **Could this layer be swapped?** If changing the rendering tool requires rewriting the data layer, the separation is wrong.
3. **Would this survive 10x scale?** 100 problems, 30 patterns, 5 lists. Does it still work?
4. **Is the AI doing work that a query could do?** If yes, move it to the view layer.

If the answer to any of these is wrong, fix the architecture before adding features.
