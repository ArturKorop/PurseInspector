using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Blog
{
    /// <summary>
    /// Class that provides a description of blog's article in database
    /// </summary>
    [Table("Blog")]
    public class Article : ShortArticle
    {
        /// <summary>
        /// User ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// All text of Article 
        /// </summary>
        public string Text { get; set; }
    }
    /// <summary>
    /// Class that provides a short description(without content) of blog's article in database
    /// </summary>
    public class ShortArticle
    {
        /// <summary>
        /// Article ID
        /// </summary>
        public int ID { get; set; }
       
        /// <summary>
        /// Short name of Article for display in archive widget
        /// </summary>
        public string ArticleShortName { get; set; }
        
        /// <summary>
        /// Time, when arricle add to database
        /// </summary>
        public DateTime Date { get; set; }
    }
}