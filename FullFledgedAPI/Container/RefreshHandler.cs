using FullFledgedAPI.Repos;
using FullFledgedAPI.Repos.Models;
using FullFledgedAPI.Service;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace FullFledgedAPI.Container
{
    public class RefreshHandler : IRefreshHandler
    {
        private readonly FullFledgedAPIContext _context;
        public RefreshHandler(FullFledgedAPIContext context)
        {
            _context = context;
        }
        public async Task<string> GenerateToken(string username)
        {
            var randomnumber = new byte[32];
            using (var randomnumbergenerator = RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                string refreshtoken = Convert.ToBase64String(randomnumber);
                var Existtoken = _context.TblRefreshtokens.FirstOrDefaultAsync(item => item.Userid == username).Result;
                if (Existtoken != null)
                {
                    Existtoken.Refreshtoken = refreshtoken;
                }
                else
                {
                    await _context.TblRefreshtokens.AddAsync(new TblRefreshtoken
                    {
                        Userid = username,
                        Tokenid = new Random().Next().ToString(),
                        Refreshtoken = refreshtoken
                    });

                }
                await _context.SaveChangesAsync();
                return refreshtoken;
            }
        }
    }
}
