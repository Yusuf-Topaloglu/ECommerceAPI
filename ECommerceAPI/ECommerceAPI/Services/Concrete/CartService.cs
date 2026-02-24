using ECommerceAPI.Data;
using ECommerceAPI.Exceptions;
using ECommerceAPI.Mappings;
using ECommerceAPI.Models;
using ECommerceAPI.Models.Dtos.Category;
using ECommerceAPI.Repositories;
using ECommerceAPI.Services.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace ECommerceAPI.Services.Concrete
{
    public class CartService : ICartService
    {
        private readonly IRepository<CartItem> _repository;
        private readonly ICartRepository _cartRepository;

        public CartService(IRepository<CartItem> repository, ICartRepository cartRepository)
        {
            _repository = repository;
            _cartRepository = cartRepository;
        }

        public async Task<List<CartItem>> GetAllCartAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CartItem?> GetCartAsync(int id)
        {
            var cartItem = await _repository.GetByIdAsync(id);
            return cartItem;
        }

        public async Task<CartItem> AddToCartAsync(int productId, int quantity, string userId)
        {
             var cartItem= await _cartRepository.GetByProductAndUserAsync(productId, userId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
               cartItem = CartMapper.ToEntity(productId, quantity, userId);

               await _repository.AddAsync (cartItem);
            }

            await _repository.SaveAsync();
            return cartItem;
        }



        public async Task<bool> UpdateCartAsync(int id, CartItemDto cartItemDto)
        {
            var existingCart = await _repository.GetByIdAsync (id);
            if (existingCart == null)
                throw new NotFoundException("Sepette böyle bir ürün bulunmamaktadır.");


            ValidateCart(cartItemDto);
            CartMapper.UpdateEntity(existingCart, cartItemDto);

            await _repository.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveFromCartAsync(int id)
        {
            var existingCart = await _repository.GetByIdAsync(id);
            if (existingCart == null)
                throw new NotFoundException("Sepette böyle bir ürün bulunmamaktadır.");

            _repository.Delete(existingCart);
            await _repository.SaveAsync();
            return true;

        }

        private void ValidateCart(CartItemDto cartItemDto)
        {
            if (cartItemDto.Quantity<=0)
            {
                throw new ValidationException("Girilen tutar 0 dan büyük olmalıdır");
            }

        }
    }
}
