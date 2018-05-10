using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GENERATOR
{
    public class GeneratorRepository : IRepository<Generator>
    {
        private GeneratorContext Ryads;
        public GeneratorRepository(GeneratorContext M)
        {
            this.Ryads = M;
        }
        public IEnumerable<Generator> GetAll(string s)
        {
           IEnumerable<Generator> M = from p in this.Ryads.Ryads
                                      where p.Creator.username == s
                                      orderby p.date descending
                                      select p;
            return M;

        }
        public void Create(Generator item)
        {
            this.Ryads.Ryads.Add(item);
        }
        public Generator Check(string login, string password)
        {
            return null;
        }
    }
}
