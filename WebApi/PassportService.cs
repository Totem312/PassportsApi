﻿using WebApi.Interfeses;

namespace WebApi
{
    public class PgPassportService : IServiseRepository
    {
        PgContext _db;
        public PgPassportService(PgContext context)
        {
            _db = context;
        }

        
        public List<Passport> GetPassports()
        {
            return _db.Passports.ToList();
        }

        
        public Passport Create(Passport passport)
        {
            _db.Passports.Add(passport);
            _db.SaveChanges();
            return passport;

        }
     
        public Passport Delite(int id)
        {
            Passport pass = new Passport();
            pass.Id = id;
            _db.Passports.Attach(pass);
            _db.Passports.Remove(pass);
            _db.SaveChanges();
            return null;
        }

       
        public Passport Update(int id, Passport uppassport)
        {
            Passport passport = _db.Passports.Find(id);
         
            passport.Number = uppassport.Number;
            passport.Series = uppassport.Series;

            _db.SaveChanges();
            return passport;
        }
    }
}
     
    

