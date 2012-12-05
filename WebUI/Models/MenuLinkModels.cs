namespace WebUI.Models
{
    /// <summary>
    /// Class, that provides a description of link
    /// </summary>
    public class MenuLinkModels
    {
        /// <summary>
        /// Link name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Controller, that was call from this link
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// Action of controllers
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// Some properties
        /// </summary>
        public string Properties { get; set; }
    }
}