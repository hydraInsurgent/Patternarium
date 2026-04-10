# Dataview Query Templates

Tested query templates for each Layer 3 replacement. Each query reads from problem frontmatter YAML arrays using FLATTEN + WHERE.

**Dependency:** All queries require problem frontmatter to have the relevant field populated (`patterns`, `ds-used`, `constructs`, or `tags`). This was completed in Phase 4.

**Data source:** All queries use `FROM "problems"` which matches any file path containing "problems" - covers all `problems/*/problem.md` files.

**Default output:** The TABLE keyword automatically renders a file link as the first column. Specified columns appear after it.

---

## Template 1 - Pattern "Solved Problems"

**Replaces:** `## Solved Problems` section in `patterns/*.md`

**Reads:** `patterns` frontmatter array in problem files

**How to use:** Copy the block into the pattern file. Replace `"HashMap"` with the exact pattern name as it appears in problem frontmatter (case-sensitive).

````markdown
```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "HashMap"
SORT number asc
```
````

**What it renders:**

| File | Problem | # | Difficulty |
|------|---------|---|------------|
| (link) | Two Sum | 1 | Easy |
| (link) | Longest Substring... | 3 | Medium |
| (link) | Longest Consecutive Sequence | 128 | Medium |
| (link) | Contains Duplicate | 217 | Easy |
| (link) | Valid Anagram | 242 | Easy |

---

## Template 2 - DS "Seen In"

**Replaces:** `## Seen In` section in `data-structures/*.md`

**Reads:** `ds-used` frontmatter array in problem files

**How to use:** Copy the block into the DS file. Replace `"hashmap"` with the lowercase DS name as it appears in problem frontmatter. Valid values: `array`, `string`, `hashmap`, `hashset`, `stack`.

````markdown
```dataview
TABLE title AS "Problem", number AS "#", difficulty, patterns
FROM "problems"
FLATTEN ds-used AS ds
WHERE ds = "hashmap"
SORT number asc
```
````

**What it renders for `hashmap`:**

| File | Problem | # | Difficulty | Patterns |
|------|---------|---|------------|---------|
| (link) | Two Sum | 1 | Easy | [HashMap, Two Pointers] |
| (link) | Longest Substring... | 3 | Medium | [Sliding Window, HashMap, Presence Array] |
| (link) | Roman to Integer | 13 | Easy | [Linear Scan, Preprocessing, Chunked Iteration] |
| (link) | Valid Parentheses | 20 | Easy | [Reverse Order Matching] |
| (link) | Valid Anagram | 242 | Easy | [Preprocessing, HashMap] |

Note: Roman to Integer and Valid Parentheses appear here because they use `Dictionary` as a construct even though their pattern is not "HashMap". This is correct - the DS Seen In query shows all problems that touched this data structure, regardless of which pattern they used.

---

## Template 3 - Construct "Seen In"

**Replaces:** `## Seen In` section in `constructs/**/*.md`

**Reads:** `constructs` frontmatter array in problem files

**How to use:** Copy the block into the construct file. Replace `"dictionary"` with the lowercase construct name as it appears in problem frontmatter. Valid values: `dictionary`, `hashset`, `stack`, `span`, `array-sort`, `math-functions`, `char-methods`, `string-replace`, `linq`.

````markdown
```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN constructs AS construct
WHERE construct = "dictionary"
SORT number asc
```
````

**What it renders for `dictionary`:**

| File | Problem | # | Difficulty |
|------|---------|---|------------|
| (link) | Two Sum | 1 | Easy |
| (link) | Longest Substring... | 3 | Medium |
| (link) | Roman to Integer | 13 | Easy |
| (link) | Valid Parentheses | 20 | Easy |
| (link) | Valid Anagram | 242 | Easy |

---

## Template 4 - Concept "Seen In"

**Replaces:** `## Seen In` section in `concepts/*.md`

**Reads:** `tags` frontmatter array in problem files

**How to use:** Copy the block into the concept file. Replace `"palindrome"` with the tag that problems about this concept share. This requires that problems using the concept were tagged consistently. Current tag values relevant to existing concepts:

| Concept | Tag to query |
|---------|-------------|
| palindrome | `palindrome` |
| xor | `xor-cancellation` |

````markdown
```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN tags AS tag
WHERE tag = "palindrome"
SORT number asc
```
````

**What it renders for `palindrome`:**

| File | Problem | # | Difficulty |
|------|---------|---|------------|
| (link) | Longest Palindromic Substring | 5 | Medium |
| (link) | Valid Palindrome | 125 | Easy |

---

## Template 5 - Goals Coverage % (NOT IMPLEMENTED)

**Status:** Cannot be automated. The VS Code Dataview extension has no aggregation functions (COUNT, SUM, AVG). There is no way to compute a percentage from the `progress` field across multiple DS files.

**What stays:** AI continues to update the `progress` field in each DS file's frontmatter after each `/save-problem`. The `goals.md` coverage table stays as a manually maintained Layer 2 file.

**If this ever becomes a priority:** Fork `yahsan2.vscode-dataview-preview` and add `count()` support. The implementation is ~50 lines referencing Obsidian Dataview's `expression/functions.ts`.

---

## Field Reference

All fields that queries can use from problem frontmatter:

| Field | Type | Example values | Used in template |
|-------|------|----------------|-----------------|
| `title` | string | `"Two Sum"` | Display column |
| `number` | integer | `1`, `128` | Sort + display |
| `difficulty` | string | `"Easy"`, `"Medium"` | Display + filter |
| `status` | string | `"solved"` | Filter if needed |
| `lists` | array | `[blind-75, phased-75]` | Filter by list |
| `patterns` | array | `[HashMap, Two Pointers]` | Template 1 |
| `ds-used` | array | `[array, hashmap]` | Template 2 |
| `constructs` | array | `[dictionary, array-sort]` | Template 3 |
| `tags` | array | `[complement-lookup, target-sum]` | Template 4 |

### Filtering by list (bonus query)

Show all blind-75 solved problems:

````markdown
```dataview
TABLE title AS "Problem", number AS "#", difficulty, patterns
FROM "problems"
FLATTEN lists AS list
WHERE list = "blind-75"
SORT number asc
```
````

---

## Notes on Query Behavior

- **Case-sensitive:** `WHERE pattern = "HashMap"` does not match `"hashmap"`. Pattern names must match exactly as stored in frontmatter.
- **ds-used values are lowercase:** `"hashmap"`, `"array"`, `"string"`, `"hashset"`, `"stack"` - no capitals.
- **Construct values are lowercase kebab-case:** `"array-sort"`, `"math-functions"`, `"char-methods"`.
- **FLATTEN creates one row per array element.** A problem with `patterns: [HashMap, Two Pointers]` appears in both the HashMap query and the Two Pointers query. This is correct behavior.
- **File links in first column** are automatic with TABLE - they link to the actual `problem.md` file.
