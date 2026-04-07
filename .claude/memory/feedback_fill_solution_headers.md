---
name: Fill active-solution.cs headers from conversation
description: AI must fill approach headers in active-solution.cs itself, not ask the user to do it
type: feedback
---

Fill in the `// Approach:`, `// Time:`, `// Space:`, and `// Key Idea:` headers in `active-solution.cs` yourself, based on answers the user has given verbally during the session. Do this before pattern collection, not incrementally during the session.

**Why:** The user does not want to manually fill in headers in the file - they already gave all the information in the conversation.

**How to apply:** After all approaches are implemented and complexity is discussed, fill all headers in one pass before moving to pattern extraction. Never ask the user to fill them in.
