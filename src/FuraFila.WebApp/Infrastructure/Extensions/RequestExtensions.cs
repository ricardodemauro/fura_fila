using FuraFila.Domain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.WebApp.Infrastructure.Extensions
{
    public static class RequestExtensions
    {
        public static T CreateServiceRequest<T>(this Controller controller)
            where T : ServiceRequestBase, new()
        {
            var sq = new T();
            sq.User = controller.User;
            return sq;
        }
    }
}
