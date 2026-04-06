# Workbench

The workbench is the navigation and planning layer for Patternarium. The library (patterns, constructs, concepts, solved problems) is the finished shelf. The workbench is how you orient yourself, plan what's next, and park what you're not ready for.

## What's Here

| File | Purpose |
|------|---------|
| [library.md](library.md) | Visual catalog of everything you know - patterns, concepts, constructs |
| [backlog.md](backlog.md) | Parked items and far-future topics |
| [goals.md](goals.md) | Current focus and targets |
| [lists/blind-75.md](lists/blind-75.md) | Blind 75 progress tracker |
| [sessions/](sessions/) | Paused problem sessions (in-progress, set aside) |

## Design Principle

The workbench points into the library. It never duplicates it.

- Solved a problem? The list file updates automatically via `/save-problem`
- Learned a new pattern? It appears in `library.md`
- Not ready for something? Park it in `backlog.md`

## How Lists Work

Each problem's `problem.md` has a `lists:` field in its YAML frontmatter. `/save-problem` reads that field and updates the relevant list file automatically. To add a new list, just reference it when pasting a problem - the file gets created on first use.
