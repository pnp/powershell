namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    /// <summary>
    /// Allows Microsoft To Do task file attachments to be specified by identifier or attachment instance.
    /// </summary>
    public sealed class TodoTaskFileAttachmentPipeBind
    {
        private readonly string _id;

        /// <summary>
        /// Creates a pipe bind from a Microsoft To Do task file attachment identifier.
        /// </summary>
        /// <param name="input">Identifier of the Microsoft To Do task file attachment.</param>
        public TodoTaskFileAttachmentPipeBind(string input)
        {
            _id = input;
        }

        /// <summary>
        /// Creates a pipe bind from a Microsoft To Do task file attachment instance.
        /// </summary>
        /// <param name="attachment">Microsoft To Do task file attachment instance.</param>
        public TodoTaskFileAttachmentPipeBind(Model.ToDo.TaskFileAttachment attachment)
        {
            _id = attachment.Id;
        }

        /// <summary>
        /// Gets the Microsoft To Do task file attachment identifier.
        /// </summary>
        public string Id => _id;
    }
}
