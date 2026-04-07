---
name: "Binary Search Tree"
status: stub
progress: 0
---

# Binary Search Tree (BST)

## What It Is
A binary tree with one ordering rule: for every node, all values in its left subtree are smaller and all values in its right subtree are larger. This rule enables O(log n) search.

## Core Operations

| Operation | Description | Average | Worst (unbalanced) |
|-----------|-------------|---------|-------------------|
| Search | Find a value | O(log n) | O(n) |
| Insert | Add a value | O(log n) | O(n) |
| Delete | Remove a value | O(log n) | O(n) |
| Min/Max | Leftmost/rightmost node | O(log n) | O(n) |
| Inorder traversal | Visit all in sorted order | O(n) | O(n) |

Worst case O(n) happens when the tree is completely unbalanced (like a linked list). Balanced BSTs (AVL, Red-Black) maintain O(log n) always.

## Space Complexity
O(n) - one node per element.

## Mental Model
Binary search applied to a tree. At every node, you ask: is my target smaller or larger? Smaller - go left. Larger - go right. Each step cuts the remaining search space in half (in a balanced tree).

## Key Property
Inorder traversal of a BST always produces elements in sorted order. This is the key insight used in many BST problems.

## When to Reach For It
- You need fast search, insert, and delete with ordering preserved
- You need the kth smallest/largest element
- You need to find values in a range efficiently
- The problem explicitly involves a BST

## When NOT to Use It
- You only need existence checks - use HashSet (O(1) vs O(log n))
- The tree is unbalanced and you cannot guarantee O(log n) - use a self-balancing variant
- Ordering does not matter - a HashMap is simpler

## vs Alternatives
- vs Binary Tree: BST has ordering; general binary tree does not
- vs HashMap: BST maintains sorted order; HashMap does not. HashMap is faster (O(1)) but unordered
- vs Sorted Array: BST supports O(log n) insert/delete; sorted array needs O(n) to insert

## C# Implementation
- Built-in ordered: `SortedSet<T>` (self-balancing BST underneath)
- Built-in ordered dictionary: `SortedDictionary<TKey, TValue>`
- Manual: define `TreeNode` same as Binary Tree, maintain BST property on insert

## Coverage

**Progress: 0 / 7 techniques explored (0%)**

| Technique | Status | Problems Solved |
|-----------|--------|-----------------|
| Search (leverage ordering) | not started | - (go left if smaller, right if larger) |
| Insert and Delete | not started | - (maintain BST property) |
| Inorder = Sorted Sequence | not started | - (kth smallest, validate BST, convert to sorted array) |
| Validate BST | not started | - (range checking: min/max bounds per node) |
| Kth Smallest / Largest | not started | - (inorder with counter) |
| Floor / Ceil / Predecessor / Successor | not started | - (find closest values) |
| Range Query | not started | - (sum or count of nodes within a value range) |

---

## Seen In
(not yet encountered in sessions)
