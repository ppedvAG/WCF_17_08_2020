using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace HalloWCF
{
    [ServiceBehavior(InstanceContextMode =InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        List<Obst> db = new List<Obst>();

        public Service1()
        {
            db.Add(new Obst()
            {
                Id = 1,
                Name = "Apfel",
                AnzahlKerne = 12,
                VerfaultAm = DateTime.Now.AddDays(20)
            });

            db.Add(new Obst()
            {
                Id = 2,
                Name = "Banane",
                AnzahlKerne = 0,
                VerfaultAm = DateTime.Now.AddHours(4)
            });

            db.Add(new Obst()
            {
                Id = 3,
                Name = "Wassermelone",
                AnzahlKerne = int.MaxValue,
                VerfaultAm = DateTime.Now.AddDays(8)
            });
        }

        public void AddObst(Obst obst)
        {
            db.Add(obst);
            obst.Id = db.Max(x => x.Id) + 1;
        }

        public IEnumerable<Obst> GetAllObst()
        {
            return db;
        }

        public string GetData(int value)
        {
            return $"You entered: {value}";
        }


    }
}
