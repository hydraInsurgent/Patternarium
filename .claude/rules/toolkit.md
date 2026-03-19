# Toolkit Rules

<!-- Toolkit version: 3.0 | Managed by LLM Peer Review. Do not edit - changes will be overwritten on update. -->

## How We Work Together

### CRITICAL RULES

<rules>

1. **Never auto-fix** - Report issues first, wait for my approval before editing files
2. **Ask questions** - If something is unclear, ask before assuming
3. **Explain simply** - Use plain English, avoid jargon
4. **Show your work** - Tell me what you're doing and why
5. **Use the Skill tool for slash commands** - Never manually replicate /ideate, /initiate-project, /start-feature, /explore, /create-plan, /ui-spec, /review, /review-*, /fix, /ship, /document, or /unit-test. Always invoke them via the Skill tool so the template is followed.
6. **No em dashes or en dashes** - Never use em dashes or en dashes in any output (conversation, file writes, file edits). Use regular hyphens or rewrite the sentence.
7. **Teach the why** - When explaining, focus on *why* things work so the user can solve similar problems independently next time.

</rules>

### Our Workflow

<procedure>

#### Project setup (once, before first feature)
0. `/ideate` - (optional) Explore and validate the idea before committing to a project
0. `/initiate-project` - Create all foundation docs (product design, architecture, guidelines, CLAUDE.md)

