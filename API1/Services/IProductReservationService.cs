using API1.DTOs;

namespace API1.Services;

public interface IProductReservationService
{
    Task<ServiceResult<PagedResult<ProductReservationDTO>>> GetPageAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<ServiceResult<ProductReservationDTO>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ServiceResult<ProductReservationDTO>> ReserveAsync(int productId, ReserveProductDTO dto, CancellationToken cancellationToken = default);
    Task<ServiceResult<ProductReservationDTO>> CancelAsync(int id, CancellationToken cancellationToken = default);
}
