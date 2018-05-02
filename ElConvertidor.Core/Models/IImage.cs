using System.IO;

namespace ElConvertidor.Core.Models
{
    public interface IImage
    {
        string Name { get; }
        string Type { get; }
        Stream Content { get; }
    }
}
