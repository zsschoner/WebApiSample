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

    //public class ValuesService : IService<Values>,
    //                             IService<GetValue>,
    //                             IService<PostValue>,
    //                             IService<PutValue>,
    //                             IService<DeleteValue>,
    //                             IRequiresRequestContext
    //{
    //    private IQueryable<ValueModel> GetValues()
    //    {
    //        return ValuesOperations.ListValues().Select(vs => new ValueModel() { Id = vs.Id, Name = vs.Name }).AsQueryable();
    //    }

    //    // Get list
    //    public object Execute(Values request)
    //    {
    //        return CommonServer.ValuesOperations.ListValues();
    //    }

    //    // Get Item
    //    public object Execute(GetValue request)
    //    {
    //        return CommonServer.ValuesOperations.GetValue(request.Id);
    //    }

    //    // Create item
    //    public object Execute(PostValue request)
    //    {
    //        return CommonServer.ValuesOperations.CreateValue(new ValueModel() { Id = request.Id, Name = request.Name });
    //    }

    //    // Update item
    //    public object Execute(PutValue request)
    //    {
    //        return CommonServer.ValuesOperations.UpdateValue(request.Id, request);
    //    }

    //    // Delete item
    //    public object Execute(DeleteValue request)
    //    {
    //        return CommonServer.ValuesOperations.DeleteValue(request);
    //    }

    //    // Request context if it is needed
    //    public IRequestContext RequestContext { get; set; }




    //}
}
