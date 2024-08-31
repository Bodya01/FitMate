namespace YourFitnessTracker.Presentation.ViewModels.Error
{
    public class ErrorViewModel
    {
        public string ExceptionName { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}