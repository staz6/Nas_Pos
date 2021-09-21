using System;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Helper;
using API.Interface;
using Microsoft.EntityFrameworkCore;
using Nas_Pos.Data;

namespace API.Data
{
    public class BasketService : IBasketService
    {
        private readonly AppDbContext _context;
        public BasketService(AppDbContext context)
        {
            _context = context;
        }

        public async Task addItem(string employeeId, int productId,decimal quantity)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id==productId);
            if(product == null ) throw new Exception("Resource not found");
            if(product.Stock < quantity) throw new Exception("Not enough stock of product type : "+product.Title);
            var obj = _context.Baskets.FirstOrDefault(x => x.EmployeeId==employeeId);
            if(obj == null){
                Basket tmp = new Basket{
                    EmployeeId=employeeId
                };
                _context.Add(tmp);
                await _context.SaveChangesAsync();
            }
            var employeeBasket = _context.Baskets.Include(x => x.BasketItems).FirstOrDefault(x => x.EmployeeId==employeeId);
            
            var itemcheck = employeeBasket.BasketItems.FirstOrDefault(x=> x.ProductId == productId);
            if(itemcheck==null){
                BasketItem basketItem = new BasketItem{
                    ProductId=product.Id,
                    PictureUrl=product.PictureUrl,
                    ProductName=product.Title,
                    Price=product.Price*quantity,
                    Quantity=quantity,   
                };
                employeeBasket.BasketItems.Add(basketItem);
            }
            else{
                itemcheck.Quantity= quantity;
                itemcheck.Price=quantity*itemcheck.Price;
            } 
            await _context.SaveChangesAsync();



        }

        public async Task deleteBasket(int id)
        {
            var basket = await _context.Baskets.FindAsync(id);
            _context.Remove(basket);
            await _context.SaveChangesAsync();
        }

        public async Task<Basket> getBasket(string employeeId)
        {
            var basket = await _context.Baskets.Include(x => x.BasketItems).FirstOrDefaultAsync( x => x.EmployeeId==employeeId);
            return basket;
        }

        public async Task removeItem(string employeeId, int productId)
        {
           var item = await _context.BasketItems.FirstOrDefaultAsync(x => x.Id==productId);
           _context.BasketItems.Remove(item);
           await _context.SaveChangesAsync();
        }
    }
}