using Grpc.Core;
using Inventory.grpc.Protos;

namespace Inventory.grpc.Services
{
    public class ProductService : ExistanceService.ExistanceServiceBase
    {

        public override async Task<ProductExistanceReply> CheckExistance(ProductRequest request, 
            ServerCallContext context)
        {
            return new ProductExistanceReply { ProductQty = 99 };
        }
    }
}






















/*
 * 1 el proyecto tiene que contar con una arquitectura limpia
 * 2 se tiene que desarrollar con el DDD
 * 3 usaremos el patron de mediador
 * 4 tendremos que tener un motor de validaciones y de control de excepciones globales
 * 5 usaremos el patron de repositorio pero haciendo uso de los generics para reutilizar el codigo
 * 6 paginacion, ordenamiento, joins y CRUD
 * 9 usar CQRS, AutoMapper, MediatR
 * 10 pruebas unitarias
 * */