namespace ToDoListBS.ViewModels
{
    public class UploadVM
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public IFormFile FFile { get;set; }
    }
}
