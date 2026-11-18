using API1.DTOs;
using API1.Models;
using AutoMapper;

namespace API1.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDTO>()
            .ForMember(dto => dto.Id, options => options.MapFrom(entity => entity.IdCategory));
        CreateMap<CreateCategoryDTO, Category>()
            .ForMember(entity => entity.IdCategory, options => options.Ignore())
            .ForMember(entity => entity.Products, options => options.Ignore());
        CreateMap<UpdateCategoryDTO, Category>()
            .ForMember(entity => entity.IdCategory, options => options.Ignore())
            .ForMember(entity => entity.Products, options => options.Ignore());
        CreateMap<PatchCategoryDTO, Category>()
            .ForMember(entity => entity.IdCategory, options => options.Ignore())
            .ForMember(entity => entity.Products, options => options.Ignore())
            .ForAllMembers(options => options.Condition((_, _, value) => value is not null));

        CreateMap<Product, ProductDTO>()
            .ForMember(dto => dto.Id, options => options.MapFrom(entity => entity.IdProduct));
        CreateMap<CreateProductDTO, Product>()
            .ForMember(entity => entity.CreatedAt, options => options.MapFrom(_ => DateTime.UtcNow))
            .ForMember(entity => entity.IdProduct, options => options.Ignore())
            .ForMember(entity => entity.CartItems, options => options.Ignore())
            .ForMember(entity => entity.Category, options => options.Ignore())
            .ForMember(entity => entity.OrderItems, options => options.Ignore())
            .ForMember(entity => entity.Reservations, options => options.Ignore());
        CreateMap<UpdateProductDTO, Product>()
            .ForMember(entity => entity.IdProduct, options => options.Ignore())
            .ForMember(entity => entity.CreatedAt, options => options.Ignore())
            .ForMember(entity => entity.CartItems, options => options.Ignore())
            .ForMember(entity => entity.Category, options => options.Ignore())
            .ForMember(entity => entity.OrderItems, options => options.Ignore())
            .ForMember(entity => entity.Reservations, options => options.Ignore());
        CreateMap<PatchProductDTO, Product>()
            .ForMember(entity => entity.IdProduct, options => options.Ignore())
            .ForMember(entity => entity.CreatedAt, options => options.Ignore())
            .ForMember(entity => entity.CartItems, options => options.Ignore())
            .ForMember(entity => entity.Category, options => options.Ignore())
            .ForMember(entity => entity.OrderItems, options => options.Ignore())
            .ForMember(entity => entity.Reservations, options => options.Ignore())
            .ForAllMembers(options => options.Condition((_, _, value) => value is not null));

        CreateMap<Role, RoleDTO>()
            .ForMember(dto => dto.Id, options => options.MapFrom(entity => entity.IdRole));
        CreateMap<CreateRoleDTO, Role>()
            .ForMember(entity => entity.IdRole, options => options.Ignore())
            .ForMember(entity => entity.Users, options => options.Ignore());
        CreateMap<UpdateRoleDTO, Role>()
            .ForMember(entity => entity.IdRole, options => options.Ignore())
            .ForMember(entity => entity.Users, options => options.Ignore());
        CreateMap<PatchRoleDTO, Role>()
            .ForMember(entity => entity.IdRole, options => options.Ignore())
            .ForMember(entity => entity.Users, options => options.Ignore())
            .ForAllMembers(options => options.Condition((_, _, value) => value is not null));

        CreateMap<User, UserDTO>()
            .ForMember(dto => dto.Id, options => options.MapFrom(entity => entity.IdUser));
        CreateMap<CreateUserDTO, User>()
            .ForMember(entity => entity.Password, options => options.MapFrom(dto => BCrypt.Net.BCrypt.HashPassword(dto.Password)))
            .ForMember(entity => entity.RegistrationDate, options => options.MapFrom(_ => DateOnly.FromDateTime(DateTime.UtcNow)))
            .ForMember(entity => entity.IdUser, options => options.Ignore())
            .ForMember(entity => entity.Cart, options => options.Ignore())
            .ForMember(entity => entity.Orders, options => options.Ignore())
            .ForMember(entity => entity.Role, options => options.Ignore())
            .ForMember(entity => entity.Reservations, options => options.Ignore());
        CreateMap<UpdateUserDTO, User>()
            .ForMember(entity => entity.Password, options => options.MapFrom(dto => BCrypt.Net.BCrypt.HashPassword(dto.Password)))
            .ForMember(entity => entity.IdUser, options => options.Ignore())
            .ForMember(entity => entity.Cart, options => options.Ignore())
            .ForMember(entity => entity.Orders, options => options.Ignore())
            .ForMember(entity => entity.Role, options => options.Ignore())
            .ForMember(entity => entity.Reservations, options => options.Ignore());
        CreateMap<PatchUserDTO, User>()
            .ForMember(entity => entity.Password, options => options.MapFrom(dto => dto.Password == null ? null : BCrypt.Net.BCrypt.HashPassword(dto.Password)))
            .ForMember(entity => entity.IdUser, options => options.Ignore())
            .ForMember(entity => entity.Cart, options => options.Ignore())
            .ForMember(entity => entity.Orders, options => options.Ignore())
            .ForMember(entity => entity.Role, options => options.Ignore())
            .ForMember(entity => entity.Reservations, options => options.Ignore())
            .ForAllMembers(options => options.Condition((_, _, value) => value is not null));

        CreateMap<Cart, CartDTO>()
            .ForMember(dto => dto.Id, options => options.MapFrom(entity => entity.IdCart));
        CreateMap<CreateCartDTO, Cart>()
            .ForMember(entity => entity.CreatedDate, options => options.MapFrom(_ => DateOnly.FromDateTime(DateTime.UtcNow)))
            .ForMember(entity => entity.IdCart, options => options.Ignore())
            .ForMember(entity => entity.CartItems, options => options.Ignore())
            .ForMember(entity => entity.User, options => options.Ignore());
        CreateMap<UpdateCartDTO, Cart>()
            .ForMember(entity => entity.IdCart, options => options.Ignore())
            .ForMember(entity => entity.CartItems, options => options.Ignore())
            .ForMember(entity => entity.User, options => options.Ignore());
        CreateMap<PatchCartDTO, Cart>()
            .ForMember(entity => entity.IdCart, options => options.Ignore())
            .ForMember(entity => entity.CartItems, options => options.Ignore())
            .ForMember(entity => entity.User, options => options.Ignore())
            .ForAllMembers(options => options.Condition((_, _, value) => value is not null));

        CreateMap<CartItem, CartItemDTO>()
            .ForMember(dto => dto.Id, options => options.MapFrom(entity => entity.IdCartItem));
        CreateMap<CreateCartItemDTO, CartItem>()
            .ForMember(entity => entity.IdCartItem, options => options.Ignore())
            .ForMember(entity => entity.Cart, options => options.Ignore())
            .ForMember(entity => entity.Product, options => options.Ignore());
        CreateMap<UpdateCartItemDTO, CartItem>()
            .ForMember(entity => entity.IdCartItem, options => options.Ignore())
            .ForMember(entity => entity.Cart, options => options.Ignore())
            .ForMember(entity => entity.Product, options => options.Ignore());
        CreateMap<PatchCartItemDTO, CartItem>()
            .ForMember(entity => entity.IdCartItem, options => options.Ignore())
            .ForMember(entity => entity.Cart, options => options.Ignore())
            .ForMember(entity => entity.Product, options => options.Ignore())
            .ForAllMembers(options => options.Condition((_, _, value) => value is not null));

        CreateMap<OrderStatus, OrderStatusDTO>()
            .ForMember(dto => dto.Id, options => options.MapFrom(entity => entity.IdStatus));
        CreateMap<CreateOrderStatusDTO, OrderStatus>()
            .ForMember(entity => entity.IdStatus, options => options.Ignore())
            .ForMember(entity => entity.Orders, options => options.Ignore());
        CreateMap<UpdateOrderStatusDTO, OrderStatus>()
            .ForMember(entity => entity.IdStatus, options => options.Ignore())
            .ForMember(entity => entity.Orders, options => options.Ignore());
        CreateMap<PatchOrderStatusDTO, OrderStatus>()
            .ForMember(entity => entity.IdStatus, options => options.Ignore())
            .ForMember(entity => entity.Orders, options => options.Ignore())
            .ForAllMembers(options => options.Condition((_, _, value) => value is not null));

        CreateMap<Order, OrderDTO>()
            .ForMember(dto => dto.Id, options => options.MapFrom(entity => entity.IdOrder));
        CreateMap<CreateOrderDTO, Order>()
            .ForMember(entity => entity.OrderDate, options => options.MapFrom(_ => DateOnly.FromDateTime(DateTime.UtcNow)))
            .ForMember(entity => entity.IdOrder, options => options.Ignore())
            .ForMember(entity => entity.OrderItems, options => options.Ignore())
            .ForMember(entity => entity.Status, options => options.Ignore())
            .ForMember(entity => entity.User, options => options.Ignore());
        CreateMap<UpdateOrderDTO, Order>()
            .ForMember(entity => entity.IdOrder, options => options.Ignore())
            .ForMember(entity => entity.OrderItems, options => options.Ignore())
            .ForMember(entity => entity.Status, options => options.Ignore())
            .ForMember(entity => entity.User, options => options.Ignore());
        CreateMap<PatchOrderDTO, Order>()
            .ForMember(entity => entity.IdOrder, options => options.Ignore())
            .ForMember(entity => entity.OrderItems, options => options.Ignore())
            .ForMember(entity => entity.Status, options => options.Ignore())
            .ForMember(entity => entity.User, options => options.Ignore())
            .ForAllMembers(options => options.Condition((_, _, value) => value is not null));

        CreateMap<OrderItem, OrderItemDTO>()
            .ForMember(dto => dto.Id, options => options.MapFrom(entity => entity.IdOrderItem));
        CreateMap<CreateOrderItemDTO, OrderItem>()
            .ForMember(entity => entity.IdOrderItem, options => options.Ignore())
            .ForMember(entity => entity.Order, options => options.Ignore())
            .ForMember(entity => entity.Product, options => options.Ignore());
        CreateMap<UpdateOrderItemDTO, OrderItem>()
            .ForMember(entity => entity.IdOrderItem, options => options.Ignore())
            .ForMember(entity => entity.Order, options => options.Ignore())
            .ForMember(entity => entity.Product, options => options.Ignore());
        CreateMap<PatchOrderItemDTO, OrderItem>()
            .ForMember(entity => entity.IdOrderItem, options => options.Ignore())
            .ForMember(entity => entity.Order, options => options.Ignore())
            .ForMember(entity => entity.Product, options => options.Ignore())
            .ForAllMembers(options => options.Condition((_, _, value) => value is not null));

        CreateMap<ProductReservation, ProductReservationDTO>()
            .ForMember(dto => dto.Id, options => options.MapFrom(entity => entity.IdReservation));
        CreateMap<ReserveProductDTO, ProductReservation>()
            .ForMember(entity => entity.ReservedAt, options => options.MapFrom(_ => DateTime.UtcNow))
            .ForMember(entity => entity.IdReservation, options => options.Ignore())
            .ForMember(entity => entity.ProductId, options => options.Ignore())
            .ForMember(entity => entity.CancelledAt, options => options.Ignore())
            .ForMember(entity => entity.Product, options => options.Ignore())
            .ForMember(entity => entity.User, options => options.Ignore());
    }
}
