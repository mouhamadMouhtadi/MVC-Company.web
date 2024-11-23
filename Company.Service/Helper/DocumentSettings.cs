using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Company.Service.Helper
{
	public class DocumentSettings
	{
		public static string UploadFile (IFormFile file , string FolderName)
		{
			//1 Folder Path 
			var FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files", FolderName);
			//2 Get File Name
			var fileName = $"{Guid.NewGuid()}-{file.FileName}";
			//3 combine folderpath with filename
			var FilePath = Path.Combine(FolderPath, fileName);
			using var FileStream = new FileStream(FilePath , FileMode.Create); 
			file.CopyTo(FileStream);
			return FilePath;
		}
	}
}
