# /add-construct - Add a New Construct

Save a language feature or data structure to the construct library. Use this when you want to explicitly document something as a reusable construct - whether you just learned it, encountered it in a problem, or want to capture it outside of a session.

## Behavior

### Step 1 - Identify the construct

If the user invoked the command with a name or description (e.g., `/add-construct Array.Sort custom comparer`), use that as the starting point.

If invoked with no argument, ask: "What construct do you want to add? Give me its name and one sentence on what it does."

### Step 2 - Check for overlap

Search `constructs/` for any existing file that covers this construct fully or partially.

- If a full match exists: tell the user "This is already documented in `constructs/<category>/<name>.md`. Want to add something to it instead?"
- If a partial match exists (e.g., the construct is a variation of something already documented): ask "This overlaps with `<existing>`. Should I extend that file, or create a separate one?"
- If no match: proceed to Step 3.

### Step 3 - Assign category

Check the category taxonomy in `docs/pattern-system.md`. Pick the best-fit category.

- If the fit is clear: proceed silently.
- If two categories are plausible: ask "This could go under `collections` or `sorting` - which feels right to you?"
- If nothing fits: propose a new category name and ask the user to confirm before creating the folder.

### Step 4 - Gather content

Ask the user to share what they know - syntax snippets, use cases, gotchas, or examples. Do not invent content. Use only what the user provides or what was discussed in the current conversation.

If the user has already shown code or explained the construct in the conversation, use that directly and confirm: "I'll base the file on what you shared. Does this look right?" before writing.

### Step 5 - Create the file

Create `constructs/<category>/<kebab-case-name>.md` using the full construct template from `docs/pattern-system.md`, including YAML frontmatter.

Populate all fields:
- `name` - human-readable display name
- `category` - assigned in Step 3
- `tags` - 2-6 lowercase hyphen-separated tags covering the concept and use cases
- `language` - `csharp`
- `related` - filenames (no path, no extension) of meaningfully connected constructs

Leave `## Seen In` empty if this construct has not been used in a solved problem yet.

### Step 6 - Update cross-references

For each construct listed in `related`:
- Open the related file
- Add a `## See Also` entry pointing back to the new file (if the section does not exist, create it)

### Step 7 - Confirm

Show the user the file path and a one-line summary: "Created `constructs/sorting/array-sort-custom-comparer.md` - custom sort logic for Array.Sort via lambda, named method, or IComparer."

## Rules

- Never invent syntax examples or gotchas - only document what the user has shown or described
- Do not create a construct file for something already fully covered by an existing file
- If the construct was encountered during an active problem session, it will also be logged in `active-problem.md` by the normal session workflow - this command does not replace that, it complements it
- Suggest related constructs the user might want to link, but do not add links without confirmation if the relationship is not obvious
