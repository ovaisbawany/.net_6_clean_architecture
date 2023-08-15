using System.ComponentModel.DataAnnotations;

namespace Sample.Core.Entities.Base
{
    public interface IBase<TKey> : IBase
    {
        [Key]
        TKey Id { get; set; }
    }

    public interface IBase
    {
    }
}
