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
            var tempText = article.Text ?? "";
            if (article.ID == 0)
            {
                _context.BlogRepository.Add(new Article
                    {
                        ArticleShortName = article.ArticleShortName,
                        Text = tempText,
                        UserID = article.UserID,
                        Date = article.Date
                    });
            }
            _context.SaveChanges();
            return _context.BlogRepository.Where(x => x.UserID == article.UserID).Max(y => y.ID);
        }

        public void EditArticle(Article article, int userID)
        {
        }

        public bool DeleteArticle(int id, int userID)
        {
            var delete = _context.BlogRepository.SingleOrDefault(x => x.ID == id && x.UserID == userID);
            if (delete != null)
            {
                _context.BlogRepository.Remove(delete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Article LastArticle
        {
            get
            {
                var maxDate = _context.BlogRepository.Max(x => x.Date);
                return _context.BlogRepository.Single(x => x.Date == maxDate);
            }
        }

        public Article GetArticle(int id)
        {
            return _context.BlogRepository.SingleOrDefault(x => x.ID == id);
        }
    }
}