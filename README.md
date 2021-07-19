# TODO
 A simple planning tool for command line.

## Commands
All commands are called from command line. You should either call them from within the app directory or with app directory being added to PATH.


**ADD** - creates a task with name and optionally due date, assignees, priority level and tags. Task is marked as todo.

`todo add <task name with spaces> [_YYYY-MM-DD,HH:mm (due date)] [@assignee] [!number (priority)] [#tag]`

**LIST** - displays all tasks. Uses simple filters (`done`, `!done`).

`todo list [filter]`

**DONE** - marks task as done. Does not remove it.

`todo done <id>`

**REOPEN** - marks task as todo.

`todo reopen <id>`

**REMOVE** - removes task completely from the list.

`todo remove <id>`

### Tips
Remember that the id on the task list is dynamic (i.e. when you remove task with id 3, the task with id 4 will take its place, and the rest will follow.)

## Roadmap
1.0 - Working Task List

1.1 - Proper Filtering Update

1.2 - Assigned People Update
