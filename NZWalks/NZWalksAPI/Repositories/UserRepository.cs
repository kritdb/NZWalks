using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NZWalksDbContext nZwalksDbContext;

        public UserRepository(NZWalksDbContext nZwalksDbContext)
        {
            this.nZwalksDbContext = nZwalksDbContext;
        }
        public async Task<User> AuthenticateAsync(string Username, string Password)
        {
            var user = await nZwalksDbContext.Users
                .FirstOrDefaultAsync(x => x.Username.ToLower() == Username.ToLower() && x.Password.ToLower() == Password.ToLower());

            if(user == null)
            {
                return null;
            }

            var userRoles = await nZwalksDbContext.User_Roles.Where(x => x.UserId == user.Id).ToListAsync();

            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await nZwalksDbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if(role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }

            user.Password = null; 
            return user;    

        }
    }
}
