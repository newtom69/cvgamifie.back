

using Microsoft.AspNetCore.Identity;

namespace Data.Model
{
    public class User : IdentityUser<int>
    {
        // todo : ajouter les attrributs spécifiques aux utilisateurs non déjà implémentés dans IdentityUser
        override public string UserName { get; set; }
    }
}