﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using webshopAPI.DataAccess.Repositories.Interfaces;
using webshopAPI.Models;

namespace webshopAPI.DAL.Repositories.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly string _connectionString;

        public ItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            var items = new List<Item>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_GetAllItems", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add(MapReaderToItem(reader));
                    }
                }
            }
            return items;
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            Item item = null;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_GetItemById", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IDItem", id);
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        item = MapReaderToItem(reader);
                    }
                }
            }
            return item;
        }

        public async Task AddAsync(Item entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_CreateItem", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ItemCategoryID", entity.ItemCategoryID);
                command.Parameters.AddWithValue("@TagID", entity.TagID);
                command.Parameters.AddWithValue("@Title", entity.Title);
                command.Parameters.AddWithValue("@Description", entity.Description);
                command.Parameters.AddWithValue("@StockQuantity", entity.StockQuantity);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.AddWithValue("@Weight", entity.Weight);

                var outputId = new SqlParameter("@IDItem", SqlDbType.Int) { Direction = ParameterDirection.Output };
                command.Parameters.Add(outputId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                entity.IDItem = (int)outputId.Value;
            }
        }

        public async Task UpdateAsync(Item entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_UpdateItem", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IDItem", entity.IDItem);
                command.Parameters.AddWithValue("@ItemCategoryID", entity.ItemCategoryID);
                command.Parameters.AddWithValue("@TagID", entity.TagID);
                command.Parameters.AddWithValue("@Title", entity.Title);
                command.Parameters.AddWithValue("@Description", entity.Description);
                command.Parameters.AddWithValue("@StockQuantity", entity.StockQuantity);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.AddWithValue("@Weight", entity.Weight);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_DeleteItem", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IDItem", id);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<Item>> GetByCategoryIdAsync(int categoryId)
        {
            var items = new List<Item>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_GetItemsByCategory", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ItemCategoryID", categoryId);

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add(MapReaderToItem(reader));
                    }
                }
            }
            return items;
        }

        public async Task<int> IsInStockAsync(int itemId)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_CheckItemStock", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IDItem", itemId);

                await connection.OpenAsync();
                var result = await command.ExecuteScalarAsync();

                return (int)result;
            }
        }

        public async Task<IEnumerable<Item>> SearchByTitleAsync(string title)
        {
            var items = new List<Item>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_SearchItemsByTitle", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Title", title);

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add(MapReaderToItem(reader));
                    }
                }
            }
            return items;
        }

        public async Task<IEnumerable<Item>> GetByTagIdAsync(int? tagId)
        {
            var items = new List<Item>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("sp_GetItemsByTagId", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (tagId.HasValue)
                {
                    command.Parameters.AddWithValue("@TagID", tagId.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@TagID", DBNull.Value);
                }

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add(MapReaderToItem(reader));
                    }
                }
            }
            return items;
        }





        private Item MapReaderToItem(SqlDataReader reader)
        {
            return new Item
            {
                IDItem = reader.GetInt32(reader.GetOrdinal("IDItem")),
                ItemCategoryID = reader.GetInt32(reader.GetOrdinal("ItemCategoryID")),
                TagID = reader.IsDBNull(reader.GetOrdinal("TagID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("TagID")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                StockQuantity = reader.GetInt32(reader.GetOrdinal("StockQuantity")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                Weight = reader.GetDecimal(reader.GetOrdinal("Weight"))
            };
        }
    }
}
