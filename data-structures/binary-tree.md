---
name: "Binary Tree"
slug: binary-tree
status: stub
progress: 0
---

# Binary Tree

## What It Is
A hierarchical data structure where each node has at most two children - a left child and a right child. There is one root node at the top.

## Core Operations

| Operation | Description | Time |
|-----------|-------------|------|
| Traversal (any order) | Visit all nodes | O(n) |
| Search (general tree) | Find a value | O(n) |
| Insert | Add a node | O(n) - must find the position |
| Height | Longest path from root to leaf | O(n) |

Note: these are for a general binary tree with no ordering. A Binary Search Tree (BST) has faster search - see [binary-search-tree.md](binary-search-tree.md).

## Space Complexity
O(n) - one node per element. Call stack during recursion adds O(h) where h is tree height.

## Mental Model
A family tree. The root is the ancestor at the top. Each person (node) has at most two children. Nodes with no children are called leaves. The height is the number of generations from root to the deepest leaf.

## Key Vocabulary
- **Root**: the top node (no parent)
- **Leaf**: a node with no children
- **Height**: number of edges on the longest path from root to a leaf
- **Depth**: number of edges from root to a specific node
- **Subtree**: a node and all its descendants

## Traversal Orders
The four ways to visit all nodes in a binary tree:

| Traversal | Order | Common Use |
|-----------|-------|-----------|
| Inorder | Left -> Root -> Right | Gives sorted order in BST |
| Preorder | Root -> Left -> Right | Copy a tree, serialize |
| Postorder | Left -> Right -> Root | Delete a tree, compute subtree values |
| Level order | Level by level (BFS) | Find depth, connect levels |

## When to Reach For It
- The problem involves parent-child relationships or hierarchy
- You need to represent decisions (left = yes, right = no)
- The problem says "tree" or gives you a tree structure
- You need to process connected nodes with a branching structure

## When NOT to Use It
- You need fast search without a specific ordering - use HashMap
- The data is linear - use an array or linked list

## vs Alternatives
- vs Binary Search Tree: BST is a binary tree with ordering - enables O(log n) search
- vs Graph: Tree is a graph with no cycles and exactly one path between any two nodes

## C# Implementation
- No built-in general binary tree in C#
- Define manually in interview problems:
  ```
  class TreeNode {
      int val;
      TreeNode left;
      TreeNode right;
  }
  ```

## Coverage

**Progress: 0 / 10 techniques explored (0%)**

| Technique | Status | Problems Solved |
|-----------|--------|-----------------|
| Inorder Traversal (recursive) | not started | - (left, root, right) |
| Preorder Traversal (recursive) | not started | - (root, left, right - copy / serialize) |
| Postorder Traversal (recursive) | not started | - (left, right, root - delete / compute subtree) |
| Level Order / BFS | not started | - (process nodes level by level) |
| Iterative Traversal (with Stack) | not started | - (inorder / preorder without recursion) |
| Height and Depth Calculation | not started | - (max depth, balanced check) |
| Path Sum Problems | not started | - (root-to-leaf sum, path equals target) |
| Lowest Common Ancestor (LCA) | not started | - (deepest node that is ancestor of both) |
| Diameter | not started | - (longest path between any two nodes) |
| Tree DP | not started | - (max path sum, count good nodes, subtree aggregation) |

---

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty, patterns
FROM "problems"
FLATTEN ds-used AS ds
WHERE ds = "binary-tree"
SORT number asc
```