#### Feature workflow
1. `/start-feature` - Classify the work, create tracking issue and branch
2. `/explore` - Understand the problem, ask clarifying questions, read project docs, analyze codebase
3. `/create-plan` - Create a step-by-step implementation plan
4. `/ui-spec` - (optional) Generate UI design spec before building
5. `/execute` - Build it, updating the plan as we go
6. `/unit-test` - Write and run unit tests for what was built
7. `/review` - Code review (report only, don't fix). For deeper review use `/review-code`, `/review-ux`, `/review-browser`, or `/review-full`
8. `/ask-gpt` or `/ask-gemini` - (optional) Get a second opinion via multi-model debate
9. `/peer-review` - (optional) Evaluate debate findings
10. `/document` - Update documentation
11. `/ship` - Close tracking issue, merge branch, tag version, create release

#### Bug fix workflow
1. `/create-issue` - Capture the bug
2. `/pair-debug` - (optional) Investigate root cause if not already known
3. `/fix` - Branch-aware fix: apply, verify, close issue, PATCH release

#### Parallel session workflow (optional)
0. `/worktree` - Create an isolated worktree for a parallel session before starting the feature workflow

</procedure>

---

## Slash Commands

<reference>

### Feature workflow

| Command | Purpose |
|---------|---------|
| `/start-feature` | Classify work, create GitHub tracking issue and branch |
| `/explore` | Understand the problem and codebase, define scope |
| `/create-plan` | Create a step-by-step implementation plan with status tracking |
| `/ui-spec` | Generate a UI design spec (colors, fonts, accessibility rules) for a plan |
| `/execute` | Build the feature, updating the plan as you go |
| `/unit-test` | Write and run unit tests for a feature, component, or the whole project |
| `/review` | Code review - report issues only, don't fix |
| `/document` | Update documentation after changes |
| `/ship` | Close tracking issue, merge branch, tag with semver, create GitHub release |

### Bug fix workflow

| Command | Purpose |
|---------|---------|
| `/create-issue` | Capture a bug or idea (ask questions first, keep short) |
| `/pair-debug` | Investigate a bug - confirm root cause before fixing |
| `/fix` | Apply a targeted fix: branch-aware, closes issue, PATCH tag |

### Specialized review commands

| Command | Purpose |
|---------|---------|
| `/review-code` | Deep review of code quality, security, and logic |
| `/review-commands` | Review slash command prompts for quality and consistency |
| `/review-plan` | Check if implementation matches the plan |
| `/review-ux` | Evaluate UX quality from code and markup |
| `/review-browser` | QA a running web app via headless browser - screenshots, interactions, diagnostics |
| `/review-full` | Pre-release cross-domain check with go/no-go recommendation |

### Project setup

| Command | Purpose |
|---------|---------|
| `/ideate` | Explore a raw idea: research, feasibility, decision (build/refine/shelve) |
| `/initiate-project` | Guided Q&A to create all foundation docs before starting work |

### Additional commands

| Command | Purpose |
|---------|---------|
| `/worktree` | Create an isolated parallel session in a new worktree |
| `/ask-gpt` | AI peer review with ChatGPT debate (3 rounds) |
| `/ask-gemini` | AI peer review with Gemini debate (3 rounds) |
| `/peer-review` | Evaluate feedback from other AI models |
| `/learning-opportunity` | Pause to learn a concept at 3 levels of depth |
| `/package-review` | Review a package or external codebase |

### Command-Specific Rules

**When Running /review or any /review-* command:**
- Output a written report using the format in the corresponding `.claude/commands/review*.md`
- Do NOT modify any files
- Wait for me to say "fix it" before making changes
- Use the "Use this when / Don't use this when" guidance at the top of each command to pick the right one

**When Running /create-issue:**
- Ask 2-3 clarifying questions first
- Keep issues short (10-15 lines max)
- No implementation details - that's for /explore and /create-plan

**When Running /fix:**
- Check branch context first - inline if on a feature branch, new branch if on main
- Verify the fix works before closing the issue
- Only change what is necessary - no refactoring alongside fixes

**When Running /unit-test:**
- Check for test infrastructure first - set it up if missing (ask before creating anything)
- Show the list of test targets and wait for confirmation before writing tests
- Do not move past Step 6 if tests are failing - diagnose first

</reference>

### Subagent Strategy

<guidelines>

- **Use subagents for research and exploration** freely - no need to ask
- **One focused task per subagent** - don't bundle unrelated work
- **Don't duplicate work** - if a subagent is researching something, don't also do it yourself
- **Parallelize independent plan steps** - tell the user what each parallel task will do and wait for approval before starting

</guidelines>

---

## Git Workflow

<guidelines>

### When to Branch
- New features that might break things
- Experimental changes you're not sure about
- When collaborating with others

### When to Work on Main
- Documentation updates
- Small fixes
- Cleanup work

### When to Commit
- After completing a logical unit of work
- Before switching to a different task
- When you want a checkpoint you can return to

### When to Push
- After commits you want to keep (backup)
- When you're done for the day
- Before asking for feedback

### Commit Messages
- Start with a verb: "Add", "Fix", "Update", "Remove", "Refactor"
- Keep the first line under 50 characters
- Describe what changed, not how

**Examples:**
- `Add git workflow guidance to CLAUDE.md`
- `Remove Next.js web app (out of scope for v1)`
- `Fix broken reference in ask-gpt command`

**Simple rule:** For solo learning projects, working on main is fine. Branch when you want to experiment safely.

### Worktree Workflow

When running multiple Claude Code sessions in parallel (via Cursor windows or Remote Control spawn mode), each session should use its own Git worktree. This prevents branch conflicts between sessions.

- **Setup:** Use `/worktree` to create an isolated session before starting the feature workflow
- **Branch naming:** When an issue is identified, rename the worktree branch to `worktree-<issue-number>-<short-label>` (e.g., `worktree-58-branch-conflicts`)
- **How it works:** `/explore` auto-renames the branch when an issue comes up. `/create-plan` does the same as a fallback if `/explore` was skipped.
- **Cleanup:** `/document` handles end-of-session cleanup - creates a PR, then offers to delete the worktree folder. The branch stays alive until the PR is merged.
- **Key concept:** A worktree is just a folder on disk. Deleting it does not delete the branch or PR. You can always re-create a worktree from the same branch if you need to make fixes.
- **End of session:** Worktree sessions end with `/document` (creates the PR), not `/ship`. The `/ship` command is for feature branches with an issue number in the branch name. Worktree branches use a different naming convention (`worktree-<N>-<label>`) and are merged via PR.

</guidelines>

---

## Permissions

<reference>

This project uses two settings files. `settings.json` is committed to the repo and provides a shared baseline (temp-file permissions for debate scripts). `settings.local.json` is user-specific and not overwritten on re-setup - your real permissions live here.

These are defined in `.claude/settings.local.json`. Each one exists for a reason:

| Permission | Why it's here |
|---|---|
| `git init`, `git add`, `git rm`, `git commit` | Initializing repos, staging files, committing work |
| `git push`, `git pull`, `git fetch` | Syncing with remote repositories |
| `git branch`, `git checkout`, `git stash` | Branch management and stashing work in progress |
| `git worktree` | Creating, listing, and removing worktrees for parallel sessions |
| `git rev-parse` | Worktree detection and repo path queries |
| `git status`, `git log`, `git diff`, `git show` | Inspecting repo state and history |
| `git tag` | Tagging releases in /fix and /ship |
| `git config`, `git remote add`, `git remote set-url` | Git setup (e.g. safe.directory, remote URLs) |
| `git check-ignore` | Verifying .gitignore rules before committing |
| `gh repo create`, `gh repo view`, `gh repo edit`, `gh repo clone` | Repository scaffolding, viewing, cloning, and settings |
| `gh auth` | GitHub authentication |
| `gh issue create`, `gh issue view`, `gh issue close`, `gh issue list`, `gh issue reopen` | `/create-issue` command and issue management |
| `gh label list`, `gh label create` | Managing GitHub labels |
| `gh pr create`, `gh pr view`, `gh pr diff` | Pull request workflows |
| `gh api`, `gh release list` | GitHub API calls and release checks |
| `npm install`, `npm uninstall` | Managing dependencies |
| `node scripts/ask-gpt.js` | Running the ask-gpt debate script |
| `node scripts/ask-gemini.js` | Running the ask-gemini debate script |
| `node scripts/browse.js` | Running the headless browser QA script |
| `Read`, `Edit`, `Write`, `Glob`, `Grep` | Claude's built-in file tools (included for documentation) |
| `WebFetch` (github.com, raw.githubusercontent.com), `WebSearch` | Fetching GitHub content and web search |
| `cp` | Copying files (e.g. `.env.local` into worktrees) |
| `ls`, `diff`, `echo`, `mkdir`, `cat` | Reading directories, comparing files, writing output, creating folders |
| `cd` | **Not included by default.** If your workflow needs it, add `"Bash(cd *)"` to your project's `.claude/settings.local.json`. Be aware: this allows directory changes anywhere on your machine, which broadens what subsequent commands can access. |

**Note:** `settings.local.json` also sets `defaultMode: acceptEdits`, which auto-approves file edits after you give a command. This is a top-level setting, not a permission entry.

</reference>

---

## Remember

<rules>

- I'm learning - explain what you do
- Report first, fix later
- Ask if unsure
- After non-trivial corrections (changed the plan, fixed a recurring mistake, or corrected a wrong assumption), update `LESSONS.md`

</rules>
