
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//	   生成时间 2018-04-03 10:11:52
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失
//     作者：Harbour CTS
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DapperTest.Model;

namespace DapperTest.Data
{	
	public partial class Tabl2Data
    { 
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Db_Tabl2 model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("INSERT INTO Tabl2(");			
            strSql.Append("AId,Name");
			strSql.Append(")  OUTPUT  INSERTED.ID VALUES (");
            strSql.Append("@AId,@Name");            
            strSql.Append(") "); 

			//var newClass = new
			//{
			//    AId = model.AId,
			//    Name = model.Name   
			//};   
			return (int)DapperHelper.ExecuteScalar(strSql.ToString(), model);            
		}
		
         /// <summary>
		/// 增加多条数据
		/// </summary>
		public bool AddBatch(List<Db_Tabl2>  model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("INSERT INTO Tabl2(");			
            strSql.Append("AId,Name");
			strSql.Append(")  OUTPUT  INSERTED.ID VALUES (");
            strSql.Append("@AId,@Name");            
            strSql.Append(") "); 

		
			return DapperHelper.Excute(strSql.ToString(), model) > 0;            
		}
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Db_Tabl2 model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("UPDATE Tabl2 set ");        
            strSql.Append(" AId = @AId , ");        
            strSql.Append(" Name = @Name  ");			
			strSql.Append(" WHERE id=@id "); 


			return DapperHelper.Excute(strSql.ToString(), model) > 0; 
		}

	   /// <summary>
		/// 更新多条数据
		/// </summary>
		public bool UpdateBatch(List<Db_Tabl2> model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("UPDATE Tabl2 set ");        
            strSql.Append(" AId = @AId , ");        
            strSql.Append(" Name = @Name  ");			
			strSql.Append(" WHERE id=@id "); 


			return DapperHelper.Excute(strSql.ToString(), model) > 0; 
		}
		
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{	
			StringBuilder strSql = new StringBuilder();
			strSql.Append("DELETE FROM Tabl2 ");
			strSql.Append(" WHERE id=@id");
			var newClass = new
			{
			    id = id   
			};   
			return DapperHelper.Excute(strSql.ToString(), newClass) > 0; 
		}

         /// <summary>
		/// 删除多条数据
		/// </summary>
         public bool DeleteBatch(List<int>  id)
		{	
			StringBuilder strSql = new StringBuilder();
			strSql.Append("DELETE FROM Tabl2 ");
			strSql.Append(" WHERE id in @id");
            var newClass = new
			{
			    id = id   
			};   
			return DapperHelper.Excute(strSql.ToString(), newClass) > 0; 
		}
        


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Db_Tabl2 GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT id,AId,Name ");
			strSql.Append(" FROM Tabl2 ");
            strSql.Append(" WHERE id=@id");
			var newClass = new
			{
			    id = id   
			};   
			return DapperHelper.FirstOrDefault<Db_Tabl2>(strSql.ToString(), newClass);
        }


		/// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Db_Tabl2> GetModelList(string strWhere, object param = null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT id,AId,Name ");
			strSql.Append(" FROM Tabl2 ");
			if (strWhere.Trim() != "")
			{
			    strSql.Append(" WHERE " + strWhere);
			}   
			return DapperHelper.Query<Db_Tabl2>(strSql.ToString(), param);
        }


		/// <summary>
		/// 获取总条数
		/// </summary>
		/// <param name="strWhere"></param>
		/// <returns></returns>
		public int GetDataRecord(string strWhere, object param = null)
		{
		    string sql = "SELECT COUNT(1) FROM Tabl2 WHERE  " + strWhere;
		    object obj = DapperHelper.ExecuteScalar(sql, param );
		    if (obj == null)
		    {
		        return 0;
		    }
		    return Convert.ToInt32(obj);
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public IEnumerable<Db_Tabl2> SelectListByPage(string strWhere, string orderby, int pageIndex, int pageSize,object param = null)
		{
		    StringBuilder strSql = new StringBuilder();
		    strSql.Append("SELECT * FROM ( ");
		    strSql.Append(" SELECT ROW_NUMBER() OVER (");
		    if (!string.IsNullOrEmpty(orderby.Trim()))
		    {
		        strSql.Append("ORDER BY T." + orderby);
		    }
		    else
		    {
		        strSql.Append("ORDER BY T.id desc");
		    }
		    strSql.Append(")AS Row, T.* from Tabl2 T ");
		    if (!string.IsNullOrEmpty(strWhere.Trim()))
		    {
		        strSql.Append(" WHERE   " + strWhere);
		    }
		    strSql.Append(" ) TT");
		    strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", (pageIndex-1)*pageSize+1,pageIndex * pageSize);

		    return DapperHelper.Query<Db_Tabl2>(strSql.ToString(), param);
		}
    }
}
