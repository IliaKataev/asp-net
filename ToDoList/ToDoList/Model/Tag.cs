namespace ToDoList.Model
{
    public class Tag
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public string Color { get; set; } = "#000000";
    }
}
