using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;

namespace Domain.Support
{
    /// <summary>
    /// Class-singleton for support in work
    /// </summary>
    public class Support
    {
        private static readonly Lazy<Support> Lazy = new Lazy<Support>(()=>new Support());
        private readonly Collection<SqlAndHtml> _collection = new Collection<SqlAndHtml>();

        private Support()
        {
            _collection.Add(new SqlAndHtml { SqlValue = "Command1", HtmlValue = "<" });
            _collection.Add(new SqlAndHtml { SqlValue = "Command2", HtmlValue = ">" });
        }
        /// <summary>
        /// Singleton
        /// </summary>
        public static Support Instance
        {
            get { return Lazy.Value; }
        }
        /// <summary>
        /// Collction of replecement chars
        /// </summary>
        public Collection<SqlAndHtml> Collection
        {
            get { return _collection; }
        }
        /// <summary>
        /// Transform string to Sql form
        /// </summary>
        /// <param name="str">String before transform</param>
        /// <returns>String after transform</returns>
        public string ForSql(string str)
        {
            string tempText = str;
            foreach (var item in _collection)
            {
                tempText = tempText.Replace(item.HtmlValue, item.SqlValue);
            }
            return tempText;
        }
        /// <summary>
        /// Transform string to Html form
        /// </summary>
        /// <param name="str">String before transform</param>
        /// <returns>String after transform</returns>
        public string ForHtml(string str)
        {
            string tempText = str;
            foreach (var item in _collection)
            {
                tempText = tempText.Replace(item.SqlValue, item.HtmlValue);
            }
            return tempText;
        }
    }
    /// <summary>
    /// Class, that provides a description of replacement char from sql to html
    /// </summary>
    public class SqlAndHtml
    {
        /// <summary>
        /// String for Sql
        /// </summary>
        public string SqlValue { get; set; }
        /// <summary>
        /// String for Html
        /// </summary>
        public string HtmlValue { get; set; }
    }
}