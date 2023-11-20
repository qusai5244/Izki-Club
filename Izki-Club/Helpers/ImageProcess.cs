using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Izki_Club.Helpers
{
    public class ImageProcess
    {
        private static readonly string imageDirectory = @"Public\Images\";

        public static string UploadImage(IFormFile imageFile)
        {
            // Validate the file before processing
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Image file is required.");
            }

            // Ensure the directory exists; create it if not
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            // Generate a unique filename
            string uniqueFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(imageFile.FileName)}";

            string filePath = Path.Combine(imageDirectory, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }

            return uniqueFileName;
        }


        public static void DeleteImage(string fileName)
        {
            string filePath = Path.Combine(imageDirectory, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }


}

