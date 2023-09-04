using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.DBcontext;

namespace Testing
{
    public class BaseTest
    {
        protected PostgresContext ConstruirContext(string nombreDB)
        {
            var opciones = new DbContextOptionsBuilder<PostgresContext>()
                .UseInMemoryDatabase(nombreDB).Options;

            var DbContext = new PostgresContext(opciones);
            return DbContext;
        }
    }
}
