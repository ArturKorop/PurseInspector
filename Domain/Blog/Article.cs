using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

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
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }

        /// <summary>
        /// All text of Article 
        /// </summary>
        [Required(ErrorMessage = "Please enter an article's text")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        /// <summary>
        /// Transform Text to Sql form
        /// </summary>
        /// <returns><see cref="Article"/></returns>
        public Article ForSql()
        {
            Text = Support.Support.Instance.ForSql(Text);
            return this;
        }
        /// <summary>
        /// Transform Text to Html form
        /// </summary>
        /// <returns><see cref="Article"/></returns>
        public Article ForHtml()
        {
            Text = Support.Support.Instance.ForHtml(Text);
            return this;
        }
    }

    /// <summary>
    /// Class that provides a short description(without content) of blog's article in database
    /// </summary>
    public class ShortArticle
    {
        /// <summary>
        /// Article ID
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        /// <summary>
        /// Short name of Article for display in archive widget
        /// </summary>
        [Required(ErrorMessage = "Please enter a short article name")]
        public string ArticleShortName { get; set; }

        /// <summary>
        /// Time, when arricle add to database
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public DateTime Date { get; set; }
    }
}