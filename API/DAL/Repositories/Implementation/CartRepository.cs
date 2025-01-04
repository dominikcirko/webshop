using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using webshopAPI.DataAccess.Repositories.Interfaces;
using webshopAPI.Models;
using System.Data;

namespace webshopAPI.DAL.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly string _connectionString;

        public CartRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            var carts = new List<Cart>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_GetAllCarts", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        carts.Add(MapReaderToCart(reader));
                    }
                }
            }
            return carts;
        }

        public async Task<Cart> GetByIdAsync(int id)
        {
            Cart cart = null;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_GetCartById", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IDCart", id);

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        cart = MapReaderToCart(reader);
                    }
                }
            }
            return cart;
        }

        public async Task AddAsync(Cart entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_AddCart", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", entity.UserID);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAsync(Cart entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_UpdateCart", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IDCart", entity.IDCart);
                command.Parameters.AddWithValue("@UserID", entity.UserID);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_DeleteCart", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IDCart", id);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<Cart> GetByUserIdAsync(int userId)
        {
            Cart cart = null;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_GetCartByUserId", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", userId);

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        cart = MapReaderToCart(reader);
                    }
                }
            }
            return cart;
        }

        public async Task<bool> IsCartEmptyAsync(int cartId)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_CheckIfCartIsEmpty", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IDCart", cartId);

                await connection.OpenAsync();
                var result = await command.ExecuteScalarAsync();

                return (int)result == 0;
            }
        }

        private Cart MapReaderToCart(SqlDataReader reader)
        {
            return new Cart
            {
                IDCart = reader.GetInt32(reader.GetOrdinal("IDCart")),
                UserID = reader.GetInt32(reader.GetOrdinal("UserID"))
            };
        }
    }
}
