using System;
using System.Collections.Generic;
using System.Linq;
using ElConvertidor.Data.Entities;
using ElConvertidor.Core.Data;
using ElConvertidor.Core.Models;
using System.Data.Entity;

namespace ElConvertidor.Data.Services
{
    public class ConversionsStore : IConversionsStore
    {
        private readonly DbContext _context;

        public ConversionsStore(DbContext context)
        {
            _context = context;
        }

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

            _context.Set<Conversion>().Add(conversion);
            _context.Set<InputImage>().AddRange(inputImages);
            _context.SaveChanges();
        }
    }
}
