using Dapper.Contrib.Extensions;

namespace Entities
{
    [Table("Players")]
    public class Player
    {
        /// <summary>
        /// identifier of database player
        /// </summary>
        [Key]
        public int PlayerId { get; set; }
        /// <summary>
        /// First name of the player
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Middle name of the player in case that must be required
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// last name of the player
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// age of the player
        /// </summary>
        public int Age { get; set; }
    }
}
