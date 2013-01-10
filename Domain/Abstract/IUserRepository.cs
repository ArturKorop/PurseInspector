namespace Domain.Abstract
{
    /// <summary>
    /// Interface for connection to users database
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Returns user ID from user name
        /// </summary>
        /// <param name="userName">User name</param>
        /// <Returns></Returns>
        int GetUserID(string userName);
        /// <summary>
        /// Returns user name from user ID
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <Returns></Returns>
        string GetUserName(int userID);
    }
}