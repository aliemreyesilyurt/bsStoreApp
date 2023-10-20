
namespace Entities.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        //portected tanimlanmasinin ilk sebebi: mevcut class'in bir abstract olmasidir (abstract class'da constructor, public olmamali)
        //ikinci sebebi ise, tanimlandigi class new'lenemeyecek ve sadece devralinidigi class'larda kullanilabilecek olmasidir
        protected NotFoundException(string message) : base(message)
        {

        }
    }
}
