using FuraFila.Domain.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuraFila.Identity;
using System.Security.Claims;

namespace FuraFila.WebApp.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static T SetCreated<T>(this Controller controller, T entity)
            where T : IEntityCreated
        {
            return entity.SetCreated();
        }

        public static T SetCreatedBy<T>(this Controller controller, T entity)
            where T : IEntityCreatedBy
        {
            return entity.SetCreatedBy(controller.User);
        }

        public static T SetCreated<T>(this T entity)
           where T : IEntityCreated
        {
            entity.Created = DateTime.UtcNow;
            return entity;
        }

        public static T SetCreatedBy<T>(this T entity, ClaimsPrincipal user)
            where T : IEntityCreatedBy
        {
            entity.CreatedBy = user.GetUserPublicId();
            return entity;
        }
    }
}
