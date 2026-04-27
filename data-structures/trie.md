---
name: "Trie"
slug: trie
status: stub
progress: 0
---

# Trie (Prefix Tree)

## What It Is
A tree where each node represents a character, and paths from root to nodes represent prefixes. Used for fast prefix-based search on strings.

## Core Operations

| Operation | Description | Time |
|-----------|-------------|------|
| Insert word | Add all characters along path | O(m) where m = word length |
| Search word | Check if complete word exists | O(m) |
| Search prefix | Check if any word starts with prefix | O(m) |
| Delete word | Mark end node as not-word | O(m) |

All operations are O(m) where m is the length of the word - independent of how many words are stored.

## Space Complexity
O(total characters across all words) - each character gets a node. Can be large if many words are stored.

## Mental Model
A branching path where each step is one character. All words starting with "ca" share the path c -> a. Searching for "cat" means following c -> a -> t and checking if that node is marked as a complete word. Searching for prefix "ca" just means following c -> a and confirming the path exists.

## When to Reach For It
- "Does any word in the dictionary start with this prefix?"
- Autocomplete systems
- Word search problems where you check many prefixes
- Problems where you need shared prefix detection across multiple words

## When NOT to Use It
- Simple existence check for exact words - use a HashSet (simpler)
- Small number of words - HashMap is simpler and sufficient
- No prefix queries needed - Trie's advantage is prefix search

## vs Alternatives
- vs HashMap: HashMap can check if a full word exists (O(1)) but not prefixes efficiently
- vs Array of strings: Prefix search on an array is O(n * m); Trie is O(m) regardless of n

## C# Implementation
- No built-in Trie in C#
- Implement manually with a `TrieNode` class:
  ```
  class TrieNode {
      Dictionary<char, TrieNode> children;
      bool isEndOfWord;
  }
  ```

## Coverage

**Progress: 0 / 6 techniques explored (0%)**

| Technique | Status | Problems Solved |
|-----------|--------|-----------------|
| Word Insertion and Search | not started | - (implement Trie, search exact word) |
| Prefix Existence Check | not started | - (startsWith, autocomplete signal) |
| Count Words with Prefix | not started | - (how many words share a prefix) |
| Longest Common Prefix | not started | - (shared prefix across all words) |
| Word Search in Grid (Trie + DFS) | not started | - (find all dictionary words in board) |
| XOR Maximum Pair (Bit Trie) | not started | - (store binary digits, find max XOR) |

---

## Seen In

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty, patterns
FROM "problems"
FLATTEN ds-used AS ds
WHERE ds = "trie"
SORT problem asc
```
