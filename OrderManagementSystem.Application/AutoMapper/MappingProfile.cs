using AutoMapper;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Responses;

namespace OrderManagementSystem.Application.AutoMapper;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderResponse>()
            .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note));

        CreateMap<OrderItem, OrderItemResponse>()
            .ForMember(dest => dest.LineItemTotal, opt => opt.Ignore()); 
        
        CreateMap<Order, AnalyticResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status != null ? src.Status.StatusName : string.Empty))
            .ForMember(dest => dest.OrderItemsCount, opt => opt.MapFrom(src =>
                src.OrderItems.Sum(item => item.Quantity)
            ));
    }
}