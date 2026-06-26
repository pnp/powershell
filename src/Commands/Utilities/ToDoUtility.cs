using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Model.ToDo;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Provides helper methods for Microsoft Graph To Do list, task, attachment, checklist item, and linked resource operations.
    /// </summary>
    internal static class ToDoUtility
    {
        /// <summary>
        /// Gets the Microsoft Graph To Do lists root URL for the current user or a specified user.
        /// </summary>
        /// <param name="cmdlet">Cmdlet requesting the To Do root URL.</param>
        /// <param name="user">Optional user to target. Required for app-only connections.</param>
        /// <returns>The Microsoft Graph To Do lists URL, or <c>null</c> if the specified user cannot be found.</returns>
        public static string GetTodoRootUrl(PnPGraphCmdlet cmdlet, EntraIDUserPipeBind user)
        {
            if (cmdlet.Connection.ConnectionMethod == ConnectionMethod.AzureADAppOnly && user == null)
            {
                throw new PSInvalidOperationException("Please specify the parameter User when invoking this cmdlet in app-only scenario");
            }

            if (user == null)
            {
                return "/v1.0/me/todo/lists";
            }

            var graphUser = user.GetUser(cmdlet.AccessToken, cmdlet.Connection.AzureEnvironment);
            if (graphUser == null)
            {
                cmdlet.LogWarning("Provided user not found");
                return null;
            }

            return $"/v1.0/users/{graphUser.Id.Value}/todo/lists";
        }

        /// <summary>
        /// Serializes a value to JSON content using Microsoft Graph compatible settings.
        /// </summary>
        /// <typeparam name="T">Type of the value to serialize.</typeparam>
        /// <param name="value">Value to serialize.</param>
        /// <returns>JSON HTTP content.</returns>
        public static StringContent ToJsonContent<T>(T value)
        {
            var options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            var stringContent = new StringContent(JsonSerializer.Serialize(value, options));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return stringContent;
        }

        /// <summary>
        /// Gets a Microsoft To Do list by identifier or display name.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="identity">List identifier or display name.</param>
        /// <returns>The matching Microsoft To Do list, or <c>null</c> if no match is found.</returns>
        public static ToDoList GetList(ApiRequestHelper requestHelper, string url, string identity)
        {
            var lists = GetLists(requestHelper, url);
            return lists.FirstOrDefault(l => string.Equals(l.Id, identity, StringComparison.OrdinalIgnoreCase) || string.Equals(l.DisplayName, identity, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets the identifier of a Microsoft To Do list by identifier or display name.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="identity">List identifier or display name.</param>
        /// <returns>The Microsoft To Do list identifier, or <c>null</c> if no match is found.</returns>
        public static string GetListId(ApiRequestHelper requestHelper, string url, string identity)
        {
            return GetList(requestHelper, url, identity)?.Id;
        }

        /// <summary>
        /// Gets all Microsoft To Do lists available at the provided URL.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <returns>The Microsoft To Do lists.</returns>
        public static IEnumerable<ToDoList> GetLists(ApiRequestHelper requestHelper, string url)
        {
            return requestHelper.GetResultCollection<ToDoList>(url);
        }

        /// <summary>
        /// Creates a Microsoft To Do list.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="displayName">Display name for the new list.</param>
        /// <returns>The created Microsoft To Do list.</returns>
        public static ToDoList CreateList(ApiRequestHelper requestHelper, string url, string displayName)
        {
            return requestHelper.Post<ToDoList>(url, ToJsonContent(new ToDoList { DisplayName = displayName }));
        }

        /// <summary>
        /// Updates the display name of a Microsoft To Do list.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="identity">List identifier.</param>
        /// <param name="displayName">New display name.</param>
        /// <returns>The updated Microsoft To Do list.</returns>
        public static ToDoList UpdateList(ApiRequestHelper requestHelper, string url, string identity, string displayName)
        {
            return requestHelper.Patch<ToDoList>($"{url}/{identity}", ToJsonContent(new ToDoList { DisplayName = displayName }));
        }

        /// <summary>
        /// Deletes a Microsoft To Do list.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="identity">List identifier.</param>
        /// <returns>The HTTP response from Microsoft Graph.</returns>
        public static HttpResponseMessage DeleteList(ApiRequestHelper requestHelper, string url, string identity)
        {
            return requestHelper.Delete($"{url}/{identity}");
        }

        /// <summary>
        /// Gets a Microsoft To Do task.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="identity">Task identifier.</param>
        /// <returns>The matching Microsoft To Do task.</returns>
        public static ToDoTask GetTask(ApiRequestHelper requestHelper, string url, string listId, string identity)
        {
            return requestHelper.Get<ToDoTask>($"{url}/{listId}/tasks/{identity}");
        }

        /// <summary>
        /// Gets all Microsoft To Do tasks in a list.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the tasks.</param>
        /// <returns>The Microsoft To Do tasks in the list.</returns>
        public static IEnumerable<ToDoTask> GetTasks(ApiRequestHelper requestHelper, string url, string listId)
        {
            return requestHelper.GetResultCollection<ToDoTask>($"{url}/{listId}/tasks");
        }

        /// <summary>
        /// Creates a Microsoft To Do task in a list.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list to create the task in.</param>
        /// <param name="task">Task values to create.</param>
        /// <returns>The created Microsoft To Do task.</returns>
        public static ToDoTask CreateTask(ApiRequestHelper requestHelper, string url, string listId, ToDoTask task)
        {
            return requestHelper.Post<ToDoTask>($"{url}/{listId}/tasks", ToJsonContent(task));
        }

        /// <summary>
        /// Updates a Microsoft To Do task.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="identity">Task identifier.</param>
        /// <param name="task">Task values to update.</param>
        /// <returns>The updated Microsoft To Do task.</returns>
        public static ToDoTask UpdateTask(ApiRequestHelper requestHelper, string url, string listId, string identity, ToDoTask task)
        {
            return requestHelper.Patch<ToDoTask>($"{url}/{listId}/tasks/{identity}", ToJsonContent(task));
        }

        /// <summary>
        /// Deletes a Microsoft To Do task.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="identity">Task identifier.</param>
        /// <returns>The HTTP response from Microsoft Graph.</returns>
        public static HttpResponseMessage DeleteTask(ApiRequestHelper requestHelper, string url, string listId, string identity)
        {
            return requestHelper.Delete($"{url}/{listId}/tasks/{identity}");
        }

        /// <summary>
        /// Gets a Microsoft To Do task file attachment, optionally including file content.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="taskId">Identifier of the task containing the attachment.</param>
        /// <param name="identity">Attachment identifier.</param>
        /// <param name="includeContent">Whether to retrieve and populate attachment content bytes.</param>
        /// <returns>The matching Microsoft To Do task file attachment.</returns>
        public static TaskFileAttachment GetTaskFileAttachment(ApiRequestHelper requestHelper, string url, string listId, string taskId, string identity, bool includeContent)
        {
            var attachment = requestHelper.Get<TaskFileAttachment>($"{url}/{listId}/tasks/{taskId}/attachments/{identity}");
            if (includeContent)
            {
                PopulateTaskFileAttachmentContent(requestHelper, url, listId, taskId, attachment);
            }

            return attachment;
        }

        /// <summary>
        /// Gets all Microsoft To Do task file attachments, optionally including file content.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="taskId">Identifier of the task containing the attachments.</param>
        /// <param name="includeContent">Whether to retrieve and populate attachment content bytes.</param>
        /// <returns>The Microsoft To Do task file attachments.</returns>
        public static IEnumerable<TaskFileAttachment> GetTaskFileAttachments(ApiRequestHelper requestHelper, string url, string listId, string taskId, bool includeContent)
        {
            var attachments = requestHelper.GetResultCollection<TaskFileAttachment>($"{url}/{listId}/tasks/{taskId}/attachments").ToList();
            if (includeContent)
            {
                foreach (var attachment in attachments)
                {
                    PopulateTaskFileAttachmentContent(requestHelper, url, listId, taskId, attachment);
                }
            }

            return attachments;
        }

        /// <summary>
        /// Populates a Microsoft To Do task file attachment with base64 encoded content bytes.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="taskId">Identifier of the task containing the attachment.</param>
        /// <param name="attachment">Attachment to populate.</param>
        private static void PopulateTaskFileAttachmentContent(ApiRequestHelper requestHelper, string url, string listId, string taskId, TaskFileAttachment attachment)
        {
            if (attachment == null)
            {
                return;
            }

            var response = requestHelper.GetResponse($"{url}/{listId}/tasks/{taskId}/attachments/{attachment.Id}/$value");
            attachment.ContentBytes = Convert.ToBase64String(response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult());
            attachment.ContentType ??= response.Content.Headers.ContentType?.MediaType;
        }

        /// <summary>
        /// Creates a Microsoft To Do task file attachment.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="taskId">Identifier of the task to attach the file to.</param>
        /// <param name="attachment">Attachment values to create.</param>
        /// <returns>The created Microsoft To Do task file attachment.</returns>
        public static TaskFileAttachment CreateTaskFileAttachment(ApiRequestHelper requestHelper, string url, string listId, string taskId, TaskFileAttachment attachment)
        {
            return requestHelper.Post<TaskFileAttachment>($"{url}/{listId}/tasks/{taskId}/attachments", ToJsonContent(attachment));
        }

        /// <summary>
        /// Deletes a Microsoft To Do task file attachment.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="taskId">Identifier of the task containing the attachment.</param>
        /// <param name="identity">Attachment identifier.</param>
        /// <returns>The HTTP response from Microsoft Graph.</returns>
        public static HttpResponseMessage DeleteTaskFileAttachment(ApiRequestHelper requestHelper, string url, string listId, string taskId, string identity)
        {
            return requestHelper.Delete($"{url}/{listId}/tasks/{taskId}/attachments/{identity}");
        }

        /// <summary>
        /// Gets a Microsoft To Do task checklist item.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="taskId">Identifier of the task containing the checklist item.</param>
        /// <param name="identity">Checklist item identifier.</param>
        /// <returns>The matching checklist item.</returns>
        public static ChecklistItem GetChecklistItem(ApiRequestHelper requestHelper, string url, string listId, string taskId, string identity)
        {
            return requestHelper.Get<ChecklistItem>($"{url}/{listId}/tasks/{taskId}/checklistItems/{identity}");
        }

        /// <summary>
        /// Gets all Microsoft To Do task checklist items.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="taskId">Identifier of the task containing the checklist items.</param>
        /// <returns>The checklist items for the task.</returns>
        public static IEnumerable<ChecklistItem> GetChecklistItems(ApiRequestHelper requestHelper, string url, string listId, string taskId)
        {
            return requestHelper.GetResultCollection<ChecklistItem>($"{url}/{listId}/tasks/{taskId}/checklistItems");
        }

        /// <summary>
        /// Creates a Microsoft To Do task checklist item.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="taskId">Identifier of the task to create the checklist item on.</param>
        /// <param name="displayName">Display name for the checklist item.</param>
        /// <returns>The created checklist item.</returns>
        public static ChecklistItem CreateChecklistItem(ApiRequestHelper requestHelper, string url, string listId, string taskId, string displayName)
        {
            return requestHelper.Post<ChecklistItem>($"{url}/{listId}/tasks/{taskId}/checklistItems", ToJsonContent(new ChecklistItem { DisplayName = displayName }));
        }

        /// <summary>
        /// Updates a Microsoft To Do task checklist item.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="taskId">Identifier of the task containing the checklist item.</param>
        /// <param name="identity">Checklist item identifier.</param>
        /// <param name="checklistItem">Checklist item values to update.</param>
        /// <returns>The updated checklist item.</returns>
        public static ChecklistItem UpdateChecklistItem(ApiRequestHelper requestHelper, string url, string listId, string taskId, string identity, ChecklistItem checklistItem)
        {
            return requestHelper.Patch<ChecklistItem>($"{url}/{listId}/tasks/{taskId}/checklistItems/{identity}", ToJsonContent(checklistItem));
        }

        /// <summary>
        /// Deletes a Microsoft To Do task checklist item.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="taskId">Identifier of the task containing the checklist item.</param>
        /// <param name="identity">Checklist item identifier.</param>
        /// <returns>The HTTP response from Microsoft Graph.</returns>
        public static HttpResponseMessage DeleteChecklistItem(ApiRequestHelper requestHelper, string url, string listId, string taskId, string identity)
        {
            return requestHelper.Delete($"{url}/{listId}/tasks/{taskId}/checklistItems/{identity}");
        }

        /// <summary>
        /// Gets a Microsoft To Do task linked resource.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="taskId">Identifier of the task containing the linked resource.</param>
        /// <param name="identity">Linked resource identifier.</param>
        /// <returns>The matching linked resource.</returns>
        public static LinkedResource GetLinkedResource(ApiRequestHelper requestHelper, string url, string listId, string taskId, string identity)
        {
            return requestHelper.Get<LinkedResource>($"{url}/{listId}/tasks/{taskId}/linkedResources/{identity}");
        }

        /// <summary>
        /// Gets all Microsoft To Do task linked resources.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="taskId">Identifier of the task containing the linked resources.</param>
        /// <returns>The linked resources for the task.</returns>
        public static IEnumerable<LinkedResource> GetLinkedResources(ApiRequestHelper requestHelper, string url, string listId, string taskId)
        {
            return requestHelper.GetResultCollection<LinkedResource>($"{url}/{listId}/tasks/{taskId}/linkedResources");
        }

        /// <summary>
        /// Creates a Microsoft To Do task linked resource.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="taskId">Identifier of the task to create the linked resource on.</param>
        /// <param name="linkedResource">Linked resource values to create.</param>
        /// <returns>The created linked resource.</returns>
        public static LinkedResource CreateLinkedResource(ApiRequestHelper requestHelper, string url, string listId, string taskId, LinkedResource linkedResource)
        {
            return requestHelper.Post<LinkedResource>($"{url}/{listId}/tasks/{taskId}/linkedResources", ToJsonContent(linkedResource));
        }

        /// <summary>
        /// Deletes a Microsoft To Do task linked resource.
        /// </summary>
        /// <param name="requestHelper">Microsoft Graph request helper.</param>
        /// <param name="url">Microsoft Graph To Do lists URL.</param>
        /// <param name="listId">Identifier of the list containing the task.</param>
        /// <param name="taskId">Identifier of the task containing the linked resource.</param>
        /// <param name="identity">Linked resource identifier.</param>
        /// <returns>The HTTP response from Microsoft Graph.</returns>
        public static HttpResponseMessage DeleteLinkedResource(ApiRequestHelper requestHelper, string url, string listId, string taskId, string identity)
        {
            return requestHelper.Delete($"{url}/{listId}/tasks/{taskId}/linkedResources/{identity}");
        }
    }
}
