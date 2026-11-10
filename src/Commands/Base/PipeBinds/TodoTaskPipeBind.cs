namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    /// <summary>
    /// Allows Microsoft To Do tasks to be specified by identifier or task instance.
    /// </summary>
    public sealed class TodoTaskPipeBind
    {
        private readonly string _id;

        /// <summary>
        /// Creates a pipe bind from a Microsoft To Do task identifier.
        /// </summary>
        /// <param name="input">Identifier of the Microsoft To Do task.</param>
        public TodoTaskPipeBind(string input)
        {
            _id = input;
        }

        /// <summary>
        /// Creates a pipe bind from a Microsoft To Do task instance.
        /// </summary>
        /// <param name="task">Microsoft To Do task instance.</param>
        public TodoTaskPipeBind(Model.ToDo.ToDoTask task)
        {
            _id = task.Id;
        }

        /// <summary>
        /// Gets the Microsoft To Do task identifier.
        /// </summary>
        public string Id => _id;
    }
}
