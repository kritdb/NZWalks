using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            //new User()
            //{
            //    FirstName="Read Only", LastName="User", EmailAddress="readonly@user.com",
            //    Id= new Guid(), Password="Readonly@user", Username="readonly@user.com",
            //    Roles = new List<string> {"reader"}
            //},
            //new User()
            //{
            //    FirstName="Read Write", LastName="User", EmailAddress="readwrite@user.com",
            //    Id= new Guid(), Password="Readwrite@user", Username="readwrite@user.com",
            //    Roles = new List<string> {"reader","writer"}
            //}

        };

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user =  Users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
            x.Password == password);
            
            return user;
        }
    }
}
