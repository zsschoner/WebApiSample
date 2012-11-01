using System.Linq;
using ServiceStack.ServiceHost;
using ApiServiceStack.ServiceModel.ValuesOperations;
using CommonServer.Model;
using CommonServer;
using System;
using ServiceStack.ServiceInterface;
using ServiceStack.Common.Web;

namespace ApiServiceStack.ServiceInterface
{
    public class DefaultRestServiceImpl : RestServiceBase<ValuesDefault>
    {        
        public override object OnGet(ValuesDefault request)
        {
            object result = null;
            if (request.Id == 0)
                result = CommonServer.ValuesOperations.ListValues();
            else
            {
                result = CommonServer.ValuesOperations.GetValue(request.Id);
            }

            return result;
        }

        public override object OnPost(ValuesDefault request)
        {
            return CommonServer.ValuesOperations.CreateValue(new ValueModel() { Id = request.Id, Name = request.Name });
        }

        public override object OnPut(ValuesDefault request)
        {
            return CommonServer.ValuesOperations.UpdateValue(request.Id, new ValueModel() { Id = request.Id, Name = request.Name });
        }

        public override object OnDelete(ValuesDefault request)
        {
            return CommonServer.ValuesOperations.DeleteValue(new ValueModel() { Id = request.Id });
        }

        public override string ServiceName
        {
            get
            {
                return "Default ValuesService";
            }
        }
        
    }
}
