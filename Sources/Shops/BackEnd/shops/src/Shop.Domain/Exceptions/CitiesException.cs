using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Exceptions;
public static class CitiesException
{
    public class CityNotFoundException : NotFoundException
    {
        public CityNotFoundException(int id)
            : base($"The City with the id {id} was not found.") { }
    }
}
