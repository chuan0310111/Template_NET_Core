using _2.Template_NET_Core.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.Template_NET_Core.Services.Interface
{
    public interface ISampleService
    {
        Task<List<HsinChuAreaDto>> GetAreaAsync();
    }
}
