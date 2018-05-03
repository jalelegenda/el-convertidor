using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElConvertidor.Data.Contexts;
using ElConvertidor.Data.Entities;
using ElConvertidor.Core.Data;
using ElConvertidor.Core.Models;

namespace ElConvertidor.Data.Services
{
    public class ConversionsStore : IConversionsStore
    {
        public void StoreConversion(IEnumerable<ISourceImage> source)
        {
            List<InputImage> inputImages = new List<InputImage>();
            foreach(var image in source)
            {
                inputImages.Add(new InputImage
                {
                    Name = image.Name,
                    Type = image.Type
                });
            }
            var conversion = new Conversion
            {
                NumberOfPages = source.Count(),
                CreatedOn = DateTime.Now,
                InputImages = inputImages
            };

            using(var dbContext = new ConversionsContext())
            {
                dbContext.Conversion.Add(conversion);
                dbContext.InputImage.AddRange(inputImages);
                dbContext.SaveChanges();
            }
        }
    }
}
