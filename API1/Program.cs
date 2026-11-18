using API1.DTOs;
using API1.Models;
using API1.Mapping;
using API1.Repositories;
using API1.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<TeaShopContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(_ => { }, typeof(MappingProfile).Assembly);

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<ICrudService<ProductDTO, CreateProductDTO, UpdateProductDTO, PatchProductDTO>,
                CrudService<Product, ProductDTO, CreateProductDTO, UpdateProductDTO, PatchProductDTO>>();
            builder.Services.AddScoped<ICrudService<CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, PatchCategoryDTO>,
                CrudService<Category, CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, PatchCategoryDTO>>();
            builder.Services.AddScoped<ICrudService<UserDTO, CreateUserDTO, UpdateUserDTO, PatchUserDTO>,
                CrudService<User, UserDTO, CreateUserDTO, UpdateUserDTO, PatchUserDTO>>();
            builder.Services.AddScoped<ICrudService<RoleDTO, CreateRoleDTO, UpdateRoleDTO, PatchRoleDTO>,
                CrudService<Role, RoleDTO, CreateRoleDTO, UpdateRoleDTO, PatchRoleDTO>>();
            builder.Services.AddScoped<ICrudService<CartDTO, CreateCartDTO, UpdateCartDTO, PatchCartDTO>,
                CrudService<Cart, CartDTO, CreateCartDTO, UpdateCartDTO, PatchCartDTO>>();
            builder.Services.AddScoped<ICrudService<CartItemDTO, CreateCartItemDTO, UpdateCartItemDTO, PatchCartItemDTO>,
                CrudService<CartItem, CartItemDTO, CreateCartItemDTO, UpdateCartItemDTO, PatchCartItemDTO>>();
            builder.Services.AddScoped<ICrudService<OrderDTO, CreateOrderDTO, UpdateOrderDTO, PatchOrderDTO>,
                CrudService<Order, OrderDTO, CreateOrderDTO, UpdateOrderDTO, PatchOrderDTO>>();
            builder.Services.AddScoped<ICrudService<OrderItemDTO, CreateOrderItemDTO, UpdateOrderItemDTO, PatchOrderItemDTO>,
                CrudService<OrderItem, OrderItemDTO, CreateOrderItemDTO, UpdateOrderItemDTO, PatchOrderItemDTO>>();
            builder.Services.AddScoped<ICrudService<OrderStatusDTO, CreateOrderStatusDTO, UpdateOrderStatusDTO, PatchOrderStatusDTO>,
                CrudService<OrderStatus, OrderStatusDTO, CreateOrderStatusDTO, UpdateOrderStatusDTO, PatchOrderStatusDTO>>();
            builder.Services.AddScoped<IProductReservationService, ProductReservationService>();

            var app = builder.Build();
            app.Services.GetRequiredService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
