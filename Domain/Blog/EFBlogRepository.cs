using System.Collections.ObjectModel;
using Domain.Abstract;
using Domain.Repository;
using System.Linq;

namespace Domain.Blog
{
    /// <summary>
    /// Class that implements interface <see cref="IBlogRepository"/>
    /// </summary>
    public class EFBlogRepository : IBlogRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public Collection<Article> GetBlog()
        {
            return null;
        }

        public int AddOperation(Article article)
        {
            _context.BlogRepository.Add(new Article{ArticleShortName = "Test1",Text = "qweasdzxcrtyfghfgjgh", UserID = article.UserID});
            _context.SaveChanges();
            return _context.BlogRepository.Where(x => x.UserID == article.UserID).Max(y => y.ID);
        }

        public void ChangeOperation(Article article, int userID)
        {
        }

        public void RemoveOperation(int id, int userID)
        {
        }
    }
}