﻿using AutoMapper;
using VShop.CartApi.Models;

namespace VShop.CartApi.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CartDTO, Cart>().ReverseMap();
            CreateMap<CartItemDTO, CartItem>().ReverseMap();
            CreateMap<CartHeaderDTO, CartHeader>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
        }
    }
}
