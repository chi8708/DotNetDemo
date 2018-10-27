using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcApi.Controllers
{
    public class UploadFile
    {
        public string Ext { get; set; }

        public string Context { get; set; }

        public byte[] ContextByte { get; set; }
    }
}
