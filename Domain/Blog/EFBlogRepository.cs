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
        private readonly Collection<Article> _article = new Collection<Article>(); 
        private readonly Collection<ShortArticle> _articlesMap = new Collection<ShortArticle>(); 

        public Collection<Article> GetBlog()
        {
            _article.Clear();
            foreach (var article in _context.BlogRepository.Select(x=>x))
            {
                _article.Add(article);
            }
            return _article;
        }

        public Collection<ShortArticle> GetBlogMap()
        {
            _articlesMap.Clear();
            foreach (var article in _context.BlogRepository.Select(x => x))
            {
                _articlesMap.Add(article);
            }
            return _articlesMap;
        }

        public int AddArticle(Article article)
        {
            _context.BlogRepository.Add(new Article{ArticleShortName = article.ArticleShortName, Text = article.Text, UserID = article.UserID, Date = article.Date});
            _context.SaveChanges();
            return _context.BlogRepository.Where(x => x.UserID == article.UserID).Max(y => y.ID);
        }

        public void ChangeAddArticle(Article article, int userID)
        {
        }

        public void RemoveAddArticle(int id, int userID)
        {
        }
    }
}