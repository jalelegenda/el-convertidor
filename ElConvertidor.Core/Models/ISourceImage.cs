using System.IO;

namespace ElConvertidor.Core.Models
{
    public interface ISourceImage
    {
        string Name { get; }
        string Type { get; }
        Stream Content { get; }
    }
}
