

namespace Entities.Exceptions
{
    public class PriceOutofRangeBadRequestException : BadRequestException
    {
        public PriceOutofRangeBadRequestException() 
            : base("Maxium price should be less than 1000 and greater than 10.")
        {
        }
    }
}
