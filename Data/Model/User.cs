

using Microsoft.AspNetCore.Identity;

namespace Data.Model
{
    /// <summary>
    /// les utilisateurs de l'appli : formateurs, stagiaires etc
    /// </summary>
    public class User : IdentityUser<int>
    {
        /// <summary>
        /// points d'expérience général en tant que stagiaire (le cas échéant)
        /// </summary>
        public int StudentXP { get; set; }
    }
}