using System.Collections.ObjectModel;
using Domain.Blog;

namespace Domain.Abstract
{
    /// <summary>
    /// Interface for Blog article in database
    /// </summary>
    public interface IBlogRepository
    {
        /// <summary>
        /// Return collection of existing articles
        /// </summary>
        /// <returns>Collection <see cref="Article"/></returns>
        Collection<Article> GetBlog();
        /// <summary>
        /// Return collection of existing articles map
        /// </summary>
        /// <returns>Collection <see cref="ShortArticle"/></returns>
        Collection<ShortArticle> GetBlogMap();
        /// <summary>
        /// Add a new finance operation to Database
        /// </summary>
        /// <param name="article">Object that provides a description of the new article</param>
        /// <Returns>ID of new operation</Returns>
        int AddArticle(Article article);
        /// <summary>
        /// Change an existing operation
        /// </summary>
        /// <param name="article">Object that provides a description of the new parameters of changed article</param>
        /// <param name="userID">Unique identifier of User</param>
        void ChangeAddArticle(Article article, int userID);
        /// <summary>
        /// Delete an existing article
        /// </summary>
        /// <param name="id">ID of article, that we delete</param>
        /// <param name="userID">Unique identifier of User</param>
        void RemoveAddArticle(int id, int userID); 
    }
}