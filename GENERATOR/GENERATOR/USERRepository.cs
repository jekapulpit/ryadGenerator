using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GENERATOR
{
    public class USERRepository : IRepository<USER>
    {
        private USERContext Users;
        
        public USERRepository(USERContext M)
        {
            this.Users = M;
        }
        public IEnumerable<USER> GetAll(string s) {
            return null;

        }
        public void Create(USER item)
        {
            this.Users.Users.Add(item); 
        }
        public USER Check(string login, string password)
        {
            USER A = this.Users.Users.Find(login);
            if (A != null && A.password == password) return A;
            else return null;
        }
    }
}
