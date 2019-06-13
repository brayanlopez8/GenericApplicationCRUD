using DAL.Repository;
using ENT.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private MyDBContext context = new MyDBContext();
        private bool disposed = false;
        private Repository<Person> personRepository;
        private Repository<Country> contryRepository;

        public Repository<Country> ContryRepository
        {
            get
            {
                if (this.contryRepository == null)
                {
                    this.contryRepository = new Repository<Country>(context);
                }
                return contryRepository;
            }
        }
        public Repository<Person> PersonRepository
        {
            get
            {
                if (this.personRepository == null)
                {
                    this.personRepository = new Repository<Person>(context);
                }
                return personRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
