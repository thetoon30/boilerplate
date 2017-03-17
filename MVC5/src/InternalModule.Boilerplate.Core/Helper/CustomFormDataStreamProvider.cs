using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace InternalModule.Boilerplate.Core.Helper
{
    public class CustomFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomFormDataStreamProvider(string path)
            : base(path)
        { }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            if (String.IsNullOrEmpty(headers.ContentDisposition.FileName))
                return base.GetStream(parent, headers);

            // restrict what images can be selected
            var extensions = new[] { "png", "gif", "jpg", "zip" };
            var filename = headers.ContentDisposition.FileName.Replace("\"", string.Empty);

            if (filename.IndexOf('.') < 0)
                return Stream.Null;

            var extension = filename.Split('.').Last();

            return extensions.Any(i => i.Equals(extension, StringComparison.InvariantCultureIgnoreCase))
                       ? base.GetStream(parent, headers)
                       : Stream.Null;

        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            // override the filename which is stored by the provider (by default is bodypart_x)
            string oldfileName = headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            string newFileName = Guid.NewGuid().ToString("N") + Path.GetExtension(oldfileName);

            return newFileName;
        }

        public override System.Threading.Tasks.Task ExecutePostProcessingAsync()
        {
            return base.ExecutePostProcessingAsync();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
