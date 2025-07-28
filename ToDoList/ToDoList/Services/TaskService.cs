using Microsoft.JSInterop;
using System.Threading.Tasks;
using ToDoList.Model;
using ToDoList.Pages;
using static System.Net.WebRequestMethods;

namespace ToDoList.Services
{
    public class TaskService
    {
        public List<TaskModel> AllTasks { get; set; } = new();
        public List<TaskModel> FilteredTasks { get; private set; } = new();

        private string currentSort = "default";
        private bool isSortReversed = false;
        private string statusFilter = "";
        private string priorityFilter = "";
        private DateTime? dateFilter = null;
        private string searchQuery = "";

        public async Task SaveAsync(IJSRuntime JS)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(AllTasks);
            await JS.InvokeVoidAsync("localStorage.setItem", "tasks", json);
        }

        public async Task LoadAsync(HttpClient http, IJSRuntime JS)
        {
            var localData = await JS.InvokeAsync<string>("localStorage.getItem", "tasks");

            if (!string.IsNullOrEmpty(localData) && localData != "[]")
            {
                AllTasks = System.Text.Json.JsonSerializer.Deserialize<List<TaskModel>>(localData);
            }
            else
            {
                var json = await http.GetStringAsync("sample-data/tasks.json");
                AllTasks = System.Text.Json.JsonSerializer.Deserialize<List<TaskModel>>(json);

                await SaveAsync(JS);
            }

            ApplyFilter();
        }

        public void DeleteAsync(Guid id)
        {
            var taskToDelete = AllTasks.FirstOrDefault(t => t.Id == id);
            if (taskToDelete != null)
            {
                AllTasks.Remove(taskToDelete);
                
            }
            ApplyFilter();
        }

        public void AddTask(TaskModel newerTask)
        {
            AllTasks.Add(newerTask);
        }

        public void SetSortOption(string sort) => currentSort = sort;
        public void SetPriorityFilter(string value) => priorityFilter = value;
        public void SetStatusFilter(string value) => statusFilter = value;
        public void SetDateFilter(DateTime? value) => dateFilter = value;
        public void SetSearchQuery(string value) => searchQuery = value;
        public void ToggleSortOrder() => isSortReversed = !isSortReversed;

        public void ApplyFilter()
        {
            IEnumerable<TaskModel> filtered = AllTasks;

            if (!string.IsNullOrEmpty(statusFilter))
            {
                filtered = filtered.Where(t => (string.IsNullOrEmpty(statusFilter) || t.IsCompleted.ToString().ToLower() == statusFilter.ToLower()));
            }

            if (!string.IsNullOrEmpty(priorityFilter))
            {
                filtered = filtered.Where(t => t.Priority.ToString() == priorityFilter);
            }

            if (dateFilter.HasValue)
            {
                filtered = filtered.Where(t => t.DueDate.Date == dateFilter.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                filtered = filtered.Where(t => t.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

            FilteredTasks = filtered.ToList();

            ApplySorting();
        }

        public void ApplySorting()
        {
            switch (currentSort)
            {
                case "priority":
                    FilteredTasks = FilteredTasks.OrderBy(t => t.Priority).ToList();
                    break;
                case "date":
                    FilteredTasks = FilteredTasks.OrderBy(t => t.DueDate).ToList();
                    break;
                case "title":
                    FilteredTasks = FilteredTasks.OrderBy(t => t.Title).ToList();
                    break;
                default:
                    break;
            }

            if (isSortReversed)
            {
                FilteredTasks.Reverse();
            }
        }
    }
}
