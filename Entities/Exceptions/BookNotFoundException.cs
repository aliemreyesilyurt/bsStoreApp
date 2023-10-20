namespace Entities.Exceptions
{
    //sealed, tanimlandigi class'in hicbir sekilde kalitim edilemeyecegini belirtir
    public sealed class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(int id) 
            : base($"The book with id: {id} could not found!")
        {
        }
    }
}
