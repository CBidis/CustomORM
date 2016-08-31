using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectOrientedSQL;
using ObjectOrientedSQL.Interfaces;

namespace TestSQLObjected
{
    class Program
    {

        static void Main(string[] args)
        {
            var test = new ObjectSQL("Server=CHRIGETA-PC;Database=SchoolBusiness;User Id=sa;Password=goku1988;");

            var result = test.SelectFirstOrDefault<Tokens>(p=>p.TokenId==1005);

            var resultMany = test.SelectMany<Tokens>(p => p.UserId == 1004);

            foreach (Tokens tok in resultMany)
            {
                var mpampis = tok.AuthToken;
            }

            var time = Convert.ToDateTime("2016-07-27 20:45:43.163");

            var timeTest = test.SelectFirstOrDefault<Tokens>(p => p.IssuedOn >= time);

            var user = test.SelectFirstOrDefault<UserSchools>(x => x.id == 1005);
        }
    }
}
