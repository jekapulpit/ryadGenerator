using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GENERATOR
{
    public class RYAD : IDisposable
    {
        USERContext Db = new USERContext();
        GeneratorContext Db1 = new GeneratorContext();

     
        USERRepository userRepository;
        GeneratorRepository generatorRepository;
        
        public USERRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new USERRepository(Db);
                return userRepository;
            }

        }
        public GeneratorRepository Ryads
        {
            get
            {
                if (generatorRepository == null)
                    generatorRepository = new GeneratorRepository(Db1);
                return generatorRepository;
            }
        }
        public void Save()
        {
            Db.SaveChanges();
            Db1.SaveChanges(); 
        }
        public void Dispose()
        {
            
        }

    }
}
