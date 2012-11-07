namespace Domain.Repository
{
    public class RepositoryOperation
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string OperationType { get; set; }
        public string OperationName { get; set; }
        public int OperationValue { get; set; }
    }
}