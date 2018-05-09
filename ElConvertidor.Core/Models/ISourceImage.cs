using System.IO;

namespace ElConvertidor.Core.Models
{
    public interface ISourceImage
    {
        int Id { get; }
        string Name { get; }
        string Type { get; }
        Stream Content { get; }
    }
}
