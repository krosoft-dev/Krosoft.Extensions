namespace Krosoft.Extensions.Data.Abstractions.Models;

public class CrudBusiness<T>
{
    public CrudBusiness()
    {
        ToAdd = new List<T>();
        ToUpdate = new List<T>();
        ToDelete = new List<T>();
    }

    public ICollection<T> ToUpdate { get; }
    public ICollection<T> ToAdd { get; }
    public ICollection<T> ToDelete { get; }
}