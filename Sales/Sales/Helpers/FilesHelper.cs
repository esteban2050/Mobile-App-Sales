namespace Sales.Helpers
{
    using System.IO;
    using System.Web;
    
    public class FilesHelper
    {
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms); return ms.ToArray();
            }
        }
    }

}
