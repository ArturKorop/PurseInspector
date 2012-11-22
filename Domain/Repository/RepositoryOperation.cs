namespace Domain.Repository
{
    /// <summary>
    /// Class that provides a description of operation in database
    /// </summary>
    public class RepositoryOperation
    {
        /// <summary>
        /// Operation ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// User ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Year of operation
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Month of operation
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// Day of operation
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// Type of opertion
        /// </summary>
        public string OperationType { get; set; }
        /// <summary>
        /// Name of operation
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// Value of operation
        /// </summary>
        public int OperationValue { get; set; }
    }
}