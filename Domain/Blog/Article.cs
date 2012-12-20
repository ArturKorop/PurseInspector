using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Blog
{
    /// <summary>
    /// Class that provides a description of blog in database
    /// </summary>
    [Table("Blog")]
    public class Article
    {
        /// <summary>
        /// Article ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// User ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Short name of Article for display in archive widget
        /// </summary>
        public string ArticleShortName { get; set; }
        /// <summary>
        /// All text of Article 
        /// </summary>
        public string Text { get; set; }
    }
}