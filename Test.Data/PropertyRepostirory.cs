using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Test.Data.DataContext;
using Test.Entities;
using Dapper;
using System.Data.SqlClient;

namespace Test.DataAccess
{
    public class PropertyRepostirory : Interfaces.Repositories.IPropertyRepository
    {

        private readonly IConnection _connection;
        public PropertyRepostirory(IConnection connection)        {
            _connection = connection;
        }

        public async Task<Property> Add(Property entity)
        {            
            using (IDbConnection db = _connection.GetConnection)
            {                
                string query = $"select public.activity_add({entity.Property_Id},'{entity.Property_Id.ToString("yyyy-MM-dd HH:MM:ss")}','{entity.Property_Address}');";

                if (db.State == ConnectionState.Closed)
                    db.Open();

                using (var tran = db.BeginTransaction())
                {
                    try
                    {
                        var result = await db.ExecuteScalarAsync<int>(query, null, commandType: CommandType.Text, transaction: tran);
                        tran.Commit();
                        return new Property { Property_Id=result};
                    }
                    catch (Npgsql.NpgsqlException ex)
                    {
                        tran.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Property>> GetAll()
        {
            string query = @"SELECT activity_id,	                            
	                            activity_schedule,
	                            activity_title,
	                            activity_created_at,
	                            activity_updated_at,
	                            activity_status,
                                property_id ,	                            
                                property_title,
	                            property_address,
	                            property_description,
	                            property_updated_at,
	                            property_disabled_at,
	                            property_status   
                            FROM public.activity_getall();";

            IEnumerable<Property> result = null;
            using (IDbConnection db = _connection.GetConnection)
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                result = await db.QueryAsync<Test.Entities.Property>( query,null, commandType:CommandType.Text);

                return result;
            }
        }

        public async Task<Property> GetById(int id)
        {
            string query = @$"SELECT 
                                property_id,
                                property_title,
                                property_address,
                                property_description,
                                property_updated_at,
                                property_disabled_at,
                                property_status
                            FROM property p WHERE property_id ={id};";

            Property result = null;
            using (IDbConnection db = _connection.GetConnection)
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                result = await db.QueryFirstOrDefaultAsync<Test.Entities.Property>(query, null, commandType: CommandType.Text);
                return result;
            }
        }

        public async Task<Property> Modify(Property entity)
        {
            using (IDbConnection db = _connection.GetConnection)
            {
                string query = @$"UPDATE property set 
                                    property_title='{entity.Property_Title}', 
                                    property_address='{entity.Property_Address}',
                                    property_description='{entity.Property_Description}',
                                    property_updated_at=now(),
                                    property_disabled_at=now(),
                                    property_status='{entity.Property_Status}'
                                WHERE property_id={entity.Property_Id};";

                if (db.State == ConnectionState.Closed)
                    db.Open();

                using (var tran = db.BeginTransaction())
                {
                    try
                    {
                        var result = await db.ExecuteAsync(query, null, commandType: CommandType.Text, transaction: tran);
                        tran.Commit();
                        return new Property { Property_Id = result };
                    }
                    catch (Npgsql.NpgsqlException ex)
                    {
                        tran.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }


        public async Task<Property> Disabled(int id)
        {
            using (IDbConnection db = _connection.GetConnection)
            {
                string query = @$"UPDATE property set                                     
                                    property_status='Disabled',
                                    property_disabled_at=now()
                                WHERE property_id={id};";

                if (db.State == ConnectionState.Closed)
                    db.Open();

                using (var tran = db.BeginTransaction())
                {
                    try
                    {
                        var result = await db.ExecuteAsync(query, null, commandType: CommandType.Text, transaction: tran);
                        tran.Commit();
                        return new Property { Property_Id = result };
                    }
                    catch (Npgsql.NpgsqlException ex)
                    {
                        tran.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }


        public async Task<Property> Enabled(int id)
        {
            using (IDbConnection db = _connection.GetConnection)
            {
                string query = @$"UPDATE property set                                     
                                    property_status='Enabled',
                                    property_disabled_at=now()
                                WHERE property_id={id};";

                if (db.State == ConnectionState.Closed)
                    db.Open();

                using (var tran = db.BeginTransaction())
                {
                    try
                    {
                        var result = await db.ExecuteAsync(query, null, commandType: CommandType.Text, transaction: tran);
                        tran.Commit();
                        return new Property { Property_Id = result };
                    }
                    catch (Npgsql.NpgsqlException ex)
                    {
                        tran.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}
