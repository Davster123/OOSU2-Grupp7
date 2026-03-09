using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entiteter;
using Microsoft.EntityFrameworkCore;

namespace Datalager
{
    public class UnitOfWork : IDisposable
    {
        private readonly OOPSU2DbContext _db;

        //här dekklararas alla repositorys så att controllern kan nå dem
        public GenericRepository<Betalning> BetalningRepository { get; private set; }
        public GenericRepository<Bokning> BokningRepository { get; private set; }
        public GenericRepository<Medlem> MedlemRepository { get; private set; }
        public GenericRepository<Personal> PersonalRepository { get; private set; }
        public GenericRepository<Resurs> ResursRepository { get; private set; }
        public GenericRepository<Utrustning> UtrustningRepository { get; private set; }

        public UnitOfWork()
        {
            _db = new OOPSU2DbContext();
            _db.Database.EnsureCreated();
            DbSeed.Populate(_db);

            // Initierar varje repository med rätt tabell från DbContext.
            BetalningRepository = new GenericRepository<Betalning>(_db.Betalningar);
            BokningRepository = new GenericRepository<Bokning>(_db.Bokningar);
            MedlemRepository = new GenericRepository<Medlem>(_db.Medlemmar);
            PersonalRepository = new GenericRepository<Personal>(_db.Personaler);
            ResursRepository = new GenericRepository<Resurs>(_db.Resurser);
            UtrustningRepository = new GenericRepository<Utrustning>(_db.Utrustningar);

        }

        public int Save() => _db.SaveChanges();
        public void Dispose() => _db.Dispose();



        //Unit of work läser från DBContext och sedan skickar till databasen för att sparas


    }
}
