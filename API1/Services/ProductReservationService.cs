using API1.DTOs;
using API1.Models;
using API1.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API1.Services;

public sealed class ProductReservationService(
    IRepository<ProductReservation> reservations,
    IRepository<Product> products,
    IRepository<User> users,
    IMapper mapper) : IProductReservationService
{
    public async Task<ServiceResult<PagedResult<ProductReservationDTO>>> GetPageAsync(
        int page, int pageSize, CancellationToken cancellationToken = default)
    {
        if (page < 1 || pageSize < 1 || pageSize > 100 || (long)(page - 1) * pageSize > int.MaxValue)
            return ServiceResult<PagedResult<ProductReservationDTO>>.Invalid(
                "Page must be positive and pageSize must be between 1 and 100.");

        var entities = await reservations.GetPageAsync(page, pageSize, cancellationToken);
        var totalCount = await reservations.CountAsync(cancellationToken);
        return ServiceResult<PagedResult<ProductReservationDTO>>.Success(
            new PagedResult<ProductReservationDTO>(mapper.Map<IReadOnlyList<ProductReservationDTO>>(entities), totalCount));
    }

    public async Task<ServiceResult<ProductReservationDTO>> GetByIdAsync(
        int id, CancellationToken cancellationToken = default)
    {
        var reservation = await reservations.GetByIdAsync(id, cancellationToken);
        return reservation is null
            ? ServiceResult<ProductReservationDTO>.NotFound("Reservation was not found.")
            : ServiceResult<ProductReservationDTO>.Success(mapper.Map<ProductReservationDTO>(reservation));
    }

    public async Task<ServiceResult<ProductReservationDTO>> ReserveAsync(
        int productId, ReserveProductDTO dto, CancellationToken cancellationToken = default)
    {
        if (dto.Quantity <= 0)
            return ServiceResult<ProductReservationDTO>.Invalid("Reservation quantity must be greater than zero.");

        var product = await products.GetByIdAsync(productId, cancellationToken);
        if (product is null) return ServiceResult<ProductReservationDTO>.NotFound("Product was not found.");

        var user = await users.GetByIdAsync(dto.UserId, cancellationToken);
        if (user is null) return ServiceResult<ProductReservationDTO>.NotFound("User was not found.");

        if (product.StockQuantity < dto.Quantity)
            return ServiceResult<ProductReservationDTO>.Conflict("There is not enough stock to reserve the requested quantity.");

        var reservation = mapper.Map<ProductReservation>(dto);
        reservation.ProductId = productId;
        reservation.Product = product;
        reservation.User = user;
        product.StockQuantity -= dto.Quantity;

        await reservations.AddAsync(reservation, cancellationToken);
        try
        {
            await reservations.SaveChangesAsync(cancellationToken);
            return ServiceResult<ProductReservationDTO>.Success(mapper.Map<ProductReservationDTO>(reservation));
        }
        catch (DbUpdateException)
        {
            return ServiceResult<ProductReservationDTO>.Conflict("The reservation could not be saved because related data changed.");
        }
    }

    public async Task<ServiceResult<ProductReservationDTO>> CancelAsync(
        int id, CancellationToken cancellationToken = default)
    {
        var reservation = await reservations.GetByIdAsync(id, cancellationToken);
        if (reservation is null || reservation.CancelledAt is not null)
            return ServiceResult<ProductReservationDTO>.NotFound("No active reservation was found.");

        var product = reservation.Product;
        if (product is null)
            return ServiceResult<ProductReservationDTO>.NotFound("The reserved product was not found.");

        if (product.StockQuantity > int.MaxValue - reservation.Quantity)
            return ServiceResult<ProductReservationDTO>.Conflict("The stock quantity cannot be restored safely.");

        product.StockQuantity += reservation.Quantity;
        reservation.CancelledAt = DateTime.UtcNow;

        try
        {
            await reservations.SaveChangesAsync(cancellationToken);
            return ServiceResult<ProductReservationDTO>.Success(mapper.Map<ProductReservationDTO>(reservation));
        }
        catch (DbUpdateException)
        {
            return ServiceResult<ProductReservationDTO>.Conflict("The reservation cancellation could not be saved.");
        }
    }
}
