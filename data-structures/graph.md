---
name: "Graph"
slug: graph
status: stub
progress: 0
---

# Graph

## What It Is
A collection of nodes (vertices) connected by edges. Unlike a tree, a graph can have cycles, multiple paths between nodes, and nodes with no parent-child relationship.

## Core Operations

| Operation | Description | Time |
|-----------|-------------|------|
| Add node | Insert a vertex | O(1) |
| Add edge | Connect two nodes | O(1) |
| BFS traversal | Level-by-level exploration | O(V + E) |
| DFS traversal | Depth-first exploration | O(V + E) |
| Check adjacency | Are two nodes connected? | O(1) with adjacency matrix, O(degree) with list |

V = number of vertices, E = number of edges.

## Space Complexity
- Adjacency list: O(V + E) - stores each edge once (or twice for undirected)
- Adjacency matrix: O(V^2) - full grid regardless of edges

## Mental Model
A map of cities and roads. Cities are nodes. Roads are edges. Some roads are one-way (directed graph). Some roads have distance or cost (weighted graph). You can travel from any city to any other city if a path exists - but unlike a tree, you might loop back to where you started.

## Key Vocabulary
- **Vertex (Node)**: a point in the graph
- **Edge**: a connection between two vertices
- **Directed**: edges have direction (one-way road)
- **Undirected**: edges go both ways (two-way road)
- **Weighted**: edges have a cost or distance
- **Cycle**: a path that loops back to the starting node
- **Connected**: every node can reach every other node

## Representations

**Adjacency List** (most common in interviews):
- Dictionary mapping each node to a list of its neighbors
- Efficient for sparse graphs (few edges)

**Adjacency Matrix**:
- 2D array where `matrix[i][j] = 1` means edge from i to j
- Efficient for dense graphs, fast adjacency check

## When to Reach For It
- Relationships between entities (social network, dependencies)
- "Number of islands" type problems (implicit graph)
- Shortest path problems
- Cycle detection
- Connected components

## When NOT to Use It
- Strictly hierarchical data with no cycles - use a tree
- Simple linear relationships - use an array or linked list

## vs Alternatives
- vs Tree: Tree is a graph with no cycles and exactly one path between any two nodes
- vs LinkedList: Linked list is a graph where each node has at most one outgoing edge (linear)

## Traversal Algorithms
- **BFS** (Breadth-First Search): uses a Queue, finds shortest path in unweighted graphs
- **DFS** (Depth-First Search): uses a Stack (or recursion), good for cycle detection and connected components

## C# Implementation
- No built-in graph in C#
- Represent as adjacency list: `Dictionary<int, List<int>> graph`
- Track visited nodes: `HashSet<int> visited`

## Coverage

**Progress: 0 / 10 techniques explored (0%)**

| Technique | Status | Problems Solved |
|-----------|--------|-----------------|
| BFS - Shortest Path / Level Traversal | not started | - (fewest steps, level-by-level exploration) |
| DFS - Connected Components | not started | - (count islands, flood fill) |
| Cycle Detection | not started | - (directed: DFS with state; undirected: parent check) |
| Topological Sort | not started | - (DFS post-order or Kahn's BFS with in-degree) |
| Union Find / Disjoint Set | not started | - (number of provinces, redundant connection) |
| Multi-Source BFS | not started | - (walls and gates, rotting oranges) |
| Dijkstra's Shortest Path | not started | - (weighted graph, non-negative edges) |
| Bellman-Ford | not started | - (negative edge weights, detect negative cycle) |
| Minimum Spanning Tree | not started | - (Kruskal's with Union Find, Prim's with heap) |
| Bipartite Check | not started | - (2-coloring with BFS or DFS) |

---

## Seen In

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty, patterns
FROM "problems"
FLATTEN ds-used AS ds
WHERE ds = "graph"
SORT problem asc
```
