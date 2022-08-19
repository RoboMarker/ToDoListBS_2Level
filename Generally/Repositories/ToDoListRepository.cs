using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Generally;
using System.Data;
using Generally.Interfaces;
using Generally.Data;

namespace Generally.Repositories
{
    public class ToDoListRepository : ITodoListRepository
    {
        private readonly string sConnectionStr = "";
        MsDBConn _DBconn = null;
        private readonly IConfiguration _config;
        public ToDoListRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<ToDoList?> Get(int id)
        {
            _DBconn = new MsDBConn(_config);
            var sqlCmd = @" select * from ToDoList where id=@id";
            var parameter = new { Id = id };
            var result = await _DBconn.LoadData<ToDoList, dynamic>(sqlCmd, parameter);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<ToDoList>> GetAll()
        {
            _DBconn = new MsDBConn(_config);
            var sqlCmd = @" select * from ToDoList ";
            var result = await _DBconn.LoadData<ToDoList>(sqlCmd);
            return result;
        }

        public async Task<int> Add(ToDoList entity)
        {
            _DBconn = new MsDBConn(_config);
            int iResult = 0;
            entity.Add_Date = DateTime.Now;
            entity.Update_Date = null;
            entity.Update_Mid = null;
            var sqlCmd = @"insert into [ToDoList]
           ([Title]
           ,[Description]
           ,[Plan_Date]
           ,[Add_Date]
           ,[Add_Mid]
           ,[Iscomplete]) values(@Title,@Description,@Plan_Date,@Add_Date,@Add_Mid,@Iscomplete)";
            var parameter =
                new
                {
                    Title = entity.Title,
                    Description = entity.Description,
                    Plan_Date = entity.Plan_Date,
                    Add_Date = entity.Add_Date,
                    Add_Mid = entity.Add_Mid,
                    Iscomplete = entity.Iscomplete
                };

            //using (var connection = new SqlConnection(sConnectionStr))
            //{
            //    connection.Open();
            //    var result = await connection.ExecuteAsync(sqlCmd, entity);
            //    return result;
            //}
            iResult = await _DBconn.ExecuteData(sqlCmd, parameter);
            //iResult = await MsDBConn.ExecuteData2<dynamic>(sqlCmd, parameter, _conn);
            return iResult;
        }

        public async Task<ToDoList?> AddRIdentity(ToDoList entity)
        {
            _DBconn = new MsDBConn(_config);
            entity.Add_Date = DateTime.Now;
            entity.Update_Date = null;
            entity.Update_Mid = null;
            var sqlCmd = @"insert into [ToDoList]
            ([Title],[Description],[Plan_Date],[Add_Date],[Add_Mid],[Iscomplete])
            values(@Title,@Description,@Plan_Date,@Add_Date,@Add_Mid,@Iscomplete)
            select * from ToDoList where id= scope_identity()";
            var parameter =
                new
                {
                    Title = entity.Title,
                    Description = entity.Description,
                    Plan_Date = entity.Plan_Date,
                    Add_Date = entity.Add_Date,
                    Add_Mid = entity.Add_Mid,
                    Iscomplete = entity.Iscomplete
                };
            var result = await _DBconn.LoadData<ToDoList, dynamic>(sqlCmd, parameter);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<ToDoList>> Search(string sTitle, string sDescription)
        {
            _DBconn = new MsDBConn(_config);
            string sWhere = "";
            var paramters = new DynamicParameters();
            if (sTitle != null)
            {
                sWhere += "  title like @title ";
                paramters.Add("title", "%"+sTitle+ "%" );
            }
            
            if (sDescription != null)
            {
                if (sWhere != "")
                {
                    sWhere +=" or ";
                }
                sWhere += $"  Description like @Description";
                paramters.Add("Description", "%" + sDescription + "%");
            }
            var sqlCmd = @" select * from ToDoList where  "+ sWhere;
            var result = await _DBconn.LoadData<ToDoList, dynamic> (sqlCmd, paramters);
            return result;
        }

        public async Task<int> Delete(int id)
        {
            _DBconn = new MsDBConn(_config);
            var sqlCmd = @" delete from ToDoList where id=@id";
            //var result = await connection.ExecuteAsync(sqlCmd, new { Id = id });
            var result = await _DBconn.ExecuteData(sqlCmd, new { Id = id });
            return result;
        }

        public async Task<int> Update(ToDoList entity)
        {
            _DBconn = new MsDBConn(_config);
            entity.Update_Date = DateTime.Now;
            var sqlCmd = @"update [ToDoList]
           set title=@title,Description=@Description,Plan_Date=@Plan_Date,Update_Date=@Update_Date,Update_Mid=@Update_Mid,Iscomplete=@Iscomplete where Id=@Id";
            var parameter =
            new
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Plan_Date = entity.Plan_Date,
                Add_Date = entity.Add_Date,
                Add_Mid = entity.Add_Mid,
                Update_Date = entity.Update_Date,
                Update_Mid = entity.Update_Mid,
                Iscomplete = entity.Iscomplete
            };
            //var result = await connection.ExecuteAsync(sql, entity);
            var result = await _DBconn.ExecuteData(sqlCmd, parameter);
            return result;
        }


    }
}
